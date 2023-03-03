using FluentValidation;
using MovieApp.Data;
using MovieApp.DTOs;
using MovieApp.Repositories;
using MovieApp.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddDbContext<MovieDbContext>();
builder.Services.AddScoped<IValidator<MovieDTO>, MovieValidator>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});

app.UseHttpsRedirection();

app.MapGet("movies", async (IMovieRepository movieRepository) =>
    Results.Ok(await movieRepository.GetMovies()));

app.MapGet("movie/{id}", async (IMovieRepository movieRepository, int id) =>
{
    var movie = await movieRepository.GetMovie(id);
    if (movie == null)
        return Results.BadRequest("Movie with this id could not found!");
    return Results.Ok(movie);
});

app.MapPost("movie/add", async (IMovieRepository movieRepository, IValidator <MovieDTO> validator, MovieDTO movieDTO) =>
{
    var validationResult = await validator.ValidateAsync(movieDTO);
    if (!validationResult.IsValid)
    {
        return Results.ValidationProblem(validationResult.ToDictionary());
    }
    var movie = await movieRepository.AddMovie(movieDTO);
    if (movie == null)
        return Results.BadRequest("The movie could not added!");
    return Results.Ok(movie);
});

app.MapPut("movie/update/{id}", async (IMovieRepository movieRepository, int id, IValidator <MovieDTO> validator, MovieDTO movieDTO) =>
{
    var validationResult = await validator.ValidateAsync(movieDTO);
    if (!validationResult.IsValid)
    {
        return Results.ValidationProblem(validationResult.ToDictionary());
    }
    var result = await movieRepository.UpdateMovie(id, movieDTO);
    if (result.Type == "BAD")
    {
        return Results.BadRequest(result.Message);
    }
    return Results.Ok(result.Message);
});

app.MapDelete("movie/delete/{id}", async (IMovieRepository movieRepository, int id) =>
{
    var result = await movieRepository.DeleteMovie(id);
    if (result.Type == "BAD")
    {
        return Results.BadRequest(result.Message);
    }
    return Results.Ok(result.Message);
});

app.Run();