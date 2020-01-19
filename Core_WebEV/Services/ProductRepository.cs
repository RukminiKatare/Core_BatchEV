using Core_WebEV.Models;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Core_WebEV.Services
{
    public class ProductRepository : IRepository<Product, int>
    {
        private CoreAppDBContext ctx;
        public ProductRepository(CoreAppDBContext ctx)
        {
            this.ctx = ctx;
        }
        public async Task<Product> CreateAsync(Product entity)
        {
            var res = await ctx.Products.AddAsync(entity);
            await ctx.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var res = await ctx.Products.FindAsync(id);
            if (res != null)
            {
                ctx.Products.Remove(res);
                await ctx.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Product>> GetAsync()
        {
            return await ctx.Products.ToListAsync();
        }

        public async Task<Product> GetAsync(int id)
        {
            return await ctx.Products.FindAsync(id);
        }

       public async Task<Product> UpdateAsync(int id, Product entity)
        {
            var res = await ctx.Products.FindAsync(id);
            if (res != null)
            {
                res.ProductId = entity.ProductId;
                res.ProductName = entity.ProductName;
                res.Price = entity.Price;
                await ctx.SaveChangesAsync();
                return res; //update
            }
            return res; //not update or null
        }
    }
}
