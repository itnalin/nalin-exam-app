using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamApp.Domain.Services;
using ExamApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExamApp.Controllers;

[ApiController]
[Route("courses")]
public class CoursesController: ControllerBase
{
    private IStudentsService _service;

    public CoursesController(IStudentsService service)
    {
        _service = service;
    }

    [HttpGet, Route("all")]
    [ProducesResponseType(typeof(IEnumerable<Course>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult GetAll()
    {
        return Ok(_service.GetCourses());
    }

    [HttpPost, Route("addstudent")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddStudentToCourseAsync(int studentId, Guid courseId)
    {
        await _service.AddStudentToCourseAsync(studentId, courseId);

        return Ok();
    }
}
