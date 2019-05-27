using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Data
{
    class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options)
        {
        }
    }
}
