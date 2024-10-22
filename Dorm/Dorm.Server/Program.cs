using Dorm.BLL.Interfaces;
using Dorm.BLL.MappingService;
using Dorm.BLL.Services;
using Dorm.BLL.Settings;
using Dorm.DAL;
using Dorm.DAL.Interfaces;
using Dorm.DAL.Repositories;
using Dorm.Domain.Entities.UserEF;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Dorm.BLL.Extensions;
using Dorm.Server.Hubs;
using Dorm.Domain.Enum.User;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", builder =>
    {
        builder.WithOrigins("http://localhost:5174")
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials();
    });
});

builder.Services.AddControllers();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

builder.Services.AddSignalR();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.Configure<AuthSettings>(builder.Configuration.GetSection(nameof(AuthSettings)));
builder.Services.AddAuth(builder.Configuration);
builder.Services.AddScoped<JwtService, JwtService>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql("Host=localhost;Port=5432;Database=DormHub;Username=postgres;Password=root"));//04nykk

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Student", policy => policy.RequireRole(UserType.Student.ToString()));
    options.AddPolicy("Moderator", policy => policy.RequireRole(UserType.Moderator.ToString()));
    options.AddPolicy("Admin", policy => policy.RequireRole(UserType.Admin.ToString()));
});


builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<IWasherRepository, WasherRepository>();
builder.Services.AddScoped<IWasherService, WasherService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IUsersRepository<UserEF>, UsersRepository>();
builder.Services.AddScoped<IStudentProfileService, StudentProfileService>();


builder.Services.AddScoped<IAdRepository, AdRepository>();
builder.Services.AddScoped<IAdService, AdService>();

builder.Services.AddScoped<IChatRepository, ChatRepository>();
builder.Services.AddScoped<IChatService, ChatService>();




var app = builder.Build();

app.UseCors("AllowSpecificOrigin"); // Разрешает отправку куки

app.UseDefaultFiles();
app.UseStaticFiles();


app.MapHub<ChatHub>("/chat");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
