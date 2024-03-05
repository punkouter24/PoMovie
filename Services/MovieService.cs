using Microsoft.EntityFrameworkCore;
using PoMovie.Data;
using PoMovie.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoMovie.Services
{
    public class MovieService
    {
        private readonly ApplicationDbContext _context;

        public MovieService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Movie>> GetMoviesByUserAsync(string userId)
        {
            return await _context.Movies
                                 .Where(m => m.UserId == userId)
                                 .ToListAsync();
        }

        public async Task AddMovieAsync(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMovieAsync(int movieId)
        {
            var movie = await _context.Movies.FindAsync(movieId);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
                await _context.SaveChangesAsync();
            }
        }
    }
}