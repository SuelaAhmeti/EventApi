using EventApi.Data;
using EventApi.Models;
using System.Collections.Generic;
using System.Linq;
 

namespace EventApi.Services
{
    public class EventService
    {
        private IRepository<Event> _repo;

        public EventService(IRepository<Event> repo)
        {
            _repo = repo;
        }

        // CREATE EVENT
        public void CreateEvent(Event ev)
       {
            if (string.IsNullOrWhiteSpace(ev.Title))
            {
             Console.WriteLine("Title cannot be empty!");
             return;
            }

            if (ev.Price <= 0)
            {
             Console.WriteLine("Price must be greater than 0!");
             return;
            }

            _repo.Add(ev);
       }

        // GET ALL EVENTS
        public List<Event> GetAll()
        {
            return _repo.GetAll();
        }

        // FILTER BY CATEGORY
        public List<Event> GetByCategory(int categoryId)
        {
            return _repo.GetAll()
                        .Where(e => e.CategoryId == categoryId)
                        .ToList();
        }
    
        public Event GetById(int id)
         {
            return _repo.GetById(id);
         }
        
        public void DeleteEvent(int id)
         {
           ((FileRepository)_repo).Delete(id);
         }

        public void UpdateEvent(Event ev)
         {
           ((FileRepository)_repo).Update(ev);
         }
    }

}

