using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamApp.Context;
using Microsoft.EntityFrameworkCore;

namespace ExamApp.Services;

public interface IStudentsService
{
    IEnumerable<Student> GetAllStudents();
    Task AddStudend(Student student);
    void Modify(int id, Student student);
    IEnumerable<Course> GetCourses();
    Course GetCourse(Guid id);
    void ModifyCourse(Guid id, Course course);
}

public class StudentsService : IStudentsService
{
    private readonly MainContext ctx;

    public StudentsService(MainContext _ctx)
    {
        ctx = _ctx;
    }
    public IEnumerable<Student> GetAllStudents()
    {        
        return ctx.Students.ToList();
    }

    public async Task AddStudend(Student student)
    {
        if (student.Age < 18)
        {
            throw new Exception();
        }

        await ctx.Students.AddAsync(student);
        await ctx.SaveChangesAsync();
    }

    public void Modify(int id, Student student)
    {
        if (student.Age < 18)
        {
            throw new Exception();
        }
            
        student.Id = id;

        ctx.Attach(student).State = EntityState.Modified;
        ctx.SaveChanges();
    }

    public IEnumerable<Course> GetCourses()
    {
        var courses = ctx.Courses
            .ToArrayAsync()
            .Result
            .AsEnumerable();

        courses.OrderBy(c => c.Title);

        return courses;
    }

    public Course GetCourse(Guid id)
    {
        return ctx.Courses
            .Include(x => x.Students)
            .FirstOrDefault(x => x.Id == id);
    }

    public void ModifyCourse(Guid id, Course course)
    {
        course.Id = id;

        ctx.Attach(course).State = EntityState.Modified;
        ctx.SaveChanges();
    }
}