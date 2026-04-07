using EventApi.Data;
using EventApi.Models;
using System;
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
        public (bool Success, string Message) CreateEvent(Event? ev)
        {
            try
            {
                if (ev == null)
                    return (false, "Eventi nuk mund të jetë null");

                if (string.IsNullOrWhiteSpace(ev.Title))
                    return (false, "Titulli nuk mund të jetë bosh");

                if (ev.Price <= 0)
                    return (false, "Çmimi duhet të jetë më i madh se 0");

                _repo.Add(ev);
                return (true, "Eventi u krijua me sukses");
            }
            catch
            {
                return (false, "Gabim gjatë krijimit të eventit.");
            }
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
    
        public Event? GetById(int id)
        {
            try
            {
                return _repo.GetById(id);
            }
            catch
            {
                return null;
            }
        }
        
        public (bool Success, string Message) DeleteEvent(int id)
        {
            try
            {
                var fileRepo = _repo as FileRepository;
                if (fileRepo == null)
                    return (false, "Repository nuk suporton këtë operacion.");

                var existing = _repo.GetById(id);
                if (existing == null)
                    return (false, "Itemi nuk u gjet");

                fileRepo.Delete(id);
                return (true, "Eventi u fshi");
            }
            catch
            {
                return (false, "Gabim gjatë fshirjes së eventit.");
            }
        }

        public (bool Success, string Message) UpdateEvent(Event? ev)
        {
            try
            {
                if (ev == null)
                    return (false, "Eventi nuk mund të jetë null");

                var fileRepo = _repo as FileRepository;
                if (fileRepo == null)
                    return (false, "Repository nuk suporton këtë operacion.");

                var existing = _repo.GetById(ev.Id);
                if (existing == null)
                    return (false, "Itemi nuk u gjet");

                fileRepo.Update(ev);
                return (true, "Eventi u përditësua");
            }
            catch
            {
                return (false, "Gabim gjatë përditësimit të eventit.");
            }
        }

        // 📊 STATS (FEATURE E RE)
        public EventStats GetPriceStats()
        {
            try
            {
                var events = _repo.GetAll();
                if (events.Count == 0)
                {
                    return new EventStats
                    {
                        Count = 0,
                        TotalPrice = 0,
                        AveragePrice = 0,
                        MinPrice = 0,
                        MaxPrice = 0
                    };
                }

                return new EventStats
                {
                    Count = events.Count,
                    TotalPrice = events.Sum(e => e.Price),
                    AveragePrice = events.Average(e => e.Price),
                    MinPrice = events.Min(e => e.Price),
                    MaxPrice = events.Max(e => e.Price)
                };
            }
            catch
            {
                return new EventStats
                {
                    Count = 0,
                    TotalPrice = 0,
                    AveragePrice = 0,
                    MinPrice = 0,
                    MaxPrice = 0
                };
            }
        }

        // ↕️ SORT (FEATURE E RE)
        public List<Event> Sort(string? by, string? dir)
        {
            try
            {
                var events = _repo.GetAll();
                var descending = string.Equals(dir, "desc", StringComparison.OrdinalIgnoreCase);

                return (by ?? string.Empty).ToLower() switch
                {
                    "title" => descending ? events.OrderByDescending(e => e.Title).ToList() : events.OrderBy(e => e.Title).ToList(),
                    "date" => descending ? events.OrderByDescending(e => e.Date).ToList() : events.OrderBy(e => e.Date).ToList(),
                    "price" => descending ? events.OrderByDescending(e => e.Price).ToList() : events.OrderBy(e => e.Price).ToList(),
                    _ => descending ? events.OrderByDescending(e => e.Id).ToList() : events.OrderBy(e => e.Id).ToList()
                };
            }
            catch
            {
                return new List<Event>();
            }
        }
    }

}

