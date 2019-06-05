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
    public class TeacherService : Service<Teacher>, ITeacherService
    {
        private readonly Context _context;

        public TeacherService(Context context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> HasAnyName(int? id, string name)
        {
            return await _context.Teachers.AnyAsync(t => t.Id != id && t.Name == name);
        }
    }
}
