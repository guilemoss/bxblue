using Fuzzy.Api.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fuzzy.Api.Infra.CrossCutting.InversionOfControl
{
    public static class SqlDependency
    {
        public static void AddSqlDependency(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(
                    options => options.UseInMemoryDatabase("FuzzyTrader"));
        }
    }
}
