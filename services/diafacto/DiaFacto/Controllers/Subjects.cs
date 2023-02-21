using System.Net;
using DiaFacto.Models;
using Microsoft.AspNetCore.Mvc;

namespace DiaFacto.Controllers;

[ApiController]
[Route("/subjects")]
public class Subjects : Controller
{
    private readonly ILogger<Subjects> _logger;
    private readonly DbAccess _dbAccess;

    public Subjects(ILogger<Subjects> logger, DbAccess dbAccess)
    {
        _logger = logger;
        _dbAccess = dbAccess;
    }

    [HttpGet]
    [Route("")]
    public List<Subject> GetSubjects()
    {
        _logger.LogInformation("GetSubjects");
        return _dbAccess.GetSubjectsList();
    }

    [HttpGet]
    [Route("{id}")]
    public IActionResult GetSubject(string id)
    {
        _logger.LogInformation("GetSubject {id}", id);
        var subject = _dbAccess.GetSubject(id);
        if (subject is null)
            return NotFound();
        //1676959200
        //1676930400
        return Ok(subject);
    }
}