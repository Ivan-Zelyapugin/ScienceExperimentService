using MediatR;
using Microsoft.EntityFrameworkCore;
using ScienceExperimentService.Application.Files.Commands.Create.CreateFile;
using ScienceExperimentService.Application.Files.Commands.Create.CreateResult;
using ScienceExperimentService.Application.Files.Commands.Create.CreateValue;
using ScienceExperimentService.Application.Files.Commands.Update.UpdateFile;
using ScienceExperimentService.Application.Files.Commands.Update.UpdateResult;
using ScienceExperimentService.Application.Files.Commands.Update.UpdateValue;
using ScienceExperimentService.Application.Interfaces;
using System.Globalization;


namespace ScienceExperimentService.Application.Files.Commands.Create
{
    public class UploadExperimentCommandHandler : IRequestHandler<UploadExperimentCommand, int>
    {
        private readonly IExperimentsDbContext _dbContext;       // бд
        private readonly IMediator _mediator;
        private List<string> validLines = new List<string>();    // валидные строки
        private DateTime currentDateTime = DateTime.Now;         // текущее время
        private DateTime minDateTime = new DateTime(2000, 1, 1); // минимальное допустимое время

        public UploadExperimentCommandHandler(IExperimentsDbContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task<int> Handle(UploadExperimentCommand request, CancellationToken cancellationToken)
        {
            if (request.File == null || request.File.Length == 0)
                throw new InvalidOperationException("File not provided.");

            var tempPath = Path.GetTempPath();
            var filePath = Path.Combine(tempPath, request.File.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await request.File.CopyToAsync(stream);
            }

            // Валидация
            using (var reader = new StreamReader(request.File.OpenReadStream()))
            {
                string? line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    // строк более 10000
                    if (validLines.Count >= 10000)
                    {
                        break;
                    }

                    var parts = line.Split(';');

                    // нет каких-то частей строки
                    if (parts.Length != 3)
                    {
                        continue;
                    }

                    // парсинг даты
                    if (!DateTime.TryParseExact(parts[0], "yyyy-MM-dd_HH-mm-ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateTime))
                    {
                        continue;
                    }

                    // проверка даты на допустимое
                    if (dateTime < minDateTime || dateTime > currentDateTime)
                    {
                        continue;
                    }

                    // парсинг времени и проверка
                    if (!int.TryParse(parts[1], out int experimentTime) || experimentTime < 0)
                    {
                        continue;
                    }

                    // прасинг показателя и проверка
                    if (!double.TryParse(parts[2], out double indicatorValue) || indicatorValue < 0)
                    {
                        continue;
                    }

                    validLines.Add(line);
                }
            }

            if (validLines.Count > 0)
            {
                var fileInfo = new FileInfo(filePath);

                var existingFile = await _dbContext.Files
                    .FirstOrDefaultAsync(f => f.FileName == fileInfo.Name, cancellationToken);

                // файл существует
                if (existingFile != null)
                {
                    await _mediator.Send(new UpdateFileCommand
                    {
                        Id = existingFile.Id,
                        FileName = fileInfo.Name,
                        FileType = request.File.ContentType,
                        FileSize = fileInfo.Length,
                        CreatedDate = fileInfo.CreationTimeUtc,
                        AuthorName = fileInfo.GetAccessControl().GetOwner(typeof(System.Security.Principal.NTAccount)).ToString()
                    }, cancellationToken);

                    foreach (var validLine in validLines)
                    {
                        var parts = validLine.Split(';');
                        var dateTime = DateTime.ParseExact(parts[0], "yyyy-MM-dd_HH-mm-ss", CultureInfo.InvariantCulture);
                        var experimentTime = int.Parse(parts[1]);
                        var indicatorValue = float.Parse(parts[2]);

                        // нашли значениe по id файла
                        var existingValue = await _dbContext.Values
                            .FirstOrDefaultAsync(v => v.FileId == existingFile.Id && v.DateTime == dateTime, cancellationToken);

                        if (existingValue != null) // обновляем
                        {
                            await _mediator.Send(new UpdateValueCommand
                            {
                                Id = existingValue.Id,
                                DateTime = dateTime,
                                Time = experimentTime,
                                Indicator = indicatorValue
                            }, cancellationToken);
                        }
                        else // добавляем новые
                        {
                            await _mediator.Send(new CreateValueCommand
                            {
                                DateTime = dateTime,
                                Time = experimentTime,
                                Indicator = indicatorValue,
                                FileId = existingFile.Id
                            }, cancellationToken);
                        }
                    }

                    // получили все значения для подсчета результата
                    var values = await _dbContext.Values
                        .Where(v => v.FileId == existingFile.Id)
                        .ToListAsync(cancellationToken);

                    var result = await _dbContext.Results.FirstOrDefaultAsync(r => r.FileId == existingFile.Id, cancellationToken);

                    if (result != null)
                    {
                        await _mediator.Send(new UpdateResultCommand
                        {
                            Id = result.Id,
                            FirstExperimentStart = values.Min(v => v.DateTime),
                            LastExperimentStart = values.Max(v => v.DateTime),
                            MaxExperimentTime = values.Max(v => v.Time),
                            MinExperimentTime = values.Min(v => v.Time),
                            AvgExperimentTime = values.Average(v => v.Time),
                            AvgIndicator = values.Average(v => v.Indicator),
                            MedianIndicator = GetMedian(values.Select(v => v.Indicator).ToList()),
                            MaxIndicator = values.Max(v => v.Indicator),
                            MinIndicator = values.Min(v => v.Indicator),
                            ExperimentCount = values.Count
                        }, cancellationToken);
                    }
                }
                else
                {
                    var file = await _mediator.Send(new CreateFileCommand
                    {
                        FileName = fileInfo.Name,
                        FileType = request.File.ContentType,
                        FileSize = fileInfo.Length,
                        CreatedDate = fileInfo.CreationTimeUtc,
                        AuthorName = fileInfo.GetAccessControl().GetOwner(typeof(System.Security.Principal.NTAccount)).ToString()
                    }, cancellationToken);

                    foreach (var validLine in validLines)
                    {
                        var parts = validLine.Split(';');
                        var dateTime = DateTime.ParseExact(parts[0], "yyyy-MM-dd_HH-mm-ss", CultureInfo.InvariantCulture);
                        var experimentTime = int.Parse(parts[1]);
                        var indicatorValue = float.Parse(parts[2]);

                        await _mediator.Send(new CreateValueCommand
                        {
                            DateTime = dateTime,
                            Time = experimentTime,
                            Indicator = indicatorValue,
                            FileId = file.Id,
                            File = file
                        }, cancellationToken);
                    }

                    var resultValues = await _dbContext.Values
                        .Where(v => v.FileId == file.Id)
                        .ToListAsync(cancellationToken);

                    await _mediator.Send(new CreateResultCommand
                    {
                        FileId = file.Id,
                        File = file,
                        FirstExperimentStart = resultValues.Min(v => v.DateTime),
                        LastExperimentStart = resultValues.Max(v => v.DateTime),
                        MaxExperimentTime = resultValues.Max(v => v.Time),
                        MinExperimentTime = resultValues.Min(v => v.Time),
                        AvgExperimentTime = resultValues.Average(v => v.Time),
                        AvgIndicator = resultValues.Average(v => v.Indicator),
                        MedianIndicator = GetMedian(resultValues.Select(v => v.Indicator).ToList()),
                        MaxIndicator = resultValues.Max(v => v.Indicator),
                        MinIndicator = resultValues.Min(v => v.Indicator),
                        ExperimentCount = resultValues.Count
                    }, cancellationToken);
                }

                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            File.Delete(filePath);

            return validLines.Count;
        }

        private static double GetMedian(List<float> values)
        {
            values.Sort();
            var count = values.Count;
            if (count % 2 == 0)
                return (values[count / 2 - 1] + values[count / 2]) / 2.0;
            return values[count / 2];
        }
    }
}
