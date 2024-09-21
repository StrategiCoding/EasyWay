using EasyWay;
using EasyWay.EntityFrameworkCore;
using EasyWay.Samples;
using EasyWay.Samples.Commands;
using EasyWay.Samples.Queries;
using Microsoft.EntityFrameworkCore;
using Testcontainers.PostgreSql;

var postgreSqlContainer = new PostgreSqlBuilder()
  .WithImage("postgres:15.1")
  .Build();

await postgreSqlContainer.StartAsync();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddEasyWay(typeof(SampleCommand).Assembly);
builder.Services.AddEntityFrameworkCore<SampleDbContext>(x => x.UseNpgsql(postgreSqlContainer.GetConnectionString()));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{

    scope.ServiceProvider.GetRequiredService<SampleDbContext>().Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapCommand<SampleCommand>();
app.MapCommand<ErrorCommand>();
app.MapQuery<SampleQuery, SampleQueryResult>();

app.Run();
