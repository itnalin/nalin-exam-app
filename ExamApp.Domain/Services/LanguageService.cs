using System.Collections.Generic;
using ExamApp.Context;
using ExamApp.Models;

namespace ExamApp.Domain.Services;

public interface ILanguageService
{
    IAsyncEnumerable<Language> GetLanguages();
}

public class LanguageService: ILanguageService
{
    private readonly MainContext ctx;

    public LanguageService(MainContext ctx)
    {
        this.ctx = ctx;
    }

    public IAsyncEnumerable<Language> GetLanguages()
    {
        return ctx.Languages.AsAsyncEnumerable();
    }
}