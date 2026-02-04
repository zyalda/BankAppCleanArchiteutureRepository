using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MyApp.Application.Interfaces;
using MyApp.Application.ServiceInterfaces;
using MyApp.Application.Services;
using MyApp.Application.ServicesInterfaces;
using MyApp.Domain;
using MyApp.Infrastructure.Repositories;
using MyApp.Infrastructure.UnitOfWorks;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
//                 throw new InvalidOperationException("Connection string 'BloggingContext'" +
//                " not found.");

//builder.Services.AddDbContext<BankAppDataContext>(options => options.UseSqlServer(
//connectionString, b => b.MigrationsAssembly(typeof(BankAppDataContext).Assembly.FullName)));


//To list a JSON object of customers informations. Informations list accounts and list transactions.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = false; // Optional: for better readability
});

//Add JWT Code here.
builder.Services.AddAuthentication(opt => {
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
})
   //Här säger vi hur vi skall jobba med JWT
   .AddJwtBearer(opt => {
       opt.TokenValidationParameters = new TokenValidationParameters
       {
           //Issuer är vem (vilken server) som utfärdat en JWT token
           ValidateIssuer = true,
           ValidateAudience = true,
           ValidateLifetime = true,
           ValidateIssuerSigningKey = true,
           ValidIssuer = "http://localhost:51597",
           ValidAudience = "http://localhost:51597",
           ClockSkew = TimeSpan.FromSeconds(300),
           IssuerSigningKey =
      new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mykey1234567&%%485734579453%&//1255362"))
       };
   });

// Dependency Injection
builder.Services.AddBankAppDataBaseContext(builder.Configuration);

builder.Services.AddScoped<ILoanRepository, LoanRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IUserTypeRepository, UserTypeRepository>();
builder.Services.AddScoped<ITransactionsRepository, TransactionsRepository>();
builder.Services.AddScoped<ICustomerAccountRepository, CustomerAccountRepository>();

builder.Services.AddScoped<IAuthenticateUserService, AuthenticateUserService>();
builder.Services.AddScoped<ICustomerAccountService, CustomerAccountService>();
builder.Services.AddScoped<ITransactionsServices, TransactionsServices>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IUserTypeService, UserTypeService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ILoanService, LoanService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Test API",
        Description = "A simple example for swagger api information",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Your Name XYZ",
            Email = "xyz@gmail.com",
            Url = new Uri("https://example.com"),
        },
        License = new OpenApiLicense
        {
            Name = "Use under OpenApiLicense",
            Url = new Uri("https://example.com/license"),
        }
    });
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Please enter your token with this format: ''Bearer YOUR_TOKEN''",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Name = "Bearer",
                In = ParameterLocation.Header,
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });
});


var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.Run();
