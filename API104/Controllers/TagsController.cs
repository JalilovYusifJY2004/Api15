using API104.DAL;
using API104.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace API104.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TagsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int page=1,int take=2)
        {
            List<Tag> tags=await _context.Tags.Skip((page-1)*take).Take(take).ToListAsync();
            return StatusCode(StatusCodes.Status200OK,tags);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if(id<=0) return StatusCode(StatusCodes.Status400BadRequest);

            Tag? tag=await _context.Tags.FirstOrDefaultAsync(t=>t.Id == id);

            if(tag is null) return StatusCode(StatusCodes.Status404NotFound);

            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Tag tag)
        {
            await _context.Tags.AddAsync(tag);
            await _context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,string name)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);

            Tag? tag = await _context.Tags.FirstOrDefaultAsync(t => t.Id == id);

            if (tag is null) return StatusCode(StatusCodes.Status404NotFound);

            tag.Name = name;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);

            Tag? tag = await _context.Tags.FirstOrDefaultAsync(t => t.Id == id);

            if (tag is null) return StatusCode(StatusCodes.Status404NotFound);

            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
