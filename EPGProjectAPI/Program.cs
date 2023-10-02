using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using EPGDataAccess;
using Microsoft.Extensions.Options;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using EPGApplication.Services.IServices;
using EPGApplication.Services.Services;
using EPGDataAccess.Repositories;
using EPGApplication.Repositories.IRepositories;
using AutoMapper;
using EPGApplication;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using EPGApplication.Repositories.NormalRepositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(configure =>
{
    configure.CacheProfiles.Add("Any-60", new CacheProfile
    {
        Location = ResponseCacheLocation.Any,
        Duration = 60
    });
    configure.CacheProfiles.Add("NothingAtAll", new CacheProfile { NoStore = true });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddResponseCaching();
builder.Services.AddMemoryCache();
builder.Services.AddProblemDetails();
builder.Services.AddDbContext<DataInstance>((Options =>
{
    Options.UseSqlServer(builder.Configuration.GetConnectionString("DataInstance"));
    Options.EnableSensitiveDataLogging(builder.Environment.IsDevelopment());
}));
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<INoteService, NoteService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IWorkService, WorkService>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<INoteRepository, NoteRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IWorkRepository, WorkRepository>();
builder.Services.AddCors();


//builder.Services.AddIdentity<ServiceUser, IdentityRole>(options =>
//{
//    options.User.RequireUniqueEmail = true;
//})
//    .AddEntityFrameworkStores<DataInstance>().AddDefaultTokenProviders();
var app = builder.Build();
app.UseCors(p => p.WithOrigins("http://localhost:3000")
    .AllowAnyHeader()
    .AllowAnyMethod());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler(applicationBuilder =>
    {
        applicationBuilder.Run(async context =>
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var problemDetails = new ProblemDetails
            {
                Title = "An unexpected error ocurred",
                Status = context.Response.StatusCode,
                Detail = "Please contact your system administrator"
            };
            var errorJson = JsonSerializer.Serialize(problemDetails);
            await context.Response.WriteAsync(errorJson);
        });
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseResponseCaching();

app.MapControllers();

app.Run();
