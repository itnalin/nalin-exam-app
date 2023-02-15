using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamApp.Context;
using ExamApp.Domain.Services;
using ExamApp.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ExamApp;

public class Program
{
    public static void Main(string[] args)
    {
        // Uncomment for dev
        //var db = new MainContext();

        //for (var i = 0; i < 10; i++)
        //{
        //    db.Languages.Add(new Language(Guid.NewGuid(), $"Lang {i}"));
        //}

        //db.SaveChanges();

        CreateApp(args).Run();
    }

    public static WebApplication CreateApp(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<MainContext>(options =>
              options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddControllers();
        builder.Services.AddScoped<IStudentsService, StudentsService>();
        builder.Services.AddScoped<ILanguageService, LanguageService>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.ConfigureCustomExceptionMiddleware();

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

        app.Map("languages", (LanguageService service) => service.GetLanguages());

        return app;
    }
}
