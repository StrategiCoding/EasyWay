using EasyWay;
using EasyWay.EntityFrameworkCore;
using EasyWay.Samples;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddEasyWay(typeof(SampleCommand).Assembly);

var app = builder.Build();

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
