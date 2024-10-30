using Mostashfety.BLL.Managers.Abstract;
using Mostashfety.BLL.ViewModels.MedicalVM;
using Mostashfety.BLL.ViewModels.PatientVM;
using Mostashfety.DAL.Models;
using Mostashfety.DAL.Repos.Abstract;
using Mostashfety.DAL.Repos.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mostashfety.BLL.Managers.Implementation
{
    public class MedicalManager:IMedicalManager
    {
        private readonly IMedicalRepo _medicalRepo;

        public MedicalManager(IMedicalRepo medicalRepo)
        {
            _medicalRepo = medicalRepo;
        }

        public CreateMedicalVM Add(CreateMedicalVM medicalVM)
        {
            var record = new MedicalRecord
            {
                PatientName = medicalVM.PatientName,
                DoctorName = medicalVM.DoctorName,
                Date = medicalVM.Date,
                Diagnosis = medicalVM.Diagnosis,
                Treatment = medicalVM.Treatment,
            };

            _medicalRepo.Create(record);
            _medicalRepo.SaveChanges();
            return medicalVM;
        }

        public void Delete(int id)
        {
            _medicalRepo.Delete(id);
            _medicalRepo.SaveChanges();
        }

        public IEnumerable<GetMedicalVM> GetAll()
        {
            var records = _medicalRepo.GetAll();
            var recordVm = records.Select(record => new GetMedicalVM
            {
                Id = record.Id,
                PatientName = record.PatientName,
                DoctorName = record.DoctorName,
                Date = record.Date,
                Diagnosis = record.Diagnosis,
                Treatment = record.Treatment,
                
            });
            return recordVm;
        }

        public GetMedicalVM GetById(int id)
        {
            var record = _medicalRepo.Get(id);
            if (record == null)
            {
                throw new KeyNotFoundException($"Medical Record with id {id} not found.");
            }
            _medicalRepo.SaveChanges();
            var RecordVm = new GetMedicalVM
            {
                Id = record.Id,
                PatientName = record.PatientName,
                DoctorName = record.DoctorName,
                Date = record.Date,
                Diagnosis = record.Diagnosis,
                Treatment = record.Treatment,
            };

            return RecordVm;
        }

        public void Update(int id, EditMedicalVM medicalVM)
        {
            var records = _medicalRepo.Get(id);
            if (records == null)
            {
                throw new KeyNotFoundException($"Medical Record with id {medicalVM.Id} not found.");
            }

            records.PatientName = medicalVM.PatientName;
            records.DoctorName = medicalVM.DoctorName;
            records.Date = medicalVM.Date;
            records.Diagnosis = medicalVM.Diagnosis;
            records.Treatment = medicalVM.Treatment;


            _medicalRepo.Update(records);
            _medicalRepo.SaveChanges();
        }
    }
}
