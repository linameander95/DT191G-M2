var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add and configure session state.
builder.Services.AddDistributedMemoryCache(); // Stores session state in memory. Suitable for development and testing.
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // The session timeout.
    options.Cookie.HttpOnly = true; // Whether the browser should allow the cookie to be accessed by client-side JavaScript.
    options.Cookie.IsEssential = true; // Whether the cookie should be marked as essential. Essential cookies are not subject to certain GDPR regulations.
});

// Specify the URLs the application will use.
builder.WebHost.UseUrls("http://localhost:5000", "https://localhost:5001");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession(); // Use session state in the application.

app.UseAuthorization();

app.MapControllerRoute(
    name: "custom",
    pattern: "{controller=Home}/{action=About}/{name?}/{age?}");

app.Run();