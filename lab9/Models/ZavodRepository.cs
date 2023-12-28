using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace lab9.Models
{
    public class ZavodRepository : IRepository<Zavod>
    {
        protected AppDbContext _dbcontext;
        public ZavodRepository(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }


        public async Task<Zavod> Create(Zavod value)
        {
            var zavod = await _dbcontext.AddAsync(value);
            await _dbcontext.SaveChangesAsync();
            return zavod.Entity;
        }

        public async Task<List<Zavod>> GetAll()
        {
            return await _dbcontext.Zavods.ToListAsync();
        }
        public async Task<Zavod> GetById(int id)
        {
            var zavod = await _dbcontext.Zavods.FindAsync(id);
            return zavod;
        }
        public async Task<Zavod> Delete(int id)
        {
            var zavod = await _dbcontext.Zavods.FindAsync(id);
            if (zavod == null)
            {
                return null;
            }
            _dbcontext.Zavods.Remove(zavod);
            await _dbcontext.SaveChangesAsync();
            return zavod;
        }
        public async Task<Zavod> Update(int id, Zavod value)
        {
            var zavod = await _dbcontext.Zavods.FindAsync(id);
            if (zavod == null)
            {
                return null;
            }
            zavod.Surname = value.Surname;
            zavod.CechNumber = value.CechNumber;
            zavod.Position = value.Position;
            zavod.Experience = value.Experience;
            zavod.Salary = value.Salary;
            _dbcontext.Zavods.Entry(zavod).State = EntityState.Modified;
            await _dbcontext.SaveChangesAsync();
            return zavod;
        }



    }
}
