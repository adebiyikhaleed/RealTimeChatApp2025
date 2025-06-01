using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using RealTimeChatApp2025.Data;
using RealTimeChatApp2025.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add SignalR + Controllers + Razor Pages
builder.Services.AddSignalR();
builder.Services.AddRazorPages();

// 📦 Add EF Core + SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=chat.db"));

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.MapRazorPages();
app.MapHub<ChatHub>("/chathub");

app.Run();
