using Dapper;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_Dapper
{
    public class DapperProductRepository : IProductRepository
    {
        //we're encapsulating the connection
        private readonly IDbConnection _connection;
        public DapperProductRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            //dapper starts here
            //dapper extends ==> IDbConnection
           return _connection.Query<Product>("SELECT * FROM PRODUCTS");
        }

        public void CreateProduct(string name, double price, int categoryID)
        {
            _connection.Execute("INSERT INTO products (Name, Price, CategoryID) VALUES ( @productName, @price, @categoryID);",
                new { productName = name, price = price, categoryID = categoryID });
            //@productname is stored in name parameter
            //it has a return type of void because it will leave the new product
            //in the sql database
        }

        public void UpdateProductName(int productId, string updatedName)
        {
            _connection.Execute("UPDATE products SET Name = @updatedName WHERE ProductId = @productId;",
                new { updatedName = updatedName, productId = productId });
        }

        //Delete data
        public void DeleteProduct(int productId)
        {
            _connection.Execute("DELETE FROM reviews WHERE ProductId = @productId;",
                new { productId = productId });

            _connection.Execute("DELETE FROM sales WHERE ProductId = @productId;",
               new { productId = productId });

            _connection.Execute("DELETE FROM products WHERE ProductId = @ productId;",
               new { productId = productId });
            //you want to also delete products from sales and reviews table since
            //products is also contained within those tables
        }
    }
}
