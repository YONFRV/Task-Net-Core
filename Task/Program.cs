

using Microsoft.EntityFrameworkCore;
using Task.Data;
using Task.Services.Task;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
    builder.Services.AddDbContext<SeeriContext>
    (options => options.UseMySql(builder.Configuration.GetConnectionString("CadenaConexionMysql"), Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.30-mysql")));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//

// Configurar la inyecci�n de dependencia para IRepository y su implementaci�n
builder.Services.AddSingleton<ITask, TaskEj>();

// Configurar la inyecci�n de dependencia para UserService
builder.Services.AddScoped<TaskP>();

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
