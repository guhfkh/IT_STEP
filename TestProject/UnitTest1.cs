using Microsoft.EntityFrameworkCore;

namespace IT_STEP.Tests
{
    [TestFixture]
    public class CrudTests
    {
        private MoviesContext db;

        [SetUp]
        public void Setup()
        {
            db = new MoviesContext();
        }

        [TearDown]
        public void TearDown()
        {
            db.Dispose();
        }

        // ================= USER CRUD =================

        [Test, Order(1)]
        public void AddUser_Test()
        {
            var user = new User
            {
                Username = "TestUser",
                Email = "test@test.com",
                Password = "1234"
            };

            db.Users.Add(user);
            db.SaveChanges();

            var result = db.Users.AsNoTracking()
                .FirstOrDefault(u => u.Username == "TestUser");

            Assert.That(result, Is.Not.Null, "User should be added.");
        }

        [Test, Order(2)]
        public void ReadUser_Test()
        {
            var user = db.Users.AsNoTracking()
                .FirstOrDefault(u => u.Username == "TestUser");

            Assert.That(user, Is.Not.Null, "User should exist.");
        }

        [Test, Order(3)]
        public void UpdateUser_Test()
        {
            var user = db.Users.FirstOrDefault(u => u.Username == "TestUser");
            Assert.That(user, Is.Not.Null, "User must exist for update.");

            user.Email = "updated@test.com";
            db.SaveChanges();

            var updated = db.Users.AsNoTracking()
                .FirstOrDefault(u => u.Username == "TestUser");

            Assert.That(updated.Email, Is.EqualTo("updated@test.com"),
                "User email should be updated.");
        }

        [Test, Order(4)]
        public void DeleteUser_Test()
        {
            var user = db.Users.FirstOrDefault(u => u.Username == "TestUser");
            Assert.That(user, Is.Not.Null, "User must exist for deletion.");

            db.Users.Remove(user);
            db.SaveChanges();

            var deleted = db.Users.AsNoTracking()
                .FirstOrDefault(u => u.Username == "TestUser");

            Assert.That(deleted, Is.Null, "User should be deleted.");
        }

        // ================= MOVIE CRUD =================

        [Test, Order(5)]
        public void AddMovie_Test()
        {
            var user = db.Users.FirstOrDefault();

            if (user == null)
            {
                user = new User
                {
                    Username = "TempUser",
                    Email = "temp@test.com",
                    Password = "123"
                };

                db.Users.Add(user);
                db.SaveChanges();
            }

            var movie = new Movie
            {
                Title = "TestMovie",
                Year = 2024,
                UserId = user.Id
            };

            db.Movies.Add(movie);
            db.SaveChanges();

            var result = db.Movies.AsNoTracking()
                .FirstOrDefault(m => m.Title == "TestMovie");

            Assert.That(result, Is.Not.Null, "Movie should be added.");
        }

        [Test, Order(6)]
        public void ReadMovie_Test()
        {
            var movie = db.Movies.AsNoTracking()
                .FirstOrDefault(m => m.Title == "TestMovie");

            Assert.That(movie, Is.Not.Null, "Movie should exist.");
        }

        [Test, Order(7)]
        public void UpdateMovie_Test()
        {
            var movie = db.Movies.FirstOrDefault(m => m.Title == "TestMovie");
            Assert.That(movie, Is.Not.Null, "Movie must exist for update.");

            movie.Title = "UpdatedMovie";
            db.SaveChanges();

            var updated = db.Movies.AsNoTracking()
                .FirstOrDefault(m => m.Id == movie.Id);

            Assert.That(updated.Title, Is.EqualTo("UpdatedMovie"),
                "Movie title should be updated.");
        }

        [Test, Order(8)]
        public void DeleteMovie_Test()
        {
            var movie = db.Movies.FirstOrDefault(m => m.Title == "UpdatedMovie");
            Assert.That(movie, Is.Not.Null, "Movie must exist for deletion.");

            db.Movies.Remove(movie);
            db.SaveChanges();

            var deleted = db.Movies.AsNoTracking()
                .FirstOrDefault(m => m.Id == movie.Id);

            Assert.That(deleted, Is.Null, "Movie should be deleted.");
        }
    }
}
