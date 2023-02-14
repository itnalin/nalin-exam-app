using System.Collections.Generic;
using ExamApp.Context;
using ExamApp.Domain.Models;

namespace ExamApp.Domain.Services;

public class LanguageService
{
    public IAsyncEnumerable<Language> GetLanguages()
    {
        var ctx = new MainContext();

        return ctx.Languages.AsAsyncEnumerable();
    }
}