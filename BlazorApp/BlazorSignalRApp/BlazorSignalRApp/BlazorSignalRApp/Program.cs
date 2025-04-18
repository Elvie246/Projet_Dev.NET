using Serilog;
using BlazorSignalRApp.Client.Pages;
using BlazorSignalRApp.Components;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.ResponseCompression;
using System.Reflection.Metadata;
using BlazorSignalRApp.Hubs;

// Configure Serilog logger
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug() // Set the minimum log level to Debug
    .WriteTo.Console() // Output logs to the console
    .WriteTo.File("logs/logs.log", rollingInterval: RollingInterval.Day) // Output logs to a file with daily rolling
    .CreateLogger(); // Create the logger with the above configuration



var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

    // Add services to the container.
    builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

    builder.Services.AddSignalR();

builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        ["application/octet-stream"]);
});
builder.Services.AddControllers();

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5000);
});


//builder.Services.AddSingleton<DndService>();
var app = builder.Build();
app.UseResponseCompression();

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

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(BlazorSignalRApp.Client._Imports).Assembly);

app.MapHub<ChatHub>("/chathub");
app.MapControllers();
app.Run();

