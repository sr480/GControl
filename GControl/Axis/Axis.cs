using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;

namespace GControl.Axis
{
    public class Axis : INotifyPropertyChanged, ITickable
    {
        private int _MinTicksPerPulse;
        private int _PulsePerUnit;
        private double _Current;
        private string _Name;
        private double _Max;
        private double _Min;
        private Queue<PlanPoint> _plan = new Queue<PlanPoint>();
        private PlanPoint _currentPlanPoint = null;
        
        public double Min
        {
            get
            {
                return _Min;
            }
            set
            {
                if (_Min == value)
                    return;
                _Min = value;
                OnPropertyChanged("Min");
            }
        }
        public double Max
        {
            get
            {
                return _Max;
            }
            set
            {
                if (_Max == value)
                    return;
                _Max = value;
                OnPropertyChanged("Min");
            }
        }
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                if (_Name == value)
                    return;
                _Name = value;
                OnPropertyChanged("Name");
            }
        }
        public double Current
        {
            get
            {
                return _Current;
            }
            set
            {
                if (_Current == value)
                    return;
                _Current = value;
                OnPropertyChanged("Current");
            }
        }

        public int PulsePerUnit
        {
            get
            {
                return _PulsePerUnit;
            }
            set
            {
                if (_PulsePerUnit == value)
                    return;
                _PulsePerUnit = value;
            }
        }
        public int MinTicksPerPulse
        {
            get
            {
                return _MinTicksPerPulse;
            }
            set
            {
                _MinTicksPerPulse = value;
                _currentTickCounter = TicksPerPulse;
            }
        }

        private int TicksPerPulse
        {
            get
            {
                if (_currentPlanPoint == null)
                {
                    if (_plan.Count > 0)
                        _currentPlanPoint = _plan.Dequeue();
                    else
                        return MinTicksPerPulse;
                }

                return (int)(_currentPlanPoint.Time / (_currentPlanPoint.Destination * (double)PulsePerUnit));
            }
        }
        
        int _currentTickCounter;

        public Axis()
        {   }
                
        public void Tick()
        {
            if (--_currentTickCounter <= 0)
            {
                _currentTickCounter = TicksPerPulse;
                if (_currentPlanPoint == null)
                    return;               
                
                _currentPlanPoint.Time -= Ticker.TICKDuration;

                if (_currentPlanPoint.Destination > 0)
                {
                    _Current += 1.0 / PulsePerUnit;
                    _currentPlanPoint.Destination -= 1.0 / PulsePerUnit;                    
                }
                else if (_currentPlanPoint.Destination < 0)
                {
                    _Current -= 1.0 / PulsePerUnit;
                    _currentPlanPoint.Destination += 1.0 / PulsePerUnit;
                }
                else
                    _currentPlanPoint = null;
            }
        }

        public void AddPlan(PlanPoint point)
        {
            _plan.Enqueue(point);            
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
