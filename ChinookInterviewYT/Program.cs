using ChinookInterviewYT.Client.Pages;
using ChinookInterviewYT.Components;
using ChinookInterviewYT.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

//uncomment to Add a connection the database. you need a db context class to do this
builder.Services.AddDbContext<ChinookContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("ChinookDb"));
});

builder.Services.AddHttpClient();

// Add Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Chinook Interview Challenge API",
        Version = "v1",
        Description = "Chinook Interview Challenge API SPEC",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Jerry McKee Jr",
            Email = "jerry@jmjmediadesign.net",
        }
    });
});

builder.Services.AddControllers(); // enable API controllers

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//normally you would use this in development only. But we want to expose the api as part of the challenge
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers(); // turn on routing for API controllers


app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(ChinookInterviewYT.Client._Imports).Assembly);

app.Run();
