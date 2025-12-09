using DataAccess.Context;
using DataAccess.Models;
using Entity.Manager;
using Microsoft.EntityFrameworkCore;
using PacketTracer.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseInMemoryDatabase("PackagesDB");
});

builder.Services.AddScoped<DataAccess.Management.PackageDA>();
builder.Services.AddScoped<BussinessLogic.Management.PackageService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    if (!db.Packages.Any())
    {
        db.Packages.AddRange(
            new Package { Carrier = "Amazon", TrackingNumber = "AMZ123", RecipientName = "John", Status = PackageStatus.EnTransito },
            new Package { Carrier = "eBay", TrackingNumber = "EBY456", RecipientName = "Mary", Status = PackageStatus.Entregado }
        );

        db.SaveChanges();
    }
}


app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
