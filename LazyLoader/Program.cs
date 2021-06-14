using System;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace LazyLoader
{
    class Program
    {
        private static readonly string dataFilename = "LazyLoader.db";

        static private void DumpEntities(ParticipantList plist)
        {
            Console.WriteLine($"-------\nParticipantList '{plist.Name}' Id={plist.Id}");
            foreach (var email in plist.Emails)
            {
                Console.WriteLine($"\t{email.Id}: {email.EmailAddress}");
            }
            Console.WriteLine("-------\n");
        }
        
        static void Main(string[] args)
        {
            Console.WriteLine("Why doesn't lazy loading work?");

            System.IO.File.Delete(dataFilename);
            var options = new DbContextOptionsBuilder<Context>()
                .UseSqlite($"Data Source={dataFilename};")
                .Options;
            var db = new Context(options);
            db.Database.EnsureCreated();

            var plist = new ParticipantList()
            {
                Name = "I'm a P List"
            };
            var email1 = new Email()
            {
                EmailAddress = "123@gmail.com",
                ParticipantList = plist,
            };
            var email2 = new Email()
            {
                EmailAddress = "456@gmail.com",
                ParticipantList = plist,
            };
            db.ParticipantLists.Add(plist);
            db.Emails.Add(email1);
            db.Emails.Add(email2);
            db.SaveChanges();

            Console.WriteLine($"Before Clearing LazyLoadingEnabled={db.ChangeTracker.LazyLoadingEnabled}:");
            DumpEntities(plist);

            Console.WriteLine($"Clearing Change Tracker");
            Console.WriteLine($"via LazyLoading LazyLoadingEnabled={db.ChangeTracker.LazyLoadingEnabled}");
            db.ChangeTracker.Clear();
            var persisted_plist = db.ParticipantLists.Find(plist.Id);
            DumpEntities(persisted_plist);

            var emails = db.Emails.Where(u => u.ParticipantListId == plist.Id);
            Console.WriteLine($"After Also selecting emails from DB LazyLoadingEnabled={db.ChangeTracker.LazyLoadingEnabled}");
            DumpEntities(persisted_plist);

            Console.WriteLine($"Clearing Change Tracker");
            Console.WriteLine($"via Include LazyLoadingEnabled={db.ChangeTracker.LazyLoadingEnabled}");
            db.ChangeTracker.Clear();
            persisted_plist = db.ParticipantLists.Where(u => u.Id == plist.Id).Include(pl => pl.Emails).First();
            DumpEntities(persisted_plist);

        }
    }
}