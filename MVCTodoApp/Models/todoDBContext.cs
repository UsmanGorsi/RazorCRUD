
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using MVCTodoApp.DTOs;

namespace MVCTodoApp.Models {
    public class todoDBContext : DbContext {
        public todoDBContext(DbContextOptions options) : base(options) {}
        public DbSet<todo> todos { get; set; }
    }
}
