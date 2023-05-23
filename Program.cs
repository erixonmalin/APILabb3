using Microsoft.EntityFrameworkCore;
using APILabb3.Data;
using APILabb3.Endpoints;
using Microsoft.OpenApi.Models;


namespace APILabb3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));


            //här har vi fyllt i lite mer information kring swaggerGen
            builder.Services.AddSwaggerGen(swaggerGenOptions =>
            {
                swaggerGenOptions.SwaggerDoc(name: "v1", info: new OpenApiInfo { Title = "Asp.Net Core Swagger API", Version = "v1" });
            });


            builder.Services.AddAuthorization();
            //// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapPersonEndpoints();
                endpoints.MapHobbieEndpoints();
                endpoints.MapLinkEndpoints();
            });

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseSwaggerUI(swaggerUiOptions =>
            {
                swaggerUiOptions.DocumentTitle = "Learning minimal API";
                swaggerUiOptions.SwaggerEndpoint("/swagger/v1/swagger.json", name: "Api that gives you a possibility to learn.");
                swaggerUiOptions.RoutePrefix = string.Empty;

            });

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<ApplicationDbContext>();
                //check if database exist, if not create database
                context.Database.EnsureCreated();
                //Add fake data to database
                //SeedData.Initialize(context);
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.Run();
        }
    }
}