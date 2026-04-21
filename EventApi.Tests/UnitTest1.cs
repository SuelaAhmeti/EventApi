using System;
using System.Globalization;
using System.IO;
using EventApi.Data;
using EventApi.Models;
using EventApi.Services;
using Xunit;

public class UnitTest1
{
    private static string CreateIsolatedWorkingDir()
    {
        var dir = Path.Combine(Path.GetTempPath(), "EventApi.Tests", Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(dir);
        return dir;
    }

    private static void WriteEventsCsv(string dir, params Event[] events)
    {
        var path = Path.Combine(dir, "events.csv");
        var lines = new string[events.Length];

        for (var i = 0; i < events.Length; i++)
        {
            var e = events[i];
            lines[i] = string.Join(",",
                e.Id.ToString(CultureInfo.InvariantCulture),
                e.Title,
                e.Date.ToString("o", CultureInfo.InvariantCulture),
                e.Price.ToString(CultureInfo.InvariantCulture),
                e.CategoryId.ToString(CultureInfo.InvariantCulture),
                e.OrganizerId.ToString(CultureInfo.InvariantCulture)
            );
        }

        File.WriteAllLines(path, lines);
    }

    [Fact]
    public void CreateEvent_EmptyTitle_ReturnsError()
    {
        var dir = CreateIsolatedWorkingDir();
        var oldCwd = Directory.GetCurrentDirectory();
        Directory.SetCurrentDirectory(dir);

        try
        {
            var repo = new FileRepository();
            var service = new EventService(repo);

            var (success, _) = service.CreateEvent(new Event
            {
                Id = 1,
                Title = "   ",
                Date = DateTime.UtcNow,
                Price = 10,
                CategoryId = 1,
                OrganizerId = 1
            });

            Assert.False(success);
        }
        finally
        {
            Directory.SetCurrentDirectory(oldCwd);
        }
    }

    [Fact]
    public void UpdateEvent_InvalidId_ReturnsError()
    {
        var dir = CreateIsolatedWorkingDir();
        var oldCwd = Directory.GetCurrentDirectory();
        Directory.SetCurrentDirectory(dir);

        try
        {
            var repo = new FileRepository();
            var service = new EventService(repo);

            var (success, message) = service.UpdateEvent(new Event
            {
                Id = 0,
                Title = "OK",
                Date = DateTime.UtcNow,
                Price = 10,
                CategoryId = 1,
                OrganizerId = 1
            });

            Assert.False(success);
            Assert.Equal("ID nuk është valid", message);
        }
        finally
        {
            Directory.SetCurrentDirectory(oldCwd);
        }
    }

    [Fact]
    public void GetPriceStats_EmptyList_ReturnsZeroCount()
    {
        var dir = CreateIsolatedWorkingDir();
        var oldCwd = Directory.GetCurrentDirectory();
        Directory.SetCurrentDirectory(dir);

        try
        {
            var repo = new FileRepository();
            var service = new EventService(repo);

            var stats = service.GetPriceStats();

            Assert.Equal(0, stats.Count);
            Assert.Equal(0, stats.TotalPrice);
            Assert.Equal(0, stats.AveragePrice);
            Assert.Equal(0, stats.MinPrice);
            Assert.Equal(0, stats.MaxPrice);
        }
        finally
        {
            Directory.SetCurrentDirectory(oldCwd);
        }
    }

    [Fact]
    public void Sort_ByPriceAsc_ReturnsCorrectOrder()
    {
        var dir = CreateIsolatedWorkingDir();
        var oldCwd = Directory.GetCurrentDirectory();
        Directory.SetCurrentDirectory(dir);

        try
        {
            WriteEventsCsv(dir,
                new Event { Id = 1, Title = "A", Date = DateTime.UtcNow.AddDays(1), Price = 50, CategoryId = 1, OrganizerId = 1 },
                new Event { Id = 2, Title = "B", Date = DateTime.UtcNow.AddDays(2), Price = 10, CategoryId = 1, OrganizerId = 1 },
                new Event { Id = 3, Title = "C", Date = DateTime.UtcNow.AddDays(3), Price = 30, CategoryId = 1, OrganizerId = 1 }
            );

            var repo = new FileRepository();
            var service = new EventService(repo);

            var sorted = service.Sort("price", "asc");

            Assert.Equal(new[] { 2, 3, 1 }, sorted.ConvertAll(e => e.Id));
        }
        finally
        {
            Directory.SetCurrentDirectory(oldCwd);
        }
    }
}
