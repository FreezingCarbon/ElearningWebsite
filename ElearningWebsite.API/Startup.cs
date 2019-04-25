using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ElearningWebsite.API.Data;
using ElearningWebsite.API.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Imgur.API;
using Microsoft.AspNetCore.Http.Features;

namespace ElearningWebsite.API
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddJsonOptions(opt => {
                    opt.SerializerSettings.ReferenceLoopHandling =
                        Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            services.AddDbContext<DataContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"))
            );
            // config for automapper and add to startup
            var mapConfig = new MapperConfiguration(mc => {
                mc.AddProfile(new AutoMapperProfile());
            });
            IMapper mapper = mapConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddCors();
            services.Configure<CloudinarySettings>(Configuration.GetSection("CloudinarySettings"));
            // config max size form upload

            // Add resolver
            services.AddScoped(typeof(IAuthRepository<>), typeof(TeacherAuthRepository<>));
            services.AddScoped(typeof(IAuthRepository<>), typeof(StudentAuthRepository<>));
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<ITeacherRepository, TeacherRepository>();
            services.AddScoped<IVideoRepository, VideoRepository>();
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // app.UseHsts();
            }

            // app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
