using System.Collections.Generic;
using ExamApp.Context;

namespace ExamApp.Services;

public class LanguageService
{
    private readonly MainContext ctx;

    public LanguageService(MainContext _ctx)
    {
        ctx = _ctx;
    }
    public IAsyncEnumerable<Language> GetLanguages()
    {
        //var ctx = new MainContext();

        return ctx.Languages.AsAsyncEnumerable();
    }
}