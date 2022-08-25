using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using postApi.Data;
using postApi.Models;
using System.Threading.Tasks;

namespace postApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PostController : ControllerBase
    {

        private readonly ApplicationDbContext _db;
    

        public PostController(ApplicationDbContext db)
        {
            _db = db;
        }


        //Methods


        [HttpGet]
        public IActionResult GetPosts()
        {
            return Ok(_db.Posts.ToList());
        }

        [HttpPost]
        public async Task<IActionResult> AddPost([FromBody] Post objPost)
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult("Error While Creating New Post");
            }
            _db.Posts.Add(objPost);
            await _db.SaveChangesAsync();

            return new JsonResult("Post Created Successfully");

        }

        [HttpPost("{id}")]
        public async Task<IActionResult> UpdatePost([FromRoute] int id, [FromBody] Post objPost)
        {
            if (objPost == null || id != objPost.id)
            {
                return new JsonResult("Post Was Not Found");
            }
            else
            {
                _db.Posts.Update(objPost);
                await _db.SaveChangesAsync();
                return new JsonResult("Post Was Updated Successfully");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetSelectedPost([FromRoute] int id)
        {
            return Ok(_db.Posts.Find(id));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost([FromRoute] int id)
        {
            var findPost = await _db.Posts.FindAsync(id);
            if (findPost == null)
            {
                return NotFound();
            }
            else
            {
                _db.Posts.Remove(findPost);
                await _db.SaveChangesAsync();
                return new JsonResult("Post Was Deleted Successfully");
            }
        }
    }
}
