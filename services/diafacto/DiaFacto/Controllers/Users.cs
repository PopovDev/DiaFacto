using System.Net;
using DiaFacto.Models;
using Microsoft.AspNetCore.Mvc;

namespace DiaFacto.Controllers;

[ApiController]
[Route("/users")]
public class Users : Controller
{
    private readonly ILogger<Users> _logger;
    private readonly DbAccess _dbAccess;

    public Users(ILogger<Users> logger, DbAccess dbAccess)
    {
        _logger = logger;
        _dbAccess = dbAccess;
    }

    [HttpGet]
    [Route("")]
    public List<User> GetUsers()
    {
        _logger.LogInformation("Get all users");
        var users = _dbAccess.GetUsersList();
        return users;
    }

    [HttpGet]
    [Route("{id}")]
    public IActionResult GetUser(string id)
    {
        _logger.LogInformation("Get user {id}", id);
        var user = _dbAccess.GetUser(id);
        if (user is null)
            return NotFound();
        return Ok(user);
    }
}