using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeWebService.Models {
    public class Employee {
        [Key]
        public string Name { get; set; }
        public double HourlyWage { get; set; }
        public double HoursPerMonth {get; set;}

        public double GetMonthlyPay()
        {
            if(HoursPerMonth <= 150)
            {
                return HourlyWage * HoursPerMonth;
            } 
            else
            {
                double overtime = HoursPerMonth - 150;
                return (150 * HourlyWage) + (overtime * 1.5);
            }
        }
    }
}
