using Microsoft.EntityFrameworkCore;
using RedbrowBackendTest.DBContext;
using RedbrowBackendTest.Repository.Interfaces;
using RedbrowBackendTest.Repository;
using RedbrowBackendTest.Services.Interfaces;
using RedbrowBackendTest.Services;
using RedbrowBackendTest.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TestDBContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.MapControllers();

app.Run();
