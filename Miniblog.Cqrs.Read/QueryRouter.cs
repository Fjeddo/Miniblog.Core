using System;
using System.Collections.Generic;
using System.Linq;
using Miniblog.Cqrs.Read.Queries;
using Miniblog.Cqrs.Read.QueryHandlers;
using Miniblog.Domain.Models;

namespace Miniblog.Cqrs.Read
{
    public class QueryRouter
    {
        private class QueryRegistry
        {
            public Type QueryType { get; }
            public Type QueryHandlerType { get; }
            public Type ReturnType { get; }

            public QueryRegistry(Type queryType, Type returnType, Type queryHandlerType)
            {
                QueryType = queryType;
                ReturnType = returnType;
                QueryHandlerType = queryHandlerType;
            }
        }

        private static readonly List<QueryRegistry> Registry = new List<QueryRegistry>
        {
            new QueryRegistry(typeof(PostsQuery), typeof(IEnumerable<Post>), typeof(PostsQueryHandler))
        };

        public TReturn ExecuteQuery<TReturn>(IQuery query)
        {
            var queryHandler = GetQueryHandler<TReturn>(query.GetType());
            return queryHandler.Execute(query);
        }
        

        private dynamic GetQueryHandler<TReturn>(Type queryType)
        {
            return Activator.CreateInstance(
                Registry.Single(x => x.QueryType == queryType && x.ReturnType == typeof(TReturn)).QueryHandlerType);;
        }
    }
}