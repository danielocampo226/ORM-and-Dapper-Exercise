using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace ORM_Dapper
{
    public class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connString = config.GetConnectionString("DefaultConnection");

            IDbConnection conn = new MySqlConnection(connString);

            var repo = new DapperProductRepository(conn);
            var products = repo.GetAllProducts();
                
            foreach(var prod in products)
            {
                Console.WriteLine($"{prod.ProductId} {prod.Name}");
            }

            repo.CreateProduct("newStuff", 20, 1);

            var repo1 = new DapperDepartmentRepository(conn);

            var departments = repo1.GetAllDepartments();

            foreach (var dept in departments)
            {
                Console.WriteLine($"{dept.DepartmentId} {dept.Name}");
            }

        }
            
    }
}
