using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HealthcareSystem.Models
{
    public class Department
    {
        [Key]
        public Guid departamentid { get; set; }

        [MinLength(3)]
        public string departmentname { get; set; }

        public int departmentrooms { get; set; }

        public string departmentdescription { get; set; }

    }
}
