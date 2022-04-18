using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HealthcareSystem.Models
{
    public class Test
    {
        [Key]
        public Guid testid { get; set; }

        public string testname { get; set; }

        public string testprice { get; set; }

        public string testdescription { get; set; }


    }
}