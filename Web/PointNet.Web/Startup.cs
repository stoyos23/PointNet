namespace PointNet.Web
{
    using System.Reflection;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Caching.SqlServer;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using PointNet.Data;
    using PointNet.Data.Common;
    using PointNet.Data.Common.Repositories;
    using PointNet.Data.Models;
    using PointNet.Data.Repositories;
    using PointNet.Data.Seeding;
    using PointNet.Services.Data;
    using PointNet.Services.Data.Catalog;
    using PointNet.Services.Data.ShoppingCart;
    using PointNet.Services.Mapping;
    using PointNet.Services.Messaging;
    using PointNet.Web.ViewModels;
    using ReflectionIT.Mvc.Paging;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(this.configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<CookiePolicyOptions>(
                options =>
                    {
                        options.CheckConsentNeeded = context => true;
                        options.MinimumSameSitePolicy = SameSiteMode.None;
                    });

            services.AddDistributedSqlServerCache(options =>
            {
                options.ConnectionString = this.configuration.GetConnectionString("DefaultConnection");
                options.SchemaName = "dbo";
                options.TableName = "CacheRecords";
            });
            services.AddSession();

            services.AddControllersWithViews(configure =>
            {
                configure.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });
            services.AddRazorPages();

            services.AddPaging();

            services.AddSingleton(this.configuration);

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            // Application services
            services.AddTransient<IEmailSender>(x => new SendGridEmailSender("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"));
            services.AddTransient<ISettingsService, SettingsService>();
            services.AddTransient<IProductsService, ProductsService>();
            services.AddTransient<ICategoriesService, CategoriesService>();
            services.AddTransient<IShoppingCartService, ShoppingCartService>();

            //services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddScoped<ShoppingCart>(sp => ShoppingCartService.GetCart(sp));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            // Seed data on application startup
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                if (env.IsDevelopment())
                {
                    dbContext.Database.Migrate();
                }

                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseSession();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(
                endpoints =>
                    {
                        endpoints.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapRazorPages();
                    });
        }
    }
}
