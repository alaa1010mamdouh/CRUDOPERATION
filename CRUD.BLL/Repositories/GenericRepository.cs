using CRUD.BLL.Interfaces;
using CRUD.DAL.Context;
using CRUD.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly CRUDDbContext _context;

        public GenericRepository(CRUDDbContext Context)
        {
            _context = Context;
        }
        public async Task Add(T item)
        {
         await   _context.AddAsync(item);
            
        }

        public void Delete(T item)
        {
           _context.Remove(item);
           
        }

        public async Task <IEnumerable<T>> GetAll()
        {
            if(typeof(T) == typeof(Employee))
            {
              return (IEnumerable<T>) await _context.employees.Include(e=>e.Department).ToListAsync();
            }
        return await _context.Set<T>().ToListAsync();

        }

        public async Task <T> GetById(int id)
            =>await _context.Set<T>().FindAsync(id);
        

        public void Update(T item)
        {
          _context.Update(item);
   
        }
    }
}
