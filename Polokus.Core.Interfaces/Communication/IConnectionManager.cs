using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polokus.Core.Interfaces.Communication
{
    /// <summary>
    /// Connection Manager is object that controls if any GUI client is connected to master.
    /// </summary>
    public interface IConnectionManager
    {
        bool ClientConnected { get; }
        void SetLostConnection();
        void SetHaveConnection();
        void DisableAdversaryTimer();
        void EnableAdversaryTimer();
    }
}
