using LandingServer.Services;

var ecuadorTimeZone = TimeZoneInfo.FindSystemTimeZoneById("America/Guayaquil");
var serverTimeUtc = DateTime.UtcNow;
var ecuadorLocalTime = TimeZoneInfo.ConvertTimeFromUtc(serverTimeUtc, ecuadorTimeZone);
Console.WriteLine($"Hora local en Ecuador: {ecuadorLocalTime}");

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddTransient<IEmailSender, EmailSender>(); // Registro del servicio
builder.Services.AddHttpClient(); // Registro de HttpClient
builder.Services.AddTransient<IServerTimeService, ServerTimeService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
