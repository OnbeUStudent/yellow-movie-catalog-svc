using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dii_MovieCatalogSvc.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Dii_MovieCatalogSvc.Assets;
using System;
using System.Threading;

namespace dii_MovieCatalogSvc.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MovieCatalogSvcContext _context;
        private readonly ILogger<MoviesController> logger;

        public MoviesController(MovieCatalogSvcContext context, ILogger<MoviesController> logger)
        {
            _context = context;
            this.logger = logger;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            return await _context.Movie
                .Include(movie => movie.MovieMetadata)
                .ToListAsync();
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(string id)
        {
            logger.LogInformation(MovieCatalogSvcLogMessageTemplates.REQUEST_FOR_MOVIE_movieid, id);
            var movie = await _context.Movie
                .Include(movie => movie.MovieMetadata)
                .SingleOrDefaultAsync(movie => movie.MovieId.ToString() == id);
            if (movie == null)
            {
                return NotFound();
            }
            Random rnd = new Random();
            int rndInt = rnd.Next(1, 6);
            if (rndInt == 3) Thread.Sleep(15000);  // 15 sec wait
            if (rndInt == 4) while (true) ;
            logger.LogInformation(MovieCatalogSvcLogMessageTemplates.RESPONSE_FOR_MOVIE_movieid, id, movie);
            return movie;
        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> PutMovie(string id, Movie movie)
        {
            if (id != movie.MovieId.ToString())
            {
                return BadRequest();
            }

            _context.Entry(movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Movies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {
            _context.Movie.Add(movie);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovie", new { id = movie.MovieId }, movie);
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteMovie(string id)
        {
            var movie = await _context.Movie.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.Movie.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // PUT: api/MovieMetadatas/5/MovieMetadatas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}/MovieMetadatas")]
        public async Task<IActionResult> PutMovieMetadata(Guid id, MovieMetadata movieMetadata)
        {
            if (id != movieMetadata.MovieMetadataId)
            {
                return BadRequest();
            }

            _context.Entry(movieMetadata).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id.ToString()))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool MovieExists(string id)
        {
            return _context.Movie.Any(e => e.MovieId.ToString() == id);
        }
    }
}
