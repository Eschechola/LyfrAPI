using System.IO;
using System.Text;
using LyfrAPI.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;

namespace APILyfr
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
            services.AddDbContext<LyfrDBContext>(options => {
                    options.UseMySql(Configuration.GetConnectionString("MyConnection"));
                    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                }
            );

            services.AddDirectoryBrowser();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSingleton<IConfiguration>(Configuration);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
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
                app.UseHsts();
            }

            app.UseCors(builder =>
            {
                builder.WithOrigins("http://lyfrapi.com.br",
                                    "http://lyfradmin.vbweb.com.br",
                                    "http://lyfr.com.br",
                                    "http://www.lyfrapi.com.br",
                                    "http://www.lyfradmin.vbweb.com.br",
                                    "http://www.lyfr.com.br",
                                    "http://localhost:49642")
                                    .AllowAnyHeader()
                                    .AllowAnyMethod();
            });

            app.UseHttpsRedirection();
            app.UseAuthentication();

            var provider = new FileExtensionContentTypeProvider();
            // Add new mappings
            provider.Mappings[".epub"] = "application/epub+zip";

            app.UseStaticFiles(new StaticFileOptions
            {
                ContentTypeProvider = provider

            });

            //código para habilitar a navegação de arquivos entre a API
            //app.UseDirectoryBrowser(new DirectoryBrowserOptions
            //{
            //    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")),
            //    RequestPath = "/navigator"
            //});

            app.UseMvc();  
        }
    }
}
