using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Data;
using QuanLyThuVien.Models;
using QuanLyThuVien.Services;

namespace QuanLyThuVien.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService; 
        }

        // GET: api/Authors
        // use authorication

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthor()
        {
            return await _authorService.GetAuthor();
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthor(int id)
        {

            var author = await _authorService.GetAuthor(id);

            if (!_authorService.AuthorExists(id))
            {
                return NotFound();
            }

            return author;
        }

        // PUT: api/Authors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize("admin")]

        public async Task<IActionResult> PutAuthor(int id, Author author)
        {
            if (!await _authorService.PutAuthor(id, author))
            {
                return NotFound();
            }

            return NoContent();


        }

        // POST: api/Authors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize("admin")]

        public async Task<ActionResult<Author>> PostAuthor(Author author)
        {

            var newAuthor = await _authorService.PostAuthor(author);


            return CreatedAtAction(nameof(GetAuthor), newAuthor);

        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        [Authorize("admin")]

        public async Task<IActionResult> DeleteAuthor(int id)
        {
            if (!await _authorService.DeleteAuthor(id))
            {
                return NotFound();
            }
            return NoContent();
        }

       
    }
}
