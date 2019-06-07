using GymSheet.Data;
using GymSheet.Models;
using GymSheet.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymSheet.Services
{
    public class StudentService : Service<Student>, IStudentService
    {
        private readonly Context _context;
        public StudentService(Context context) : base(context)
        {
            _context = context;
        }

        public new async Task<List<Student>> FindAllAsync()
        {
            return await _context.Students.Include(s => s.Teacher).Include(s => s.Objective).ToListAsync();
        }

        public async Task<bool> HasAnyName(int? id, string name)
        {
            return await _context.Students.AnyAsync(x => x.Id != id && x.Name == name);
        }
    }
}
