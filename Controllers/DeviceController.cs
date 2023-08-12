using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static locationapi.Controllers.InventarioController;
using System.Data;
using Newtonsoft.Json;
using locationapi.Modelos;
using Microsoft.AspNetCore.Authorization;

namespace locationapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeviceController : ControllerBase
    {
        [HttpGet(Name = "GetDevice")]
        [Authorize]
        public Device Get(int ID)
        {            
            Device device;

            AdoHelper ado = new AdoHelper("Server=zartbit.database.windows.net;Database=zbGPS; user id=denis;password=nessD1zbsql;");
            DataRow drDevice = ado.ExecDataSet("SELECT * FROM device where id=@id","@id", ID).Tables[0].Rows[0];
           
                device = new Device();
                device.ID = (int)drDevice["id"];
                device.IMEI = drDevice["imei"].ToString();
                device.CustomerID = drDevice["customer_id"].ToString();
                device.IsActive = (bool)drDevice["is_active"];
                device.IsHistoryActive = (bool)drDevice["is_history_active"];

                // Agregado para dibujar circulo de área definida en la Aplicación de .NET MAUI.
                device.LatitudActual = drDevice["LatitudActual"].ToString();
                device.LongitudActual = drDevice["LongitudActual"].ToString();

            return device;
        }

        // Función que hace un PUT ( UPDATE ) al IsHistoryActive.
        //[Consumes("application/json")]
        //[Produces("application/json")]
        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Put(int id, DeviceHistoryStatus value) // El DeviceHistoryStatus se pasa en el body.
        {
            try
            {

                //DeviceHistoryStatus dispositivo = JsonConvert.DeserializeObject<DeviceHistoryStatus>(value);

                AdoHelper ado = new AdoHelper("Server=zartbit.database.windows.net;Database=zbGPS;" +
                                              " user id=denis;password=nessD1zbsql;");

                ado.ExecNonQueryProc("spUpdateHistoryStatus", "@device_id", id,
                                     "@status", value.IsHistoryActive); // Un valor bool puede ser usado como valor tipo bit en SQL.
                ado.Dispose();
                return Ok(value);
            }
            catch
            {
                return BadRequest();
            }
        }


        //foreach (DataRow dr in tbDevices.Rows)
        //    {
        //        device = new Device();
        //device.ID = (int) dr["id"];
        //device.IMEI = dr["imei"].ToString();
        //device.Active = (bool) dr["active"];
        //device.IsOn = (bool) dr["is_on"];
        //device.IsHistoryActive = (bool) dr["is_history_active"];
        //deviceList.Add(device);
        //    }
    }
}
