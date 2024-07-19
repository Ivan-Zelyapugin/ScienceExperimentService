using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceExperimentService.Persistence
{
    public class DbInitializer
    {
        public static void Initialize(ExperimentsDbContext context) 
        {
            context.Database.EnsureCreated();
        }
    }
}
