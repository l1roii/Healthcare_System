using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HealthcareSystem.Models
{
    public class Appointment
    {
        [Key]
        public Guid appointmentid { get; set; }

        public string username { get; set; }

        public string userlastname { get; set; }

        public string userphone { get; set; }

        public string useremail { get; set; }

        public string appointmentdate { get; set; }

        public string textmessage { get; set; }

        public string department { get; set; }

        public string doctor { get; set; }
    }
}
