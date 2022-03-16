using Api.Middleware;
using API.Extensions;
using Persistence;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.ConfigureCors();
            services.AddPersistance(Configuration);
            services.AddAuthentication();
            services.ConfigureJwt(Configuration);
            services.AddHttpContextAccessor();
            services.ConfigureSwagger();
            services.AddControllers()
                .AddXmlDataContractSerializerFormatters();
            services.ConfigureMvc();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseHttpsRedirection();
            app.UseRouting();


            app.UseAuthentication();

            app.UseAuthorization();

            app.UseErrorHandler();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
