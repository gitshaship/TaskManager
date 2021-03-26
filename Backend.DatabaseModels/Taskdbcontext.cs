using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class Taskdbcontext : DbContext

    {

        public Taskdbcontext(DbContextOptions<Taskdbcontext> options) : base(options)
        {

        }

        public DbSet<DBtask> Tasks{ get; set; }
    }
}
