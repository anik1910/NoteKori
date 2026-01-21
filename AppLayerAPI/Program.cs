using BLL.Services;
using DAL;
using DAL.EF;
using DAL.Interfaces;
using DAL.Repos;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<DataAccessFactory>();

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<NoteService>();
builder.Services.AddScoped<EmailService>();

builder.Services.AddScoped<NoteRepo>();
builder.Services.AddScoped<ReviewRepo>();
builder.Services.AddScoped<UserRepo>();

builder.Services.AddDbContext<NMSContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DbConn"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
