using ELearn.BLL.Services.Abstract;
using Mostashfety.BLL.ViewModels.AdminVM;
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
    public class PatientManager:IPatientManager
    {
        private readonly IPatientRepo _patientRepo;
        public PatientManager(IPatientRepo patientRepo)
        {
            _patientRepo = patientRepo;
        }

        public CreatePatientVM Add(CreatePatientVM patientVM)
        {
            var patient = new Patient
            {
                Name = patientVM.Name,
                BirthDate = patientVM.BirthDate,
                Address = patientVM.Address,
                Phone = patientVM.Phone,
            };

            _patientRepo.Create(patient);
            _patientRepo.SaveChanges();
            return patientVM;
        }

        public void Delete(int id)
        {
            _patientRepo.Delete(id);
            _patientRepo.SaveChanges();
        }

        public IEnumerable<GetPatientVM> GetAll()
        {
            var patients = _patientRepo.GetAll();
            var patientVm = patients.Select(patient => new GetPatientVM
            {
                Id = patient.Id,
                Name = patient.Name,
                BirthDate = patient.BirthDate,
                Address = patient.Address,
                Phone = patient.Phone,

               
            });
            return patientVm;
        }

        public GetPatientVM GetById(int id)
        {
            var patientEntities = _patientRepo.Get(id);
            if (patientEntities == null)
            {
                throw new KeyNotFoundException($"Patient with id {id} not found.");
            }
            _patientRepo.SaveChanges();
            var patientVm = new GetPatientVM
            {
                Id = patientEntities.Id,
                Name = patientEntities.Name,
                Phone = patientEntities.Phone,
                BirthDate = patientEntities.BirthDate
            };

            return patientVm;
        }

        public void Update(int id, EditPatientVM patientVM)
        {
            var patients = _patientRepo.Get(id);
            if (patients == null)
            {
                throw new KeyNotFoundException($"Course with id {patientVM.Id} not found.");
            }

            patients.Id = patientVM.Id;
            patients.Address = patientVM.Address;
            patients.Phone = patientVM.Phone;
            patients.BirthDate = patientVM.BirthDate;
            patients.Name = patientVM.Name;


            _patientRepo.Update(patients);
            _patientRepo.SaveChanges();
        }
    }
}
