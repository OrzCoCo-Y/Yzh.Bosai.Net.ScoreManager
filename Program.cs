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

            // �����־��¼����
            builder.Logging.AddConsole();

            // ע�����
            ServiceRegistrations.RegisterServices(builder.Services);

            // ��ӿ���֧�֣�������UI��Ŀ����
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyMethod()
                           .AllowAnyHeader()
                           .WithOrigins("http://localhost:5268")
                           .AllowCredentials(); // �������ƾ��;
                });
            });

            // ע��SignalR����
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
