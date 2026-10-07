using System.Collections.Generic;

namespace ReportGeneration;

public interface IReportFormatter
{
    string FormatName { get; }

    string Format(IReadOnlyDictionary<string, object?> data);
}