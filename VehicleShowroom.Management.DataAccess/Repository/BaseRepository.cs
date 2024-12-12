using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using VehicleShowroom.Management.DataAccess.DataAccess;

namespace VehicleShowroom.Management.DataAccess.Repository
{
    // Generic repository for CRUD operations with EF Core
    public class BaseRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        // Constructor to inject the DbContext
        public BaseRepository(ApplicationDbContext context) { 
            _context = context;
        }

        //Get all entities with optional filter and navigation properties
        public async Task<IEnumerable<T>> GetAllAsync(
           Expression<Func<T, bool>> expression = null,
           Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (expression != null) query = query.Where(expression); // Apply filter
            if (include != null) query = include(query); // Include navigation properties
            query = query.OrderByDescending(t => EF.Property<object>(t, "CreateDate"));
            return await query.AsNoTracking().ToListAsync();
        }

        //Get a single entity by a condition
        public async Task<T?> GetSingleAsync(Expression<Func<T, bool>> expression = null)
        {
            return await _context.Set<T>().AsNoTracking().SingleOrDefaultAsync(expression);
        }

        //Add a new entity to the database
        public async Task AddAsync(T entity) 
        { 
            await _context.Set<T>().AddAsync(entity);
        }

        //Update an existing entity
        public void UpdateAsync(T entity) 
        {
            _context.Set<T>().Update(entity);
        }

        // Delete an entity
        public void DeleteAsync(T entity) 
        {
            _context.Set<T>().Remove(entity);
        }

        // Delete a list of entities
        public void DeleteRangeAsync(List<T> entity)
        {
            _context.Set<T>().RemoveRange(entity);
        }

        // Save changes to the database
        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        // count
        public async Task<int> CountAsync()
        {
            return await _context.Set<T>().CountAsync();
        }
    }
}
