using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Prueba.Models
{
    public class PaisesModel : BDModel
    {

        public string nomPais { get; set; }


        public List<PaisesModel> getPaises()
        {

            List<PaisesModel> listPaises = new List<PaisesModel>();
            string query = "EXEC SelectAllCustomers";
            SqlDataReader reader = null;

            consultasBD(ref reader, query);

            while (reader.Read())
            {
                PaisesModel pais = new PaisesModel()
                {
                    nomPais = reader.GetString(0)
                };

                listPaises.Add(pais);
                

            }

            sqlConnection.Close();
            SqlConnection.ClearAllPools();
            return listPaises;





        }

    }
}