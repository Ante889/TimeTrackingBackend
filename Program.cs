using System.Reflection;
using dotenv.net;
using MediatR;
using TimeTracking.App.Base;
using TimeTracking.App.Person;
using TimeTracking.App.Project;
using TimeTracking.App.Time;
using TimeTracking.App.Category;
using TimeTracking.App.Phase;

var builder = WebApplication.CreateBuilder(args);

DotEnv.Load();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.ConfigureSqlContext(connectionString);

var secretKey = DotEnv.Read()["JWT_SECRET_KEY"];
builder.Services.ConfigureJWT(secretKey);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

builder.Services.ConfigureSwagger();

builder.Services.AddPersonServices();
builder.Services.AddProjectServices();
builder.Services.AddTimeServices();
builder.Services.AddCategoryServices();
builder.Services.AddPhaseServices();

builder.Services.AddControllers()
    .AddApplicationPart(typeof(Program).Assembly)
    .AddControllersAsServices();

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

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
