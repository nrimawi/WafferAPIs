
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using WafferAPIs.Models;
using Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WafferAPIs.DAL.Entites;
using WafferAPIs.DAL.Repositories;
using WafferAPIs.Dbcontext;
using WafferAPIs.DAL.Helpers.EmailAPI.Model;
using WafferAPIs.DAL.Helpers.EmailAPI;
using WafferAPIs.DAL.Helpers.EmailAPI.Service;
using WafferAPIs.DAL.Helpers.SMSAPI.Model;
using WafferAPIs.DAL.Helpers.SMSAPI;
using WafferAPIs.Models.Dtos;
using WafferAPIs.DAL.Entities;
using WafferAPIs.Utilites;
using System.Reflection;
using System.IO;


namespace WafferAPIs
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            #region  Dependency injection 
            services.AddScoped<ISellerRepository, SellerRepository>();
            services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<ICutomizePackegeManager, CutomizePackegeManager>();

            #endregion

            #region Mapper 
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Seller, SellerData>().ReverseMap();
                cfg.CreateMap<Category, CategoryData>().ReverseMap();
                cfg.CreateMap<Item, ItemData>().ReverseMap();


                cfg.CreateMap<SubCategory, SubCategoryData>().ForMember(dest =>
                dest.Fetures, opt => opt.MapFrom(src => new FeatureMapper().ToDto(src.Fetures)));
                cfg.CreateMap<SubCategoryData, SubCategory>().ForMember(dest =>
                dest.Fetures, opt => opt.MapFrom(src => new FeatureMapper().ToEntity(src.Fetures)));


            });
            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);
            #endregion

            #region DataBase

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            #endregion

            #region SMS service
            services.Configure<SMSSettings>(Configuration.GetSection("SMSSettings"));
            services.AddTransient<ISMSSender, SMSSender>();
            #endregion

            #region Email service
            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
            services.AddScoped<IEmailSender, EmailSender>();
            #endregion

            #region Authentication
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = Configuration["JWT:ValidIssuer"],
                    ValidAudience = Configuration["JWT:ValidAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:SecretKey"]))
                };
            });
            #endregion

            #region CORS
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyOrigin();
                        builder.AllowAnyMethod();
                        builder.AllowAnyHeader();
                    });
            });
            #endregion

            #region Swagger

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyOrigin();
                        builder.AllowAnyMethod();
                        builder.AllowAnyHeader();
                    });
            });
            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WafferAPIs", Version = "v1", Description = "These services were built for Waffer System" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter `Bearer`[space] and then valid token in the text input below"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme

                        {
                            Reference= new OpenApiReference
                            {
                                 Type=ReferenceType.SecurityScheme,
                                  Id="Bearer"
                            }
                        },

                        new string[]
                        {

                        }
                    }
                });




            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WafferAPIs v1"));



            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseCors(builder =>
            {
                builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
