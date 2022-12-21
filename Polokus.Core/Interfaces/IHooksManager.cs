using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.Interfaces
{
    public interface IHooksManager : IHooksProvider
    {
        void RegisterHooksProvider(IHooksProvider hookProvider);
        void DeregisterHooksProvider(IHooksProvider hooksProvider);
        IEnumerable<IHooksProvider> GetHooksProviders();
    }
}
