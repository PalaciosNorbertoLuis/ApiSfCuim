using ApiSfCuim.Data.Context;
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
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiSfCuim;
using System.Text;

namespace ApiSfCuim
{
    public class Startup
    {
        private readonly string  _MyCors = "MyCors"; 
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiSfCuim", Version = "v1" });
            });

            services.AddCors(options => 
            {
                options.AddPolicy(name: _MyCors, builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    //builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
                    //                                                    .AllowAnyOrigin()
                    //                                                    .AllowAnyHeader()
                    //                                                    .AllowAnyMethod();
                });
            });

            services.AddDbContext<RenarContext>(options => options.UseSqlServer(Configuration.GetConnectionString("RenarConnection")));
            services.AddDbContext<SigimacContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SigimacConnection")));
            services.AddDbContext<RenarAuxiliarContext>(options => options.UseSqlServer(Configuration.GetConnectionString("RenarAuxiliarConnection")));
 
            var key = Encoding.UTF8.GetBytes(Configuration.GetValue<string>("tokenSettings:SecurityKey"));
            var ex = Configuration.GetValue<string>("tokenSettings:SecurityKey");
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                //x.RequireHttpsMetadata = !environment.IsDevelopment();
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiSfCuim v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(_MyCors);

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
