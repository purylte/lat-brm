using lat_brm.Contracts;
using lat_brm.Dtos.Employee;
using lat_brm.Models;
using lat_brm.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace lat_brm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<TbMEmployee> employees;
            try
            {
                employees = _employeeRepository.GetAll();
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            var employeeResponses = employees.Select(employee => (EmployeeResponse)employee);
            return Ok(employeeResponses);
        }

        [HttpGet("{id:Guid}")]
        public IActionResult GetById(Guid id)
        {
            TbMEmployee? employee;
            try
            {
                employee = _employeeRepository.GetByGuid(id);
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            if (employee == null)
            {
                return NotFound("Employee not found");
            }
            return Ok((EmployeeResponse)employee);
        }

        [HttpPost]
        public IActionResult Insert(EmployeeRequestInsert request)
        {
            TbMEmployee employee;
            try
            {
                employee = _employeeRepository.Insert(request);
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return Ok((EmployeeResponse)employee);
        }

        [HttpPut]
        public IActionResult Update(EmployeeRequestUpdate request)
        {
            TbMEmployee? employee;
            try
            {
                employee = _employeeRepository.GetByGuid(request.Guid);
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            if (employee == null)
            {
                return NotFound("Employee not found");
            }

            TbMEmployee requestObj = request;
            requestObj.CreatedDate = employee.CreatedDate;

            TbMEmployee response;
            try
            {
                //response = _employeeRepository.Update(employee, requestObj);
                response = _employeeRepository.Update(requestObj);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return Ok((EmployeeResponse)response);
        }

        [HttpDelete("{id:Guid}")]
        public IActionResult Delete(Guid id)
        {
            TbMEmployee? employee;
            try
            {
                employee = _employeeRepository.GetByGuid(id);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            if (employee == null)
            {
                return NotFound("Employee not found");
            }

            try
            {
                _employeeRepository.Delete(employee);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return NoContent();
        }

    }
}
