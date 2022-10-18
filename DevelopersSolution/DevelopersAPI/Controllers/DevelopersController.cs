using DevelopersAPI.Models;
using Microsoft.AspNetCore.Mvc;
using DevelopersAPI.Adapters;
using DevelopersAPI.Domain;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Bson;

namespace DevelopersAPI.Controllers;

public class DevelopersController : ControllerBase
{

    private readonly MongoDevelopersAdapter _mongoDevelopersAdapter;

    public DevelopersController(MongoDevelopersAdapter mongoDevelopersAdapter)
    {
        _mongoDevelopersAdapter = mongoDevelopersAdapter;
    }

    // PUT /on-call-developer
    [HttpPut("/on-call-developer")]
    public async Task<ActionResult> AssignOnCallDeveloper([FromBody] DeveloperSummaryModel request)
    {
        var objectId = ObjectId.Parse(request.Id);
        var whereIsOnCallFilter = Builders<DeveloperEntity>.Filter.Where(d => d.IsOnCallDeveloper == true);
        var updateToUnsetOnCallDeveloper = Builders<DeveloperEntity>.Update.Set(d => d.IsOnCallDeveloper, false);

        await _mongoDevelopersAdapter.Developers.UpdateOneAsync(whereIsOnCallFilter, updateToUnsetOnCallDeveloper);
        // If there is one that is already on call, take them "off call"

        var newIsOnCallFilter = Builders<DeveloperEntity>.Filter.Where(d => d.Id == objectId);
        var newInOnCallUpdate = Builders<DeveloperEntity>.Update.Set(d => d.IsOnCallDeveloper, true);


        await _mongoDevelopersAdapter.Developers.UpdateOneAsync(newIsOnCallFilter, newInOnCallUpdate);
        // we need to change the developer passed in here's oncall property to true and save it.
        return Accepted();
    }

    // GET /on-call-developer
    [HttpGet("/on-call-developer")]
    public async Task<ActionResult> GetOnCallDeveloper()
    {
        var response = await _mongoDevelopersAdapter.Developers.AsQueryable()
            .Where(d => d.IsOnCallDeveloper == true)
            .Select(d => new DeveloperDetailsModel(d.Id.ToString(), d.FirstName, d.LastName, d.Phone, d.Email))
            .SingleOrDefaultAsync();

        return Ok(response); // automatically json format
    }

    [HttpGet("/developers")]
    public async Task<ActionResult> GetAllDevelopersAsync()
    {
        var response = new CollectionResponse<DeveloperSummaryModel>();

        var data = _mongoDevelopersAdapter.Developers.AsQueryable()
             .Select(d => new DeveloperSummaryModel(
                 d.Id.ToString(), d.FirstName, d.LastName, d.Email));

        response.Data = await data.ToListAsync();

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
            IsOnCallDeveloper = false
        };

        await _mongoDevelopersAdapter.Developers.InsertOneAsync(developerToAdd);

        var response = new DeveloperSummaryModel(developerToAdd.Id.ToString(), developerToAdd.FirstName, developerToAdd.LastName, developerToAdd.Email);
        return StatusCode(201, response); // "Good. Ok. I created this.
    }

}
