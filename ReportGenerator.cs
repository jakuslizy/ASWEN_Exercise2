using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace ReportGeneration;

public sealed class ReportGenerator
{
    private readonly IReadOnlyCollection<IReportFormatter> _formatters;
    private readonly IReportSender _sender;
    private readonly ILogger<ReportGenerator> _logger;

    public ReportGenerator(
        IEnumerable<IReportFormatter> formatters,
        IReportSender sender,
        ILogger<ReportGenerator> logger)
    {
        _formatters = formatters.ToArray();
        _sender = sender;
        _logger = logger;
    }

    public string GenerateReport(
        IReadOnlyDictionary<string, object?> data,
        string format)
    {
        ArgumentNullException.ThrowIfNull(data);

        if (string.IsNullOrWhiteSpace(format))
        {
            throw new ArgumentException(
                "Report format must be specified.",
                nameof(format));
        }

        var formatter = _formatters.FirstOrDefault(
            f => string.Equals(
                f.FormatName,
                format,
                StringComparison.OrdinalIgnoreCase));

        if (formatter is null)
        {
            _logger.LogError(
                "Unsupported report format requested: {Format}",
                format);

            throw new ArgumentException(
                $"Unsupported report format: '{format}'.",
                nameof(format));
        }

        _logger.LogInformation(
            "Starting report generation using format {Format}",
            formatter.FormatName);

        try
        {
            var report = formatter.Format(data);

            _logger.LogInformation(
                "Report formatted successfully using {Format}",
                formatter.FormatName);

            _sender.Send(report);

            _logger.LogInformation(
                "Report sent successfully using {Sender}",
                _sender.GetType().Name);

            return report;
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Report generation or sending failed for format {Format}",
                formatter.FormatName);

            throw;
        }
    }
}