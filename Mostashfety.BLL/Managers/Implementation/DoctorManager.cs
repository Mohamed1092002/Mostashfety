using Mostashfety.BLL.Managers.Abstract;
using Mostashfety.BLL.ViewModels.AdminVM;
using Mostashfety.BLL.ViewModels.DoctorVM;
using Mostashfety.BLL.ViewModels.PatientVM;
using Mostashfety.DAL.Models;
using Mostashfety.DAL.Repos.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mostashfety.BLL.Managers.Implementation
{
    public class DoctorManager:IDoctorManager
    {
        private readonly IDoctorRepo _doctorRepo;
        public DoctorManager(IDoctorRepo doctorRepo)
        {
            _doctorRepo = doctorRepo;
        }

        public void Add(CreateDoctorVM doctorVM)
        {
            var doctor = new Doctor
            {
                FullName = doctorVM.FullName,
                specialization=doctorVM.specialization,
                Department = doctorVM.Department,
            };

            _doctorRepo.Create(doctor);
            _doctorRepo.SaveChanges();
            
        }

        public void Delete(int id)
        {
            _doctorRepo.Delete(id);
            _doctorRepo.SaveChanges();
        }

        public IEnumerable<GetDoctorVm> GetAll()
        {
            var doctors = _doctorRepo.GetAll();
            var doctorVm = doctors.Select(doctor => new GetDoctorVm
            {
                Id = doctor.Id,
                FullName = doctor.FullName,
                specialization = doctor.specialization,
                Department = doctor.Department,  
            });
            return doctorVm;
        }

        public GetDoctorVm GetById(int id)
        {
            var doctorEntities = _doctorRepo.Get(id);
            if (doctorEntities == null)
            {
                throw new KeyNotFoundException($"Doctor with id {id} not found.");
            }
            _doctorRepo.SaveChanges();
            var doctorVM = new GetDoctorVm
            {
                Id= doctorEntities.Id,
                FullName = doctorEntities.FullName,
                specialization= doctorEntities.specialization,
                Department = doctorEntities.Department,
            };

            return doctorVM;
        }

        public void Update(int id, EditDoctorVM doctorVM)
        {
            var doctors = _doctorRepo.Get(id);
            if (doctors == null)
            {
                throw new KeyNotFoundException($"Doctor with id {doctorVM.Id} not found.");
            }

           doctors.Id = id;
            doctors.FullName = doctorVM.FullName;
            doctors.specialization = doctorVM.specialization;
            doctors.Department = doctorVM.Department;


            _doctorRepo.Update(doctors);
            _doctorRepo.SaveChanges();
        }
    }
}
