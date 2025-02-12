using Microsoft.EntityFrameworkCore;
using PatientManagementAPI.Data;
using PatientManagementAPI.Interfaces;
using PatientManagementAPI.Repositories.Implementations;
using PatientManagementAPI.Repositories.Interfaces;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Add services to the container.
builder.Services.AddControllers();

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// 🔹 Register Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 🔹 Configure Database Connection
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// 🔹 Register Repositories & Services
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IRecordRepository, RecordRepository>();


// 🔹 Add Authorization
builder.Services.AddAuthorization();

var app = builder.Build();
app.UseRouting();

// 🔹 Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 🔹 Enable Authentication & Authorization Middleware
app.UseAuthentication();
app.UseAuthorization();

// 🔹 Map Controllers (Automatically maps all controllers)
app.MapControllers();

app.Run();