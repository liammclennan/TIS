using System.Linq;

namespace TodayIShall.Core.Queries
{
    public interface IMongoQuery<T>
    {
        IQueryable<T> Execute(IQueryable<T> queryable);
    }
}