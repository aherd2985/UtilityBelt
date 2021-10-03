using System;
using System.Collections.Generic;
using System.Composition;
using UtilityBelt.Models;
using Microsoft.Extensions.Options;
using System.Globalization;

namespace UtilityBelt.Utilities
{
  [Export(typeof(IUtility))]
  public class DateDiffCalculator : IUtility
  {
    public IList<string> Commands => new List<string> { "date", "date diff", "date diff calc", "date difference", "date difference calculator", "date calc", "date calculator" };
    public string Name => "Date Diff Calculator";

    public void Configure(IOptions<SecretsModel> options)
    {
    }

  
    public string TryGetCultureInfo(string cultureCode)
    {
      CultureInfo culture;
      try
      {
        if (string.IsNullOrEmpty(cultureCode))
        {
          culture = CultureInfo.CurrentCulture;
          return culture.ToString();
        }
        else
        {
          if (cultureCode.ToLower().Trim() == "list")
          {
            ListCultures();
            Console.WriteLine("Please enter a Culture Code");
            TryGetCultureInfo(Console.ReadLine());
          }
          else
          {
            culture = CultureInfo.GetCultureInfo(cultureCode);
            return culture.ToString();
          }
        }
      }
      catch (CultureNotFoundException)
      {
        Console.WriteLine("Please try again");
        TryGetCultureInfo(Console.ReadLine());
      }
      culture = CultureInfo.CurrentCulture;
      return culture.ToString();
    }


    public DateTime ParseDate(string dateInput, string specificCulture)
    {
      CultureInfo culture;
      DateTimeStyles styles;
      DateTime dateResult;
      // Parse a date and time with no styles.
      
      culture = CultureInfo.CreateSpecificCulture(specificCulture);
      styles = DateTimeStyles.None;
      if (DateTime.TryParse(dateInput, culture, styles, out dateResult))
        Console.WriteLine("{0} converted to {1} {2}.",
                          dateInput, dateResult, dateResult.Kind);
      else
      {
        Console.WriteLine("Unable to convert {0} to a date and time.",
                          dateInput);
        Console.WriteLine("Please try again");
        ParseDate(Console.ReadLine(), specificCulture);
      }

      return dateResult;
    }

    public void ListTimeZones()
    {
      foreach (TimeZoneInfo z in TimeZoneInfo.GetSystemTimeZones())
      {
        Console.WriteLine(z.Id);
      }
    }

    public void ListCultures()
    {
      Console.ForegroundColor = ConsoleColor.Cyan;
      foreach (CultureInfo culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
      {
        Console.WriteLine(culture.ToString());
      }
      Console.ForegroundColor = ConsoleColor.Green;
      Console.WriteLine("");
    }

    public void Run()
    {
      Console.WriteLine("Please enter the culture or skip by hitting enter");
      Console.ForegroundColor = ConsoleColor.Blue;
      Console.WriteLine("( default is your current culture )");
      Console.WriteLine("( enter List for options )");
      Console.ForegroundColor = ConsoleColor.Green;
      Console.WriteLine("");

      string culture = TryGetCultureInfo(Console.ReadLine());


      Console.WriteLine("Please enter the first date");
      DateTime dateFirst = ParseDate(Console.ReadLine(), culture);
      Console.WriteLine("");



      Console.WriteLine("Please enter the first date");
      DateTime dateSecond = ParseDate(Console.ReadLine(), culture);
      Console.WriteLine("");

      TimeSpan diffDates = dateSecond.Subtract(dateFirst);

      if(diffDates.TotalMilliseconds < 0)
        diffDates = diffDates * -1;

      Console.ForegroundColor = ConsoleColor.Yellow;

      Console.WriteLine("Exact timespan in Days = " + diffDates.TotalDays);
      Console.WriteLine("Difference in Days = " + diffDates.Days);

      Console.WriteLine("Exact Difference in Hours = " + diffDates.TotalHours);
      Console.WriteLine("Difference in Hours = " + diffDates.Hours);

      Console.WriteLine("Exact Difference in Minutes = " + diffDates.TotalMinutes);
      Console.WriteLine("Difference in Minutes = " + diffDates.Minutes);

      Console.WriteLine("Exact Difference in Seconds = " + diffDates.TotalSeconds);
      Console.WriteLine("Difference in Seconds = " + diffDates.Seconds);

      Console.WriteLine("Exact Difference in Milliseconds = " + diffDates.TotalMilliseconds);
      Console.WriteLine("Difference in Milliseconds = " + diffDates.Milliseconds);

      Console.ForegroundColor = ConsoleColor.Green;
      Console.WriteLine("");
    }
  }
}
