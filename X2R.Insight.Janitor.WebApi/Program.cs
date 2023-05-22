using Microsoft.EntityFrameworkCore;
using X2R.Insight.Janitor.WebApi.Data;
using X2R.Insight.Janitor.WebApi.interfaces;
using X2R.Insight.Janitor.WebApi.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IQueryInterface, QueryRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<QueryContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("X2R_Janitor"));
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
