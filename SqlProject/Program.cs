using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.SqlClient;
namespace SqlProject
{
    class Program
    {
        public static bool CheckExistCategory(int  categoryId, string connectionString)
        {
            string query = $"select Category_id from Categories_tbl";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        if(categoryId == (int)reader[0])
                              return true; 
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return false;
                
            }
        } 
        public static void insertDataToCategoriesTable(string connectionString)
        {
            string category_name, product_name, product_describtion, product_url;
            int product_price;
            Console.WriteLine("Enter category name");
            category_name = Console.ReadLine();

            string query = "INSERT INTO Categories_tbl (Category_name)" + "VALUES (@category_name)";

            using (SqlConnection connection = new SqlConnection(connectionString))

            using (SqlCommand command = new SqlCommand(query, connection))

            {

                connection.Open();
                command.Parameters.Add("@Category_name", SqlDbType.VarChar, 50).Value = category_name;
                int rowsAffected = command.ExecuteNonQuery();

                connection.Close();
uo
                Console.WriteLine("rowsAffected: " + rowsAffected);

            }

        }
        public static void insertDataToProductsTable(string connectionString)
        {
            string category_name, product_name, product_describtion, product_picture;
            float product_price;
            int category_id;

            Console.WriteLine("Enter category id");
            category_id = int.Parse(Console.ReadLine());
            if(!CheckExistCategory(category_id, connectionString))
            {
                Console.WriteLine("Enter exist category");
                return;
            }
            Console.WriteLine("Enter product name");
            product_name = Console.ReadLine();
            Console.WriteLine("Enter product describtion");
            product_describtion = Console.ReadLine();
            Console.WriteLine("Enter product url to picture");
            product_picture = Console.ReadLine();
            Console.WriteLine("Enter product price");
            product_price = float.Parse(Console.ReadLine());

            string query = "INSERT INTO Product_tbl (Category_id,Product_name,Product_describtion,Product_price,Product_picture)"
                + "VALUES (@category_id, @product_name, @product_describtion, @product_price, @product_picture)";

            using (SqlConnection connection = new SqlConnection(connectionString))

            using (SqlCommand command = new SqlCommand(query, connection))

            {

                connection.Open();
                command.Parameters.Add("@category_id", SqlDbType.Int, 1).Value = category_id;
                command.Parameters.Add("@product_name", SqlDbType.VarChar, 50).Value = product_name;
                command.Parameters.Add("@product_describtion", SqlDbType.VarChar, 50).Value = product_describtion;
                command.Parameters.Add("@product_price", SqlDbType.Float, 50).Value = product_price;
                command.Parameters.Add("@product_picture", SqlDbType.VarChar, 50).Value = product_picture;
                int rowsAffected = command.ExecuteNonQuery();

                connection.Close();

                Console.WriteLine("rowsAffected: " + rowsAffected);

            }

        }

        public static void InsertData(string connectionString)
        {
            string ans = "y";
            Console.WriteLine("Insert categories to categories table");
            while (ans == "y")
            {
                insertDataToCategoriesTable(connectionString);
                Console.WriteLine("Would you like to continue?(y/n)");
                ans = Console.ReadLine();
            }
            ans = "y";
            Console.WriteLine("Insert products to products table");
            while (ans == "y")
            {
                insertDataToProductsTable(connectionString);
                Console.WriteLine("Would you like to continue?(y/n)");
                ans = Console.ReadLine();
            }

        }

        public static void PrintTable(string table, string connectionString)
        {
            string query = $"select * from {table}";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            Console.Write($"\t{reader[i]}");
                        }
                        Console.WriteLine();
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            string connectionString = "Data Source=srv2\\pupils;Initial Catalog=MyDB_329114565;Integrated Security=True;Encrypt=False";
            PrintTable("Categories_tbl", connectionString);
            PrintTable("Product_tbl", connectionString);
            InsertData(connectionString);
            
        }
    }
}