using System.Data;
using locationapi.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace locationapi.Controllers;

[ApiController]
[Route("[controller]")]
public class LocationController : ControllerBase
{
     private readonly ILogger<LocationController> _logger;

    public LocationController(ILogger<LocationController> logger)
    {
        _logger = logger;
    }
    
    [HttpGet(Name = "GetIMEILocation")]
    //Authorize(Policy = "AdminOnly")]
    [Authorize]
    public IEnumerable<IMEILocation> Get()
    {
        List<IMEILocation> loclist = new List<IMEILocation>();
        IMEILocation loc = new IMEILocation();
        
        AdoHelper ado = new AdoHelper("Server=zartbit.database.windows.net;Database=zbgps;User Id=denis;Password=nessD1zbsql;");
        DataRow dr = ado.ExecDataSet(@"SELECT TOP (1) [id]
                              ,[device_id]
                              ,[latitude]
                              ,[longitude]
	                          ,[timestamp]'utc'
	                          ,(dateadd( hh,-8, [location_history].[timestamp]))'pacific'
                          FROM [dbo].[location_history]                        
                          order by id desc").Tables[0].Rows[0];

        loc.Latitude = dr["latitude"].ToString();
        loc.Longitude = dr["longitude"].ToString();
        loc.DeviceID = (int)dr["device_id"];
        loclist.Add(loc);
        
        return loclist;
    }

    [HttpGet]
    [Route("{id:int}")]
    [Authorize]
    public IEnumerable<IMEILocation> Get(int ID)
    {
        List<IMEILocation> loclist = new List<IMEILocation>();
        IMEILocation loc = new IMEILocation();

        AdoHelper ado = new AdoHelper("Server=zartbit.database.windows.net;Database=zbgps;User Id=denis;Password=nessD1zbsql;");
        DataRow dr = ado.ExecDataSet(@"SELECT TOP (1) [id]
                              ,[device_id]
                              ,[latitude]
                              ,[longitude]
	                          ,[timestamp]'utc'
	                          ,(dateadd( hh,-8, [location_history].[timestamp]))'pacific'
                          FROM [dbo].[location_history]
                          where device_id =@id
                          order by id desc","@id",ID).Tables[0].Rows[0];

        loc.Latitude = dr["latitude"].ToString();
        loc.Longitude = dr["longitude"].ToString();
        loc.DeviceID = (int)dr["device_id"];
        loclist.Add(loc);

        return loclist;
    }

}
