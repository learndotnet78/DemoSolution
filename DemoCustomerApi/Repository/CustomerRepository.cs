using DemoCustomerApi.Interface;
using DemoCustomerApi.Model;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;

namespace DemoCustomerApi.Repository
{
    public class CustomersRepository : ICustomers
    {
        private string _connectionStrings = null;
        public CustomersRepository(IOptions<ConnectionStrings> connectionStrings)
        {
            _connectionStrings = connectionStrings.Value.DbConnection;
        }
        List<Customer> lsCustomers = new List<Customer>
        {
            new Customer{CustomerId=1, FirstName="Sam", LastName="Neil", Address="New York" },
            new Customer{CustomerId=2, FirstName="Jake", LastName="Simon", Address="Texas" },
            new Customer{CustomerId=3, FirstName="San", LastName="Juan", Address="New Mexico" },
            new Customer{CustomerId=4, FirstName="Pedro", LastName="Pascal", Address="California" },
            new Customer{CustomerId=5, FirstName="Ram", LastName="Narayan", Address="Washinton DC" },
            new Customer{CustomerId=6, FirstName="Eli", LastName="Shaw", Address="New Jersey" },
        };

        public List<Customer> GetAllCustomer()
        {
            List<Customer> lsCustomers = new List<Customer>();

            using (SqlConnection conn = new SqlConnection(_connectionStrings))
            {
                conn.Open();

                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = conn;
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.CommandText = "sp_GetCustomers";

                SqlDataAdapter sda = new SqlDataAdapter(sqlCommand);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                DataTable dt = ds.Tables[0];
                sda.Dispose();

                
                foreach (DataRow dr in dt.Rows)
                {
                    Customer m = new Customer
                    {
                        CustomerId = (int)dr["CustomerId"],
                        FirstName = dr["FirstName"].ToString(),
                        LastName = dr["LastName"].ToString(),
                        Address = dr["Address"].ToString()
                    };
                    lsCustomers.Add(m);
                }
            }

            return lsCustomers;
        }

        public Customer GetCustomer(int id)
        {
            Customer m = null;

            using (SqlConnection conn = new SqlConnection(_connectionStrings))
            {
                conn.Open();

                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = conn;
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.CommandText = "sp_GetCustomers";
                sqlCommand.Parameters.Add(new SqlParameter("@CustomerID", id));

                SqlDataAdapter sda = new SqlDataAdapter(sqlCommand);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                DataTable dt = ds.Tables[0];
                sda.Dispose();

                foreach (DataRow dr in dt.Rows)
                {
                    m = new Customer
                    {
                        CustomerId = (int)dr["CustomerId"],
                        FirstName = dr["FirstName"].ToString(),
                        LastName = dr["LastName"].ToString(),
                        Address = dr["Address"].ToString()
                    };
                }
            }

            return m;

        }

        public void AddCustomer(Customer customer)
        {
            using (SqlConnection conn = new SqlConnection(_connectionStrings))
            {
                conn.Open();

                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = conn;
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.CommandText = "sp_AddCustomers";
                sqlCommand.Parameters.Add(new SqlParameter("@FirstName", customer.FirstName));
                sqlCommand.Parameters.Add(new SqlParameter("@LastName", customer.LastName));
                sqlCommand.Parameters.Add(new SqlParameter("@Address", customer.Address));

                sqlCommand.ExecuteNonQuery();
            }
        }

        public void UpdateCustomer(Customer customer)
        {
            using (SqlConnection conn = new SqlConnection(_connectionStrings))
            {
                conn.Open();

                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = conn;
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.CommandText = "sp_UpdateCustomers";
                sqlCommand.Parameters.Add(new SqlParameter("@CustomerID", customer.CustomerId));
                sqlCommand.Parameters.Add(new SqlParameter("@FirstName", customer.FirstName));
                sqlCommand.Parameters.Add(new SqlParameter("@LastName", customer.LastName));
                sqlCommand.Parameters.Add(new SqlParameter("@Address", customer.Address));

                sqlCommand.ExecuteNonQuery();
            }
        }

        public void DeleteCustomer(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionStrings))
            {
                conn.Open();

                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = conn;
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.CommandText = "sp_DeleteCustomers";
                sqlCommand.Parameters.Add(new SqlParameter("@CustomerID", id));

                sqlCommand.ExecuteNonQuery();
            }
        }
    }
}
