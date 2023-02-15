using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamApp.Context;
using Microsoft.EntityFrameworkCore;

namespace ExamApp.Services;

public interface IStudentsService
{
    Task<IEnumerable<Student>> GetAllStudentsAsync();
    Task<Student?> GetStudentAsync(int id);
    Task AddStudendAsync(Student student);
    Task ModifyAsync(int id, Student student);
    IEnumerable<Course> GetCourses();
    Task<Course?> GetCourseAsync(Guid id);
    Task ModifyCourseAsync(Guid id, Course course);
    Task AddStudentToCourseAsync(int studentId, Guid courseId);
}

public class StudentsService : IStudentsService
{
    private readonly MainContext ctx;

    public StudentsService(MainContext _ctx)
    {
        ctx = _ctx;
    }
    public async Task<IEnumerable<Student>> GetAllStudentsAsync()
    {
        return await ctx.Students.ToListAsync();
    }

    public async Task<Student?> GetStudentAsync(int id)
    {
        return await ctx.Students.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddStudendAsync(Student student)
    {
        if (student.Age < 18)
        {
            throw new Exception();
        }

        await ctx.Students.AddAsync(student);
        await ctx.SaveChangesAsync();
    }

    public async Task ModifyAsync(int id, Student student)
    {
        if (student.Age < 18)
        {
            throw new Exception();
        }

        student.Id = id;

        ctx.Attach(student).State = EntityState.Modified;
        await ctx.SaveChangesAsync();
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

    public async Task<Course?> GetCourseAsync(Guid id)
    {
        return await ctx.Courses
            .Include(x => x.Students)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task ModifyCourseAsync(Guid id, Course course)
    {
        course.Id = id;

        ctx.Attach(course).State = EntityState.Modified;
        await ctx.SaveChangesAsync();
    }

    public async Task AddStudentToCourseAsync(int studentId, Guid courseId)
    {
        var course = await this.GetCourseAsync(courseId);
        var student = await this.GetStudentAsync(studentId);

        if (course == null)
        {
            throw new Exception();
        }

        if (student == null || student.Age < 18)
        {
            throw new Exception();
        }

        course.Students.Add(student);
        await this.ModifyCourseAsync(courseId, course);
    }
}