using Discount.API.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;
using System;
using System.Threading;

namespace Discount.API.Extentions
{
    public static class HostExtentions
    {
        public static IHost MigrateDatabase<TContext>(this IHost host, int? retry = 0)
        {
            int retryForAvailability = retry.Value;

            using IServiceScope scope = host.Services.CreateScope();
            IServiceProvider service = scope.ServiceProvider;
            IConfiguration configuration = service.GetRequiredService<IConfiguration>();
            ILogger logger = service.GetRequiredService<ILogger<TContext>>();
            DiscountContext context = service.GetRequiredService<DiscountContext>();

            try
            {
                logger.LogInformation("Migrating postresql database.");

                using NpgsqlConnection connection = new(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
                connection.Open();

                using NpgsqlCommand command = new()
                {
                    Connection = connection
                };

                command.CommandText = "DROP TABLE IF EXISTS coupon";
                command.ExecuteNonQuery();

                command.CommandText = @"CREATE TABLE coupon(Id Serial PRIMARY KEY,
                                                                ProductName VARCHAR(24) NOT NULL,
                                                                Description TEXT,
                                                                Amount INT)";
                command.ExecuteNonQuery();

                context.Coupon.Add(new()
                {
                    ProductName = "IPhone X",
                    Description = "IPhone Discount",
                    Amount = 150
                });

                context.Coupon.Add(new()
                {
                    ProductName = "Samsung 10",
                    Description = "Samsung Discount",
                    Amount = 100
                });

                context.SaveChanges();

                logger.LogInformation("Migrated postresql database.");
            }
            catch (NpgsqlException ex)
            {
                logger.LogError(ex, "An error occured while migrating the postresql database");
                if (retryForAvailability < 50)
                {
                    retryForAvailability++;
                    Thread.Sleep(2000);
                    MigrateDatabase<TContext>(host, retryForAvailability);
                }
            }

            return host;
        }
    }
}
