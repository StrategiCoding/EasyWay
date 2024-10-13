using EasyWay.Samples;
using EasyWay.Samples.Commands;
using EasyWay.Samples.Databases;
using EasyWay.Samples.Queries;

var webKernelBuilder = WebKernel.CreateBuilder(args);

var builder = webKernelBuilder.AppBuilder;

webKernelBuilder.AddModule<SampleModule, SampleModuleConfigurator>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.MapCommand<SampleModule, SampleCommand>();
app.MapCommand<SampleModule, ErrorCommand>();
app.MapQuery<SampleModule, SampleQuery, SampleQueryResult>();

await webKernel.RunAsync();