using System.Collections.Generic;
using Miniblog.Cqrs.Read.Queries;
using Miniblog.Domain.Models;

namespace Miniblog.Cqrs.Read.QueryHandlers
{
    public class PostsQueryHandler : IQueryHandler<PostsQuery, IEnumerable<Post>>
    {
        public IEnumerable<Post> Execute(PostsQuery query)
        {
            throw new System.NotImplementedException();
        }
    }
}