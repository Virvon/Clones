using System;

namespace Clones.GameLogic
{
    public class GameTimer
    {
        private bool _isStarted = false;
        private DateTime _startDate;

        public int LastMeasurement { get; private set; }

        public void Start()
        {
            if (_isStarted)
                return;

            _isStarted = true;
            _startDate = DateTime.Now;
        }

        public int Stop()
        {
            if (_isStarted == false)
                return 0;

            _isStarted = false;
            LastMeasurement = (int)(DateTime.Now - _startDate).TotalSeconds;

            return LastMeasurement;
        }
    }
}
