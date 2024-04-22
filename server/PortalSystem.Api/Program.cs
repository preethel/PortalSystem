using Microsoft.EntityFrameworkCore;
using PortalSystem.Service;
using Portal.Database;
using PortalSystem.Repository;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
//builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MSSQLConnection")));
builder.Services.AddPortalServices(builder.Configuration);
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
const string ALLOWED_ORIGINS_RULE = "_test_allowed_origins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: ALLOWED_ORIGINS_RULE, policy =>
    {
        policy.WithOrigins("https://localhost:7287", "https://preethel.github.io/", "http://localhost:5222","https://test4.bdjobs.com")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(ALLOWED_ORIGINS_RULE);
app.UseAuthorization();
app.UseAuthorization();
app.MapControllers();

app.Run();
