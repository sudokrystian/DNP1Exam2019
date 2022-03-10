using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeWebService.Models;

namespace EmployeeWebService {
    public class EmployeeService : IEmployeeService {

        public List<Employee> FilterEmployeesBasedOnOvertime(List<Employee> employees, bool hasOvertime)
        {
            List<Employee> employeeList = new List<Employee>();
            if(hasOvertime)
            {
                foreach(Employee employee in employees)
                {
                    if(employee.HoursPerMonth > 150)
                    {
                        employeeList.Add(employee);
                    }
                }
            }
            else
            {
                foreach (Employee employee in employees)
                {
                    if (employee.HoursPerMonth <= 150)
                    {
                        employeeList.Add(employee);
                    }
                }
            }

            return employeeList;
        }

        public double GetTotalMonthlyExpense(List<Employee> employees)
        {
            double total = 0;
            foreach(Employee employee in employees)
            {
                total += employee.GetMonthlyPay();
            }
            return total;
        }
    }
}
