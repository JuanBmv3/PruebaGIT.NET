using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Prueba.Models
{
    public class CustomersModel 
    {
        public string customerID { get; set; }
        public string companyName { get; set; }
        public string contactName { get; set; }

        BDModel bd = new BDModel();
        public List<CustomersModel> getCompanies(string country)
        {
            
            using (var con = new SqlConnection(bd.conectionBD))
            {
                con.Open();
                List<CustomersModel> listCustomers = new List<CustomersModel>();
                var query = new SqlCommand("Exec SelectAllCustomersP @Country", con);
                query.Parameters.AddWithValue("@Country", country);

                using (var read = query.ExecuteReader())
                {
                    while (read.Read())
                    {
                        CustomersModel customers = new CustomersModel()
                        {
                            customerID = read.GetString(0),
                            companyName = read.GetString(1),
                            contactName = read.GetString(2)
                        };
                        listCustomers.Add(customers);
                    }
                    con.Close();
                    return listCustomers;
                }
            }
        }

    }
}