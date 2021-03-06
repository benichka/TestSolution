﻿using System;
using System.Collections;
using System.Globalization;

namespace DateManipulation
{
    class Program
    {
        static void Main(string[] args)
        {
            // Current date as the system "sees" it
            DateTime dateSystem = DateTime.Now;

            // Multiple strings representing the system date in different culture
            var dateEN = dateSystem.ToString("d", CultureInfo.CreateSpecificCulture("en-US"));
            var dateDE = dateSystem.ToString("d", CultureInfo.CreateSpecificCulture("de-DE"));
            var dateDEWithSlashes = dateSystem.ToString("yyyy/MM/dd", CultureInfo.CreateSpecificCulture("de-DE"));

            Console.WriteLine($"english date: {dateEN}");
            Console.WriteLine($"german date : {dateDE}");
            // The "/" is not a literal string: it represents the date separator. So, for a german date, this separator
            // is a dot : that's why the output doesn't show a "/" but a "."
            Console.WriteLine($"german date, with '/' separator in the toString : {dateDEWithSlashes}");

            // Dates as string in different cultures
            var dateFRAsString = "31/12/2017";
            var dateDEAsString = "31.12.2017";
            var dateENAsString = "12/31/2017";

            // Date parsing and the utility of the culture
            var dateFRAsDate = DateTime.ParseExact(dateFRAsString, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            //var dateDEAsDate = DateTime.ParseExact(dateDEAsString, "dd/MM/yyyy", CultureInfo.InvariantCulture); // => Error!
            //var dateDEAsDate2 = DateTime.ParseExact(dateDEAsString, "d", CultureInfo.InvariantCulture); // => Error !
            // For the invariant culture, the format string must be exact (for the invariant culture, the date separator is "/").
            var dateDEAsDate = DateTime.ParseExact(dateDEAsString, "dd.MM.yyyy", CultureInfo.InvariantCulture);

            // this time it's OK because we specifically told the program to handle the input date as a german one. the "/" is
            // considered as a "neutral" delimiter. Actually, it's the only date separator in c#.
            var dateDEAsDateWithDECulture = DateTime.ParseExact(dateDEAsString, "dd/MM/yyyy", CultureInfo.CreateSpecificCulture("de-DE"));
            var dateDEAsDateWithDECulture2 = DateTime.ParseExact(dateDEAsString, "d", CultureInfo.CreateSpecificCulture("de-DE"));

            // For the output: it doesn't matter as both object has been parsed as date; they're now date objects and their string representation
            // are exactly the same
            var invariantToString = dateDEAsDate.ToString();
            var specificToString = dateDEAsDateWithDECulture.ToString();

            // The same goes for en english date
            var dateENAsDate = DateTime.ParseExact(dateENAsString, "MM/dd/yyyy", CultureInfo.InvariantCulture);

            // Another example
            CultureInfo us = new CultureInfo("en-US");
            CultureInfo sa = new CultureInfo("ar-SA"); // Culture for Saudi Arabia

            string text = "1434-09-23T15:16";
            string format = "yyyy'-'MM'-'dd'T'HH':'mm";

            // Invariant culture: parses as an english date.
            // The output is 23 september 1434 if you run this on a environnement with occidental culture
            // (US, Europe...) because the WriteLine invoke the ToString method of DateTime, which default
            // to the current culture
            Console.WriteLine(DateTime.ParseExact(text, format, CultureInfo.InvariantCulture)); // 23 september 1434
            // same here, because it's in US culture
            Console.WriteLine(DateTime.ParseExact(text, format, us)); // 23 september 1434
            // Here, the calendar is not the same! This date will display as 31 july 2013,
            // in "European" culture!
            Console.WriteLine(DateTime.ParseExact(text, format, sa));
            // if we print it in the ar-SA culture however, the result is 23 september 1434
            Console.WriteLine(DateTime.ParseExact(text, format, sa).ToString(format, sa));

            Console.ReadLine();
        }
    }
}
