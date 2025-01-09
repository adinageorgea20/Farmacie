using Microsoft.EntityFrameworkCore;
using Farmacie.Data;
using Farmacie.Services;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<FarmacieContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FarmacieContext")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<PharmacyIdentityContext>();

builder.Services.AddRazorPages();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddDbContext<PharmacyIdentityContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PharmacyIdentityContextConnection")
    ?? throw new InvalidOperationException("Connection string 'PharmacyIdentityContextConnection' not found.")));

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<ShoppingCartService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
