using Authentication.API;

// Load environment variables file
switch (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"))
{
    case "Development":
        DotNetEnv.Env.Load(".env.dev");
        break;

    case "Quality":
        DotNetEnv.Env.Load(".env.qa");
        break;

    default:
        DotNetEnv.Env.Load(".env");
        break;
}
var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

var app = builder.Build();

startup.Configure(app, app.Environment);

app.Run();
