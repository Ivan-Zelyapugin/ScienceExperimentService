﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceExperimentService.Domain.Entitys
{
    public class Files
    {
        public Guid Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FileType {  get; set; } = string.Empty; 
        public long FileSize { get; set; }
        public DateTime CreatedDate { get; set; }
        public string AuthorName { get; set; } = string.Empty;
    }
}
