using System.Collections.Generic;

public interface IReportFormatter
{
    string FormatName { get; }

    string Format(IReadOnlyDictionary<string, object?> data);
}