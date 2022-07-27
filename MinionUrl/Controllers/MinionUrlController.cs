using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinionUrl.Data;
using MinionUrl.Models;

namespace MinionUrl.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class MinionUrlController : ControllerBase
    {
        private MinionUrlDbContext minionUrlDbContext;

        public MinionUrlController(MinionUrlDbContext minionUrlDbContext)
        {
            this.minionUrlDbContext = minionUrlDbContext;
        }

        [HttpGet("GetAllUrls")]
        public async Task<IActionResult> GetAllUrls()
        {
            var context = await minionUrlDbContext.UrlData.ToListAsync();
            return Ok(context);
        }

        [HttpGet("GetUrl")]
        public async Task<IActionResult> GetSingleUrl([FromRoute] Guid id)
        {
            var context = await minionUrlDbContext.UrlData.FirstOrDefaultAsync(x => x.Id == id);
            if (context is not null)
            {
                return Ok(context);
            }
            return NotFound("URL not found");
        }

        [HttpPost]
        public async Task<IActionResult> AddUrl([FromBody] UrlBuffer UrlBuff)
        {
            if (UrlBuff is null)
            {
                throw new ArgumentNullException(nameof(UrlBuff));
            }

            var UrlFinal = new UrlData(UrlBuff.Url, UrlBuff.CreatorId);

            await minionUrlDbContext.UrlData.AddAsync(UrlFinal);
            await minionUrlDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSingleUrl), new { id = UrlFinal.Id }, UrlFinal);
        }

        //TODO add shortingUrl method
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateUrl([FromRoute] Guid id, [FromBody] UrlBuffer UrlBuff)
        {
            var UrlFinal = await minionUrlDbContext.UrlData.FirstOrDefaultAsync(x => x.Id == id);
            if (UrlFinal is null)
            {
                return BadRequest("Null input");
            }
            UrlFinal.FullUrl = UrlBuff.Url;
            UrlFinal.CreationDateTime = DateTime.Now;
            await minionUrlDbContext.SaveChangesAsync();
            return Ok(UrlFinal);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteUrl([FromRoute] Guid id)
        {
            var url = await minionUrlDbContext.UrlData.FirstOrDefaultAsync(x => x.Id == id);
            if (url is null)
            {
                return BadRequest("Null input");
            }
            minionUrlDbContext.Remove(url);
            await minionUrlDbContext.SaveChangesAsync();
            return Ok(url);
        }
    }
}
