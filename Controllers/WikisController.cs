using Microsoft.AspNetCore.Mvc;

namespace CoreWorkshop.Api.Controllers;

[ApiController]
[Route("/wikis")]
public class WikisController : ControllerBase
{
    [HttpGet(Name = "Wikis")]
    public async Task Get()
    {
        var duplicationCheck = new HashSet<string>();

        await new EventsService(Response).WriteNewEvents(async record =>
        {
            if (record.Wiki != null && !duplicationCheck.Contains(record.Wiki))
            {
                duplicationCheck.Add(record.Wiki);

                await Response.WriteAsync($"type: data\ndata: {record.Wiki}\n\n");
            }
        });
    }
}