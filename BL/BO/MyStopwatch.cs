using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BO
{
    class MyStopwatch
    {
        static MyStopwatch m_instance;
        Stopwatch m_stopwatch;
        Thread m_thread;
        bool m_flag;
        TimeSpan m_startTime;
        int m_rate;
        public Action<TimeSpan> m_action;

        #region Sinelton
        public static MyStopwatch Instance
        {
            get
            {
                if (m_instance == null)
                    m_instance = new MyStopwatch();
                return m_instance;
            }
        }
        #endregion
        public void StartCouting(TimeSpan startTime, int rate)
        {
            m_startTime = startTime;
            m_rate = rate;
            m_flag = true;

            m_stopwatch = new Stopwatch();
            m_stopwatch.Start();
            m_thread = new Thread(Updator);
            m_thread.Start();
        }
        void Updator()
        {
            while(m_flag)
            {
                m_action?.Invoke(TimeSpan.FromMilliseconds(m_stopwatch.Elapsed.TotalMilliseconds * m_rate) + m_startTime);
                Thread.Sleep(100);
            }
        }
        public void StopCounting()
        {
            m_flag = false;
            m_stopwatch.Stop();
            m_thread.Abort();
        }
    }
}
