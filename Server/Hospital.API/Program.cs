using Hospital.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Hospital.Persistence.Repository.TableRepository;
using Hospital.Bussiness.Services;
using Hospital.Bussiness.Services.AuthServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using System.Text;
using Hospital.Business.DTOs;
using Hpospital.Bussiness.Services.MailServices;
using Hospital.Business.Cloudnary;
using Hospital.Business.Services.ImageService;
using CloudinaryDotNet;
using Hospital.Bussiness.Services.OTPServices;
using Hospital.Business.Services;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddAuthorization();
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddScoped<IMailService, MailService>();
builder.Services.Configure<CloudinarySettings>(
builder.Configuration.GetSection("CloudinarySettings"));
builder.Services.AddScoped<IImageService, ImageService>();

builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));
builder.Services.AddScoped(x =>
{
    var settings = builder.Configuration.GetSection("CloudinarySettings").Get<CloudinarySettings>();
    var acc = new Account(settings.CloudName, settings.ApiKey, settings.ApiSecret);
    return new Cloudinary(acc);
});

builder.Services.AddScoped<IPatientServices, PatientServices>();
builder.Services.AddScoped<IDoctorServices, DoctorServices>();
builder.Services.AddScoped<IImageService, ImageService>();

builder.Services.AddScoped<IAppointmentServices, AppointmentServices>();
builder.Services.AddScoped<IOTPRepository, OTPRepository>();
builder.Services.AddScoped<IOTPService, OTPService>();
builder.Services.AddScoped<IDepartmentServices, DepartmentServices>();
builder.Services.AddScoped<IBillingTransicationServices, BillingTransicationServices>();
builder.Services.AddScoped<IAdmissionDischargeServices, AdmissionDischargeServices>();
builder.Services.AddScoped<IEmployeeStaffServices, EmployeeStaffServices>();
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<IEmployeeStaffRepository, EmployeeStaffRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IBillingTrasicationRepository, BillingTrasicationRepository>();
builder.Services.AddScoped<IAdmissionDischargeRepository, AdmissionDischargeRepository>();
builder.Services.AddScoped<ITokenServices, TokenServices>();
builder.Services.AddScoped<ILoginServices, LoginServices>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hospital API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' followed by your token. Example: Bearer <your_token>"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[] {}
        }
    });
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });

    options.AddPolicy(name: "ONLYEXAMPLE", policy =>
    {
        policy.WithOrigins("https://example.com") // replace with your specific frontend URL
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

builder.Services.AddDbContext<HospitalDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("HospitalDbConnection"));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
