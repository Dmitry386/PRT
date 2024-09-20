using Microsoft.EntityFrameworkCore;

namespace RestProjectRouTeam.DataAccess.Repositories
{
    public abstract class AbstractRepository<TModel, TContext>
        where TModel : class
        where TContext : DbContext
    {
        protected readonly TContext _context;

        public AbstractRepository(TContext context)
        {
            _context = context;
        }

        public virtual async Task<List<TModel>> Get()
        {
            return await _context.Set<TModel>()
                .AsNoTracking()
                .ToListAsync();
        }

        public virtual async Task<TModel> Create(TModel entity)
        {
            await _context.Set<TModel>().AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public virtual async Task<TModel> Update(TModel entity)
        {
            _context.Set<TModel>().Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public virtual async Task<int> Delete(int id)
        {
            var obj = await Get(id);
            if (obj != null) _context.Set<TModel>().Remove(obj);
            return id;
        }

        public virtual async Task<TModel> Get(int id)
        {
            return await _context.Set<TModel>()
                .FindAsync(id);
        }
    }
}
