using Bogus;
using dev_api.Models;
using Microsoft.AspNetCore.Mvc;

namespace dev_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Student>> GetStudents([FromQuery] int count = 10)
        {
            var studentFaker = new Faker<Student>()
                .RuleFor(s => s.Id, f => f.Random.Guid())
                .RuleFor(s => s.FirstName, f => f.Name.FirstName())
                .RuleFor(s => s.LastName, f => f.Name.LastName())
                .RuleFor(s => s.Email, f => f.Internet.Email())
                .RuleFor(s => s.DateOfBirth, f => f.Date.Past(25, DateTime.Now.AddYears(-18)))
                .RuleFor(s => s.Major, f => f.PickRandom("Computer Science", "Mathematics", "Physics", "Chemistry", "Biology", "Engineering", "Business", "Psychology"))
                .RuleFor(s => s.GPA, f => f.Random.Double(2.0, 4.0));

            var students = studentFaker.Generate(count);

            return Ok(students);
        }
    }
}
