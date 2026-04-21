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
            try
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine("File nuk u gjet, po krijoj file të ri...");
                    File.WriteAllText(filePath, string.Empty);
                    return new List<Event>();
                }

                var lines = File.ReadAllLines(filePath);
                var result = new List<Event>();

                foreach (var line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    var parts = line.Split(',');
                    if (parts.Length < 6)
                        continue;

                    if (!int.TryParse(parts[0], out var id))
                        continue;

                    var title = parts[1];
                    if (string.IsNullOrWhiteSpace(title))
                        continue;

                    if (!DateTime.TryParse(parts[2], out var date))
                        continue;

                    if (!double.TryParse(parts[3], out var price))
                        continue;

                    if (!int.TryParse(parts[4], out var categoryId))
                        continue;

                    if (!int.TryParse(parts[5], out var organizerId))
                        continue;

                    result.Add(new Event
                    {
                        Id = id,
                        Title = title,
                        Date = date,
                        Price = price,
                        CategoryId = categoryId,
                        OrganizerId = organizerId
                    });
                }

                return result;
            }
            catch
            {
                Console.WriteLine("Gabim gjatë leximit të file-it.");
                return new List<Event>();
            }
        }

        public Event? GetById(int id)
        {
            return GetAll().FirstOrDefault(e => e.Id == id);
        }

        public void Add(Event entity)
        {
            try
            {
                var events = GetAll();
                events.Add(entity);
                SaveAll(events);
            }
            catch
            {
                Console.WriteLine("Gabim gjatë ruajtjes së eventit.");
            }
        }

        public void Save()
        {
            // nuk përdoret direkt
        }

        private void SaveAll(List<Event> events)
        {
            try
            {
                var lines = events.Select(e =>
                    $"{e.Id},{e.Title},{e.Date:o},{e.Price},{e.CategoryId},{e.OrganizerId}");

                File.WriteAllLines(filePath, lines);
            }
            catch
            {
                Console.WriteLine("Gabim gjatë shkrimit në file.");
            }
        }

        public void Delete(int id)
        {
            try
            {
                var events = GetAll();
                events = events.Where(e => e.Id != id).ToList();
                SaveAll(events);
            }
            catch
            {
                Console.WriteLine("Gabim gjatë fshirjes në file.");
            }
        }

        public void Update(Event updated)
        {
            try
            {
                var events = GetAll();
                var index = events.FindIndex(e => e.Id == updated.Id);

                if (index != -1)
                {
                    events[index] = updated;
                    SaveAll(events);
                }
            }
            catch
            {
                Console.WriteLine("Gabim gjatë update në file.");
            }
        }
    }
}
 