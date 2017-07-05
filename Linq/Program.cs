using System;
using System.Collections.Generic;
using System.Linq;

namespace Linq
{
    class Program
    {
        static void Main(string[] args)
        {
            var customers = new[] {
                                new { CustomerID = 1, FirstName = "Kim", LastName = "Abercrombie",
                                CompanyName = "Alpine Ski House" },
                                new { CustomerID = 2, FirstName = "Jeff", LastName = "Hay",
                                CompanyName = "Coho Winery" },
                                new { CustomerID = 3, FirstName = "Charlie", LastName = "Herb",
                                CompanyName = "Alpine Ski House" },
                                new { CustomerID = 4, FirstName = "Chris", LastName = "Preston",
                                CompanyName = "Trey Research" },
                                new { CustomerID = 5, FirstName = "Dave", LastName = "Barnett",
                                CompanyName = "Wingtip Toys" },
                                new { CustomerID = 6, FirstName = "Ann", LastName = "Beebe",
                                CompanyName = "Coho Winery" },
                                new { CustomerID = 7, FirstName = "John", LastName = "Kane",
                                CompanyName = "Wingtip Toys" },
                                new { CustomerID = 8, FirstName = "David", LastName = "Simpson",
                                CompanyName = "Trey Research" },
                                new { CustomerID = 9, FirstName = "Greg", LastName = "Chapman",
                                CompanyName = "Wingtip Toys" },
                                new { CustomerID = 10, FirstName = "Tim", LastName = "Litton",
                                CompanyName = "Wide World Importers 2" }
                            };

            var addresses = new[] {
                                new { CompanyName = "Alpine Ski House", City = "Berne",
                                Country = "Switzerland"},
                                new { CompanyName = "Coho Winery", City = "San Francisco",
                                Country = "United States"},
                                new { CompanyName = "Trey Research", City = "New York",
                                Country = "United States"},
                                new { CompanyName = "Wingtip Toys", City = "London",
                                Country = "United Kingdom"},
                                new { CompanyName = "Wide World Importers", City = "Tetbury",
                                Country = "United Kingdom"}
                            };

            IEnumerable<string> usCompanies = addresses.Where(addr => String.Equals(addr.Country, "United States"))
                                                        .Select(usComp => usComp.CompanyName);

            foreach (string name in usCompanies)
            {
                Console.WriteLine(name);
            }

            IEnumerable<string> companyNames = addresses.OrderBy(addr => addr.CompanyName)
                                                        .Select(comp => comp.CompanyName);

            foreach (string name in companyNames)
            {
                Console.WriteLine(name);
            }

            var companiesGroupedByCountry = addresses.GroupBy(addrs => addrs.Country);
            foreach (var companiesPerCountry in companiesGroupedByCountry)
            {
                Console.WriteLine(
                $"Country: {companiesPerCountry.Key}\t{companiesPerCountry.Count()} companies");
                foreach (var companies in companiesPerCountry)
                {
                    Console.WriteLine($"\t{companies.CompanyName}");
                }
            }

            var countriesAndCustomers = from a in addresses
                                        join c in customers
                                        on a.CompanyName equals c.CompanyName
                                        select new { c.FirstName, c.LastName, a.Country };

            Console.WriteLine("\njointure : tous les compagnies dans la liste d'adresses qui sont aussi des compagnies dans la liste de client :");
            foreach (var item in countriesAndCustomers)
            {
                Console.WriteLine($"first name: {item.FirstName} – last name: {item.LastName} – country: {item.Country}");
            }
        }
    }
}
