using fl_students_api.Configurations;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Mongo config
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));

builder.Services.AddSingleton<IMongoClient>(
    sp => new MongoClient(builder.Configuration.GetSection("MongoDbSettings")["ConnectionString"]));

// JSON config
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.UseMemberCasing();
        options.SerializerSettings.Converters.Add(new ObjectIdConverter());
    });

// CORS: permitir cualquier origen
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddOpenApi();

var app = builder.Build();

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapOpenApi(); // Habilita documentación Swagger (siempre)

app.Run();
