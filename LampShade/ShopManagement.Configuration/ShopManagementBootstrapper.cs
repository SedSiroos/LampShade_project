using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shopmanagement.infrastructure.EFCore.Repository;
using ShopManagement.Application;
using ShopManagement.Application.Contracts.IApplication;
using ShopManagement.Domain.ProductCategoryAgg;
using Shopmanagement.infrastructure.EFCore;

namespace ShopManagement.Configuration
{
    public class ShopManagementBootstrapper
    {
        public static void Configure(IServiceCollection services,string connectionString)
        {
            services.AddTransient<IProductCategoryApplication, ProductCategoryApplication>();
            services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();

            services.AddDbContext<ShopContext>(option =>
                option.UseSqlServer(connectionString));
        }
    }
}
