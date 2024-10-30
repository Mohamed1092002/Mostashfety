using Mostashfety.BLL.ViewModels.DoctorVM;
using Mostashfety.BLL.ViewModels.PatientVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mostashfety.BLL.Managers.Abstract
{
    public interface IDoctorManager
    {
        IEnumerable<GetDoctorVm> GetAll();
        GetDoctorVm GetById(int id);
        void Add(CreateDoctorVM doctorVM);
        void Delete(int id);
        void Update(int id, EditDoctorVM doctorVM);
    }
}
