using ApplicationCore;
using ApplicationCore.Interfaces;
using Infraestructure;
using Infraestructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers();
builder.Services.AddApplicationCore();
builder.Services.AddInfraestructure(builder.Configuration);
builder.Services.AddScoped<IEmailService, EmailService>();


var app = builder.Build();
await app.Services.InitializeDatabasesAsync();

app.UseInfraestructure();
app.Run();
