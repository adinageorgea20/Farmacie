using Farmacie.Data;
using Farmacie.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);

// Adăugăm serviciile necesare
builder.Services.AddRazorPages();

// Adăugăm serviciul DbContext pentru conectarea la baza de date
builder.Services.AddDbContext<FarmacieContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FarmacieContext")
        ?? throw new InvalidOperationException("Connection string 'FarmacieContext' not found.")));

// Permitem utilizarea sesiunii în aplicație
builder.Services.AddDistributedMemoryCache(); // Permite stocarea în memorie
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Setează timpul de expirare al sesiunii
    options.Cookie.HttpOnly = true; // Asigură securitatea cookie-ului
    options.Cookie.IsEssential = true; // Setează ca esențial pentru aplicație
});

// Adăugăm acces la contextul HTTP pentru sesiune
builder.Services.AddHttpContextAccessor(); // Permite accesul la sesiune

// Înregistrăm serviciul ShoppingCart
builder.Services.AddScoped<ShoppingCartService>();

builder.Services.AddScoped<ShoppingCartService>();


// Construim aplicația
var app = builder.Build();

// Configurăm aplicația pentru a rula
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Activăm utilizarea sesiunii
app.UseSession();

app.MapRazorPages();

app.Run();
