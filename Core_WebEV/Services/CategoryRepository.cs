﻿using Core_WebEV.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_WebEV.Services
{
    public class CategoryRepository : IRepository<Category, int>
    {
        private CoreAppDBContext ctx;
        //Inject the coreAppDBContext in to the Repository class

            public CategoryRepository(CoreAppDBContext ctx)
        {
            this.ctx = ctx;
        }
       public async Task<Category> CreateAsync(Category entity)
        {
            var res = await ctx.Categories.AddAsync(entity);
            await ctx.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var res = await ctx.Categories.FindAsync(id);
            if (res != null)
            {
                ctx.Categories.Remove(res);
                await ctx.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Category>> GetAsync()
        {
            return await ctx.Categories.ToListAsync();
        }

       public async Task<Category> GetAsync(int id)
        {
            return await ctx.Categories.FindAsync(id);
        }

        public async Task<Category> UpdateAsync(int id, Category entity)
        {
            var res = await ctx.Categories.FindAsync(id);
            if (res != null) {
                res.CategoryId = entity.CategoryId;
                res.CateforyName = entity.CateforyName;
                res.BasePrice = entity.BasePrice;
                await ctx.SaveChangesAsync();
                return res; //update
            }
            return res; //not update or null
        }
       
    }
}
