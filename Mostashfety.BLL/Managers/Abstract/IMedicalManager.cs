using Mostashfety.BLL.ViewModels.AppointmentVM;
using Mostashfety.BLL.ViewModels.MedicalVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mostashfety.BLL.Managers.Abstract
{
    public interface IMedicalManager
    {
        IEnumerable<GetMedicalVM> GetAll();
        GetMedicalVM GetById(int id);
        CreateMedicalVM Add(CreateMedicalVM medicalVM);
        void Delete(int id);
        void Update(int id, EditMedicalVM medicalVM);
    }
}
