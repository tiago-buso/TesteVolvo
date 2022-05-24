using AspNetCoreHero.ToastNotification;
using Microsoft.EntityFrameworkCore;
using TesteVolvo.Data;
using TesteVolvo.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

//builder.Services.AddDbContext<AppDbContext>(options =>
//                options.UseInMemoryDatabase("InMem"));

builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IBaseTruckModelService, BaseTruckModelService>();
builder.Services.AddScoped<ITruckModelService, TruckModelService>();

builder.Services.AddScoped<IBaseTruckModelRepository, BaseTruckModelRepository>();
builder.Services.AddScoped<ITruckModelRepository, TruckModelRepository>();
builder.Services.AddScoped<ITruckRepository, TruckRepository>();

builder.Services.AddNotyf(config => { config.DurationInSeconds = 10; config.IsDismissable = true; config.Position = NotyfPosition.BottomRight; });

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
