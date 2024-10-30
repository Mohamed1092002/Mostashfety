using Mostashfety.BLL.Managers.Abstract;
using Mostashfety.BLL.ViewModels.AdminVM;
using Mostashfety.DAL.Models;
using Mostashfety.DAL.Repos.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mostashfety.BLL.Managers.Implementation
{
    public class AdminManager:IAdminManager
    {
        private readonly IAdminRepo _adminRepo;
         public AdminManager(IAdminRepo adminRepo) 
        {
            _adminRepo = adminRepo;
        }

        public CreateAdminVM Add(CreateAdminVM adminVM)
        {
            var admin = new Admin
            {
                FullName = adminVM.FullName,
                Email = adminVM.Email,
                Password = adminVM.Password,
                PhoneNumber = adminVM.PhoneNumber,
                UserName = adminVM.UserName,
            };

            _adminRepo.Create(admin);
            _adminRepo.SaveChanges();
            return adminVM;
        }

        public void Delete(int id)
        {
            _adminRepo.Delete(id);
            _adminRepo.SaveChanges();
        }

        public IEnumerable<GetAdminsVM> GetAll()
        {
            var admins = _adminRepo.GetAll();
            var adminVm = admins.Select(admin => new GetAdminsVM
            {
                Id = admin.Id,
                FullName = admin.FullName,
                Email = admin.Email,
                Password = admin.Password,
                PhoneNumber = admin.PhoneNumber,
                UserName = admin.UserName,
            });
            return adminVm;
        }

        public GetAdminsVM GetById(int id)
        {
            var adminEntities = _adminRepo.Get(id);
            if (adminEntities == null)
            {
                throw new KeyNotFoundException($"Admin with id {id} not found.");
            }
            _adminRepo.SaveChanges();
            var adminVm = new GetAdminsVM
            {
                Id = adminEntities.Id,
                FullName= adminEntities.FullName,
                Email = adminEntities.Email,
                Password = adminEntities.Password,
                PhoneNumber = adminEntities.PhoneNumber,
                UserName = adminEntities.UserName,

               
            };

            return adminVm;
        }

        public void Update(int id, EditAdminVM adminVM)
        {
            var admins = _adminRepo.Get(id);
            if (admins == null)
            {
                throw new KeyNotFoundException($"Admin with id {adminVM.Id} not found.");
            }

            admins.Id = adminVM.Id;
            admins.FullName = adminVM.FullName;
            admins.Email = adminVM.Email;
            admins.Password = adminVM.Password;
            admins.PhoneNumber = adminVM.PhoneNumber;
            admins.UserName = adminVM.UserName;
           

            _adminRepo.Update(admins);
            _adminRepo.SaveChanges();
        }
    }
}
