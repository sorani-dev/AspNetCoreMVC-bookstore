using BookStoreMvc.Data;
using BookStoreMvc.Models;
using BookStoreMvc.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BookStoreMvc
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BookStoreContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
                );

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<BookStoreContext>();

            services.AddControllersWithViews();

#if DEBUG            
            services.AddRazorPages().AddRazorRuntimeCompilation();
            
            // Umcomment this code to disable client-side validations.
            //.AddViewOptions(option =>
            //{
            //    option.HtmlHelperOptions.ClientValidationEnabled = false;
            //});
#endif
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<ILanguageRepository, LanguageRepository>();
            services.AddSingleton<IMessageRepository, MessageRepository>();

            services.Configure<NewBookAlertConfig>("InternalBook", configuration.GetSection("NewBookAlert"));
            services.Configure<NewBookAlertConfig>("ThirdPartyBook", configuration.GetSection("ThirdPartyBookAlert"));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();

            //endpoints.MapControllerRoute(
            //        name: "Default",
            //        pattern: "{controller}/{action}/{id?}"
            //        );
                //endpoints.MapControllerRoute(
                //    name: "AboutUs",
                //    pattern: "about-us",
                //    defaults: new { controller = "Home", action = "AboutUs"}
                //    );
            });
        }
    }
}
