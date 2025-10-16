using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;

namespace Negocio
{
    public class Tramite
    {
        //Creamos una instancia de la clase Procedimientos para asi
        //poder usar sus metodos
        private Procedimientos procedure = new Procedimientos();

        public DataTable mostrar()
        {
            DataTable table = new DataTable();
            table = procedure.Mostrar();//si no se crea con Procedimientos no se podra usar el metodo Mostrar
            return table;
        }

        //creamos los metodos de insertar, actualizar y eliminar
     /*   public void Agregar(DateTime FECHA_DE_ENTRADA, string Equipo, string AREA, string MODELO_DE_EQUIPO, string INVENTARIO, string DIAGNOSTICO, DateTime FECHA_DE_SALIDA, string Service_tag, string SOPORTE, string NOTA)
        {
            procedure.Agregar(FECHA_DE_ENTRADA, Equipo, AREA, MODELO_DE_EQUIPO, INVENTARIO, DIAGNOSTICO, FECHA_DE_SALIDA, Service_tag, SOPORTE, NOTA);
        } */
        public void Agregar2(DateTime FECHA_DE_ENTRADA, string Equipo, string AREA, string MODELO_DE_EQUIPO, string INVENTARIO, string DIAGNOSTICO, string Service_tag, string SOPORTE, string NOTA)
        {
            procedure.Agregar2(FECHA_DE_ENTRADA, Equipo, AREA, MODELO_DE_EQUIPO, INVENTARIO, DIAGNOSTICO, Service_tag, SOPORTE, NOTA);
        }

        public void ModificarData(int Id, DateTime FECHA_DE_ENTRADA, string Equipo, string AREA, string MODELO_DE_EQUIPO, string INVENTARIO, string DIAGNOSTICO, DateTime FECHA_DE_SALIDA, string Service_tag, string SOPORTE, string NOTA)
        {
            procedure.Modificar(Id, FECHA_DE_ENTRADA, Equipo, AREA, MODELO_DE_EQUIPO, INVENTARIO, DIAGNOSTICO, FECHA_DE_SALIDA, Service_tag, SOPORTE, NOTA);
        }

        public void EliminarData(int Id)
        {
            procedure.Eliminar(Id);
        }
    }
}
