using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Model;
using Domain.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Service.Interfaces;
using Service.Implementation;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Repository.Interfaces;
using Repository.Repositories;
using File = Domain.Model.File;
using Repository.Common;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;

namespace DMSWebApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        //services.AddTransient<IRepositiry,Repository>();
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            IdentityBuilder builder = services.AddIdentityCore<User>(opt =>
            {
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequiredLength = 6;

            });
            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), builder.Services);
            builder.AddEntityFrameworkStores<Context>();
            builder.AddSignInManager<SignInManager<User>>();
            builder.AddRoleManager<RoleManager<IdentityRole>>();

            //Inject AppSettings
            services.Configure<ApplicationSettings>(Configuration.GetSection("ApplicationSettings"));
           // services.AddIdentityCore<User>().AddEntityFrameworkStores<Context>();
            services.AddTransient<IRepFile<File>, FileRepository>();


            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DMSWebApplication", Version = "v1" });
            });


            services.AddDbContext<Context>(item => item.UseSqlServer(
            Configuration.GetConnectionString("IdentityConnection"),
            b => b.MigrationsAssembly("Domain")));

            services.AddScoped<IAuth, ServiceAuthen>();
            services.AddScoped<IFile, ServiceFile>();


           

            /* services.Configure<IdentityOptions>(options =>
             {
                 options.Password.RequireNonAlphanumeric = false;
                 options.Password.RequireDigit = false;
                 options.Password.RequireLowercase = false;
                 options.Password.RequireUppercase = false;
             });*/

            services.AddCors();

            // JWT
            var key = Encoding.UTF8.GetBytes(Configuration["ApplicationSettings:JWT_Secret"].ToString());
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateAudience = false,
                        ValidateIssuer = false
                    }; 
                });

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            });

           

           /* services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = false;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ClockSkew = TimeSpan.Zero
                };
            });
           */


            // Upload File
            services.Configure<FormOptions>(o =>
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        }


        //  private void AddJwtBearer(Action<object> p){throw new NotImplementedException();}
         

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DMSWebApplication v1"));
            }

            //migration database 
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<Context>();
                context.Database.Migrate();
                context.Database.EnsureCreated();
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseHttpsRedirection();

            app.UseCors(builder =>
                builder.WithOrigins(Configuration["ApplicationSettings:Client_URL"].ToString())
                  .AllowAnyMethod()
                  .AllowAnyHeader());
            app.UseCors("MyPolicyCors");

            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            Task.Run(() => this.CreateRoles(roleManager)).Wait();
        }
        private async Task CreateRoles(RoleManager<IdentityRole> roleManager)
        {
            foreach (string role in this.Configuration.GetSection("Roles").Get<List<string>>())
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

    }
}
