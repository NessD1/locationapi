using Microsoft.AspNetCore.Mvc;
using locationapi.Modelos;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;

namespace locationapi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccessToDeviceController : ControllerBase
    {

        [HttpGet]
        [Route("usuario/{IDDeUsuario}")] // El valor entre llaves debe ser el mismo nombre ( exactamente igual ) que la 
                                         // variable en donde va a llegar el valor.
        [Authorize]
        public IEnumerable<Device> Get(int IDDeUsuario) // El WebAPI automáticamente convierte el valor devuelto a JSON.
        {
            List<Device> listaDeDispositivosPermitidos = new List<Device>();


            string cadenaDeConexión = "Server=zartbit.database.windows.net;Database=zbGPS;" +
                                      " user id=denis;password=nessD1zbsql;";

            string qryObtenerIDsDeDispositivosPermitidos = "SELECT id,ID_DeDispositivoPermitido,ID_DeUsuario " +
                                                      "FROM dbo.AccesoADispositivos where ID_DeUsuario = @IDDeUsuario";

            string qryObtenerDispositivosPermitidos = "SELECT id,IMEI,NúmeroDeControl,customer_id,is_active," +
                                                      "is_history_active FROM dbo.device WHERE id = @id";

            string qryObtenerDetallesDelVehículoCorrespondiente = "SELECT [NombreAsignadoAlDispositivo],[Modelo],Tipo," +
                                                                  "[Propietario],ID_DeDispositivo FROM " +
                                                                  "[dbo].[DetallesDelVehículo]" +
                                                                  "WHERE ID_DeDispositivo = @id";

            using (SqlConnection conexión = new SqlConnection(cadenaDeConexión))
            {
                conexión.Open();
                using (SqlCommand comando = new SqlCommand(qryObtenerIDsDeDispositivosPermitidos, conexión))
                {
                    comando.CommandType = CommandType.Text;
                    SqlParameter prmIDDeUsuario = new SqlParameter("IDDeUsuario", IDDeUsuario);// TODO: Ver si funciona 
                                                                                               // colocar @IDDeUsuario
                    comando.Parameters.Add(prmIDDeUsuario);

                    SqlDataAdapter adaptador = new SqlDataAdapter(comando);
                    DataSet ds = new DataSet();
                    adaptador.Fill(ds);
                    // Se debe guardar la lista de IDs de dispositivos permitidos y luego buscar la información de cada
                    // uno de ellos para llenar la lista de dispositivos y enviarla al cliente, a la app de MAUI.
                    DataTable dt = ds.Tables[0];
                    foreach(DataRow dr in dt.Rows)
                    {
                        Device dispositivoPermitido = new Device();
                        dispositivoPermitido.ID = (int)dr["ID_DeDispositivoPermitido"];
                        listaDeDispositivosPermitidos.Add(dispositivoPermitido);
                    }
                }

                using (SqlCommand comando = new SqlCommand(qryObtenerDispositivosPermitidos, conexión))
                {
                    comando.CommandType = CommandType.Text;
                    foreach (Device dispositivo in listaDeDispositivosPermitidos)
                    {
                        SqlParameter prmIDDeDispositivo = new SqlParameter("@id", dispositivo.ID);
                        comando.Parameters.Add(prmIDDeDispositivo);

                        SqlDataAdapter adaptador = new SqlDataAdapter(comando);
                        DataSet ds = new DataSet();
                        adaptador.Fill(ds);
                        DataTable dt = ds.Tables[0]; // Sólo tiene un renglón porque cada dispositivo tiene su id único.
                        dispositivo.IMEI = dt.Rows[0]["IMEI"].ToString(); 
                        dispositivo.NúmeroDeControl = dt.Rows[0]["NúmeroDeControl"].ToString();
                        dispositivo.IsActive = (bool)dt.Rows[0]["is_active"];
                        dispositivo.IsHistoryActive = (bool)dt.Rows[0]["is_history_active"];
                        dispositivo.CustomerID = dt.Rows[0]["customer_id"].ToString();
                        // NOTA: Los posibles nulls se prohíben desde la tabla.
                        comando.Parameters.Clear();
                    }
                }

                using (SqlCommand comando = new SqlCommand(qryObtenerDetallesDelVehículoCorrespondiente, conexión))
                {
                    comando.CommandType = CommandType.Text;
                    foreach(Device dispositivo in listaDeDispositivosPermitidos)
                    {
                        SqlParameter prmIDDeDispositivo = new SqlParameter("@id", dispositivo.ID);
                        comando.Parameters.Add(prmIDDeDispositivo);

                        SqlDataAdapter adaptador = new SqlDataAdapter(comando);
                        DataSet ds = new DataSet();
                        adaptador.Fill(ds);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataTable dt = ds.Tables[0]; // Sólo tiene un renglón porque cada dispositivo tiene su id único.
                            dispositivo.NombreAsignadoAlDispositivo = dt.Rows[0]["NombreAsignadoAlDispositivo"].ToString();
                            dispositivo.ModeloDeAuto = dt.Rows[0]["Modelo"].ToString();
                            dispositivo.Propietario = dt.Rows[0]["Propietario"].ToString();
                        }
                        else
                        {
                            dispositivo.NombreAsignadoAlDispositivo = "No especificado";
                            dispositivo.ModeloDeAuto = "No especificado";
                            dispositivo.Propietario = "No especificado";
                        }
                        comando.Parameters.Clear();
                    }
                }
            }

            return listaDeDispositivosPermitidos;
        }

    }
}
