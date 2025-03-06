using EasyWay.Samples;
using EasyWay.Samples.Queries;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);



var kernel = Kernel.Create();

await kernel
    .AddModule<SampleModule>()
    .BuildAsync(builder.Services);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapPost("/query", async ([FromBody] SampleQuery query, IModuleExecutor<SampleModule> executor) =>
{
    return await executor.Execute(query);
});


app.Run();

Console.WriteLine("TEST");