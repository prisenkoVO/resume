using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using Ingoport.Interfaces;
using Ingoport.Models;
using Ingoport.Services;

namespace Ingoport
{
    public class Startup
    {
        private readonly CheckFileService file;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            this.file = new CheckFileService();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddCors();
            string SecurityKey = file.GetFile()["key"];

            var symSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityKey));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "smesk.in",
                        ValidAudience = "readers",
                        IssuerSigningKey = symSecurityKey,
                    });

            services.AddDistributedMemoryCache();
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
                /*Make the session cookie essential*/
                options.Cookie.IsEssential = true;
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<UserContext>(opts => opts.UseSqlServer(Configuration["ConnectionString:internPlatformDB"]));

            // Add Interfaces
            services.AddTransient<INews, NewsService>();
            services.AddTransient<IPersonalArea, PersonalAreaService>();
            services.AddTransient<IQuestionsAndAnswers, QuestionsAndAnswersService>();
            services.AddTransient<IAuthorization, AuthService>();
            services.AddTransient<IRegistration, RegistrationService>();
            services.AddTransient<IRandomCoffee, RandomCoffeeService>();
            services.AddTransient<IBot, ChatBotServices>();
            services.AddTransient<ISearch, SearchService>();
            services.AddTransient<ICheckFile, CheckFileService>();
            services.AddTransient<ISetting, SettingService>();


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCookiePolicy();
            app.UseSession();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                });

            }
            else
            {
                //app.UseExceptionHandler("/Error");
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseCors(builder =>
              builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                if (env.IsDevelopment())
                {
                    spa.Options.SourcePath = "ClientApp";
                    spa.Options.StartupTimeout = new TimeSpan(hours: 0, minutes: 2, seconds: 0);
                    spa.UseAngularCliServer("start");
                }
                else
                {
                    spa.Options.SourcePath = "ClientApp/dist";
                }
            });
        }
    }
}
