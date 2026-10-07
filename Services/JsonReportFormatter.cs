using System.Collections.Generic;
using System.Text.Json;

public sealed class JsonReportFormatter : IReportFormatter
{
    public string FormatName => "json";

    public string Format(IReadOnlyDictionary<string, object?> data)
    {
        return JsonSerializer.Serialize(data);
    }
}