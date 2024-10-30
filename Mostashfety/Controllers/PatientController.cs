using ELearn.BLL.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mostashfety.BLL.Enum;
using Mostashfety.BLL.ViewModels.AppointmentVM;
using Mostashfety.BLL.ViewModels.PatientVM;
using Mostashfety.DAL.Context;
using Mostashfety.DAL.Models;

namespace Mostashfety.Controllers
{
    public class PatientController : Controller
    {
        private readonly IPatientManager _patientManager;

        public PatientController(IPatientManager patientManager)
        {
            _patientManager = patientManager;
        }
        public IActionResult Index()
        {
            var patients = _patientManager.GetAll();
            return View(patients);
        }
     
        public IActionResult Details(int id)
        {
            var patients = _patientManager.GetById(id);
            if (patients == null)
            {
                return NotFound();
            }
            return View(patients);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreatePatientVM patientVM)
        {
            if (ModelState.IsValid)
            {
                _patientManager.Add(patientVM);
                return RedirectToAction(nameof(Index));
            }
            return View(patientVM);
        }
        public IActionResult Edit(int id)
        {
            var patient = _patientManager.GetById(id);
            if (patient == null)
            {
                return NotFound();
            }

            var patientVM = new EditPatientVM
            {
              Id = patient.Id,
              Name = patient.Name,
              BirthDate = patient.BirthDate,
              Address = patient.Address,
              Phone = patient.Phone,
            };
            return View(patientVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, EditPatientVM patientVM)
        {
            if (id != patientVM.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                _patientManager.Update(id, patientVM);
                return RedirectToAction(nameof(Index));
            }
            return View(patientVM);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var course = _patientManager.GetById(id);
            if (course == null)
            {
                return NotFound();
            }
            _patientManager.Delete(id);

            return RedirectToAction(nameof(Index));

        }
    }
}

