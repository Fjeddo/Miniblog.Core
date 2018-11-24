using Miniblog.Cqrs.Read.Queries;
using Miniblog.Domain.Models;

namespace Miniblog.Cqrs.Read.QueryHandlers
{
    public class PostsQueryHandler : IQueryHandler<PostsQuery, Post>
    {
        public Post Execute(PostsQuery query)
        {
            throw new System.NotImplementedException();
        }
    }
}