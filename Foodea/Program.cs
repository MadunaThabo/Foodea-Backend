using Foodea.Data;
using Foodea.Services;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(option => {
    option.AddPolicy("allowedOrigin",
         builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
            );
});

builder.Services.AddScoped<IUserServices, UserService>();
builder.Services.AddScoped<ISpoonacularServices, SpoonacularService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<FoodeaDbContext>(options =>options.UseNpgsql(builder.Configuration.GetConnectionString("CockroachDb")));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("allowedOrigin");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
