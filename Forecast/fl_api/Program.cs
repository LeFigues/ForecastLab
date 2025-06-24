using System.Net.Http.Headers;
using fl_api.Configurations;
using fl_api.Interfaces;
using fl_api.Interfaces.Guides;
using fl_api.Repositories.Guides;
using fl_api.Services;
using fl_api.Services.Guides;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using University.Interfaces;
using University.Services;
using fl_api.Interfaces.University;
using fl_api.Repositories.University;
using fl_api.Services.University;
using fl_api.Interfaces.Reports;
using fl_api.Services.Reports;
using fl_api.Repositories.Reports;
using fl_api.Interfaces.Planification;
using fl_api.Repositories.Planification;
using fl_api.Services.Planification;
using fl_api.Repositories.Forecast;
using fl_api.Interfaces.IForecast;
using fl_api.Services.Forecast;
using fl_api.Repositories.Impl;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using fl_api.Interfaces.Students;
using fl_api.Repositories.Student;
using fl_api.Services.Students;
using fl_api.Interfaces.Purchases;
using fl_api.Repositories.Purchases;
using fl_api.Services.Purchases;

var builder = WebApplication.CreateBuilder(args);

// 1. CONFIG BINDING
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));
builder.Services.Configure<OpenAISettings>(builder.Configuration.GetSection("OpenAI"));
builder.Services.Configure<ApiStudentsSettings>(builder.Configuration.GetSection("ApiStudents"));
builder.Services.Configure<ApiLabsSettings>(builder.Configuration.GetSection("ApiLabs"));
builder.Services.Configure<PdfStorageSettings>(builder.Configuration.GetSection("PdfStorage"));
builder.Services.Configure<PromptSettings>(
    builder.Configuration.GetSection("PromptSettings"));


// 2. REGISTER MONGO CLIENT
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var cfg = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    return new MongoClient(cfg.ConnectionString);
});

// 3. MONGO WRAPPER
builder.Services.AddScoped<IMongoDbService, MongoDbService>();

// 4. OPENAI CLIENT
builder.Services.AddHttpClient<IOpenAIService, OpenAIService>((sp, client) =>
{
    var cfg = sp.GetRequiredService<IOptions<OpenAISettings>>().Value;
    client.BaseAddress = new Uri(cfg.BaseUrl.TrimEnd('/') + "/v1/");
    client.DefaultRequestHeaders.Authorization =
        new AuthenticationHeaderValue("Bearer", cfg.ApiKey);
    client.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue("application/json"));

    client.DefaultRequestHeaders.Add("OpenAI-Beta", "assistants=v2");
});


// 5. OTHER APIs
builder.Services.AddHttpClient<IStudentsApiService, StudentsApiService>((sp, client) =>
{
    var cfg = sp.GetRequiredService<IOptions<ApiStudentsSettings>>().Value;
    client.BaseAddress = new Uri(cfg.BaseUrl);
    client.Timeout = TimeSpan.FromSeconds(20);
});

builder.Services.AddHttpClient<ILabsApiService, LabsApiService>((sp, client) =>
{
    var cfg = sp.GetRequiredService<IOptions<ApiLabsSettings>>().Value;
    client.BaseAddress = new Uri(cfg.BaseUrl);
    client.Timeout = TimeSpan.FromSeconds(20);
});
builder.Services.AddHttpClient<IUniversityApiClient, UniversityApiClient>((sp, client) =>
{
    var cfg = builder.Configuration.GetSection("ApiLabs").Get<ApiLabsSettings>();
    client.BaseAddress = new Uri(cfg.BaseUrl);
    client.Timeout = TimeSpan.FromSeconds(120);
});
//builder.Services.AddHttpClient<IUniversityApiClient, UniversityApiClient>()
//    .ConfigureHttpClient(async (sp, client) =>
//    {
//        var repo = sp.GetRequiredService<IUniversityApiConfigRepository>();
//        var config = await repo.GetAsync();
//        var baseUrl = config?.BaseUrl ?? "https://api.students.underflabs.com"; // fallback
//        client.BaseAddress = new Uri(baseUrl);
//        client.Timeout = TimeSpan.FromSeconds(120);
//    });


