using Microsoft.Extensions.Options;
using System.Data.SqlClient;
using System.Data;
using Modelos;

namespace Data
{
    public class EmpleadoData
    {
        //variables 
        private readonly ConnectionStrings _connection;
        //constructor de la clase
        public EmpleadoData( IOptions <ConnectionStrings> conexion ) //inyectamos la dependencia de la conexion
        {
            _connection = conexion.Value;
        }

        //listamos todos los empleaos
        public async Task<List<Empleado>> listar()
        {
            //creo una lista de empleado 
            List<Empleado> empleado = new List<Empleado>();

            //usamos adont.net
            using (var conn = new SqlConnection(_connection.cadenaConexion)) 
            {
                //abrimos la conexion
               await conn.OpenAsync();

                //creo un cmd 
                var cmd = new SqlCommand("sp_listarEmpleado",conn);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync()) {
                        //agrego datos a la lista
                        empleado.Add( new Empleado
                        {
                            IdEmpleado = Convert.ToInt32(reader["IdEmpleado"]),
                            NombreCompleto = reader["Empleado"].ToString()!,
                            Cedula = reader["Cedula"].ToString()!,
                           sueldo = Convert.ToDecimal(reader["Sueldo"]),
                           fechaContrato = reader["FechaContrato"].ToString()!,
                           refDepartamento = new Departamento
                           {
                              // IdDepartamento = Convert.ToInt32(reader["IdDepartamento"])!,
                               Nombre = reader["Departamento"].ToString()!,
                           }

                        });
                    }
                }
                return empleado;
            } 

        }

        //creamos todos los empleados
        public async Task<string>Crear(Empleado objeto)
        {
            //respuesta del metodo
            string respuesta = "";
            //obtengo la cadena de conexion
            using (var conn = new SqlConnection(_connection.cadenaConexion))
            {
                //abrimos la conexion
               await conn.OpenAsync();
                //creamos el cmd
                var cmd = new SqlCommand("sp_crearEmpleado", conn);
                cmd.Parameters.AddWithValue("@NombreCompleto", objeto.NombreCompleto);
                cmd.Parameters.AddWithValue("@cedula",objeto.Cedula);
                cmd.Parameters.AddWithValue("@IdDepartamento", objeto.refDepartamento.IdDepartamento);
                cmd.Parameters.AddWithValue("@Sueldo", objeto.sueldo);
                cmd.Parameters.AddWithValue("@FechaContrato", objeto.fechaContrato);
                cmd.Parameters.Add("@Mensaje",SqlDbType.VarChar,100).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;

                //ejecutamos el cmd
                try
                {
                   var resp =await cmd.ExecuteNonQueryAsync(); // 1  completado || 0 no completado  //ejecutamos el procedimiento

                    if (resp==0)
                    {
                       respuesta = Convert.ToString(cmd.Parameters["@mensaje"].Value)!;

                    }
                    else if(resp>01) 
                    {
                        respuesta = "empleado registrado con exito";
                    }    

                }
                catch (Exception ex)
                {
                    respuesta = ex.Message;
                }

            }

            return respuesta;

        }

        //editar empleado
        public async Task<string>Editar(Empleado objeto)
        {
            //una repuesta que retornara
            string respuesta = "";

            using (var conn = new SqlConnection(_connection.cadenaConexion))
            {
             //abrimos la conexion
                await conn.OpenAsync();
                //creamos el cmd
                var cmd = new SqlCommand("sp_editarEmpleado", conn);
                cmd.Parameters.AddWithValue("@IdEmpleado", objeto.IdEmpleado);
                cmd.Parameters.AddWithValue("@NombreCompleto", objeto.NombreCompleto);
                cmd.Parameters.AddWithValue("@cedula", objeto.Cedula);
                cmd.Parameters.AddWithValue("@IdDepartamento", objeto.refDepartamento.IdDepartamento);
                cmd.Parameters.AddWithValue("@Sueldo", objeto.sueldo);
                cmd.Parameters.AddWithValue("@FechaContrato", objeto.fechaContrato);
                cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;

                //aplicamos la logica
                try
                {
                    var resp = await cmd.ExecuteNonQueryAsync();

                    if (resp == 0)
                    {
                        respuesta = Convert.ToString(cmd.Parameters["@Mensaje"].Value)!;

                    }else if (resp>0)
                    {
                        respuesta = "empleado modificado con exito";
                    }

                }
                catch (Exception ex)
                {
                    respuesta = ex.Message;
                }
            }

            return respuesta;
        }

        //eliminar empleado
        public async Task<string>Eliminar(int id)
        {
            string respuesta = "";
            using ( var conn = new SqlConnection(_connection.cadenaConexion))
            {
                 await conn.OpenAsync();

                var cmd = new SqlCommand("sp_eliminarEmpleado",conn);
                cmd.Parameters.AddWithValue("@IdEmpleado", id);
                cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;

                // ahora la logica

                try
                {
                    var rep = await cmd.ExecuteNonQueryAsync();
                    if(rep == 0)
                    {
                        respuesta = " usuario no se pudo eliminar";

                    }else if (rep > 0)
                    {
                        respuesta = Convert.ToString(cmd.Parameters["@Mensaje"].Value)!;
                    }
                }
                catch (Exception ex)
                {
                    respuesta = ex.Message;
                }

            }

            return respuesta;
        }

    }
}
