using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RangoAgil.API.DbContext;
using RangoAgil.API.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<RangoDbContext>(
    o => o.UseSqlite(
        builder.Configuration["ConnectionStrings:RangoDbConStr"]
    )
);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/rangos",
    async Task<Results<NoContent, Ok<IEnumerable<RangoDTO>>>>
    (RangoDbContext rangoDbContext,
        IMapper mapper,
        [FromQuery(Name = "name")] string? rangoNome) =>
    {
        var rangoEntities = await rangoDbContext.Rangos
            .Where(r => rangoNome == null || r.Name.ToLower().Contains(rangoNome.ToLower()))
            .ToListAsync();

        if (rangoEntities.Count == 0)
            return TypedResults.NoContent();

        var results = mapper.Map<IEnumerable<RangoDTO>>(rangoEntities);
        return TypedResults.Ok(results);
    });


app.MapGet("/rango/{id:int}",
    async Task<Results<Ok<RangoDTO>, NotFound>> (RangoDbContext rangoDbContext, IMapper mapper, int id) =>
    {
        var rango = await rangoDbContext.Rangos.FirstOrDefaultAsync(x => x.Id == id);
        if (rango == null) return TypedResults.NotFound();

        var results = mapper.Map<RangoDTO>(rango);
        return TypedResults.Ok(results);
    });


app.MapGet("/rango/{rangoId:int}/ingredientes", async (
    RangoDbContext rangoDbContext,
    IMapper mapper,
    int rangoId) =>
{
    return mapper.Map<IEnumerable<IngredienteDTO>>((await rangoDbContext.Rangos
        .Include(rango => rango.Ingredientes)
        .FirstOrDefaultAsync(rango => rango.Id == rangoId))?.Ingredientes);
});

app.Run();