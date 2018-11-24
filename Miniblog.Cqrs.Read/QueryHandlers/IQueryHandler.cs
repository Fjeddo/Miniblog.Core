using Miniblog.Cqrs.Read.Queries;

namespace Miniblog.Cqrs.Read.QueryHandlers
{
    public interface IQueryHandler<in TQuery, out TReturn> where TQuery : IQuery
    {
        TReturn Execute(TQuery query);
    }
}