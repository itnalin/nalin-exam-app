using ExamApp.Context;
using ExamApp.Domain.Services;
using ExamApp.Models;
using ExamApp.Tests.Utilities;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ExamApp.Domain.Tests.Services
{
    public class StudentsServiceTest
    {
        private readonly IInMemoryDatabaseFactory<MainContext> _inMemoryDatabaseFactory;
        public StudentsServiceTest()
        {
            _inMemoryDatabaseFactory = new InMemoryDatabaseFactory<MainContext>();
        }

        [Fact]
        public async Task GetStudent_ShouldGetAnyStudent()
        {
            //Arrange
            var dbContext = _inMemoryDatabaseFactory.Create("SupplierInMemoryDb");
            SeedDatabase(dbContext);

            IStudentsService studentService = new StudentsService(dbContext);

            var result = await studentService.GetStudentAsync(1);

            //Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.Name.Should().Be("Sam");
            result.Course.Should().NotBeNull();
        }

        private void SeedDatabase(MainContext dbContext)
        {
            // SupplierDetail            
            dbContext.Students.AddAsync(new Student(){
                Id = 1,
                Name = "Sam",
                Age= 21,
            });

            dbContext.Students.AddAsync(new Student()
            {
                Name = "Tom",
                Age = 22,
            });

            dbContext.SaveChanges();
        }
    }
}
