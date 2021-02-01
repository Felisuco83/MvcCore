using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MvcCore.Data;
using MvcCore.Helpers;
using MvcCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MvcCore
{
    public class Startup
    {
        IConfiguration Configuration;
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            #region session
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(15);
            });
            #endregion
            String cadenasql = this.Configuration.GetConnectionString("cadenasqlhospitalcasa");
            string cadenaoracle = this.Configuration.GetConnectionString("cadenaoraclehospitalcasa");
            string cadenamysql = this.Configuration.GetConnectionString("cadenamysqlhospitalcasa");
            string cadenaazure = this.Configuration.GetConnectionString("cadenaazurehospital");
            services.AddSingleton<IConfiguration>(this.Configuration);
            services.AddSingleton<PathProvider>();
            services.AddSingleton<UploadService>();
            services.AddSingleton<MailService>();
            services.AddTransient<RepositoryJoyerias>();
            services.AddTransient<RepositoryAlumnos>();
            services.AddTransient<RepositoryUsuarios>();
            services.AddDbContext<HospitalContext>(options => options.UseSqlServer(cadenaazure));
            //services.AddTransient<IRepositoryDepartamentos>(z => new RepositoryDepartamentosOracle(cadenaoracle));
            //services.AddDbContext<DepartamentosContextMySQL>(options =>
            //options.UseMySQL(cadenamysql));
            //services.AddDbContextPool<DepartamentosContextMySQL>(options => options.UseMySql(cadenamysql, ServerVersion.AutoDetect(cadenamysql)) 
            //);

            services.AddTransient<IRepositoryDepartamentos, RepositoryDepartamentosSQL>();
            services.AddTransient<IRepositoryHospital, RepositoryHospitalSql>();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseStaticFiles();
            //to use session functionality
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }
}
