using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeWebService.Models;

namespace EmployeeWebService {
    public interface IEmployeeService {
        public List<Employee> FilterEmployeesBasedOnOvertime(List<Employee> employees, bool hasOvertime);
        public double GetTotalMonthlyExpense(List<Employee> employees);

    }
}
