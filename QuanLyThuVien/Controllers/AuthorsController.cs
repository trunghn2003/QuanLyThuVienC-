using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Data;
using QuanLyThuVien.Dtos;
using QuanLyThuVien.Models;
using QuanLyThuVien.Services;

namespace QuanLyThuVien.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        private readonly IMapper _mapper;

        public AuthorsController(IAuthorService authorService, IMapper mapper)
        {
            _authorService = authorService;
            _mapper = mapper;
        }

       

        // GET: api/Authors
        // use authorication

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAuthor()
        {
            var authors = await _authorService.GetAuthor();
            var authorDtos = authors.Select(author => _mapper.Map<AuthorDto>(author)).ToList();
            return Ok(authorDtos);
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDto>> GetAuthor(int id)
        {
            if (!_authorService.AuthorExists(id))
            {
                return NotFound();
            }
          
            var author = await _authorService.GetAuthor(id);
            var authorDto = _mapper.Map<AuthorDto>(author); 

            return Ok(authorDto);
        }

        // PUT: api/Authors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
    [Authorize(Roles = "admin")]

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
    [Authorize(Roles = "admin")]

        public async Task<ActionResult<Author>> PostAuthor(Author author)
        {

            var newAuthor = await _authorService.PostAuthor(author);


            return CreatedAtAction(nameof(GetAuthor), newAuthor);

        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
    [Authorize(Roles = "admin")]

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
