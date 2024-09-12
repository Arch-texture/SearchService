using Microsoft.Extensions.Options;
using searchService.Data;
using searchService.Services;

var builder = WebApplication.CreateBuilder(args);

// Register SearchServiceDatabaseSettings
builder.Services.Configure<SearchServiceDatabaseSettings>(builder.Configuration.GetSection("SearchDatabase"));
builder.Services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<SearchServiceDatabaseSettings>>().Value);

builder.Services.AddControllers();
builder.Services.AddScoped<StudentsService>();

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