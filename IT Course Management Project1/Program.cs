
using IT_Course_Management_Project1.Database;


namespace IT_Course_Management_Project1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DbConnect");
            DatabaseInitializer database = new DatabaseInitializer();
            database.InitializeDatabase();
            database.CreateTable();
            database.InserSampleData();

            // Add services to the container.
           // var connectionString = builder.Configuration.GetConnectionString("DbConnect");

         

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

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
