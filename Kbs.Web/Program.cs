using Kbs.Business.Boat;
using Kbs.Business.BoatType;
using Kbs.Business.Course;
using Kbs.Business.Damage;
using Kbs.Business.Game;
using Kbs.Business.Reservation;
using Kbs.Business.Session;
using Kbs.Business.User;
using Kbs.Data.Boat;
using Kbs.Data.BoatType;
using Kbs.Data.Course;
using Kbs.Data.Damage;
using Kbs.Data.Game;
using Kbs.Data.Reservation;
using Kbs.Data.User;
using Kbs.Web;
using Kbs.Web.Components;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();

builder.Services.AddScoped<SessionManagerAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(services => services.GetRequiredService<SessionManagerAuthenticationStateProvider>());
builder.Services.AddScoped<IBoatRepository, BoatRepository>();
builder.Services.AddScoped<IDamageRepository, DamageRepository>();
builder.Services.AddScoped<IBoatTypeRepository, BoatTypeRepository>();
builder.Services.AddScoped<IGameRepository, GameRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<SessionManager>(services => new SessionManager(services.GetRequiredService<IUserRepository>(), TimeSpan.MaxValue));
builder.Services.AddScoped<ReservationMaker>();
builder.Services.AddScoped<NotificationService>();

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
