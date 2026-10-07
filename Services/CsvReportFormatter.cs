using System.Collections.Generic;
using System.Globalization;

namespace ReportGeneration;

public sealed class CsvReportFormatter : IReportFormatter
{
    public string FormatName => "csv";

    public string Format(IReadOnlyDictionary<string, object?> data)
    {
        var lines = new List<string>();

        foreach (var entry in data)
        {
            var key = EscapeCsv(entry.Key);
            var value = EscapeCsv(
                Convert.ToString(entry.Value, CultureInfo.InvariantCulture)
                ?? string.Empty);

            lines.Add($"{key},{value}");
        }

        return string.Join(Environment.NewLine, lines);
    }

    private static string EscapeCsv(string value)
    {
        if (value.Contains('"') ||
            value.Contains(',') ||
            value.Contains('\n') ||
            value.Contains('\r'))
        {
            return $"\"{value.Replace("\"", "\"\"")}\"";
        }

        return value;
    }
}