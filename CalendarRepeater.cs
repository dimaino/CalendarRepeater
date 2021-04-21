﻿using System;
using System.Threading;

namespace CalendarRepeater
{
    public class CalendarRepeater
    {
        private Timer _Timer;
        private DateTime _RepeatingDate;

        private DateTime _OriginalStartDate;
        private int _Minutes;
        private bool _Monthly;
        private bool _Weekly;
        private bool _Daily;
        private bool _Hourly;
        private bool _Minutely;

        private bool _Debug;

        public int Years { get; set; }
        public int Months { get; set; }
        public int Days { get; set; }
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }
        public bool Monthly { get; set; }
        public bool Weekly { get; set; }
        public bool Daily { get; set; }
        public bool Hourly { get; set; }

        public CalendarRepeater()
        {
            _RepeatingDate = DateTime.Now;
            _OriginalStartDate = _RepeatingDate;
            _Monthly = false;
            _Weekly = false;
            _Daily = false;
            _Hourly = false;
            _Minutely = false;
            _Debug = false;
        }

        public void CreateCalendarRepeater(Action RunnerMethod)
        {
            _Timer = new Timer(x => { RunnerMethod(); RepeaterFunctionRunner(); }, null, Timeout.Infinite, Timeout.Infinite);
        }

        public void Debugging()
        {
            _Debug = true;
        }

        public void StartDateTimeForRepeater(int year, int month, int day, int hour, int minute, int second)
        {
            _RepeatingDate = new DateTime(year, month, day, hour, minute, second);

            if(DateTime.Now > _RepeatingDate)
            {
                Console.WriteLine("The Start Date needs to be in the future. Defaulting to DateTime.Now");
                _RepeatingDate = DateTime.Now;
            }
        }

        public void RepeaterMonthly(int day, int hour, int minute)
        {
            _Monthly = true;
            _RepeatingDate = new DateTime(_RepeatingDate.Year, _RepeatingDate.Month, day, hour, minute, 0);
        }

        public void RepeaterWeekly(int day, int hour, int minute)
        {
            _Weekly = true;
            _RepeatingDate = new DateTime(_RepeatingDate.Year, _RepeatingDate.Month, day, hour, minute, 0);
        }

        public void RepeatDaily(int hour, int minute)
        {
            _Daily = true;
            _RepeatingDate = new DateTime(_RepeatingDate.Year, _RepeatingDate.Month, _RepeatingDate.Day, hour, minute, 0);
        }

        public void RepeatHouly(int minute)
        {
            _Hourly = true;
            _RepeatingDate = new DateTime(_RepeatingDate.Year, _RepeatingDate.Month, _RepeatingDate.Day, _RepeatingDate.Hour, minute, 0);
        }

        public void RepeatEveryNthMinutes(int minute)
        {
            _Minutely = true;
            _Minutes = minute;
        }

        public void RepeaterFunctionRunner()
        {
            Setup_CalendarRepeater();
            Console.WriteLine("Type the string exit to leave.");
        }

        public void Setup_CalendarRepeater()
        {
            DateTime currentTime = DateTime.Now;

            if(currentTime > _RepeatingDate)
            {
                if(_Minutely == true)
                {
                    _RepeatingDate = _RepeatingDate.AddMinutes(_Minutes);
                }
                else if(_Hourly == true)
                {
                    _RepeatingDate = _RepeatingDate.AddHours(1);
                }
                else if(_Daily == true)
                {
                    _RepeatingDate = _RepeatingDate.AddDays(1);
                }
                else if(_Weekly == true)
                {
                    _RepeatingDate = _RepeatingDate.AddDays(7);
                }
                else if(_Monthly == true)
                {
                    _RepeatingDate = _RepeatingDate.AddMonths(1);
                }
            }

            // Sets the Milliseconds so no overlap occurs over time. Eventually the time would show 
            _RepeatingDate = new DateTime(_RepeatingDate.Year, _RepeatingDate.Month, _RepeatingDate.Day, _RepeatingDate.Hour, _RepeatingDate.Minute, _RepeatingDate.Second, 0);
            currentTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, currentTime.Hour, currentTime.Minute, currentTime.Second, 0);

            TimeSpan tickTime = _RepeatingDate - currentTime;

            // Debug
            if(_Debug == true)
            {
                Console.WriteLine($"currentTime: {currentTime}");
                Console.WriteLine($"timerRunningTime: {_RepeatingDate}");
                Console.WriteLine($"tickTime: {tickTime}");
            }
            
            _Timer.Change(tickTime, TimeSpan.FromMilliseconds(1));
        }
    }
}
