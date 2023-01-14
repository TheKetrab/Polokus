using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.Interfaces.Managers
{
    public interface IConnectionManager
    {
        
        bool ClientConnected { get; }
        void SetLostConnection();
        void SetHaveConnection();
        void DisableAdversaryTimer();
        void EnableAdversaryTimer();
        
    }
}
