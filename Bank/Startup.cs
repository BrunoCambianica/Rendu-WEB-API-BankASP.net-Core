using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Resources;

namespace Bank
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

            //connexion db MSSQLLocalDB
            var connection = @"Server=(localdb)\mssqllocaldb;Database=BankApplication;Trusted_Connection=True;ConnectRetryCount=0";
            services.AddDbContext<BankDbContext.BankContext>(options => options.UseSqlServer(connection));

            //db en memoire 
            //services.AddDbContext<BankDbContext.BankContext>(opt => opt.UseInMemoryDatabase());

            services.AddMvc();

            // test autorisations auth0
            //services.AddAuthorization(options => { options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme).RequireAuthenticatedUser().Build(); });


            //auth0
            string domain = $"https://{Configuration["brunoynov.eu.auth0.com"]}/";
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.Authority = domain;                               
                o.Audience = Configuration["localhost:61139/api"];  //identifier
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("user", policy => policy.Requirements.Add(new HasScopeRequirement("read:user", domain)));
                options.AddPolicy("admin", policy => policy.Requirements.Add(new HasScopeRequirement("admin:all", domain)));
            });

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // register the scope authorization handler
            services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();




            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //var options = new JwtBearerOptions
            //{
            //    Audience = Configuration["localhost:61139/api"],
            //    Authority = $"https://{Configuration["brunoynov.eu.auth0.com"]}/"
            //};

            //app.UseJwtBearerAuthentication(options);

            app.UseMvc(
                routes =>
                {
                    routes.MapRoute(
                        name: "default",
                        template: "{controller=User}/{action=GetAll}");
                }
            );
        }
    }
}
