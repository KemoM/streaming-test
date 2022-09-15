using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;

namespace CoreWorkshop.Api.Controllers;

[ApiController]
[Route("")]
public class TitlesController : ControllerBase
{
    [HttpGet(Name = "Titles")]
    [Route("/titles")]
    public async Task GetTitles()
    {
        await new EventsService(Response).WriteNewEvents(async record =>
        {
            await Response.WriteAsync($"type: data\ndata: {record.Title}\n\n");
        });
    }

    [HttpGet(Name = "WikiCurrentTitles")]
    [Route("/titles/current")]
    public IEnumerable<string?> GetWikiTitles()
    {
        return WikiService.GetNewWikiRecords(null).Where(record => record.Title != null).Select(record => record.Title);
    }

    [HttpGet(Name = "WikiTitles")]
    [Route("/titles/{wiki}")]
    public async Task GetWikiTitles(string wiki)
    {
        await new EventsService(Response).WriteNewEvents(async record =>
        {
            if (record.Wiki != null && record.Wiki.Equals(wiki))
            {
                await Response.WriteAsync($"type: data\ndata: {record.Wiki}\n\n");
            }
        });
    }

    [HttpGet(Name = "FilterWikiTitles")]
    [Route("/titles/filter/{word}")]
    public async Task GetWikiTitlesFiltered(string word)
    {
        await new EventsService(Response).WriteNewEvents(async record =>
        {
            if (record.Title != null && record.Title.IndexOf(word, 0, StringComparison.OrdinalIgnoreCase) != -1)
            {
                await Response.WriteAsync($"type: data\ndata: {record.Title}\n\n");
            }
        });
    }
}