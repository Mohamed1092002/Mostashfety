using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mostashfety.BLL.ViewModels.DoctorVM
{
    public class GetDoctorVm
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string specialization { get; set; }
        public string Department { get; set; }
    }
}
