using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanLyThuVien.Dtos;
using QuanLyThuVien.Models;
using QuanLyThuVien.Services;

namespace QuanLyThuVien.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;
        private readonly IMapper _mapper;

        public GenresController(IGenreService genreService,
            IMapper mapper)

        {
            _mapper = mapper;
            _genreService = genreService;
        }

        // GET: api/Genres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenreDto>>> GetGenres()
        {
            var genres = await _genreService.GetGenres();
            var genreDtos = genres.Select(genres => _mapper.Map<GenreDto>(genres)).ToList();
            return genreDtos; 
        }

        // GET: api/Genres/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GenreDto>> GetGenre(int id)
        {
            var genre = await _genreService.GetGenre(id);

            if (!_genreService.GenreExists(id))
            {
                return NotFound();
            }
            var genreDto = _mapper.Map<GenreDto>(genre);

            return genreDto;
        }

        // PUT: api/Genres/5
        [HttpPut("{id}")]
    [Authorize(Roles = "admin")]

        public async Task<IActionResult> PutGenre(int id, Genre genre)
        {
            if (!await _genreService.PutGenre(id, genre))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Genres
        [HttpPost]
    [Authorize(Roles = "admin")]

        public async Task<ActionResult<Genre>> PostGenre(Genre genre)
        {
            var newGenre = await _genreService.PostGenre(genre);
            return CreatedAtAction(nameof(GetGenre), new { id = newGenre.Value.GenreID }, newGenre);
        }

        // DELETE: api/Genres/5
        [HttpDelete("{id}")]
    [Authorize(Roles = "admin")]

        public async Task<IActionResult> DeleteGenre(int id)
        {
            if (!await _genreService.DeleteGenre(id))
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
