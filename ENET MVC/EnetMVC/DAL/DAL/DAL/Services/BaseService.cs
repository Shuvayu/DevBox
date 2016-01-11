using System;

namespace DAL.Services
{
    public class BaseService : IDisposable
    {
        protected EnetContext Context;

        public BaseService()
        {
            Context = new EnetContext();
        }

        public BaseService(EnetContext context)
        {
            this.Context = context;
        }
        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
