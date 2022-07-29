using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using la_mia_pizzeria_static.Data;
using la_mia_pizzeria_static.Models.Repositories.Interfaces;
using la_mia_pizzeria_static.Models.Repositories;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("PizzaContextConnection") ?? throw new InvalidOperationException("Connection string 'PizzaContextConnection' not found.");

builder.Services.AddDbContext<PizzaContext>(options =>
    options.UseSqlServer(connectionString));


//rigo aggiunto da noi per la gestione della dependency injection.
//Quando passeremo questa interfaccia, passeremo un'istanza di tipo DbPizzaRepository
builder.Services.AddScoped<IPizzaRepository, DbPizzaRepository>();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<PizzaContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.UseAuthentication(); //rigo aggiunto per la gestione di login


app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.MapRazorPages(); //rigo aggiunto per la gestione di login

app.Run();
