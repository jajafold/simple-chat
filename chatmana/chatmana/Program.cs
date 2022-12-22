#pragma warning disable CA1416

using Infrastructure;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext
    <ChatDbContext>(o => o.UseSqlite("Filename=chatdb.db"),
        ServiceLifetime.Transient);
builder.Services.AddTransient<ISerializer, Serializer>();
builder.Services.AddTransient<IChatRepository, ChatRepository>();

var app = builder.Build();
AddChatData(app);

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

app.UseAuthorization();

app.MapControllerRoute(
    "default",
    "{controller=Home}/{action=Index}/{id?}");

app.Run();

static void AddChatData(WebApplication app)
{
    var scope = app.Services.CreateScope();
    var chatRepository = scope.ServiceProvider.GetService<IChatRepository>();
    
    chatRepository.Create();
    if (chatRepository.AllChatRooms.Any()) return;
    chatRepository.AddRoom("Server1", "Main1", null, 1);
    chatRepository.AddRoom("Server2", "Main2", null, 0);
    chatRepository.AddRoom("Server3", "Main3", null, 0);
    chatRepository.AddRoom("Server4", "Main4", null, 0);
}