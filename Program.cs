using state_district_city.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("dbInstitute");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllersWithViews();

builder.Services.AddAuthorization();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
else
{
    app.UseDeveloperExceptionPage();
    app.UseStatusCodePages();
}
app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseAuthentication();
app.UseAuthorization();
app.UseRouting();




app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();

