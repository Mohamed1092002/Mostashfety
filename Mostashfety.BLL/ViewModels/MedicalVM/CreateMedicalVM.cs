using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mostashfety.BLL.ViewModels.MedicalVM
{
    public class CreateMedicalVM
    {
        public string DoctorName { get; set; }
        public string PatientName { get; set; }
        [Required]
        public string Diagnosis { get; set; }

        public string Treatment { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}
