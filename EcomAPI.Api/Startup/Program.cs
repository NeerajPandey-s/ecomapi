using EcomAPI.Api.Startup.ActionFilters;
using EcomAPI.Api.Startup.Setup.Middleware;
using EcomAPI.Common.ServiceInstallers.Extension;
using EcomAPI.Repository.Setup;
using EcomAPI.Service;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using NLog;
using NLog.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace EcomAPI.Api.Startup
{
    public class Program
    {
        //private readonly string AllOrigins = "AllOrigins";

        public Program(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers(options =>
            {
                options.Filters.Add(new ApiResponseFilter());
            });

            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
            });
            services.AddHttpContextAccessor();


            services.AddDbContext<AppDbContext>(optionsBuilder =>
            {
                var connectionString = Configuration["ConnectionString"];

                optionsBuilder.UseNpgsql(connectionString, b => b.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName));
            });

            services.Configure<ServiceConfig>(Configuration);
            //services.AddAWSLambdaHosting(LambdaEventSource.RestApi);

            services.RegisterApplicationServices(Assembly.GetExecutingAssembly(),
                Assembly.GetAssembly(typeof(ServiceConfig))!,
                Assembly.GetAssembly(typeof(AppDbContext))!);

            services.AddValidatorsFromAssemblyContaining<ServiceConfig>();

            services.AddEndpointsApiExplorer();
            AddSwagger(services);
            AddLogger(services);
            AddCORS(services);

            var jwtSettings = Configuration.GetSection("JWTSettings").Get<JWTSettings>()!;
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                //x.Audience = jwtSettings.Audience;//TODO: add the the jwt generation API
                //x.ClaimsIssuer = jwtSettings.Issuer;//TODO: add the the jwt generation API
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                    ClockSkew = TimeSpan.Zero,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuers = [jwtSettings.Issuer],
                    //ValidAudience = [] todo: add the the jwt generation API
                };
            });
            ValidationTypeExtension.InitValidationServiceProvider();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();

            app.UseMiddleware<ExceptionMiddleware>();
            app.UseCors(Configuration.GetSection("CORSSettings:DefaultPolicyName").Value!);

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("v1/swagger.json", "ecom v1"));

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }


        private void AddSwagger(IServiceCollection services) =>
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ecom", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n " +
                                    "Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\n" +
                                    "Example: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(document => new() { [new OpenApiSecuritySchemeReference("Bearer", document)] = [] });


            });

        private void AddLogger(IServiceCollection services)
        {
            var logLevel = (Microsoft.Extensions.Logging.LogLevel)Enum.Parse(typeof(Microsoft.Extensions.Logging.LogLevel), Configuration.GetSection("Logging:LogLevel:Default").Value!);
            var nLogConfig = Configuration.GetSection("NLog");
            services.AddLogging(logBuilder =>
            {
                logBuilder.ClearProviders();
                logBuilder.SetMinimumLevel(logLevel);

                LogManager.Configuration = new NLogLoggingConfiguration(nLogConfig);
                logBuilder.AddNLog(nLogConfig);
            });
        }
        private void AddCORS(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(Configuration.GetSection("CORSSettings:DefaultPolicyName").Value!,
                    builder => builder.WithOrigins(Configuration.GetSection("CORSSettings:ProdClientAppUrl").Get<string[]>()!)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                    );
            });
        }

        private record JWTSettings(string Secret, string Issuer);

    }
}