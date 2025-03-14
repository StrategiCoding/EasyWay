using EasyWay.Samples;
using EasyWay.Samples.Commands;
using EasyWay.Samples.Commands.WithResult;
using EasyWay.Samples.Domain.Policies;
using EasyWay.Samples.Queries;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);



var kernel = Kernel.Create();

await kernel
    .AddModule<SampleModule>()
    .BuildAsync(builder.Services);
builder.Services.AddLogging();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddEasyWayWebApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseEasyWay();


app.MapPost("/query", async ([FromBody] SampleQuery query, IWebApiModulExecutor<SampleModule> executor) =>
{
    return await executor.Query<SampleQuery, SampleQueryResult>(query);
});

app.MapPost("/commandWithError", async ([FromBody] ErrorCommand command, IWebApiModulExecutor<SampleModule> executor) =>
{
    return await executor.Command(command);
});

app.MapPost("/command", async ([FromBody] SampleCommand command, IWebApiModulExecutor<SampleModule> executor) =>
{
    return await executor.Command(command);
});

app.MapPost("/commandwithresult", async ([FromBody] SampleCommandWithResult command, IWebApiModulExecutor<SampleModule> executor) =>
{
    return await executor.Command<SampleCommandWithResult, SampleCommandResult>(command);
});

app.Run();

Console.WriteLine("TEST");