using System;

public sealed class ConsoleReportSender : IReportSender
{
    public void Send(string report)
    {
        Console.WriteLine(report);
    }
}