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
            X.Name = "X";
            Y = new Axis.Axis();
            Y.PulsePerUnit = 2;
            Y.MinTicksPerPulse = 1;
            Y.Name = "Y";
            _ticker.Register(X);
            _ticker.Register(Y);
            _ticker.Start();

            Axis.MotionHelper.Linear2D(X, 50, Y, 20, 20.0);
            Axis.MotionHelper.Linear2D(X, 20, Y, 20, 20.0);
            Axis.MotionHelper.Linear2D(X, 20, Y, 50, 20.0);


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
