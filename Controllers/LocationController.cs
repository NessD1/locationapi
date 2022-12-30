using System.Data;
using Microsoft.AspNetCore.Mvc;

namespace locationapi.Controllers;

[ApiController]
[Route("[controller]")]
public class LocationController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<LocationController> _logger;

    public LocationController(ILogger<LocationController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetIMEILocation")]
    public IEnumerable<IMEILocation> Get()
    {
        List<IMEILocation> loclist = new List<IMEILocation>();
        IMEILocation loc = new IMEILocation();
        
        AdoHelper ado = new AdoHelper("Server=denisdb.mssql.somee.com;Database=denisdb;User Id=nesshack_SQLLogin_1;Password=mfwj5bo5it;");
        DataRow dr = ado.ExecDataSet("select latitude,longitude from location_history").Tables[0].Rows[0];

        loc.Latitude = dr["latitude"].ToString();
        loc.Longitude = dr["longitude"].ToString();
        loclist.Add(loc);
        
        return loclist;
    }
}
