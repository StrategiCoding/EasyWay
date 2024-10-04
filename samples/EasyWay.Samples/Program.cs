using EasyWay.Samples;
using EasyWay.Samples.Commands;
using EasyWay.Samples.Databases;
using EasyWay.Samples.Queries;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var webKernelBuilder = WebKernel.CreateBuilder(args);

var builder = webKernelBuilder.AppBuilder;

string connectionString = builder.Configuration.GetConnectionString("Database");
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddEasyWay(new List<Assembly> { typeof(SampleCommand).Assembly });
builder.Services.AddEntityFrameworkCore<SampleDbContext>(x => x.UseNpgsql(connectionString));

var webKernel = webKernelBuilder.Build();

var app = webKernel.App;

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

app.UseEasyWay();

app.MapCommand<SampleCommand>();
app.MapCommand<ErrorCommand>();
app.MapQuery<SampleQuery, SampleQueryResult>();

await webKernel.RunAsync();