using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace UtilityBelt.Helpers
{
  public static class ImageToConsole
  {
    /// <summary>
    /// Displays an image directly in the console.
    /// Supports both file paths and base64 strings.
    /// Falls back to ASCII mode if ANSI colors are not supported.
    /// </summary>
    /// <param name="source">File path or base64-encoded image data</param>
    /// <param name="maxWidth">Maximum width of the image in console characters</param>
    /// <param name="fallbackAscii">If true, use ASCII fallback when ANSI is not supported</param>
    public static void Show(string source, int maxWidth = 80, bool fallbackAscii = true)
    {
      if (string.IsNullOrWhiteSpace(source))
      {
        Console.WriteLine("Error: empty or null source.");
        return;
      }

      Image<Rgba32> img;

      try
      {
        if (File.Exists(source))
        {
          img = Image.Load<Rgba32>(source);
        }
        else if (IsUrl(source))
        {
          using var client = new HttpClient();
          var bytes = client.GetByteArrayAsync(source).Result;
          img = Image.Load<Rgba32>(bytes);
        }
        else if (IsBase64String(source))
        {
          var bytes = Convert.FromBase64String(source);
          img = Image.Load<Rgba32>(bytes);
        }
        else
        {
          Console.WriteLine("Error: input is not a valid path, URL, or base64 string.");
          return;
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error loading image: {ex.Message}");
        return;
      }

      using (img)
      {
        // Maintain aspect ratio, adjust height because console characters are taller
        double aspect = (double)img.Height / img.Width;
        int width = Math.Min(maxWidth, img.Width);
        int height = (int)(width * aspect / 2);

        img.Mutate(x => x.Resize(width, height));

        bool ansi = SupportsAnsi();

        if (!ansi && fallbackAscii)
        {
          RenderAscii(img);
          return;
        }

        // Render using ANSI truecolor background blocks
        for (int y = 0; y < img.Height; y++)
        {
          for (int x = 0; x < img.Width; x++)
          {
            var p = img[x, y];
            Console.Write($"\x1b[48;2;{p.R};{p.G};{p.B}m ");
          }
          Console.Write("\x1b[0m\n");
        }

        Console.WriteLine("\x1b[0m");
      }
    }

    private static bool IsUrl(string s)
    {
      return Uri.TryCreate(s, UriKind.Absolute, out var uri)
          && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps);
    }

    private static bool IsBase64String(string s)
    {
      // Quick sanity check
      if (string.IsNullOrWhiteSpace(s) || s.Length % 4 != 0)
        return false;

      // Try decode
      try
      {
        Convert.FromBase64String(s);
        return true;
      }
      catch
      {
        return false;
      }
    }

    /// <summary>
    /// Detects whether the current terminal supports ANSI truecolor escape codes.
    /// </summary>
    private static bool SupportsAnsi()
    {
      if (OperatingSystem.IsWindows())
      {
        string term = Environment.GetEnvironmentVariable("TERM_PROGRAM") ?? "";
        return term.Contains("vscode", StringComparison.OrdinalIgnoreCase)
            || term.Contains("Windows Terminal", StringComparison.OrdinalIgnoreCase)
            || Environment.GetEnvironmentVariable("WT_SESSION") != null;
      }

      // Most Linux and macOS terminals support ANSI by default
      return true;
    }

    /// <summary>
    /// Fallback renderer that prints the image as grayscale ASCII characters.
    /// </summary>
    private static void RenderAscii(Image<Rgba32> img)
    {
      string chars = "@%#*+=-:. ";
      for (int y = 0; y < img.Height; y++)
      {
        for (int x = 0; x < img.Width; x++)
        {
          var p = img[x, y];
          var gray = (p.R + p.G + p.B) / 3;
          int index = gray * (chars.Length - 1) / 255;
          Console.Write(chars[index]);
        }
        Console.WriteLine();
      }
    }
  }
}
