using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeWebService.Data;
using EmployeeWebService.Models;

namespace EmployeeWebService.Controllers
{
    [Route("api")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeContext _context;
        private readonly IEmployeeService _employeeService;

        public EmployeesController(EmployeeContext context, IEmployeeService employeeService)
        {
            _context = context;
            _employeeService = employeeService;
        }


        [HttpGet]
        [Route("employees")]
        public async Task<ActionResult<IEnumerable<Employee>>> Getemployees(Boolean? hasOvertime)
        {
            List<Employee> employees = await _context.employees.ToListAsync();
            List<Employee> filteredList = new List<Employee>();
            if (hasOvertime == null)
            {
                return employees;
            }
            if(hasOvertime == true)
            {
                filteredList = _employeeService.FilterEmployeesBasedOnOvertime(employees, true);
                return filteredList;
            }
            else if(hasOvertime == false)
            {
                filteredList = _employeeService.FilterEmployeesBasedOnOvertime(employees, false);
                return filteredList;
            }
            else
            {
                return employees;
            }

        }

        // GET: api/Employees
        //[HttpGet]
        //[Route("employees")]
        //public async Task<ActionResult<IEnumerable<Employee>>> GetAllemployees()
        //{
        //    return await _context.employees.ToListAsync();
        //}



        // GET: api/Employees/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Employee>> GetEmployee(string id)
        //{
        //    var employee = await _context.employees.FindAsync(id);

        //    if (employee == null)
        //    {
        //        return NotFound();
        //    }

        //    return employee;
        //}

        // PUT: api/Employees/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutEmployee(string id, Employee employee)
        //{
        //    if (id != employee.Name)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(employee).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!EmployeeExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Employees
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        [Route("employees")]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            _context.employees.Add(employee);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EmployeeExists(employee.Name))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetEmployee", new { id = employee.Name }, employee);
        }

        // DELETE: api/Employees/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Employee>> DeleteEmployee(string id)
        //{
        //    var employee = await _context.employees.FindAsync(id);
        //    if (employee == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.employees.Remove(employee);
        //    await _context.SaveChangesAsync();

        //    return employee;
        //}
        //[HttpGet]
        //[Route("payments")]
        //public async Task<ActionResult<double>> GetPayments()
        //{
        //    List<Employee> employees = await _context.employees.ToListAsync();
        //    double total = _employeeService.GetTotalMonthlyExpense(employees);
        //    return total;
        //}

        [HttpGet]
        [Route("payments")]
        public async Task<ActionResult<double>> GetPaymentsFilteres(Boolean? hasOvertime)
        {
            List<Employee> employees = await _context.employees.ToListAsync();
            List<Employee> filteredList = new List<Employee>();
            if (hasOvertime == null)
            {
                double total = _employeeService.GetTotalMonthlyExpense(employees);
                return total;
            }
            if (hasOvertime == true)
            {
                filteredList = _employeeService.FilterEmployeesBasedOnOvertime(employees, true);
                double total = _employeeService.GetTotalMonthlyExpense(filteredList);
                return total;
            }
            else if (hasOvertime == false)
            {
                filteredList = _employeeService.FilterEmployeesBasedOnOvertime(employees, false);
                double total = _employeeService.GetTotalMonthlyExpense(filteredList);
                return total;
            }
            else
            {
                return 0;
            }

        }

        private bool EmployeeExists(string id)
        {
            return _context.employees.Any(e => e.Name == id);
        }
    }
}
