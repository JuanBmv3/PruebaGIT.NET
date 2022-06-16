using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Prueba.Models
{
    public class OrderModel
    {

        public int orderID { get; set; }
        public string companyName { get; set; }
        public DateTime orderDate { get; set; }
        public DateTime requiredDate { get; set; }
        public string shipCity { get; set; }
        public decimal freight { get; set; }

        BDModel bd = new BDModel();

        public List<OrderModel> getOrders(string customerID)
        {

            using (var con = new SqlConnection(bd.conectionBD))
            {
                con.Open();
                List<OrderModel> listOrders = new List<OrderModel>();
                var query = new SqlCommand("Exec TOPTEN_ORDERS @customerID", con);
                query.Parameters.AddWithValue("@customerID",customerID );

                using (var read = query.ExecuteReader())
                {
                    while (read.Read())
                    {
                        OrderModel order = new OrderModel();

                        order.orderID = read.GetInt32(0);
                        order.companyName = read.GetString(1);
                        order.orderDate = read.GetDateTime(2);
                        order.requiredDate = read.GetDateTime(3);
                        order.shipCity = read.GetString(4);
                        order.freight = read.GetDecimal(5);
                        listOrders.Add(order);
                    }
                    con.Close();
                    return listOrders;
                }
            }
        }

        public bool deleteOrder(string id)
        {
            try
            {
                using (var con = new SqlConnection(bd.conectionBD))
                {
                    con.Open();
                    List<OrderModel> listOrders = new List<OrderModel>();
                    var query = new SqlCommand("Exec DeleteOrder @orderID", con);
                    query.Parameters.AddWithValue("@orderID", id);

                    query.ExecuteReader();
                    return true;

                }
            }
            catch (Exception e)
            {
                return false;
            }
           
            
            
        }


        public bool updateOrder(string id , float freight)
        {
            using (var con = new SqlConnection(bd.conectionBD))
            {
                con.Open();
                List<OrderModel> listOrders = new List<OrderModel>();
                var query = new SqlCommand("Exec updateOrder @orderID, @freight", con);
                query.Parameters.AddWithValue("@orderID", id);
                query.Parameters.AddWithValue("@freight", freight);

                query.ExecuteReader();
                return true;

            }


        }

        public bool createOrder(string id, float freight, string orderDate, string requiredDate, string shipCity)
        {
            using (var con = new SqlConnection(bd.conectionBD))
            {
                con.Open();
                var query = new SqlCommand("Exec CreateOrder @CustomerID, @OrderDate, @RequiredDate, @freight, @ShipCity", con);
                query.Parameters.AddWithValue("@CustomerID", id);
                query.Parameters.AddWithValue("@OrderDate", orderDate);
                query.Parameters.AddWithValue("@RequiredDate", requiredDate);
                query.Parameters.AddWithValue("@freight", freight);
                query.Parameters.AddWithValue("@ShipCity", shipCity);

                query.ExecuteReader();
                return true;

            }


        }
    }
}