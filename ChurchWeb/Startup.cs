using ChurchWebAuthorization;
using ChurchWebEntities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository;

namespace ChurchWeb
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
            DependencyInjection(services);

            AuthenticationServices(services);

            AuthorizationServices(services);

            MVCServices(services);            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            SecurityHeaders(app, env);

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            InitializeInMemoryDatabase(app, env);
        }

        private void AuthenticationServices(IServiceCollection services)
        {
            // https://docs.microsoft.com/en-us/aspnet/core/security/authentication/cookie?tabs=aspnetcore2x
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.AccessDeniedPath = "/Home/ErrorForbidden";
                    options.LoginPath = "/Home/ErrorNotLoggedIn";
                });
        }

        private void AuthorizationServices(IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(CustomClaims.ChurchMember, policy => policy.Requirements.Add(new ChurchMember()));
            });

            services.AddSingleton<IAuthorizationHandler, ChurchMemberHandler>();
        }

        private void DependencyInjection(IServiceCollection services)
        {
            services.AddDbContext<ChurchWebDbContext>(options => options.UseInMemoryDatabase("TestInMemory"));
            services.AddScoped<ICarouselItemRepository, CarouselItemRepository>();
            services.AddScoped<INavBarItemRepository, NavBarItemRepository>();
        }

        private void MVCServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                options.Filters.Add(new RequireHttpsAttribute());
            }
            );
        }

        private void InitializeInMemoryDatabase(IApplicationBuilder app, IHostingEnvironment env)
        {
            /*
             * does appear to continue to be the recommended approach for seeding from what I have seen Well, 
             * not exactly. Using scoped yes, but not inside Configure method, do to the way EF Core 2.0 does 
             * discovery and instantiation of DbContext at design time. See stackoverflow.com/a/45942026/455493 
             * for the currently recommended approach. If you keep doing seeding in Configure method, then 
             * running dotnet ef migrations or dotnet ef database update will also execute the seeding, 
             * something you pretty much don't want when running the command line tools – Tseng Sep 5 '17 at 23:10 
             * Just FYI; there's a CreateScope extension method directly on IServiceProvider, so you can 
             * cut .GetRequiredService<IServiceScopeFactory>() and just call that directly :) – khellang Sep 6 
             * '17 at 5:28
             * https://stackoverflow.com/questions/46063945/cannot-resolve-dbcontext-in-asp-net-core-2-0
             * https://stackoverflow.com/questions/45941707/why-remove-migration-run-my-app/45942026#45942026
             */
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                //using (var dbContext = app.ApplicationServices.GetService<ChurchWebDbContext>())
                using (var dbContext = serviceScope.ServiceProvider.GetService<ChurchWebDbContext>())
                {
                    dbContext.CarouselItems.Add(
                        new CarouselItem()
                        {
                            CarouselItemId = 1,
                            SortOrder = 0,
                            SourceImage = "/images/Chania.png",
                            AltImageString = "ASP.NET",
                            Link = "https://go.microsoft.com/fwlink/?LinkID=525028&clcid=0x409",
                            LinkHeading = "Learn how to build ASP.NET apps that can run anywhere.",
                            LinkName = "Learn More"
                        });

                    dbContext.CarouselItems.Add(
                        new CarouselItem()
                        {
                            CarouselItemId = 2,
                            SortOrder = 2,
                            SourceImage = "/images/Chania.png",
                            AltImageString = "ASP.NET",
                            Link = "https://go.microsoft.com/fwlink/?LinkID=525028&clcid=0x409",
                            LinkHeading = "Learn how to build ASP.NET apps that can run anywhere.",
                            LinkName = "Learn More"
                        });

                    dbContext.NavBarItems.Add(
                        new NavBarItem()
                        {
                            NavBarItemId = 1,
                            Controller = "Home",
                            Action = "Index",
                            Name = "Home"
                        });
                    dbContext.NavBarItems.Add(
                        new NavBarItem()
                        {
                            NavBarItemId = 4,
                            Controller = "Home",
                            Action = "Directory",
                            Name = "Directory"
                        });

                    dbContext.SaveChanges();
                }
            }
        }

        private void SecurityHeaders(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseHsts(options => options.MaxAge(days: 365).IncludeSubdomains());
            app.UseXXssProtection(options => options.EnabledWithBlockMode());
            app.UseXContentTypeOptions();
            app.UseReferrerPolicy(options => options.StrictOriginWhenCrossOrigin());
            app.UseCsp(options => options.UpgradeInsecureRequests());
        }
    }
}
