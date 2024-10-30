using Mostashfety.DAL.Context;
using Mostashfety.DAL.Models;
using Mostashfety.DAL.Repos.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mostashfety.DAL.Repos.Implementation
{
    public class PatientRepo:GenericRepo<Patient>,IPatientRepo
    {
        private readonly ApplicationDbContext _context;
        public PatientRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
