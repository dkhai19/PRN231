using APIsDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIsDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        public static List<Students> data = new List<Students>()
        {
            new Students(1,"Cao Dung", 21),
            new Students(2,"Duc Khai", 21),
            new Students(3,"Tra My",22)
        };

        //GET: api/get
        [HttpGet(Name ="Hahahoho")]
        public IActionResult Get()
        {
            return Ok(data);
        }

        [HttpGet("id")]
        public IActionResult GetStudentByCode(int id)
        {
            Students sv = data.SingleOrDefault(x => x.Code == id);
            if(sv == null)
            {
                return NotFound();
            }
            return Ok(sv);
        }

        [HttpPost]
        public IActionResult AddNewStudent(Students student)
        {
            Students sv = data.SingleOrDefault(x => x.Code == student.Code);
            if(sv == null)
            {
                data.Add(student);
                return Ok(student);
            }
            return BadRequest();
        }

        [HttpPut]
        public IActionResult UpdateStudent(int id, string name, int age)
        {
            Students sv = data.SingleOrDefault(x => x.Code == id);
            if (id != null)
            {
                    sv.FullNam = name;
                    sv.Age = age;
                    return Ok(data);
            }
            return NotFound();
        }
        [HttpDelete]
        public IActionResult DeleteStudent(int id)
        {
            Students sv = data.SingleOrDefault(x => x.Code == id);
            if(id != null)
            {
                data.Remove(sv);
                return Ok(data);
            }
            return NotFound();
        }
    }
}
