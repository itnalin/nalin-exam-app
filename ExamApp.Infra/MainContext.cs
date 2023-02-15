using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using ExamApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamApp.Context;

public class MainContext : DbContext
{
    public MainContext([NotNullAttribute] DbContextOptions<MainContext> options) : base(options)
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Uncomment for dev
        //optionsBuilder.UseInMemoryDatabase("Dev");

        // Comment for dev        
    }

    public DbSet<Student> Students { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Language> Languages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {


        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasMany(d => d.Students)
                .WithOne(p => p.Course)
                .HasForeignKey(d => d.CourseId);

            entity.HasOne(d => d.Language);
        });
    }
}