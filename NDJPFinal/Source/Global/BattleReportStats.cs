using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDJPFinal.Source.Global
{
    internal class BattleReportStats
    {
        public static int AmmoShot;
        public static int AmmoHits;
        public static TimeSpan PlaySession;
        public static DateTime StartTime;
        public static DateTime Now;
        public static string MissionStatus;
        public static int HitsTaken;
        public static double Minutes;
        public static double Seconds;
        public static int TotalScore;

        public static void ResetBattleReport()
        {
            AmmoShot = 0;
            AmmoHits = 0;
            HitsTaken = 0;
            Seconds = 0;
            Minutes= 0;
            StartTime = DateTime.Now;
            MissionStatus = string.Empty;
        }

        public static void UpdateDateTime()
        {
            DateTime Now = DateTime.Now;
            PlaySession = Now - StartTime;
        }
    }
}
