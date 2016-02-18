using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procajas.Repository
{
    public interface IRepository
    {
        Task<T> Query<T>(IDictionary<string, string> filter = null);

        Task<bool> Persist<T>(T entity);
    }
}
