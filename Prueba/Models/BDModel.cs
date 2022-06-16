using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Prueba.Models
{
    public class BDModel
    {
        public dynamic conectionBD = System.Configuration.ConfigurationManager.AppSettings["conexionBD"];
        public SqlConnection sqlConnection;


        public bool consultasBD(ref SqlDataReader reader, string query)
        {
            sqlConnection = new SqlConnection(conectionBD);
            SqlCommand command = sqlConnection.CreateCommand();
            sqlConnection.Open();
            command.CommandText = query;

            try
            {
                reader = command.ExecuteReader();
                return true;
            }
            catch (Exception e)
            {
                sqlConnection.Close();
                SqlConnection.ClearAllPools();
                return false;
            }


        }

        public bool executeCommands(string query)
        {
            sqlConnection = new SqlConnection(conectionBD);
            SqlCommand command = sqlConnection.CreateCommand();
            sqlConnection.Open();
            command.CommandText = query;

            try
            {
                command.ExecuteReader();
                return true;
            }
            catch (Exception)
            {
                sqlConnection.Close();
                SqlConnection.ClearAllPools();
                throw;
            }


        }




    }
}