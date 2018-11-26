using Microsoft.Extensions.DependencyInjection;
using Miniblog.Cqrs.Read.Queries;
using Miniblog.Cqrs.Read.QueryHandlers;

namespace Miniblog.Cqrs.Read
{
    public static class QueryRouting
    {
        public static IServiceCollection AddQueryRouting(this IServiceCollection services)
        {
            services.AddSingleton<QueryRouter>();

            return services;
        }
    }
}
