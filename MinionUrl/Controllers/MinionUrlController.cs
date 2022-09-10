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


        //public virtual UrlData UrlConvert(UrlBuffer urlBuff)
        //{
        //    var urlData = new UrlData
        //    {

        //    };
        //    return urlData;
        //}
        //public virtual UrlBuffer UrlConvert(UrlData urlData)
        //{
        //    var urlBuff = new UrlBuffer
        //    {
        //        Id = urlData.Id,
        //        FullUrl = urlData.FullUrl,
        //        ShortUrl = urlData.ShortUrl,
        //        CreatorId = urlData.CreatorId,
        //        CreationDateTime = Convert.ToString(urlData.CreationDateTime)
        //    };
        //    return urlBuff;
        //}

        //GetAllUrls
        [HttpGet]
        public async Task<IActionResult> getAllUrls()
        {
            var context = await minionUrlDbContext.UrlData.ToListAsync();

            return Ok(context);
        }

        //GetSingleUrl
        [HttpGet("{id:guid}")]
        public async virtual Task<IActionResult> getSingleUrl([FromRoute] Guid id)
        {
            var url = await minionUrlDbContext.UrlData.FirstOrDefaultAsync(x => x.Id == id);
            if (url is null)
            {
                return NotFound("URL not found");
            }
            return Ok(url);
        }

        //AddUrl
        [HttpPost]
        public async Task<IActionResult> addUrl([FromBody] UrlBuffer UrlBuff)
        {
            if (UrlBuff is null)
            {
                return BadRequest("Null input");
            }

            var UrlFinal = new UrlData(UrlBuff.FullUrl!, Convert.ToInt32(UrlBuff.CreatorId));

            await minionUrlDbContext.UrlData.AddAsync(UrlFinal);
            await minionUrlDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(getSingleUrl), new { id = UrlFinal.Id }, UrlFinal);
        }

        //TODO add shortingUrl method
        //UpdateUrl
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> updateUrl([FromRoute] Guid id, [FromBody] UrlBuffer UrlBuff)
        {
            var UrlFinal = await minionUrlDbContext.UrlData.FirstOrDefaultAsync(x => x.Id == id);
            if (UrlFinal is null)
            {
                BadRequest("Null input");
            }

            UrlFinal!.FullUrl = UrlBuff.FullUrl!;
            UrlFinal.CreationDateTime = DateTime.Now;
            await minionUrlDbContext.SaveChangesAsync();
            return Ok(UrlFinal);
        }

        //DeleteUrl
        [HttpDelete]

        [Route("{id:guid}")]
        public async Task<IActionResult> deleteUrl([FromRoute] Guid id)
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
