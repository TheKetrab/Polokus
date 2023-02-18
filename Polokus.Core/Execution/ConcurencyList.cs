using Polokus.Core.Interfaces.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.Execution
{
    public class ConcurencyList<T> : IConcurencyList<T>
    {
        private object _lock = new object();
        private List<T> _list = new List<T>();

        public void Add(T item)
        {
            lock (_lock)
            {
                _list.Add(item);
            }
        }

        public bool Any()
        {
            lock (_lock)
            {
                return _list.Any();
            }
        }

        public bool Any(Predicate<T> predicate)
        {
            lock (_lock)
            {
                return _list.Any(x => predicate(x));
            }
        }

        public List<T> GetAll()
        {
            lock (_lock)
            {
                return new List<T>(_list);
            }
        }

        public int Remove(Predicate<T> predicate)
        {
            lock (_lock)
            {
                return _list.RemoveAll(predicate);
            }
        }
    }
}
