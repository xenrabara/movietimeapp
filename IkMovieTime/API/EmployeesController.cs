using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.ModelBinding;
using IkMovieTime.Models;

namespace IkMovieTime.API
{
    public class EmployeesController : ApiController
    {
        private MovieTimeEntities db = new MovieTimeEntities();

        // GET: api/Employees
        public IQueryable<Employee> GetEmployees()
        {
            return db.Employees;
        }

        public IQueryable<Employee> GetEmployeesPerTeam(long? id)
        {
            return db.Employees.Where(e => e.TeamID == id).OrderByDescending(e => e.IsTeamLeader).ThenBy(e => e.FirstName);
        }

        // GET: api/Employees/5
		//[ResponseType(typeof(Employee))]
		//public async Task<IHttpActionResult> GetEmployee(long? id)
		//{
		//	Employee employee = await db.Employees.FindAsync(id);
		//	if (employee == null)
		//	{
		//		return NotFound();
		//	}

		//	return Ok(employee);
		//}

        public async Task<IHttpActionResult> AddRemoveEmployeeFromTeam(EmployeeTeamWrapper employeeInTeam)
		{
			var employee = await db.Employees.FirstAsync(e => e.EmployeeID == employeeInTeam.EmployeeId);
			employee.TeamID = employeeInTeam.TeamId == 0? (long?) null: employeeInTeam.TeamId;
			await db.SaveChangesAsync();
            return Ok();
        }

        // PUT: api/Employees/5
		//[ResponseType(typeof(void))]
		//public async Task<IHttpActionResult> PutEmployee(long id, Employee employee)
		//{
		//	if (!ModelState.IsValid)
		//	{
		//		return BadRequest(ModelState);
		//	}

		//	if (id != employee.EmployeeID)
		//	{
		//		return BadRequest();
		//	}

		//	db.Entry(employee).State = EntityState.Modified;

		//	try
		//	{
		//		await db.SaveChangesAsync();
		//	}
		//	catch (DbUpdateConcurrencyException)
		//	{
		//		if (!EmployeeExists(id))
		//		{
		//			return NotFound();
		//		}
		//		else
		//		{
		//			throw;
		//		}
		//	}

		//	return StatusCode(HttpStatusCode.NoContent);
		//}

		//// POST: api/Employees
		//[ResponseType(typeof(Employee))]
		//public async Task<IHttpActionResult> PostEmployee(Employee employee)
		//{
		//	if (!ModelState.IsValid)
		//	{
		//		return BadRequest(ModelState);
		//	}

		//	db.Employees.Add(employee);
		//	await db.SaveChangesAsync();

		//	return CreatedAtRoute("DefaultApi", new { id = employee.EmployeeID }, employee);
		//}

        // DELETE: api/Employees/5
        [ResponseType(typeof(Employee))]
        public async Task<IHttpActionResult> DeleteEmployee(long id)
        {
            Employee employee = await db.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            db.Employees.Remove(employee);
            await db.SaveChangesAsync();

            return Ok(employee);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeeExists(long id)
        {
            return db.Employees.Count(e => e.EmployeeID == id) > 0;
        }
    }
}