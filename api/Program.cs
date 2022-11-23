using Microsoft.AspNetCore.Mvc;
using SimpleFinder;
using SimpleFinder.Features.Files;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<SimpleFinderDbContext>();
builder.Services.AddScoped<GetFolderFromPathService>();
builder.Services.AddControllers();
builder.Services.AddCors();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials());
}

using (var serviceScope = app.Services.CreateScope())
{
    var db = serviceScope.ServiceProvider.GetRequiredService<SimpleFinderDbContext>();
    db.Database.EnsureCreated();
}

app.MapGet("/files",
                    ([FromQuery] string path,
                    [FromServices] SimpleFinderDbContext db)
                    => GetFilesFromPath.Handle(path, db));

app.MapPost("/files",
                    ([FromBody] Create.Command command,
                    [FromServices] SimpleFinderDbContext db)
                    => Create.Handle(command, db));
                    
app.Run();
