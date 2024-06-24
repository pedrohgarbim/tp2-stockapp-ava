using Microsoft.AspNetCore.Authentication.JwtBearer;
using StockApp.Infra.IoC;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using StockApp.Application.DTOs;
using StockApp.Domain.Interfaces;
using StockApp.Infra.Data.Repositories;
using StockApp.Application.Services;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Microsoft.OpenApi.Models;
using StockApp.Application.Interfaces;
using StockApp.API.GraphQL;
using Serilog;
using System.Net.Mail;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.SignalR;
using StockApp.API.Hubs;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Configuração do Serilog
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        // Usar o Serilog como logger da aplicação
        builder.Host.UseSerilog();

        // Add services to the container.
        builder.Services.AddInfrastructureAPI(builder.Configuration);
        builder.Services.AddSingleton<ISupplierRelationshipManagementService, SupplierRelationshipManagementService>();
        builder.Services.AddControllers();
        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddSingleton<IMarketTrendAnalysisService, MarketTrendAnalysisService>();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddSingleton<IReviewModerationService, ReviewModerationService>();
        builder.Services.AddScoped<IOrderRepository, OrderRepository>();
        builder.Services.AddScoped<IRecommendationService, RecommendationService>();
        builder.Services.AddScoped<IInventoryManagementService, InventoryManagementService>();
        builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        builder.Services.AddScoped<IEmployeePerformanceEvaluationService, EmployeePerformanceEvaluationService>();



        builder.Services.AddGraphQLServer()
            .AddQueryType<Query>()
            .AddMutationType<Mutation>();

        builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

        builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

        builder.Services.AddResponseCaching();

        builder.Services.AddSingleton<ITaxService, TaxService>();

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("RequireAdministratorRole",
                 policy => policy.RequireRole("Administrator"));
        });
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });
        });

        builder.Services.AddScoped<IUserAuditService, UserAuditService>();
        builder.Services.AddScoped<IEmailService, EmailService>();
        builder.Services.AddHttpClient<IWebhookService, WebhookService>();

        // Configuração do serviço de notificação por email
        builder.Services.AddSingleton(sp => new SmtpClient("smtp.nicolas.undiciati@fatec.sp.gov.br")
        {
            Port = 587,
            Credentials = new System.Net.NetworkCredential("username", "password"),
            EnableSsl = true,
        });
        builder.Services.AddSingleton<IEmailNotificationService, EmailNotificationService>();

        var key = Encoding.ASCII.GetBytes("3xmpl3V3ryS3cur3S3cr3tK3y!@#123");
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["JwtSettings:MyAppIssuer"],
                ValidAudience = builder.Configuration["JwtSettings:MyAppAudience"],
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };
        });
        builder.Services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = builder.Configuration.GetConnectionString("Redis");
        });

        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

            var securitySchema = new OpenApiSecurityScheme
            {
                Description = "Authorization: Bearer {token}",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            };
            c.AddSecurityDefinition("Bearer", securitySchema);

            var securityRequirement = new OpenApiSecurityRequirement
            {
                { securitySchema, new [] { "Bearer" } }
            };

            c.AddSecurityRequirement(securityRequirement);
        });
        builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        builder.Services.AddSingleton<IDiscountService, DiscountService>();
        builder.Services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = builder.Configuration.GetConnectionString("Redis");
        });

        builder.Services.AddSingleton<IPerformanceMetricsService, PerformanceMetricsService>();

        builder.Services.AddSingleton<IProjectManagementService, ProjectManagementService>();

        // Add SignalR
        builder.Services.AddSignalR();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors("AllowAll");
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseDirectoryBrowser();

        app.UseRouting();

        // Middleware para log de requisições.
        app.UseSerilogRequestLogging();

        app.UseAuthentication();
        app.UseAuthorization();


        var supportedCultures = new[] { new CultureInfo("en-US"), new CultureInfo("pt-BR") };
        app.UseRequestLocalization(new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture("en-US"),
            SupportedCultures = supportedCultures,
            SupportedUICultures = supportedCultures
        });

        app.UseResponseCaching();

        app.MapHub<StockHub>("/stockhub");

        app.MapControllers();

        app.MapGraphQL();

        app.Run();

        Log.CloseAndFlush();
    }
}