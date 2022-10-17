using DevelopersAPI.Models;
using Microsoft.AspNetCore.Mvc;
using DevelopersAPI.Adapters;
using DevelopersAPI.Domain;

namespace DevelopersAPI.Controllers;

public class DevelopersController : ControllerBase
{

    private readonly MongoDevelopersAdapter _mongoDevelopersAdapter;

    public DevelopersController(MongoDevelopersAdapter mongoDevelopersAdapter)
    {
        _mongoDevelopersAdapter = mongoDevelopersAdapter;
    }

    // GET /on-call-developer
    [HttpGet("/on-call-developer")]
    public ActionResult GetOnCallDeveloper()
    {
        var response = new DeveloperDetailsModel("1", "Elliott", "Ullman", "740-885-9456", "elliott_ullman@progressive.com");

        return Ok(response); // automatically json format
    }

    [HttpGet("/developers")]
    public ActionResult GetAllDevelopers()
    {
        var response = new CollectionResponse<DeveloperSummaryModel>();
        response.Data = new List<DeveloperSummaryModel>()
        {
            new DeveloperSummaryModel("1", "Jeff", "Gonzalez", "jeff@hypertheory.com"),
            new DeveloperSummaryModel("2", "Sue", "Jones", "sue@aol.com")
        };
        return Ok(response);
    }

    [HttpPost("/developers")]
    public async Task<ActionResult> AddADeveloper([FromBody] DeveloperCreateModel request)
    {
        var developerToAdd = new DeveloperEntity
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Phone = request.Phone,
            IsOnCallDeveloper = true
        };

        await _mongoDevelopersAdapter.Developers.InsertOneAsync(developerToAdd);

        return StatusCode(201); // "Good. Ok. I created this.
    }

}
