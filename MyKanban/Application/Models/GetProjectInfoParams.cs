using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models
{
    public class GetProjectInfoParams
    {
        public int ProjectId { get; set; }

        public bool IsUserOnly { get; set; }

        public string UserName { get; set; }
    }
}
