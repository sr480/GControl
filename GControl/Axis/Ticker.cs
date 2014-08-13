using System;
using System.Collections.Generic;
using System.Text;

namespace GControl.Axis
{
    public class Ticker
    {
        public const int TICKDuration = 5;
        //private System.Timers.Timer _tick;

        private List<ITickable> _Subscribers;

        public Ticker()
        {
            _Subscribers = new List<ITickable>();
            
            //_tick = new System.Timers.Timer(TICKDuration);
            //_tick.Elapsed += _tick_Elapsed;
        }

        public void Register(ITickable subscriber)
        {
            _Subscribers.Add(subscriber);
        }

        public void Start()
        {
            //_tick.Start();
            System.Threading.Thread thrd = new System.Threading.Thread(Tick);
            thrd.Start();
        }
        private void Tick()
        {
            while(true)
            {
                foreach (var subscriber in _Subscribers)
                    subscriber.Tick();
                System.Threading.Thread.Sleep(TICKDuration);
            }
        }

        void _tick_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            foreach (var subscriber in _Subscribers)
                subscriber.Tick();
        }
    }
}
