using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using RealTimeChatApp.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add SignalR service
builder.Services.AddSignalR();

var app = builder.Build();

app.MapGet("/", () => "RealTimeChatApp2025 running!");

// Map the chat hub
app.MapHub<ChatHub>("/chatHub");

app.Run();
