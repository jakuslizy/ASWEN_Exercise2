using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ReportGeneration;

var services = new ServiceCollection();

services.AddLogging(builder =>
{
    builder.AddConsole();
});

services.AddSingleton<IReportFormatter, JsonReportFormatter>();
services.AddSingleton<IReportFormatter, CsvReportFormatter>();

services.AddSingleton<IReportSender, ConsoleReportSender>();

services.AddSingleton<ReportGenerator>();

var serviceProvider = services.BuildServiceProvider();

var generator = serviceProvider.GetRequiredService<ReportGenerator>();