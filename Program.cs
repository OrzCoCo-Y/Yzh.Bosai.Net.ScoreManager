using Yzh.Bosai.Net.ScoreManager.Api.Application.SignalR;

namespace Yzh.Bosai.Net.ScoreManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // 添加日志记录服务
            builder.Logging.AddConsole();

            // 注册服务
            ServiceRegistrations.RegisterServices(builder.Services);

            // 添加跨域支持，用来给UI项目调用
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyMethod()
                           .AllowAnyHeader()
                           .WithOrigins("http://localhost:5268")
                           .AllowCredentials(); // 允许包含凭据;
                });
            });

            // 注册SignalR服务
            builder.Services.AddSignalR();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors();

            app.UseAuthorization();

            app.MapControllers();
            app.MapHub<ScoreRankHub>("/scoreRankHub");
            app.Run();
        }
    }
}
