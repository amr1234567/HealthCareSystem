
using HSS.DataAccess;
using HSS.Services;
using HSS.Services.Abstractions;
using HSS.Services.Services;

namespace HSS.Presentation.Patient.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
       
            builder.Services.AddScoped<IReceptionServices, ReceptionServices>();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddDataAccessServices(builder.Configuration)
                .AddServiceLayerServicesForApi(builder.Configuration);
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseApiAuth();
            app.MapControllers();
            app.Run();
        }
    }
}
