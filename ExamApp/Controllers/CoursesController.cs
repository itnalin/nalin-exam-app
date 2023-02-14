using System;
using System.Linq;
using System.Threading.Tasks;
using ExamApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExamApp.Controllers;

public class CoursesController: ControllerBase
{
    private IStudentsService _service;

    public CoursesController(IStudentsService service)
    {
        _service = service;
    }

    [HttpGet, Route("all")]
    public IActionResult GetAll()
    {
        return Ok(_service.GetCourses());
    }

    [HttpPost]
    [HttpPost, Route("addstudent")]
    public async Task<IActionResult> AddStudentToCourseAsync(int studentId, Guid courseId)
    {
        await _service.AddStudentToCourseAsync(studentId, courseId);

        return Ok();
    }
}
