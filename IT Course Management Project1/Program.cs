
<<<<<<< Updated upstream
=======
using IT_Course_Management_Project1.Database;
>>>>>>> Stashed changes
using IT_Course_Management_Project1.IRepository;
using IT_Course_Management_Project1.IServices;
using IT_Course_Management_Project1.Repositories;
using IT_Course_Management_Project1.Services;
<<<<<<< Updated upstream
=======

>>>>>>> Stashed changes

namespace IT_Course_Management_Project1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
<<<<<<< Updated upstream

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DbConnect");

            builder.Services.AddSingleton<IStudentRepository>(provider => new StudentRepository(connectionString));
            builder.Services.AddScoped<IStudentService, StudentService>();
=======
            var connectionString = builder.Configuration.GetConnectionString("DbConnect");

            DatabaseInitializer database = new DatabaseInitializer();
            database.InitializeDatabase();
            database.CreateTable();
            //database.InserSampleData();

            // Register services
            builder.Services.AddSingleton<ICourseRepository>(new CourseRepository(connectionString));
            builder.Services.AddScoped<ICourseService, CourseService>();
>>>>>>> Stashed changes

            builder.Services.AddSingleton<ICourseRepository>(new CourseRepository(connectionString));
            builder.Services.AddScoped<ICourseService, CourseService>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
