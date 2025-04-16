// Program.cs
using OrderManagementAPI.Data;
using OrderManagementAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// 添加数据库上下文
builder.Services.AddScoped<OrderContext>();

// 添加订单服务
builder.Services.AddScoped<OrderService>();

// 添加控制器
builder.Services.AddControllers();

// 配置 Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 开发环境下使用 Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();