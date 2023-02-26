using Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class DAOInventario
    {
        //carga los datos de un solo registro especificado por id
        public Inventario CargarUno(int id)
        {
            try
            {
                if (Conexion.conectar())
                {
                    MySqlCommand comando = new MySqlCommand(
                        @"select i.id, i.nombrecorto, i.descripcion,
                            i.serie, i.color, i.fechaAdquision, i.tipoAdquision, i.observaciones, a.nombre
                            from inventario i, areas a where i.areas_id=a.id and i.id=@id");

                    comando.Parameters.AddWithValue("@id", id);
                    //Se establece la conexión con la que se ejecutará la consulta
                    comando.Connection = Conexion.conexion;

                    //Este objeto nos ayudará a llenar una tabla con el resultado de la consulta
                    MySqlDataAdapter adapter = new MySqlDataAdapter(comando);
                    DataTable resultado = new DataTable();
                    adapter.Fill(resultado);

                    Inventario objInventario = null;
                    //Cuando la consulta si obtuvo resultados la tabla trae filas

                    if (resultado.Rows.Count > 0)
                    {
                        DataRow fila = resultado.Rows[0];
                        objInventario = new Inventario();
                        objInventario.id = Convert.ToInt32(fila["id"]);
                        objInventario.nombreCorto = fila["nombreCorto"].ToString();
                        objInventario.descripcion = fila["descripcion"].ToString();
                        objInventario.serie = fila["serie"].ToString();
                        objInventario.color = fila["color"].ToString();
                        if (!fila["fechaAdquision"].ToString().Equals(""))
                        {
                            objInventario.fechaAdquision = Convert.ToDateTime(fila["fechaAdquision"]).Date;
                        }
                        objInventario.tipoAdquision = fila["tipoAdquision"].ToString();
                        objInventario.observaciones = fila["observaciones"].ToString();
                        objInventario.nombreArea = fila["nombre"].ToString();

                    }

                    return objInventario;
                }
                else
                {
                    throw new Exception("No se ha podido conectar con el servidor");
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("No se pudo obtener la información del inventario");
            }
            finally
            {
                Conexion.desconectar();
            }
        }
        //carga todos los datos al datagridview
        public List<Inventario> cargarDatos()
        {
            try
            {
                if (Conexion.conectar())
                {
                    //Se cargarán los datos que no tengan borrado lógico
                    MySqlCommand comando = new MySqlCommand(@"select i.id, i.nombrecorto, i.descripcion,
                            i.serie, i.color, i.fechaAdquision, i.tipoAdquision, i.observaciones, a.id,a.nombre
                            from inventario i, areas a
                            where i.areas_id = a.id
                            order by i.nombreCorto asc;");

                    //Se establece la conexión con la que se ejecutará la consulta
                    comando.Connection = Conexion.conexion;

                    //Este objeto nos ayudará a llenar una tabla con el resultado de la consulta
                    MySqlDataAdapter adapter = new MySqlDataAdapter(comando);
                    DataTable resultado = new DataTable();
                    adapter.Fill(resultado);
                    List<Inventario> lista = new List<Inventario>();
                    Inventario objInventario = null;
                    //Cuando la consulta si obtuvo resultados la tabla trae filas

                    foreach (DataRow fila in resultado.Rows)
                    {
                        objInventario = new Inventario();
                        objInventario.id = Convert.ToInt32(fila["id"]);
                        objInventario.nombreCorto= fila["nombreCorto"].ToString();
                        objInventario.descripcion = fila["descripcion"].ToString();
                        objInventario.serie = fila["serie"].ToString();
                        objInventario.color = fila["color"].ToString();
                        if (!fila["fechaAdquision"].ToString().Equals(""))
                        {
                            objInventario.fechaAdquision = Convert.ToDateTime(fila["fechaAdquision"]).Date;
                        }
                        objInventario.tipoAdquision = fila["tipoAdquision"].ToString();
                        objInventario.observaciones = fila["observaciones"].ToString();
                        objInventario.areas_id= Convert.ToInt32(fila["id"]);
                        objInventario.nombreArea = fila["nombre"].ToString();

                        lista.Add(objInventario);
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
                throw new Exception("No se pudo obtener la información de los inventarios");
            }
            finally
            {
                Conexion.desconectar();
            }
        }

        public int agregarInventario(Inventario inventario)
        {
            try
            {
                if (Conexion.conectar())
                {
                    MySqlCommand comando = new MySqlCommand(
                        @"INSERT INTO inventario VALUES(@id,@nombreCorto,@descripcion,@serie,@color,@fechaAdquision,
                                    @tipoAdquision,@observaciones,@idArea);");

                    comando.Parameters.AddWithValue("@id", inventario.id);
                    comando.Parameters.AddWithValue("@nombreCorto", inventario.nombreCorto);
                    comando.Parameters.AddWithValue("@descripcion", inventario.descripcion);
                    comando.Parameters.AddWithValue("@serie", inventario.serie);
                    comando.Parameters.AddWithValue("@color", inventario.color);
                    comando.Parameters.AddWithValue("@fechaAdquision", inventario.fechaAdquision);
                    comando.Parameters.AddWithValue("@tipoAdquision", inventario.tipoAdquision);
                    comando.Parameters.AddWithValue("@observaciones", inventario.observaciones);
                    comando.Parameters.AddWithValue("@idArea", inventario.areas_id);


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
                throw new Exception();
            }
            finally
            {
                Conexion.desconectar();
            }
        }

        public bool editar(Inventario inventario)
        {
            try
            {
                if (Conexion.conectar())
                {
                    MySqlCommand comando = new MySqlCommand(
                        @"UPDATE inventario
                            SET id=@id, nombreCorto=@nombreCorto, descripcion=@descripcion, serie=@serie, color=@color, fechaAdquision=@fechaAdquision,
                            tipoAdquision=@tipoAdquision, observaciones=@observaciones, areas_id=@areas_id
                            WHERE id=@id");

                    comando.Parameters.AddWithValue("@id", inventario.id);
                    comando.Parameters.AddWithValue("@nombreCorto", inventario.nombreCorto);
                    comando.Parameters.AddWithValue("@descripcion", inventario.descripcion);
                    comando.Parameters.AddWithValue("@serie", inventario.serie);
                    comando.Parameters.AddWithValue("@color", inventario.color);
                    comando.Parameters.AddWithValue("@fechaAdquision", inventario.fechaAdquision);
                    comando.Parameters.AddWithValue("@tipoAdquision", inventario.tipoAdquision);
                    comando.Parameters.AddWithValue("@observaciones", inventario.observaciones);
                    comando.Parameters.AddWithValue("@areas_id", inventario.areas_id);

                    //Se establece la conexión con la que se ejecutará la consulta
                    comando.Connection = Conexion.conexion;

                    int filasActualizadas = comando.ExecuteNonQuery();

                    return (filasActualizadas > 0);
                }
                else
                {
                    throw new Exception("No se ha podido conectar con el servidor");
                }
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1062)
                {
                    throw new Exception("No se pudo realizar la edición, el id ya se encuentra en uso en otro producto");
                }
                else
                {
                    throw new Exception("No se pudo realizar la edición");
                }
            }
            finally
            {
                Conexion.desconectar();
            }
        }

        public bool eliminar(int id)
        {
            try
            {
                if (Conexion.conectar())
                {
                    MySqlCommand comando = new MySqlCommand(
                        @"DELETE FROM inventario
                        WHERE id=@id");

                    comando.Parameters.AddWithValue("@id", id);
                    comando.Connection = Conexion.conexion;
                    int filasBorradas = comando.ExecuteNonQuery();
                    return (filasBorradas > 0);
                }
                else
                {
                    throw new Exception("No se ha podido conectar con el servidor");
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("No se pudo realizar la eliminación");
            }
            finally
            {
                Conexion.desconectar();
            }
        }
    }
}
