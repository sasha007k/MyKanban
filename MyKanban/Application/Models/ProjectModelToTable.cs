using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models
{
    public class ProjectModelToTable
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Owner { get; set; }
        public int Issues { get; set; }
        public int DoneIssues { get; set; }
        public string UserIdWhoView { get; set; }
        public double Percentage
        {
            get
            {
                if (this.Issues != 0)
                {
                    return this.DoneIssues * 100 / this.Issues;
                }

                return 100;
            }
        }
    }
}