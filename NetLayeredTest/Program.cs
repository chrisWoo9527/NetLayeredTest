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

// �������ݿ�
builder.Services.AddDbContext<TestDbContext>(options =>
{
    var conn = builder.Configuration.GetSection("Conn").Value;
    options.UseSqlServer(conn);
});

// ע���쳣fileter�� ��ִ����������Զ����쳣 Ȼ������������쳣��
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
