using System.Data.SqlClient;

namespace CapaDatos
{
    public class Conexion
    {
        // Apunta exactamente a .\SQLEXPRESS02 que es tu servidor activo
        private static string cadena = @"Server=.\SQLEXPRESS02; Database=TecsupDB; Integrated Security=True; TrustServerCertificate=True;";

        public static SqlConnection ObtenerConexion()
        {
            return new SqlConnection(cadena);
        }
    }
}