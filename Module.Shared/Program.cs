using ACommon;
using BusinessLogicLayer;
using DataAccessLayer;

namespace Module.Shared
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var env = builder.Environment;

            // Access config settings
            var config = builder.Configuration;
            var connectionSection = config.GetSection("ConnectionSettings");

            // Pull UseLocalDb toggle
            bool useLocalDb = connectionSection.GetValue<bool>("UseLocalDb");

            // Pull correct connection string
            string connectionString = useLocalDb ? connectionSection.GetValue<string>("LocalDb") : connectionSection.GetValue<string>("PublishedDb");
            bool isIISExpress = Environment.GetEnvironmentVariable("ASPNETCORE_HOSTINGSTARTUPASSEMBLIES")?.Contains("IIS") ?? false;

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSingleton(new ConnectionOptions
            {
                ConnectionString = connectionString,
                IsLocal = useLocalDb,
                IsIISExpress = isIISExpress
            });

            builder.Services.AddDataAccessServices();
            builder.Services.AddBusinessLogicServices();


            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });

            app.UseHttpsRedirection();
            //app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
