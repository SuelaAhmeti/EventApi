using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using EventApi.Models;

namespace EventApi.Data
{
    public class FileRepository : IRepository<Event>
    {
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "events.csv");

        public List<Event> GetAll()
        {
            if (!File.Exists(filePath))
                return new List<Event>();

            var lines = File.ReadAllLines(filePath);

            return lines.Select(line =>
            {
                var parts = line.Split(',');
                return new Event
                {
                    Id = int.Parse(parts[0]),
                    Title = parts[1],
                    Date = DateTime.Parse(parts[2]),
                    Price = double.Parse(parts[3]),
                    CategoryId = int.Parse(parts[4]),
                    OrganizerId = int.Parse(parts[5])
                };
            }).ToList();
        }

        public Event GetById(int id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return GetAll().FirstOrDefault(e => e.Id == id);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public void Add(Event entity)
        {
            var events = GetAll();
            events.Add(entity);

            SaveAll(events);
        }

        public void Save()
        {
            // nuk përdoret direkt
        }

        private void SaveAll(List<Event> events)
        {
            var lines = events.Select(e =>
                $"{e.Id},{e.Title},{e.Date},{e.Price},{e.CategoryId},{e.OrganizerId}");

            File.WriteAllLines(filePath, lines);
        }
        public void Delete(int id)
         { 
             var events = GetAll();
             events = events.Where(e => e.Id != id).ToList();
             SaveAll(events);
         }

        public void Update(Event updated)
         {
             var events = GetAll();
             var index = events.FindIndex(e => e.Id == updated.Id);

             if (index != -1)
               {
                 events[index] = updated;
                SaveAll(events);
               }
         }
    }
}
 