using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Common;
using DataBaseUtil;
using Core_v1;

namespace CenoCC {
    public class SessionTimer {
        private static System.Timers.Timer GetRecentNoAnswerRecord_Timer;
        public static void StartTimerServices() {
            GetRecentNoAnswerRecord_Timer = new System.Timers.Timer();
            GetRecentNoAnswerRecord_Timer.Interval = 5000;
            GetRecentNoAnswerRecord_Timer.Enabled = true;
            GetRecentNoAnswerRecord_Timer.Elapsed += new System.Timers.ElapsedEventHandler(GetRecentNoAnswerRecord_Timer_Elapsed);
            GetRecentNoAnswerRecord_Timer.Start();
        }

        static void GetRecentNoAnswerRecord_Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e) {
            ((System.Timers.Timer)sender).Stop();
            try {
                ///未接来电显示
                bool m_bShow = false;
                if (m_cPower.Has(Model_v1.m_mOperate.noanswer_number_show)) m_bShow = true;
                CCFactory.RecentNoanswerRecords = Call_Record.GetRecentNoanswerData(int.Parse(AgentInfo.AgentID), m_bShow);
            } catch(Exception ex) {
                Log.Instance.Error($"[CenoCC][SessionTimer][GetRecentNoAnswerRecord_Timer_Elapsed][Exception][{ex.Message}]");
            } finally {
                ((System.Timers.Timer)sender).Start();
            }
        }
    }
}
