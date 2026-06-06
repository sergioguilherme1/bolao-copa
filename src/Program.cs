using BolaoCopa.API.Data;
using BolaoCopa.API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<BolaoDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<ApostaService>();
builder.Services.AddScoped<ApuracaoService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
