using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EmployeeWeb.Models;
using EmployeeWebService.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace EmployeeWeb.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View("~/Views/Home/CreateEmployee.cshtml");
        }

        public IActionResult CreateEmployee(Employee employee)
        {
            var httpMessage = postEmployeeAsync(employee).Result;
            if(httpMessage.IsSuccessStatusCode)
            {
                return View("~/Views/Home/Index.cshtml");
            }
            else
            {
                ModelState.AddModelError("Error", "Connection problem");
                return View("~/Views/Home/Index.cshtml");
            }
        }

        public IActionResult DisplayEmployees()
        {
            List<Employee> employees = getAllEmployees().Result;
            return View("~/Views/Home/Employees.cshtml", employees);
        }

        public IActionResult DisplayOvertimeEmployees()
        {
            List<Employee> employees = getAllOvertimeEmployees().Result;
            return View("~/Views/Home/Employees.cshtml", employees);
        }

        public IActionResult DisplayNonOvertimeEmployees()
        {
            List<Employee> employees = getEmployeesWithoutObertime().Result;
            return View("~/Views/Home/Employees.cshtml", employees);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async Task<HttpResponseMessage> postEmployeeAsync(Employee employee)
        {
            HttpClient client = new HttpClient();
            var json = JsonConvert.SerializeObject(employee);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await client.PostAsync("https://localhost:44382/api/employees", content);
            return result;
        }

        private async Task<List<Employee>> getAllEmployees()
        {
            HttpClient client = new HttpClient();
            var getStringTask = await client.GetStringAsync("https://localhost:44382/api/employees");
            var result = JsonConvert.DeserializeObject<List<Employee>>(getStringTask);
            return result;
        }

        private async Task<List<Employee>> getAllOvertimeEmployees()
        {
            HttpClient client = new HttpClient();
            var getStringTask = await client.GetStringAsync("https://localhost:44382/api/employees?hasOvertime=true");
            var result = JsonConvert.DeserializeObject<List<Employee>>(getStringTask);
            return result;
        }

        private async Task<List<Employee>> getEmployeesWithoutObertime()
        {
            HttpClient client = new HttpClient();
            var getStringTask = await client.GetStringAsync("https://localhost:44382/api/employees?hasOvertime=false");
            var result = JsonConvert.DeserializeObject<List<Employee>>(getStringTask);
            return result;
        }
    }
}
