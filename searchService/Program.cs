using Microsoft.Extensions.Options;
using searchService.Data;
using searchService.Services;

var builder = WebApplication.CreateBuilder(args);

// Register SearchServiceDatabaseSettings
builder.Services.Configure<SearchServiceDatabaseSettings>(builder.Configuration.GetSection("SearchDatabase"));

// Inject SearchServiceDatabaseSettings as a singleton
builder.Services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<SearchServiceDatabaseSettings>>().Value);

builder.Services.AddControllers();
builder.Services.AddSingleton<StudentsService>();
builder.Services.AddSingleton<GradesService>();
builder.Services.AddSingleton<RestrictionService>();
builder.Services.AddSingleton<SearchService>();


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();
app.Run();
