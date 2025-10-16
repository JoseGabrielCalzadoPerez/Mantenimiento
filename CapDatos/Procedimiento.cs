using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class Procedimientos
    {
        private Conexion conexion = new Conexion();

        public DataTable Mostrar()
        {
            DataTable table = new DataTable();
            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd.Connection = conexion.Abrir();
                cmd.CommandText = "sp_MostrarMantenimiento";
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader leer = cmd.ExecuteReader();
                table.Load(leer);
                leer.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al mostrar datos: " + ex.Message);
            }
            finally
            {
                conexion.Cerrar();
            }

            return table;
        }

      /*  public void Agregar(DateTime FECHA_DE_ENTRADA, string Equipo, string AREA, string MODELO_DE_EQUIPO,
                                   string INVENTARIO, string DIAGNOSTICO, DateTime FECHA_DE_SALIDA,
                                   string Service_tag, string SOPORTE, string NOTA)
        {
            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd.Connection = conexion.Abrir();
                cmd.CommandText = "sp_AgregarMantenimiento";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FECHA_DE_ENTRADA", FECHA_DE_ENTRADA);
                cmd.Parameters.AddWithValue("@Equipo", Equipo ?? "");
                cmd.Parameters.AddWithValue("@AREA", AREA ?? "");
                cmd.Parameters.AddWithValue("@MODELO_DE_EQUIPO", MODELO_DE_EQUIPO ?? "");

                // Si INVENTARIO está vacío o es null, enviamos DBNull.Value para que el SP use el default
                if (string.IsNullOrWhiteSpace(INVENTARIO))
                {
                    cmd.Parameters.AddWithValue("@INVENTARIO", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@INVENTARIO", INVENTARIO.Trim());
                }

                cmd.Parameters.AddWithValue("@DIAGNOSTICO", DIAGNOSTICO ?? "");
                cmd.Parameters.AddWithValue("@FECHA_DE_SALIDA", FECHA_DE_SALIDA);
                cmd.Parameters.AddWithValue("@Service_tag", Service_tag ?? "");
                cmd.Parameters.AddWithValue("@SOPORTE", SOPORTE ?? "");
                cmd.Parameters.AddWithValue("@NOTA", NOTA ?? "");

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar registro: " + ex.Message);
            }
            finally
            {
                cmd.Parameters.Clear();
                conexion.Cerrar();
            }
        } */

        public void Agregar2(DateTime FECHA_DE_ENTRADA, string Equipo, string AREA, string MODELO_DE_EQUIPO,
                                  string INVENTARIO, string DIAGNOSTICO,
                                  string Service_tag, string SOPORTE, string NOTA)
        {
            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd.Connection = conexion.Abrir();
                cmd.CommandText = "sp_AgregarMantenimiento2";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FECHA_DE_ENTRADA", FECHA_DE_ENTRADA);
                cmd.Parameters.AddWithValue("@Equipo", Equipo ?? "");
                cmd.Parameters.AddWithValue("@AREA", AREA ?? "");
                cmd.Parameters.AddWithValue("@MODELO_DE_EQUIPO", MODELO_DE_EQUIPO ?? "");

                // Si INVENTARIO está vacío o es null, enviamos DBNull.Value para que el SP use el default
                if (string.IsNullOrWhiteSpace(INVENTARIO))
                {
                    cmd.Parameters.AddWithValue("@INVENTARIO", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@INVENTARIO", INVENTARIO.Trim());
                }

                cmd.Parameters.AddWithValue("@DIAGNOSTICO", DIAGNOSTICO ?? "");
                cmd.Parameters.AddWithValue("@Service_tag", Service_tag ?? "");
                cmd.Parameters.AddWithValue("@SOPORTE", SOPORTE ?? "");
                cmd.Parameters.AddWithValue("@NOTA", NOTA ?? "");

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar registro: " + ex.Message);
            }
            finally
            {
                cmd.Parameters.Clear();
                conexion.Cerrar();
            }
        }

        public void Modificar(int Id, DateTime FECHA_DE_ENTRADA, string Equipo, string AREA, string MODELO_DE_EQUIPO,
                     string INVENTARIO, string DIAGNOSTICO, DateTime FECHA_DE_SALIDA,
                     string Service_tag, string SOPORTE, string NOTA)
        {
            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd.Connection = conexion.Abrir();
                cmd.CommandText = "sp_ModificarMantenimiento";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@FECHA_DE_ENTRADA", FECHA_DE_ENTRADA);
                cmd.Parameters.AddWithValue("@Equipo", Equipo ?? "");
                cmd.Parameters.AddWithValue("@AREA", AREA ?? "");
                cmd.Parameters.AddWithValue("@MODELO_DE_EQUIPO", MODELO_DE_EQUIPO ?? "");

                // Si INVENTARIO está vacío o es null, enviamos DBNull.Value para que el SP use el default
                if (string.IsNullOrWhiteSpace(INVENTARIO))
                {
                    cmd.Parameters.AddWithValue("@INVENTARIO", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@INVENTARIO", INVENTARIO.Trim());
                }

                cmd.Parameters.AddWithValue("@DIAGNOSTICO", DIAGNOSTICO ?? "");
                cmd.Parameters.AddWithValue("@FECHA_DE_SALIDA", FECHA_DE_SALIDA);
                cmd.Parameters.AddWithValue("@Service_tag", Service_tag ?? "");
                cmd.Parameters.AddWithValue("@SOPORTE", SOPORTE ?? "");
                cmd.Parameters.AddWithValue("@NOTA", NOTA ?? "");

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar registro: " + ex.Message);
            }
            finally
            {
                cmd.Parameters.Clear();
                conexion.Cerrar();
            }
        }

        public void Eliminar(int Id)
        {
            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd.Connection = conexion.Abrir();
                cmd.CommandText = "sp_EliminarMantenimiento";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar registro: " + ex.Message);
            }
            finally
            {
                cmd.Parameters.Clear();
                conexion.Cerrar();
            }
        }
    }
}