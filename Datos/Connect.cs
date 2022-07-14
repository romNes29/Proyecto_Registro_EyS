using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace Datos
{
    public class Connect
    {

        private static NpgsqlConnection conn = new NpgsqlConnection("Server=localhost; Database=Marcas; User Id=fundamentos; Password=choco29;");

        public static void conectarPostgreSQL()
        {
            conn.Open();
            Console.WriteLine("Conexion a base de datos establecida.");
        }



        public static void desconectarPostgreSQL()
        {
            conn.Close();
            Console.WriteLine("Desconexion de base de datos realizada.");


        }

        public static string insertaDatos(string tabla, string[] datos)
        {
            try
            {
                conectarPostgreSQL();
                string campos = "(";
                for (int i = 0; i < datos.Length; i++)
                {
                    campos += "'" + datos[i] + "'";
                    if (i != datos.Length - 1)
                    {
                        campos += ",";
                    }
                }
                campos += ")";
                string insertSql = "Insert into \"" + tabla + "\" values " + campos;
                NpgsqlCommand ejecutar = new NpgsqlCommand(insertSql, conn);
                ejecutar.ExecuteNonQuery();
                desconectarPostgreSQL();
                MessageBox.Show("Insertado con exito");
            }
            catch (Exception)
            {
                return "Error";
            }
            return "Ok";

        }

        public static DataTable consultaUnDato(string query)
        {
            try
            {
                conectarPostgreSQL();
                NpgsqlCommand conector = new NpgsqlCommand(query, conn);
                NpgsqlDataAdapter datos = new NpgsqlDataAdapter(conector);
                DataTable tabla = new DataTable();
                datos.Fill(tabla);
                desconectarPostgreSQL();
                return tabla;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public void consultaListaDatos(string tabla, string consulta)
        {


        }




    }
}
