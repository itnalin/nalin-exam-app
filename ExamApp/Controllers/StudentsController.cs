using System;
using System.Linq;
using System.Threading.Tasks;
using ExamApp.Context;
using ExamApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExamApp.Controllers;

[ApiController]
[Route("students")]
public class StudentsController : ControllerBase
{
    private IStudentsService _service;

    public StudentsController(IStudentsService service)
    {
        _service = service;
    }

    [HttpGet, Route("all")]
    public async Task<IActionResult> GetAllAsync()
    {
        try
        {
            return Ok(await _service.GetAllStudentsAsync());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync(int id)
    {
        try
        {
            return Ok(await _service.GetStudentAsync(id));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(Student student)
    {
        try
        {
            await _service.AddStudendAsync(student);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut, Route("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] Student student)
    {
        try
        {
            await _service.ModifyAsync(id, student);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}