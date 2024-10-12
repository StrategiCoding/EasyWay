using EasyWay;
using Payments.Infrastructure;

var webKernelBuilder = WebKernel.CreateBuilder(args);

var builder = webKernelBuilder.AppBuilder;

webKernelBuilder.AddModule<PaymentsModule>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var webKernel = webKernelBuilder.Build();

var app = webKernel.App;

using (var scope = app.Services.CreateScope())
{

    scope.ServiceProvider.GetRequiredService<PaymentsDbContext>().Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.MapCommand<CreatePaymentCommand>();

await webKernel.RunAsync();