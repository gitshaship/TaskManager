using Backend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Backend.Services
{
    public interface ITaskService
    {
        public Task<IEnumerable<DBtask>> GetTasks(int? status);
        public Task<DBtask> UpdateTask(int id, DBtask dBtask);
        public Task<DBtask> GetTask(int id);
        public Task<DBtask> DeleteTask(int id);

        public Task<DBtask> PostTask(DBtask dBtask);
        public bool taskExistsByName(string name);



    }
    public class TaskService : ITaskService
    {
        private readonly Taskdbcontext _context;

        public TaskService(Taskdbcontext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<DBtask>> GetTasks(int? status) {
            if (status.HasValue && status == 1)
            {
                return await _context.tasks.Where(x => x.status == 1).ToListAsync();
            }
            else if (status == 0)
            {
                return await _context.tasks.Where(x => x.status == 0).ToListAsync();
            }
            else
            {
                return await _context.tasks.ToListAsync();
            }
        }

        public async Task<DBtask> UpdateTask(int id, DBtask dBtask)
        {
            dBtask.id = id;
            _context.Entry(dBtask).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (DBtaskExists(id))
                {
                    throw;
                }
            }

            return await GetTask(id);
        }

        public async Task<DBtask> DeleteTask(int id)
        {
            var task = await GetTask(id);
            _context.tasks.Remove(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<DBtask> PostTask(DBtask task)
        {
            _context.tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        private bool DBtaskExists(int id)
        {
            return _context.tasks.Any(e => e.id == id);
        }

        public bool taskExistsByName(string name)
        {
            return _context.tasks.Any(e => e.taskName.Equals(name));
        }

        public async Task<DBtask> GetTask(int id)
        {
           return  await _context.tasks.FindAsync(id);
        }



    }
}
