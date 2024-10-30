using Mostashfety.BLL.ViewModels.AppointmentVM;
using Mostashfety.BLL.ViewModels.PatientVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mostashfety.BLL.Managers.Abstract
{
    public interface IAppointmentManager
    {
        IEnumerable<GetAppointmentVM> GetAll();
        GetAppointmentVM GetById(int id);
        CreateAppointmentVM Add(CreateAppointmentVM appointmentVM);
        void Delete(int id);
        void Update(int id, EditAppointmentVM appointmentVM);
    }
}
