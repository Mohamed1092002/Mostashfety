using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mostashfety.BLL.Enum;
using Mostashfety.BLL.Managers.Abstract;
using Mostashfety.BLL.ViewModels.AppointmentVM;
using Mostashfety.DAL;
using Mostashfety.DAL.Context;
using Mostashfety.DAL.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Mostashfety.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly IAppointmentManager _appointmentManager; 
        public AppointmentsController(IAppointmentManager appointmentManager) 
        {
            _appointmentManager = appointmentManager;
        }
        public IActionResult Index()
        {
            var appointments = _appointmentManager.GetAll();
            return View(appointments);
        }
        public IActionResult Details(int id)
        {
            var appointment = _appointmentManager.GetById(id);
            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateAppointmentVM appointmentVM)
        {
            if (ModelState.IsValid)
            {
                _appointmentManager.Add(appointmentVM);
                return RedirectToAction(nameof(Index));
            }
            return View(appointmentVM);
        }
        public IActionResult Edit(int id)
        {
            var appointment = _appointmentManager.GetById(id);
            if (appointment == null)
            {
                return NotFound();
            }

            var appointmentVM = new EditAppointmentVM
            {
                Id = appointment.Id,
                DoctorName = appointment.DoctorName,
                PatientName = appointment.PatientName,
                AppointmentDate = appointment.AppointmentDate,
                Status = appointment.Status,
            };
            return View(appointmentVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, EditAppointmentVM appointmentVM)
        {
            if (id != appointmentVM.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                _appointmentManager.Update(id, appointmentVM);
                return RedirectToAction(nameof(Index));
            }
            return View(appointmentVM);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var course = _appointmentManager.GetById(id);
            if (course == null)
            {
                return NotFound();
            }
            _appointmentManager.Delete(id);

            return RedirectToAction(nameof(Index));

        }
    }
}