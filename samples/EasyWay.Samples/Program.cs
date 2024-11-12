using EasyWay.Samples;
using EasyWay.Samples.Commands;
using EasyWay.Samples.Commands.WithResult;
using EasyWay.Samples.Queries;

var webKernelBuilder = WebKernelBuilder.Create(args);

var builder = webKernelBuilder.AppBuilder;

webKernelBuilder.AddModule<SampleModule, SampleModuleConfigurator>();

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

app.MapCommand<SampleModule, SampleCommand>();
app.MapCommand<SampleModule, SampleCommandWithResult, SampleCommandResult>();
app.MapCommand<SampleModule, ErrorCommand>();
app.MapQuery<SampleModule, SampleQuery, SampleQueryResult>();

await webKernel.RunAsync();