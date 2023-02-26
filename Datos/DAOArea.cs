using Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class DAOArea
    {
        public List<Area> cargarDatos()
        {
            try
            {
                if (Conexion.conectar())
                {
                    //Se cargarán los datos que no tengan borrado lógico
                    MySqlCommand comando = new MySqlCommand(@"select * from areas order by nombre");

                    //Se establece la conexión con la que se ejecutará la consulta
                    comando.Connection = Conexion.conexion;

                    //Este objeto nos ayudará a llenar una tabla con el resultado de la consulta
                    MySqlDataAdapter adapter = new MySqlDataAdapter(comando);
                    DataTable resultado = new DataTable();
                    adapter.Fill(resultado);
                    List<Area> lista = new List<Area>();
                    Area objArea = null;
                    //Cuando la consulta si obtuvo resultados la tabla trae filas

                    foreach (DataRow fila in resultado.Rows)
                    {
                        objArea = new Area();
                        objArea.id = Convert.ToInt32(fila["id"]);
                        objArea.nombre = fila["nombre"].ToString();
                        objArea.ubicacion = fila["ubicacion"].ToString();

                        lista.Add(objArea);
                    }

                    return lista;
                }
                else
                {
                    throw new Exception("No se ha podido conectar con el servidor");
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("No se pudo obtener la información de las areas");
            }
            finally
            {
                Conexion.desconectar();
            }
        }
        public int agregarArea(Area area)
        {
            try
            {
                if (Conexion.conectar())
                {
                    MySqlCommand comando = new MySqlCommand(
                        @"INSERT INTO areas VALUES(@id,@nombre,@ubicacion);");

                    comando.Parameters.AddWithValue("@id", area.id);
                    comando.Parameters.AddWithValue("@nombre", area.nombre);
                    comando.Parameters.AddWithValue("@ubicacion", area.ubicacion);

                    comando.Connection = Conexion.conexion;

                    int agregado = comando.ExecuteNonQuery();

                    return agregado;
                }
                else
                {
                    throw new Exception("No se ha podido conectar con el servidor");
                }
            }
            catch (MySqlException)
            {
                throw new Exception("Ya existe un id " + area.id);
            }
            finally
            {
                Conexion.desconectar();
            }
        }
    }
}
