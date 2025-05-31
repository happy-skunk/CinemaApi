using CinemaApi.Cache;
using CinemaApi.Data;
using CinemaApi.Decorator;
using CinemaApi.Repository;
using CinemaApi.Repository.Specific;
using CinemaApi.Services.Actor;
using CinemaApi.Services.Director;
using CinemaApi.Services.Genre;
using CinemaApi.Services.Movie;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;

var builder = WebApplication.CreateBuilder(args);

// Add DB context
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add controllers
builder.Services.AddControllers();

// Register generic repository
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// Register specific repositories
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IActorRepository, ActorRepository>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<IDirectorRepository, DirectorRepository>();

// Register Cache
builder.Services.AddMemoryCache();
builder.Services.AddSingleton<ICacheService, CacheService>();

// Register services
builder.Services.AddScoped<MovieService>();
builder.Services.AddScoped<IMovieService>(provider => 
{
    var inner = provider.GetRequiredService<MovieService>();
    var cache = provider.GetRequiredService<ICacheService>();
    return new MovieCacheDecorator(inner, cache);
});
builder.Services.AddScoped<IActorService, ActorService>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IDirectorService, DirectorService>();


// Swagger setup
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Dev only Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();