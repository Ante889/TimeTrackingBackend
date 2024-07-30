using dotenv.net;
using TimeTracking.App.Base;

var builder = WebApplication.CreateBuilder(args);

DotEnv.Load();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.ConfigureSqlContext(connectionString);

var secretKey = DotEnv.Read()["JWT_SECRET_KEY"];
builder.Services.ConfigureJWT(secretKey);

builder.Services.ConfigureSwagger();

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