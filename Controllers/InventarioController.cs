using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace locationapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InventarioController : Controller
    {
        [HttpGet(Name = "GetInventario")]
        [Authorize]           
        public IEnumerable<Inventario> Get()
        {
            List<Inventario> invelist = new List<Inventario>();
            Inventario inve;

            AdoHelper ado = new AdoHelper("Server=zartbit.database.windows.net;Database=zbGPS; user id=denis;password=nessD1zbsql;");
            DataTable tbInventario = ado.ExecDataSet("SELECT * FROM [Inventario actual];").Tables[0];

            foreach (DataRow dr in tbInventario.Rows)
            {
                inve = new Inventario();
                inve.Cantidad = (int)dr["cantidad"];
                inve.NumParte = dr["numero de parte"].ToString();
                inve.Descripcion = dr["descripcion"].ToString();
                inve.Proveedor = dr["proveedor"].ToString();
                invelist.Add(inve);
            }

            return invelist;
        }

        public class Inventario
        {
            public int Cantidad { get; set; }
            public string NumParte { get; set; }
            public string Descripcion { get; set; }
            public string Proveedor { get; set; }

        }
    }
}