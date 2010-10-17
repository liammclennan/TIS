using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Norm;
using TodayIShall.Core.Queries;

namespace TodayIShall.Core.AppServices
{
    public interface IDocumentService
    {
        IMongo Mongo { get; }
        IQueryable<T> Query<T>(IMongoQuery<T> query);
        void Save<T>(T entity);
    }

    public class DocumentService : IDocumentService
    {
        public IMongo Mongo { get; private set; }

        public DocumentService(IMongo mongo)
        {
            Mongo = mongo;
        }

        public void Save<T>(T entity)
        {
            Mongo.GetCollection<T>().Save(entity);
        }

        public IQueryable<T> Query<T>(IMongoQuery<T> query)
        {
            return query.Execute(Mongo.GetCollection<T>().AsQueryable());
        }
    }

    
}
