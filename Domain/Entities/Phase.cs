using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
   public class Phase
    {

        public int PhaseId { get; set; }
        public string PhaseName { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime End_Date { get; set; }
        public string Duration { get; set; }
        public string Description { get; set; }
        public int ProjectFK { get; set; }
        public Project Project { get; set; }

    }
}
