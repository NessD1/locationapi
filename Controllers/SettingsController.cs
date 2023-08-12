using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace locationapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SettingsController : ControllerBase
    {
        [HttpGet]
        [Route("{id}")] // El id del cliente
        [Authorize]
        public int Get(int id)
        {
            int distPermitida = 0; // TODO: Revisar si afecta que valga cero.

            AdoHelper ado = new AdoHelper("Server=zartbit.database.windows.net;Database=zbGPS; user id=denis;password=nessD1zbsql;");
            DataRow drDistPermitida = ado.ExecDataSet("SELECT DistanciaPermitida FROM ConfiguraciónGeneral where id=@id", "@id", id).Tables[0].Rows[0];

            distPermitida = (int)drDistPermitida["DistanciaPermitida"];

            return distPermitida; // TODO: Revisar.
        }
    }
}
