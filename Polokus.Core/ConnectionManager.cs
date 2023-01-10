using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using System.Timers;

namespace Polokus.Core
{
    public class ConnectionManager
    {

        private bool _clientConnected;
        private bool _lostConnection;

        public bool ClientConnected => _clientConnected;
        private System.Timers.Timer? _adversaryTimer;
        const int TIMER_DELAY = 1000;

        public ConnectionManager(bool isGUIAppManaged)
        {
            if (isGUIAppManaged)
            {
                _clientConnected = true;
                _lostConnection = false;
            }
            else
            {
                _adversaryTimer = CreateAdversaryTimer();
                _clientConnected = false;
                _lostConnection = true;
            }
        }

        private System.Timers.Timer CreateAdversaryTimer()
        {
            var timer = new System.Timers.Timer(TIMER_DELAY);

            timer.Elapsed += (s,e) => SetLostConnection();
            timer.AutoReset = true;
            timer.Enabled = true;

            return timer;
        }

        public void SetLostConnection()
        {
            if (_lostConnection)
            {
                _clientConnected = false;
            }
            else
            {
                _lostConnection = true;
            }
        }

        public void SetHaveConnection()
        {
            _lostConnection = false;
            _clientConnected = true;
        }

        public void DisableAdversaryTimer()
        {
            if (_adversaryTimer != null)
            {
                _adversaryTimer.Enabled = false;
            }
            _lostConnection = false;
            _clientConnected = true;
        }

        public void EnableAdversaryTimer()
        {
            if (_adversaryTimer != null)
            {
                _adversaryTimer.Enabled = true;
            }
            else
            {
                _adversaryTimer = CreateAdversaryTimer();
            }
        }

    }
}
