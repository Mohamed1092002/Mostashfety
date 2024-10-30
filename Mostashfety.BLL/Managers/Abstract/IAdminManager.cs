using Mostashfety.BLL.ViewModels.AdminVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mostashfety.BLL.Managers.Abstract
{
    public interface IAdminManager
    {
        IEnumerable<GetAdminsVM> GetAll();
        GetAdminsVM GetById(int id);
        CreateAdminVM Add(CreateAdminVM adminVM);
        void Delete(int id);
        void Update(int id,EditAdminVM adminVM);
       
    }
}
