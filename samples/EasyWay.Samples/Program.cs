using EasyWay.Samples;
using EasyWay.Samples.Commands;
using EasyWay.Samples.Commands.WithResult;
using EasyWay.Samples.Queries;
/*
var webKernelBuilder = WebKernelBuilder.Create(args);

var builder = webKernelBuilder.AppBuilder;

webKernelBuilder.AddModule<SampleModule>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var webKernel = await webKernelBuilder.BuildAsync();

var app = webKernel.App;

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

webKernel.MapCommand<SampleModule, SampleCommand>();
webKernel.MapCommand<SampleModule, SampleCommandWithResult, SampleCommandResult>();
webKernel.MapCommand<SampleModule, ErrorCommand>();
webKernel.MapQuery<SampleModule, SampleQuery, SampleQueryResult>();



await webKernel.RunAsync();
*/

Console.WriteLine("TEST");