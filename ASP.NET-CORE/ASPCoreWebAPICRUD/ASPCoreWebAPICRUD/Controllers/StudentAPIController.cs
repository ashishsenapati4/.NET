using ASPCoreWebAPICRUD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPCoreWebAPICRUD.Controllers
{
   
    [ApiController]
    [Route("api/[Controller]")]
    public class StudentAPIController : Controller
    {
        private readonly MyDbContext myDbContext;
        public StudentAPIController(MyDbContext _dbContext)
        {
            myDbContext = _dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Student>>> GetAllStudent()
        {
            var students = await myDbContext.Students.ToListAsync();
            return Ok(students);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudentById(int id)
        {

            var student = await myDbContext.Students.FirstOrDefaultAsync(x => x.Id == id);
            if(student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        [HttpPost]
        public async Task<ActionResult<Student>> AddOrUpdateStudent(Student std)
        {
            if(std.Id == null || std.Id == 0)
            {
                //insert
                await myDbContext.Students.AddAsync(std);
                await myDbContext.SaveChangesAsync();
                return Ok(std);
            }
            else
            {
                //update
                var exStudent = await myDbContext.Students.FindAsync(std.Id);
                if(exStudent == null)
                {
                    return NotFound();
                }
                else
                {
                    exStudent.StudentName = std.StudentName;
                    exStudent.FatherName = std.FatherName;
                    exStudent.StudentGender = std.StudentGender;
                    exStudent.Age = std.Age;
                    exStudent.Standard = std.Standard;
                    myDbContext.Students.Update(exStudent);
                    await myDbContext.SaveChangesAsync();
                    return Ok(exStudent);
                }
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> DeleteStudent(int id)
        {
            var student = await myDbContext.Students.FindAsync(id);
            if(student == null)
            {
                return NotFound();
            }

            myDbContext.Students.Remove(student);
            await myDbContext.SaveChangesAsync();
            return Ok("Deleted");

        }
    }
}
