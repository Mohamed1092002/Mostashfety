using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mostashfety.DAL.Models;

public class Doctor:User
{
    public string specialization { get; set; }
    public string Department { get; set; }
    public ICollection<Appointment> Appointments { get; set; }
    public ICollection<MedicalRecord> MedicalRecords { get; set; }
    [ForeignKey("Admin")]
    public int? AdminID { get; set; }
    public Admin Admin { get; set; }
}
