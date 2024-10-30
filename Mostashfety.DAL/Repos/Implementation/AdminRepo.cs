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
    public class AdminRepo : GenericRepo<Admin>, IAdminRepo
    {
        private readonly ApplicationDbContext _context;
        public AdminRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
