using Microsoft.AspNetCore.Mvc;
using 自定义限流器;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();

builder.Services.AddStackExchangeRedisCache(opts =>
{
    opts.InstanceName = "DisCache_";
    opts.Configuration = "192.168.136.130:8081,defaultDatabase=8";
});
builder.Services.Configure<MvcOptions>(opts => opts.Filters.Add<DistributeTimesRestrictorActionFilter>());

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
