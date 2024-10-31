namespace Utilities;

/// <summary>
/// Provides utility functions for handling ISO8601 time formatting and parsing.
/// </summary>
class Time
{
  /// <summary>
  /// Converts a <see cref="DateTime"/> object to an ISO8601-compliant string.
  /// </summary>
  /// <param name="time">The <see cref="DateTime"/> value to convert.</param>
  /// <returns>A string representation of the date and time in ISO8601 format (yyyy-MM-ddTHH:mm:ssZ).</returns>
  static public string ToISOString(DateTime time)
  {
    // Converts to UTC to comply with the ISO8601 format, then formats as "yyyy-MM-ddTHH:mm:ss" in a culture-invariant way.
    return time.ToUniversalTime().ToString("s", System.Globalization.CultureInfo.InvariantCulture);
  }

  /// <summary>
  /// Parses an ISO8601-formatted string and converts it to a <see cref="DateTime"/> object.
  /// </summary>
  /// <param name="iso8601String">A string in ISO8601 format (yyyy-MM-ddTHH:mm:ssZ).</param>
  /// <returns>A <see cref="DateTime"/> object representing the parsed date and time.</returns>
  /// <exception cref="ArgumentNullException">Thrown when <paramref name="iso8601String"/> is null or empty.</exception>
  /// <exception cref="FormatException">Thrown when the string is not in a valid ISO8601 format.</exception>
  static public DateTime FromISOString(string iso8601String)
  {
    // Checks if the string is null or empty before parsing, which would otherwise throw an ArgumentNullException.
    if (string.IsNullOrEmpty(iso8601String))
    {
      throw new ArgumentNullException(nameof(iso8601String), "Input string cannot be null or empty.");
    }

    // Parses the string strictly using the exact ISO8601 format (yyyy-MM-ddTHH:mm:ssZ).
    return DateTime.ParseExact(iso8601String, "yyyy-MM-ddTHH:mm:ssZ", System.Globalization.CultureInfo.InvariantCulture);
  }
}
