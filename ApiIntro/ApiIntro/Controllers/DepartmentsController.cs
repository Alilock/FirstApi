using ApiIntro.DAL;
using ApiIntro.DTOs;
using ApiIntro.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiIntro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public DepartmentsController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _context.Departments.ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute]int id)
        {
            Department? department =await _context.Departments.FindAsync(id);
            if (department == null) return NotFound(new {
            Message = "Axtardiginiz department tapilmadi",
            });
            return Ok(department);

        }
        [HttpPost]
        public async Task<IActionResult> Create(DepartmentCreateDTO department)
        {
            await _context.Departments.AddAsync(new Department { Name=department.Name });
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPut("{id}/update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, DepartmentUpdateDTO departmentUpdate)
        {
            Department? department =await _context.Departments.FindAsync(id);
            if (department is null) return NotFound();
            department.Name = departmentUpdate.Name is null ? department.Name : departmentUpdate.Name;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}/delete")]
        public async Task<IActionResult> DeleteAsync([FromRoute]int id)
        {
            Department? department =await _context.Departments.FindAsync(id);
            if (department is null) return NotFound();
            _context.Remove(department);
            await _context.SaveChangesAsync();
            return Ok(new
            {
                Message = "Departmen is deleted"
            });
        }
    }
}
