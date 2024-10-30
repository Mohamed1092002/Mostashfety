using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mostashfety.DAL.Models
{
    public class MedicalRecord
    {
        public int Id { get; set; }

        public int? PatientId { get; set; }
        public string PatientName { get; set; }

        public int? DoctorId { get; set; }
        public string DoctorName { get; set; }

        [Required]
        public string Diagnosis { get; set; }

        public string Treatment { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
    }
}
