using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;

namespace GControl
{
    class MainViewModel : INotifyPropertyChanged
    {
        private Axis.Ticker _ticker;
        public Axis.Axis X
        { get; set; }
        public Axis.Axis Y
        { get; set; }
        public MainViewModel()
        {
            _ticker = new Axis.Ticker();

            X = new Axis.Axis();
            X.PulsePerUnit = 2;
            X.MinTicksPerPulse = 1;
            Y = new Axis.Axis();
            Y.PulsePerUnit = 2;
            Y.MinTicksPerPulse = 1;
            _ticker.Register(X);
            _ticker.Register(Y);
            _ticker.Start();

            X.AddPlan(new Axis.PlanPoint(50, 30000.0 /Axis.Ticker.TICKDuration));
            Y.AddPlan(new Axis.PlanPoint(50, 30000.0 / Axis.Ticker.TICKDuration));
        }
        #region INotifyPropertyChanged
        private SynchronizationContext _synchronizationContext = SynchronizationContext.Current;
        private void OnPropertyChanged(string propertyName)
        {
            if (SynchronizationContext.Current != _synchronizationContext)
                RaisePropertyChanged(propertyName);
            else
                _synchronizationContext.Post(RaisePropertyChanged, propertyName);
        }
        private void RaisePropertyChanged(object param)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs((string)param));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}
