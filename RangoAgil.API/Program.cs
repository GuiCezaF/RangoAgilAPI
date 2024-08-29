using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RangoAgil.API.DbContext;
using RangoAgil.API.Entities;
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

app.MapGet("/rangos", async Task<Results<NoContent, Ok<List<Rango>>>>
    (RangoDbContext rangoDbContext,
    [FromQuery(Name = "name")] string? rangoNome) =>
{
    
    var rangoEntity = await rangoDbContext.Rangos
        .Where(r => rangoNome == null ||  r.Name.ToLower().Contains(rangoNome.ToLower()))
        .ToListAsync();

    if (rangoEntity.Count <= 0)
        return TypedResults.NoContent();
        
    return TypedResults.Ok(rangoEntity);
});

app.MapGet("/rango/{id:int}",
    async (RangoDbContext rangoDbContext, int id) =>
    {
        return await rangoDbContext.Rangos.FirstOrDefaultAsync(x => x.Id == id);
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