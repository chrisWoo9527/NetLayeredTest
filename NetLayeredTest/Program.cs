using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NetLayeredTest;
using Sql.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 链接数据库
builder.Services.AddDbContext<TestDbContext>(options =>
{
    var conn = builder.Configuration.GetSection("Conn").Value;
    options.UseSqlServer(conn);
});

// 注册异常fileter的 先执行最下面的自定义异常 然后处理最上面的异常类
builder.Services.Configure<MvcOptions>(options =>
{
    options.Filters.Add<MyExcepiton>();
});

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
