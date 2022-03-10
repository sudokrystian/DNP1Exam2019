using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeWebService.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeWebService.Data {
    public class EmployeeContext : DbContext {
        public EmployeeContext(DbContextOptions<EmployeeContext> ctxOptions) : base(ctxOptions)
        {

        }

        public DbSet<Employee> employees { get; set; }
    } 
}
