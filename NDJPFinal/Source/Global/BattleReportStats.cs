/*
 * Author : Nathan Dinh
 * 
 * Revision: Nathan Dinh Decemeber 10
 */
using System;

namespace NDJPFinal.Source.Global
{
    internal class BattleReportStats
    {
        // Various static fields to hold battle-related statistics
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

        // Method to reset all the statistics to their initial values
        public static void ResetBattleReport()
        {
            AmmoShot = 0;
            AmmoHits = 0;
            HitsTaken = 0;
            Seconds = 0;
            Minutes = 0;
            StartTime = DateTime.Now;
            Now = DateTime.Now;
            MissionStatus = string.Empty;
        }

        // Method to update the PlaySession time duration
        public static void UpdateDateTime()
        {
            // Update the current time
            DateTime Now = DateTime.Now;

            // Calculate the PlaySession time duration since StartTime
            PlaySession = Now - StartTime;
        }
    }
}
