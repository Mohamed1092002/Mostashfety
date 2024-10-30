
using Microsoft.AspNetCore.Identity;
using Mostashfety.BLL.ViewModels.PatientVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELearn.BLL.Services.Abstract
{
    public interface IPatientManager
    {
        IEnumerable<GetPatientVM> GetAll();
        GetPatientVM GetById(int id);
        CreatePatientVM Add(CreatePatientVM patientVM);
        void Delete(int id);
        void Update(int id,EditPatientVM patientVM);
        
    }
}
