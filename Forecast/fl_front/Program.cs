using ApexCharts;
using fl_front;
using fl_front.Interfaces;
using fl_front.Interfaces.Students;
using fl_front.Models;
using fl_front.Services;
using fl_front.Services.ForecastF;
using fl_front.Services.Health;
using fl_front.Services.Impl;
using fl_front.Services.OrdersF;
using fl_front.Services.Pdfs;
using fl_front.Services.Pdfs.Impl;
using fl_front.Services.Planification;
using fl_front.Services.Reports;
using fl_front.Services.Students;
using fl_front.Services.UniversityForecast;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp =>
    new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

var forecastBase = new Uri("https://forecast.labs.underflabs.com/");
var studentsBase = new Uri("https://api.students.underflabs.com/");

builder.Services.AddHttpClient<IHealthService, HealthService>(client => client.BaseAddress = forecastBase);
builder.Services.AddHttpClient<IDocumentService, DocumentService>(client => client.BaseAddress = forecastBase);
builder.Services.AddHttpClient<IDemandReportService, DemandReportService>(client => client.BaseAddress = forecastBase);
builder.Services.AddHttpClient<IForecastService, ForecastService>(client => client.BaseAddress = forecastBase);
//builder.Services.AddHttpClient<IPdfAnalysisService, PdfAnalysisService>(client => client.BaseAddress = forecastBase);
builder.Services.AddHttpClient<IPdfAnalysisService, PdfAnalysisService>(client =>
{
    client.BaseAddress = new Uri("https://forecast.labs.underflabs.com/");
});
builder.Services.AddHttpClient<IPdfAnalysisServiceF, PdfAnalysisServiceF>(client =>
{
    client.BaseAddress = new Uri("https://forecast.labs.underflabs.com/");
});
builder.Services.AddHttpClient<IForecastServiceF, ForecastServiceF>(client =>
{
    client.BaseAddress = new Uri("https://forecast.labs.underflabs.com/");
});
builder.Services.AddHttpClient<IOrderServiceF, OrderServiceF>(client => client.BaseAddress = forecastBase);
builder.Services.AddHttpClient<IPlanningServiceF, PlanningServiceF>(client => client.BaseAddress = forecastBase);
builder.Services.AddHttpClient<IReportServiceF, ReportServiceF>(client => client.BaseAddress = forecastBase);
builder.Services.AddHttpClient<IUniversityForecastServiceF, UniversityForecastServiceF>(client =>
{
    client.BaseAddress = new Uri("https://forecast.labs.underflabs.com/");
});
builder.Services.AddScoped<IUniversityStudentServiceF, UniversityStudentServiceF>(sp =>
{
    var client = new HttpClient { BaseAddress = new Uri("https://api.students.underflabs.com/") };
    return new UniversityStudentServiceF(client);
});



builder.Services.AddHttpClient<IStudentsService, StudentsService>(client => client.BaseAddress = studentsBase);

builder.Services.AddSingleton<AppState>();
builder.Services.AddApexCharts();

await builder.Build().RunAsync();
