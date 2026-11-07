using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Data
{
    public static class SeedData
    {
        public static void Generate(ApplicationDbContext context)
        {
            if (context.Users.Any())
            {
                return;
            }

            using var transaction = context.Database.BeginTransaction();

            var firstNames = new[]
            {
                "Mari", "Jaan", "Kati", "Marten", "Liis", "Karl", "Anna",
                "Rasmus", "Laura", "Markus", "Eeva", "Sander", "Grete",
                "Andres", "Kadri", "Martin", "Helen", "Kristjan", "Piret",
                "Tanel", "Birgit", "Oliver", "Merle", "Siim", "Anneli",
                "Taavi", "Karin", "Peeter", "Diana", "Erik", "Triin",
                "Aivar", "Kaisa", "Robert", "Monika"
            };

            var lastNames = new[]
            {
                "Tamm", "Saar", "Sepp", "Mägi", "Kask", "Kukk", "Ilves",
                "Rebane", "Pärn", "Lepp", "Karu", "Oja", "Aas", "Kuusk",
                "Põder", "Raud", "Kivi", "Teder", "Lind", "Koppel",
                "Kallas", "Vaher", "Paju", "Mets", "Järv", "Kangur",
                "Aru", "Õun", "Koppel", "Sild", "Nurm", "Põld",
                "Koit", "Roos", "Uibo"
            };

            var users = new List<User>();

            for (var i = 1; i <= 35; i++)
            {
                users.Add(new User
                {
                    user_name = $"kasutaja{i:00}",
                    first_name = firstNames[i - 1],
                    last_name = lastNames[i - 1],
                    email = $"kasutaja{i:00}@example.com",
                    password_hash = "demo-seed-data-not-for-login",
                    registered_at = DateTime.UtcNow.AddDays(-i * 12),
                    role = i == 1 ? "admin" : "user"
                });
            }

            context.Users.AddRange(users);

            var carTypeNames = new[]
            {
                "Väikeauto", "Kompaktklass", "Sedaan", "Universaal",
                "Maastur", "Elektriauto", "Kaubik"
            };

            var carTypes = new List<CarType>();

            for (var i = 1; i <= 35; i++)
            {
                var typeName = carTypeNames[(i - 1) % carTypeNames.Length];

                carTypes.Add(new CarType
                {
                    name = $"{typeName} ({i})",
                    description = $"Rendiauto kategooria: {typeName.ToLowerInvariant()}."
                });
            }

            context.CarTypes.AddRange(carTypes);
            context.SaveChanges(); // Siin saavad kasutajad ja autotüübid ID-d.

            var makesAndModels = new[]
            {
                ("Toyota", "Corolla"),
                ("Škoda", "Octavia"),
                ("Volkswagen", "Golf"),
                ("Toyota", "RAV4"),
                ("Tesla", "Model 3"),
                ("Ford", "Focus"),
                ("Volvo", "V60"),
                ("Kia", "Ceed"),
                ("Hyundai", "Tucson"),
                ("Škoda", "Superb")
            };

            var cars = new List<Car>();
            for (var i = 1; i <= 35; i++)
            {
                var car = makesAndModels[(i - 1) % makesAndModels.Length];

                var plateNumber = 100 + i;
                var lettersIndex = i + 27;
                var letters =
                    $"{(char)('A' + lettersIndex / 676)}" +
                    $"{(char)('A' + (lettersIndex / 26) % 26)}" +
                    $"{(char)('A' + lettersIndex % 26)}";

                cars.Add(new Car
                {
                    registration_no = $"{plateNumber:000} {letters}",
                    make = car.Item1,
                    model = car.Item2,
                    car_type_id = carTypes[(i - 1) % carTypes.Count].car_type_id,
                    time_rate = 25m + (i % 6) * 5m,
                    kilometer_rate = 0.15m + (i % 4) * 0.05m,
                    status = i % 5 == 0 ? "rented" : "available"
                });
            }

            context.Cars.AddRange(cars);
            context.SaveChanges(); // Siin saavad autod ID-d.

            var bookings = new List<Booking>();

            for (var i = 1; i <= 35; i++)
            {
                var start = DateTime.UtcNow.Date
                    .AddDays(-i * 2)
                    .AddHours(9);

                var end = start.Date
                    .AddDays(2 + (i % 5))
                    .AddHours(18);

                bookings.Add(new Booking
                {
                    user_id = users[i - 1].user_id,
                    car_id = cars[i - 1].car_id,
                    start_time = start,
                    end_time = end,
                    starting_odometer = 12000m + i * 850m,
                    ending_odometer = 12300m + i * 850m,
                    time_rate_at_booking = 25m + (i % 6) * 5m,
                    kilometer_rate_at_booking = 0.15m + (i % 4) * 0.05m,
                    status = i % 4 == 0 ? "completed" : "active"
                });
            }

            context.Bookings.AddRange(bookings);
            context.SaveChanges(); // Siin saavad broneeringud ID-d.

            var invoices = new List<Invoice>();

            for (var i = 1; i <= 35; i++)
            {
                invoices.Add(new Invoice
                {
                    booking_id = bookings[i - 1].booking_id,
                    user_id = users[i - 1].user_id,
                    created_at = DateTime.UtcNow.AddDays(-i),
                    total_amount = 100m + i * 17.50m,
                    payment_status = i % 3 == 0 ? "paid" : "unpaid",
                    marked_paid_by_user_id = i % 3 == 0
                        ? users[0].user_id
                        : 0,
                    marked_paid_at = i % 3 == 0
                        ? DateTime.UtcNow.AddDays(-i + 1)
                        : DateTime.MinValue
                });
            }

            context.Invoices.AddRange(invoices);
            context.SaveChanges(); // Siin saavad arved ID-d.

            var invoiceLines = new List<InvoiceLine>();

            for (var i = 1; i <= 35; i++)
            {
                var quantity = 3m + (i % 5);
                var unitPrice = 25m + (i % 6) * 5m;

                invoiceLines.Add(new InvoiceLine
                {
                    invoice_id = invoices[i - 1].invoice_id,
                    description = $"Autorent, broneering {i}",
                    quantity = quantity,
                    unit_price = unitPrice,
                    amount = quantity * unitPrice
                });
            }

            context.InvoiceLines.AddRange(invoiceLines);
            context.SaveChanges();

            transaction.Commit();
        }
    }
}