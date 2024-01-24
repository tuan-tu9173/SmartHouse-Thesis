using Application.Services;
using SignalRChat.Hubs;
using BackEnd_SmartHouseThesis;
using BackEnd_SmartHouseThesis.Controllers;
using Domain.Entities;
using Infrastructure;
using Infrastructure.Mapper;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Options;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddSignalR();
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddScoped<AppDbContext>();
//Account
builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<AccountRepository>();
//Owner
builder.Services.AddScoped<OwnerService>();
builder.Services.AddScoped<OwnerRepository>();
//Device
builder.Services.AddScoped<DeviceService>();
builder.Services.AddScoped<DeviceRepository>();
//Manufacture
builder.Services.AddScoped<ManufacturerService>();
builder.Services.AddScoped<ManufactureRepository>();
//Package
builder.Services.AddScoped<PackageServices>();
builder.Services.AddScoped <PackageRepository>();
//Policy
builder.Services.AddScoped<PolicyService>();
builder.Services.AddScoped<PolicyRepository>();
//Promotion
builder.Services.AddScoped<PromotionService>();
builder.Services.AddScoped<PromotionRepository>();
//Account
builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<AccountRepository>();
//Contract
builder.Services.AddScoped<ContractService>();
builder.Services.AddScoped<ContractRepository>();
//Role
builder.Services.AddScoped<RoleService>();
builder.Services.AddScoped<RoleRepository>();


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(typeof(AccountMappingProfile));

builder.Services.AddIdentity<Account, Role>();
builder.Services.AddIdentity<Account, Customer>();
builder.Services.AddIdentity<Account, Staff>();
builder.Services.AddIdentity<Account, Teller>();
builder.Services.AddIdentity<Account, Owner>();

builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(option =>
{
    option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "ISHE",
        ValidAudience = "ISHE",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SmartHomeISHE"))
    };
});

//builder.Services.ConfigureServices(builder.Configuration);


builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.MapHub<ChatHub>("/chatHub");
//////////////////////////////////////////////////////////////////////////////////////////
///Add Cors 
builder.Services.AddCors();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("TellerOrCustomerPolicy", policy => policy.RequireRole("Teller", "Customer"));
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

///useCors
app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
});

app.UseHttpsRedirection();

//use Routing
app.UseRouting();
//use Authentication
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
