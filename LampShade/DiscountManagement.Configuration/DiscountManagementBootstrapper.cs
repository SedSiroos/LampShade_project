using DiscountManagement.Application;
using DiscountManagement.Application.Contract.CollegueDiscount;
using DiscountManagement.Application.Contract.CustomerDiscount;
using DiscountManagement.Domain.ColleagueDiscountAgg;
using Microsoft.Extensions.DependencyInjection;
using DiscountManagement.Domain.CustomerDiscountAgg;
using DiscountManagement.Infrastructure.EFCore;
using DiscountManagement.Infrastructure.EFCore.Repository;
using Microsoft.EntityFrameworkCore;

namespace DiscountManagement.Configuration
{
    public class DiscountManagementBootstrapper 
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddTransient<ICustomerDiscountApplication, CustomerDiscountApplication>();
            services.AddTransient<ICustomerDiscountRepository, CustomerDiscountRepository>();

            services.AddTransient<IColleagueDisCountApplication, ColleagueDisCountApplication>();
            services.AddTransient<IColleagueDiscountRepository, ColleagueDiscountRepository>();



            services.AddDbContext<DiscountContext>(x => x.UseSqlServer(connectionString));
        }
    }
}
