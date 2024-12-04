using dotenv.net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TourAPI.Data;
using TourAPI.Interfaces;
using TourAPI.Interfaces.Repository;
using TourAPI.Interfaces.Service;
using TourAPI.Middleware;
using TourAPI.Models;
using TourAPI.Repository;
using TourAPI.Service;

var builder = WebApplication.CreateBuilder(args);

// Load .env file
DotEnv.Load();

// Add services to the container.

builder.Services.AddControllers();
// Swagger/OpenAPI configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

// Configure Email Sender
builder.Services.AddSingleton<IEmailSender>(new EmailSender(
    smtpHost: builder.Configuration["EmailSettings:SmtpServer"],
    smtpPort: int.Parse(builder.Configuration["EmailSettings:SmtpPort"]),
    smtpUser: builder.Configuration["EmailSettings:Username"],
    smtpPass: builder.Configuration["EmailSettings:Password"]
));

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

// Database context
builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentity<Account, IdentityRole>(options =>
{
    options.Lockout.AllowedForNewUsers = false;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 12;
})
.AddEntityFrameworkStores<ApplicationDBContext>()
.AddDefaultTokenProviders();


// Configure JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme =
    options.DefaultChallengeScheme =
    options.DefaultForbidScheme =
    options.DefaultScheme =
    options.DefaultSignInScheme =
    options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:Audience"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"])
        )
    };
});

// Register services
builder.Services.AddTransient<ExceptionMiddleware>();

builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAccountService, AccountService>();

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddScoped<ITourService, TourService>();
builder.Services.AddScoped<ITourRepository, TourRepository>();

builder.Services.AddScoped<ITourImageService, TourImageService>();
builder.Services.AddScoped<ITourImageRepository, TourImageRepository>();

builder.Services.AddScoped<ITourScheduleService, TourScheduleService>();
builder.Services.AddScoped<ITourScheduleRepository, TourScheduleRepository>();

builder.Services.AddScoped<ITransportService, TransportService>();
builder.Services.AddScoped<ITransportRepository, TransportRepository>();

builder.Services.AddScoped<ITransportDetailService, TransportDetailService>();
builder.Services.AddScoped<ITransportDetailRepository, TransportDetailRepository>();

builder.Services.AddScoped<IPromotionService, PromotionService>();
builder.Services.AddScoped<IPromotionRepository, PromotionRepository>();

builder.Services.AddScoped<ITourPromotionService, TourPromotionService>();
builder.Services.AddScoped<ITourPromotionRepository, TourPromotionRepository>();

builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();

builder.Services.AddScoped<IBookingDetailService, BookingDetailService>();
builder.Services.AddScoped<IBookingDetailRepository, BookingDetailRepository>();

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddSingleton<CloudinaryService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TourAPI v1");
    });
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

// Configure CORS
app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials()
    .WithOrigins("http://localhost:5173")
    .SetIsOriginAllowed(origin => true));

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
