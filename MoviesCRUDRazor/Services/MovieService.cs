using MoviesCRUDRazor.Models;
using MoviesCRUDRazor.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System;

namespace InvestManager.Services
{
    public class MovieService
    {
        private readonly MoviesCRUDRazorContext _context;

        public MovieService(MoviesCRUDRazorContext context)
        {
            _context = context;
        }

        public async Task<List<Movie>> FindAllAsync()
        {
            return await _context.Movies.ToListAsync();
        }

        public async Task InsertAsync(Movie obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Movie> FindByIdAsync(int id)
        {
            return await _context.Movies.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                _context.Movies.Remove(await _context.Movies.FindAsync(id));
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new DBConcurrencyException(e.Message);
            }
        }

        public async Task UpdateAsync(Movie obj)
        {
            bool hasAny = await _context.Movies.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny)
                throw new Exception("Id not Found");

            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DBConcurrencyException(e.Message);
            }
        }
    }
}
