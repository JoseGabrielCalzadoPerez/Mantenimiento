using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class Conexion
    {
        //creamos cadena de coneccion
        private SqlConnection conexion = new SqlConnection("Data Source=LAPTOP-IHVUID08\\SQLEXPRESS;Initial Catalog=SoporteMantenimiento;Integrated Security=true");

        //Creamos dos metodos para cerrar y abrir


        public SqlConnection Abrir()
        {
            if (conexion.State == ConnectionState.Closed)
            {
                conexion.Open();
            }

            return conexion;

        }

        public SqlConnection Cerrar()
        {
            if (conexion.State == ConnectionState.Open)
            {
                conexion.Close();
            }

            return conexion;

        }
    }
}
