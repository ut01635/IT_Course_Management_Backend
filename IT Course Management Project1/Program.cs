
using IT_Course_Management_Project1.Database;
using IT_Course_Management_Project1.IRepository;
using IT_Course_Management_Project1.IServices;
using IT_Course_Management_Project1.Repositories;
using IT_Course_Management_Project1.Services;
using Microsoft.Extensions.DependencyInjection;


namespace IT_Course_Management_Project1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DbConnect");
            DatabaseInitializer database = new DatabaseInitializer();
            database.InitializeDatabase();
            database.CreateTable();
            database.InsertSampleData();

            // Add services to the container.

            // Add CORS policy
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder => builder.AllowAnyOrigin()
                                      .AllowAnyHeader()
                                      .AllowAnyMethod());
            });



            builder.Services.AddSingleton<IStudentRepository>(new StudentRepository(connectionString));
            builder.Services.AddScoped<IStudentService, StudentService>();

            builder.Services.AddSingleton<ICourseRepository>(new CourseRepository(connectionString));
            builder.Services.AddScoped<ICourseService, CourseService>();

            builder.Services.AddSingleton<IEnrollmentRepository>(new EnrollmentRepository(connectionString));
            builder.Services.AddScoped<IEnrollmentService, EnrollmentService>();

            builder.Services.AddSingleton<IPaymentRepository>(new PaymentRepository(connectionString));
            builder.Services.AddScoped<IPaymentService, PaymentService>();

            builder.Services.AddSingleton<INotificationRepository>(new NotificationRepository(connectionString));
            builder.Services.AddScoped<INotificationService, NotificationService>();

            builder.Services.AddSingleton<IContactUsRepository>(new ContactUsRepository(connectionString));
            builder.Services.AddScoped<IContactUsService, ContactUsService>();

            builder.Services.AddSingleton<IAdminRepository>(new AdminRepository(connectionString));
            builder.Services.AddScoped<IAdminService, AdminService>();




            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

           

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();


            // Use the CORS policy
            app.UseCors("AllowAll");

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
