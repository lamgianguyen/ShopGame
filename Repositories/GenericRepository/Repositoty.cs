using AutoMapper;
using DUCtrongAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DUCtrongAPI.Repositories.GenericRepository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ShopingContext context;
        private DbSet<T> _entities;
        protected readonly IMapper mapper;
        public Repository(ShopingContext context, IMapper mapper)
        {
            this.context = context;
            _entities = context.Set<T>();
            this.mapper = mapper;
        }
        public Repository(ShopingContext context)
        {
            this.context = context;
            _entities = context.Set<T>();
        }

        public async Task<T> Get(string id)
        {
            return await _entities.FindAsync(id);   
        }

        public async Task<bool> Insert(T entity)
        {
            await _entities.AddAsync(entity);
            await Update();
            return true;
        }

        public async Task<bool> Remove(T entity)
        {
            context.Remove(entity);
            await Update();
            return true;
        }

        public async Task<bool> Update()
        {
            await context.SaveChangesAsync();
            return true;
        }
    }
}
