using DevelopersApi.Adapters;
using Microsoft.AspNetCore.Mvc;

namespace DevelopersAPI.Controllers;

public class StatusController : ControllerBase
{

    private readonly OutageSupplierHttpAdapter _outageSupplierHttpAdapter;

    public StatusController(OutageSupplierHttpAdapter outageSupplierHttpAdapter)
    {
        _outageSupplierHttpAdapter = outageSupplierHttpAdapter;
    }

    [HttpGet("/status")]
    public async Task<ActionResult> GetStatusAsync()
    {
        List<ScheduledOutage>? outages;
        try
        {
            outages = await _outageSupplierHttpAdapter.GetScheduledOutagesAsync();
        }
        catch (Exception)
        {
            // log it, whatever
            outages = null;
        }



        var response = new StatusResponse("Looks Good, Captain!", DateTime.Now, outages);
        return Ok(response);
    }
}

public record StatusResponse(string Status, DateTime whenChecked, List<ScheduledOutage> Outages);

public record ScheduledOutage(DateTime StartTime, DateTime EndTime, string Reason);