

using Microsoft.EntityFrameworkCore;
using Task.Data;
using Task.Services.Task.DeleteTaskService;
using Task.Services.Task.GetByTaskService;
using Task.Services.Task.GetTaskService;
using Task.Services.Task.PostTaskService;
using Task.Services.Task.PutTaskService;
using Task.Services.TypeStateService.DeleteTypeStateService;
using Task.Services.TypeStateService.GetAllTypeStateService;
using Task.Services.TypeStateService.GetByTypeStateService;
using Task.Services.TypeStateService.PostSaveTypeStateService;
using Task.Services.TypeStateService.UpdateTypeStateService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<SeeriContext>
(options => options.UseMySql(builder.Configuration.GetConnectionString("CadenaConexionMysql"), Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.30-mysql")));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Configurar la inyección de dependencia para UserService
builder.Services.AddScoped<IGetAllTaskService, GetAllTaskService>();
builder.Services.AddScoped<IGetByTaskService, GetByTaskService>();
builder.Services.AddScoped<IPostTaskService, PostTaskService>();
builder.Services.AddScoped<IPutTaskService, PutTaskService>();
builder.Services.AddScoped<IDeleteTaskService, DeleteTaskService>();
//
builder.Services.AddScoped<IGetAllTypeStateService, GetAllTypeStateService>();
builder.Services.AddScoped<IGetByTypeStateService, GetByTypeStateService>();
builder.Services.AddScoped<IPostSaveTypeStateService, PostSaveTypeStateService>();
builder.Services.AddScoped<IPutTypeStateService, PutTypeStateService>();
builder.Services.AddScoped<IDeleteTypeStateService, DeleteTypeStateService>();
//
builder.Services.AddLogging(builder =>
{
    builder.AddConsole();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