// 6. CORE SERVICES
builder.Services.AddScoped<IHealthService, HealthService>();

// 7. PDF TEXT EXTRACTOR SERVICE (PdfPig)
builder.Services.AddScoped<IPdfExtractorService, PdfExtractorService>();

// 8. GUIDE ANALYSIS
builder.Services.AddScoped<IGuideFileRepository, GuideFileRepository>();
builder.Services.AddScoped<IGuideAnalysisRepository, GuideAnalysisRepository>();
builder.Services.AddScoped<IGuideAnalysisService, GuideAnalysisService>();
builder.Services.AddScoped<IGuideGroupAnalysisService, GuideGroupAnalysisService>();
builder.Services.AddScoped<ILabGuideExtractionService, LabGuideExtractionService>();
builder.Services.AddScoped<ILabGuideRepository, LabGuideRepository>();
builder.Services.AddScoped<ILabGuideVerificationService, LabGuideVerificationService>();
builder.Services.AddScoped<IGuideUploadService, GuideUploadService>();
builder.Services.AddScoped<ISemesterGuideAnalysisService, SemesterGuideAnalysisService>();

// UNIVERSITY
builder.Services.AddScoped<IUniversityApiConfigRepository, UniversityApiConfigRepository>();
builder.Services.AddScoped<IUniversityApiConfigService, UniversityApiConfigService>();
builder.Services.AddScoped<IUniversityForecastService, UniversityForecastService>();

// REPORTS
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IPrecioEstimadoRepository, PrecioEstimadoRepository>();

// PLANIFICATION
builder.Services.AddScoped<IPlanningService, PlanningService>();
builder.Services.AddScoped<IPurchasePlanRepository, PurchasePlanRepository>();
builder.Services.AddScoped<ISupplyPurchasePlanService, SupplyPurchasePlanService>();
builder.Services.AddScoped<ISupplyPurchasePlanRepository, SupplyPurchasePlanRepository>();

// ORDERS
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IPurchaseOrderRepository, PurchaseOrderRepository>();
// SEMESTER PURCHASE
builder.Services.AddScoped<ISemesterPurchaseRepository, SemesterPurchaseRepository>();
builder.Services.AddScoped<ISemesterPurchaseService, SemesterPurchaseService>();

// STUDENT DEMAND FORECAST
builder.Services.AddScoped<IStudentDemandForecastService, StudentDemandForecastService>();
builder.Services.AddScoped<IStudentDemandForecastRepository, StudentDemandForecastRepository>();
builder.Services.AddScoped<IStudentDemandExportService, StudentDemandExportService>();


//FORECAST
builder.Services.AddScoped<IForecastSemestreRepository, ForecastSemestreRepository>();
// FORECAST (faltantes)
builder.Services.AddScoped<IForecastService, ForecastService>();
builder.Services.AddScoped<IPromptService, PromptService>();
builder.Services.AddScoped<IForecastHistoricoRepository, ForecastHistoricoRepository>();
builder.Services.AddScoped<IForecastPracticaRepository, ForecastPracticaRepository>();
builder.Services.AddScoped<IForecastRiesgoRepository, ForecastRiesgoRepository>();
builder.Services.AddScoped<IDemandReportService, DemandReportService>();


// SYNCRONIZATION
// Normalization
builder.Services.AddScoped<INormalizedSupplyRepository, NormalizedSupplyRepository>();
builder.Services.AddScoped<ISupplyNormalizationService, SupplyNormalizationService>();
builder.Services.AddScoped<ISupplyUpdateService, SupplyUpdateService>();


// 9. MVC + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile("purchaseSettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile("prompts.json", optional: false, reloadOnChange: true);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();
app.UseCors("AllowAll");
app.Use(async (context, next) =>
{
    context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
    context.Response.Headers.Add("X-Frame-Options", "DENY");
    context.Response.Headers.Add("Referrer-Policy", "no-referrer");
    context.Response.Headers.Add("Content-Security-Policy", "default-src 'self'");
    await next();
});
app.UseExceptionHandler("/error"); // Y crea el endpoint /error

// 11. Middlewares
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
