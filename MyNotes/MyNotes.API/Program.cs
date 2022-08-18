using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using MyNotes.API.Extensions;
using MyNotes.Application.Profiles;
using MyNotes.Application.UserCommands;
using MyNotes.Domain.Repositories;
using MyNotes.Domain.Services.Abstractions;
using MyNotes.Infrastructure.Data;
using MyNotes.Infrastructure.Services;
using MyNotes.Infrastructure.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerDoc();

builder.Services.AddDbContext<MyNotesDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddAutoMapper(typeof(UserProfile));
builder.Services.AddMediatR(typeof(CreateUserCommand));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAuthManager, AuthManager>();

builder.Services.ConfigureIdentity();
builder.Services.ConfigureJwt(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
