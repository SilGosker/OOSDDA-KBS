using Kbs.Business.Boat;
using Kbs.Business.BoatType;
using Kbs.Business.Course;
using Kbs.Business.Damage;
using Kbs.Data.Boat;
using Kbs.Data.BoatType;
using Kbs.Data.Course;
using Kbs.Data.Damage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<IBoatRepository, BoatRepository>();
builder.Services.AddScoped<IDamageRepository, DamageRepository>();
builder.Services.AddScoped<IBoatTypeRepository, BoatTypeRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();

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
