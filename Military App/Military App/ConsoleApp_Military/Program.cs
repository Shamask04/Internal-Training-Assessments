using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MilitaryApp.Data;
using MilitaryApp.Domain;

namespace ConsoleApp_Military
{
    class Program
    {
        static MilitaryContext context = new MilitaryContext();
        static void Main(string[] args)
        {
            using var context = new MilitaryContext();

            var militaries = context.Militaries.ToList();

            //InsertMultipleMilitary();
            //InsertVariousType();
            //context.Database.EnsureCreated();
            //GetMilitary("Before Add: ");
            //AddMilitary();
            //GetMilitary("After Add: ");

            //RunQueries();
            //RunAggregates();
            //UpdateMilitary();
            //EagerLoadMilitaryWithQuotes();

            //FilteringWithRelatedData();
            QueryUsingFromRawSqlProcedure();
            InterpolatedRawSqlQueryStoredProc();
            ExecuteSomeRawSql();
            
        }
        private static void GetMilitary(string text)
        {
            var militarys = context.Militaries.ToList();
            Console.WriteLine($"{text}: Military Count is {militarys.Count}");

            foreach (var military in militarys)
            {
                Console.WriteLine(military.Name);
            }
        }

        private static void AddMilitary()
        {
            var militarys = new Military { Name = "Amit" };
            context.Militaries.Add(militarys);
            context.SaveChanges();
        }

        static void InsertMultipleMilitary()
        {
            using var context = new MilitaryContext();

            var military1 = new Military { Name = "Military 1" };
            var military2 = new Military { Name = "Military 2" };
            var military3 = new Military { Name = "Military 3" };
            var military4 = new Military { Name = "Military 4" };

            context.Militaries.AddRange(
                military1,
                military2,
                military3,
                military4
            );

            context.SaveChanges();
        }

        static void InsertVariousType()
        {
            using var context = new MilitaryContext();

            //var military = new Military { Name = "Military with King" };
            //var king = new King { KingName = "King 1" };
            var military = new Military
            {
                Name = "Military with King",
                King = new King
                {
                    KingName = "King 1"
                }
            };

            //context.AddRange(military, king);
            context.Militaries.Add(military);
            Console.WriteLine(context.Entry(military.King).State);

            context.SaveChanges();
        }

        static void RunQueries()
        {
            using var context = new MilitaryContext();

            //var militaries = context.Militaries.ToList();

            //foreach (var m in militaries)
            //{
            //    Console.WriteLine($"{m.Id} - {m.Name}");
            //}

            var militaries = context.Militaries
           .Include(m => m.King)
           .ToList();

            foreach (var m in militaries)
            {
                Console.WriteLine($"{m.Name} - King: {m.King?.KingName}");
            }

             (from m in context.Militaries
              select m).ToList();

            (from m in context.Militaries
             where m.Name == "Military with King"
             select m).ToList();

            context.Militaries.Where(x => EF.Functions.Like(x.Name, "Am%")).ToList();
        }

        static void RunAggregates()
        {
            using var context = new MilitaryContext();

            int total = context.Militaries.Count();
            Console.WriteLine($"Total Militaries: {total}");

            int count = context.Militaries
                .Count(m => m.Name.StartsWith("A"));
            Console.WriteLine($"Starts with A: {count}");

            int maxId = context.Militaries.Max(m=> m.Id);
            Console.WriteLine($"Max Id: {maxId}");

            int minId = context.Militaries.Min(m => m.Id);
            Console.WriteLine($"Min Id: {minId}");

            var First = context.Militaries.FirstOrDefault();
            Console.WriteLine(First?.Name);

            var grouped = context.Militaries
                .GroupBy(m=> m.KingId)
                .Select(g=> new
                {
                    KingId = g.Key,
                    Count = g.Count()
                })
                .ToList();

            foreach(var g in grouped)
            {
                Console.WriteLine($"KingId: {g.KingId}, Count: {g.Count}");
            }

            var groupedByKing = context.Militaries
                .Include(m => m.King)
                .GroupBy(m => m.King.KingName)
                .Select(g => new
                {
                    KingName = g.Key,
                    Total = g.Count()
                })
                .ToList();

            foreach (var g in groupedByKing)
            {
                Console.WriteLine($"{g.KingName}: {g.Total}");
            }

        }

        static void UpdateMilitary()
        {
            using var context = new MilitaryContext();

            var military = context.Militaries.FirstOrDefault();

            if (military != null)
            {
                military.Name = "Updated Military Name";
                context.SaveChanges();
            }

            //var military = context.Militaries
            //    .FirstOrDefault(m => m.Name == "Amit");

            //if (military != null)
            //{
            //    military.Name = "Amit Updated";
            //    context.SaveChanges();
            //}

            //var military = context.Militaries
            //    .Include(m => m.King)
            //    .FirstOrDefault(m => m.King != null);

            //if (military?.King != null)
            //{
            //    military.King.KingName = "Updated King";
            //    context.SaveChanges();
            //}

            //var military = new Military
            //{
            //    Id = 1,
            //    Name = "Updated Name"
            //};

            //context.Militaries.Attach(military);
            //context.Entry(military).Property(m => m.Name).IsModified = true;
            //context.SaveChanges();

        }

        private static void EagerLoadMilitaryWithQuotes()
        {
            // left join 
            var militaryQuotes = context.Militaries
                .Include(s => s.Quotes).ToList();

            var militaryQuotesLeftJoin = 
                context.Militaries
                .Include(s => s.Quotes)
                .Include(s => s.King).ToList();

        }
        private static void FilteringWithRelatedData()
        {
            var military = context.Militaries
                .Where(s => s.Quotes.Any(q => q.Text.Contains("Happy")))
                .ToList();
        }

        private static void QuerySQLView()
        {
            var military = context.viewMilitary.FirstOrDefault();
        }

        private static void QueryUsingRawSql()
        {
            var military = context.Militaries
            .FromSqlRaw("select Name from military").ToList();

        }

        private static void QueryUsingRawSqlWithInterpolation()
        {
            string name = "amit";
            var military = context.Militaries
                .FromSqlInterpolated($"select Name from military where name { name}").ToList(); 
        }
        private static void QueryUsingFromRawSqlProcedure()
        {
            int militaryId = 1;

            var military = context.Militaries
                .FromSqlRaw("EXEC dbo.uspGetMilitary @militaryId",
                    new SqlParameter("@militaryId", militaryId))
                .ToList();
        }

        // Recommended 
        private static void InterpolatedRawSqlQueryStoredProc()
        {
            string name = "amit";

            var military = context.Militaries
                .FromSqlInterpolated($"SELECT Id, Name, KingId FROM Militaries WHERE Name = {name}")
                .ToList();
        }

        private static void ExecuteSomeRawSql()
        {
            int militaryId = 1;

            context.Database
                .ExecuteSqlInterpolated($"EXEC dbo.DeleteMilitary {militaryId}");
        }
    }
}
