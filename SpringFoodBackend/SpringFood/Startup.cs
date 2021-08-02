using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SpringFoodBackend;
using SpringFoodBackend.Interfaces;
using SpringFoodBackend.Models.Auth;
using SpringFoodBackend.Repos;
using SpringFoodBackend.Services;

namespace SpringFood
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
            string name = "name=ConnectionStrings:Db1";
            services.AddScoped<SpringFoodService, SpringFoodService>();
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.Events = new JwtBearerEvents
                {
                    OnTokenValidated = (c =>
                    {
                        SpringFoodService serv = c.HttpContext.RequestServices.GetRequiredService<SpringFoodService>();
                        int id = int.Parse(c.Principal.Claims.Single(claim => claim.Type == ClaimTypes.NameIdentifier.ToString()).Value);
                        User foundUser = serv.GetUserById(id);
                        if (foundUser == null)
                        {
                            c.Fail("Unauthorized User");
                        }
                        return Task.CompletedTask;
                    })
                };
                o.RequireHttpsMetadata = false; //for development only, turn this off during production 
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(AppSettings.Secret)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

            }
            );

            services.AddControllers().AddNewtonsoftJson(o => o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
            services.AddDbContext<SpringFoodDbContext>((o) => o.UseSqlServer(name));
            services.AddTransient<IProduct, ProductRepo>();
            services.AddScoped<IUser, UserRepo>();
            services.AddScoped<ICategory, CategoryRepo>();
            services.AddScoped<IOrder, OrderRepo>();
            services.AddScoped<ISpringFoodService, SpringFoodService>();
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.UseCors(options =>
            //options.WithOrigins("http://localhost:4200")
            //.AllowAnyMethod()
            //.AllowAnyHeader());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(o => o.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader());


            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller=Home}/{action=Index}/{id?}");
            //});
        }
    }
}