using DomainObjects;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SearchWorkPuzzleApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SearchPuzzleController : ControllerBase
{
    [HttpPatch("search")]
    public IEnumerable<string> Search([FromBody][Required] string[] matrix, [FromQuery][Required] string[] words)
    {
        var wordFinder = new WordFinder(matrix);
        return wordFinder.Find(words);
    }
}
