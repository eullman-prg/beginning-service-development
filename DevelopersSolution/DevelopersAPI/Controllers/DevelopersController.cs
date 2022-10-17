using DevelopersAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevelopersAPI.Controllers;

public class DevelopersController : ControllerBase
{

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
    public ActionResult AddADeveloper([FromBody] DeveloperCreateModel request)
    {
        var response = new DeveloperDetailsModel(Guid.NewGuid().ToString(), request.FirstName, request.LastName, request.Email, request.Phone);
        return StatusCode(201, response); // "Good. Ok. I created this.
    }

}
