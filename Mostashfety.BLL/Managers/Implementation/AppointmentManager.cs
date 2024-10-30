using Mostashfety.BLL.Managers.Abstract;
using Mostashfety.BLL.ViewModels.AppointmentVM;
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
    public class AppointmentManager : IAppointmentManager
    {
        private readonly IAppoinmentRepo _appointmentRepo;

        public AppointmentManager(IAppoinmentRepo appointmentRepo) 
        {
            _appointmentRepo = appointmentRepo;
        }
        public CreateAppointmentVM Add(CreateAppointmentVM appointmentVM)
        {
            var appointment = new Appointment
            {
                DoctorName = appointmentVM.DoctorName,
                PatientName = appointmentVM.PatientName,
                AppointmentDate = appointmentVM.AppointmentDate,
                Status = appointmentVM.Status,
            };

            _appointmentRepo.Create(appointment);
            _appointmentRepo.SaveChanges();
            return appointmentVM;
        }

        public void Delete(int id)
        {
            _appointmentRepo.Delete(id);
            _appointmentRepo.SaveChanges();
        }

        public IEnumerable<GetAppointmentVM> GetAll()
        {
            var apponitments = _appointmentRepo.GetAll();
            var apponitmentsVm = apponitments.Select(apponitment => new GetAppointmentVM
            {
                Id = apponitment.Id,
                DoctorName= apponitment.DoctorName,
                PatientName= apponitment.PatientName,
                Status = apponitment.Status,
                AppointmentDate= apponitment.AppointmentDate,

            });
            return apponitmentsVm;
        }

        public GetAppointmentVM GetById(int id)
        {
            var appointmentEntities = _appointmentRepo.Get(id);
            if (appointmentEntities == null)
            {
                throw new KeyNotFoundException($"Appointment with id {id} not found.");
            }
            _appointmentRepo.SaveChanges();
            var appointmentVm = new GetAppointmentVM
            {
               Id= appointmentEntities.Id,
               DoctorName = appointmentEntities.DoctorName,
               PatientName= appointmentEntities.PatientName,
               Status = appointmentEntities.Status,
               AppointmentDate= appointmentEntities.AppointmentDate,
            };

            return appointmentVm;
        }

        public void Update(int id, EditAppointmentVM appointmentVM)
        {
            var appiontments = _appointmentRepo.Get(id);
            if (appiontments == null)
            {
                throw new KeyNotFoundException($"Course with id {appointmentVM.Id} not found.");
            }

            appiontments.Id = appointmentVM.Id;
            appiontments.DoctorName = appointmentVM.DoctorName;
            appiontments.PatientName = appointmentVM.PatientName;
            appiontments.Status = appointmentVM.Status;
            appiontments.AppointmentDate = appointmentVM.AppointmentDate;

            _appointmentRepo.Update(appiontments);
            _appointmentRepo.SaveChanges();
        }
    }
}
