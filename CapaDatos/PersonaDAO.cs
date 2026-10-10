using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CapaEntidades;

namespace CapaDatos
{
    public class PersonaDAO
    {
        // 1. Listar solo personas activas (Estado = 1)
        public List<Persona> Listar()
        {
            List<Persona> lista = new List<Persona>();
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                string query = "SELECT IdPersona, Documento, Nombres, Apellidos, Estado FROM Persona WHERE Estado = 1";
                SqlCommand cmd = new SqlCommand(query, cn);
                cn.Open(); // <-- Aquí se abre la conexión
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new Persona
                        {
                            IdPersona = Convert.ToInt32(dr["IdPersona"]),
                            Documento = dr["Documento"].ToString(),
                            Nombres = dr["Nombres"].ToString(),
                            Apellidos = dr["Apellidos"].ToString(),
                            Estado = Convert.ToBoolean(dr["Estado"])
                        });
                    }
                }
            }
            return lista;
        }

        // 2. Buscar por Nombre, Apellido o Documento (Estado = 1)
        public List<Persona> Buscar(string filtro)
        {
            List<Persona> lista = new List<Persona>();
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                string query = "SELECT IdPersona, Documento, Nombres, Apellidos, Estado FROM Persona " +
                               "WHERE Estado = 1 AND (Nombres LIKE @filtro OR Apellidos LIKE @filtro OR Documento LIKE @filtro)";
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@filtro", "%" + filtro + "%");
                cn.Open(); // <-- Conexión abierta
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new Persona
                        {
                            IdPersona = Convert.ToInt32(dr["IdPersona"]),
                            Documento = dr["Documento"].ToString(),
                            Nombres = dr["Nombres"].ToString(),
                            Apellidos = dr["Apellidos"].ToString(),
                            Estado = Convert.ToBoolean(dr["Estado"])
                        });
                    }
                }
            }
            return lista;
        }

        // 3. Registrar Persona
        public bool Registrar(Persona obj)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                string query = "INSERT INTO Persona (Documento, Nombres, Apellidos, Estado) VALUES (@doc, @nom, @ape, 1)";
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@doc", obj.Documento);
                cmd.Parameters.AddWithValue("@nom", obj.Nombres);
                cmd.Parameters.AddWithValue("@ape", obj.Apellidos);
                cn.Open(); // <-- Conexión abierta
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // 4. Modificar Persona
        public bool Modificar(Persona obj)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                string query = "UPDATE Persona SET Documento = @doc, Nombres = @nom, Apellidos = @ape WHERE IdPersona = @id";
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@id", obj.IdPersona);
                cmd.Parameters.AddWithValue("@doc", obj.Documento);
                cmd.Parameters.AddWithValue("@nom", obj.Nombres);
                cmd.Parameters.AddWithValue("@ape", obj.Apellidos);
                cn.Open(); // <-- Conexión abierta
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // 5. Eliminación Lógica (Estado = 0)
        public bool Eliminar(int idPersona)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                string query = "UPDATE Persona SET Estado = 0 WHERE IdPersona = @id";
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@id", idPersona);
                cn.Open(); // <-- Conexión abierta
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}