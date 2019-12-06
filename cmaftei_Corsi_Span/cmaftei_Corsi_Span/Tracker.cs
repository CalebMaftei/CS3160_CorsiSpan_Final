using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cmaftei_Corsi_Span
{
    class Tracker
    {
        //Keeps track of the time that an item is logged
        private DateTime logDate;
        private Player target;
        private string targetLogLocation;
        private TimeSpan totalSessionTime;
        private TimeSpan lastEventTrigger;
        //Encrypts what is written at the bottom of the logs.
        private XOR_Encryption xorEncryption = new XOR_Encryption();

        public Tracker(Player target)
        {
            this.target = target;
            this.logDate = DateTime.Now;
            this.targetLogLocation = target.GetTrackerLog();
            this.totalSessionTime = TimeSpan.Zero;
        }

        //Saves when user starts a brand new session
        public void LogStartMessage()
        {
            string beginningMessage = String.Format(
                "========== BEGINNING SESSION || DATE: {0} ==========\n", this.logDate);
            try
            {
                using (StreamWriter sw = File.AppendText(this.targetLogLocation))
                {
                    sw.WriteLine(xorEncryption.EncryptDecrypt(beginningMessage, 7919));
                }
                this.lastEventTrigger = DateTime.Now.TimeOfDay;
                this.totalSessionTime = DateTime.Now.TimeOfDay;
            }
            catch(Exception e)
            {
                //No logs will be made for the temp account.
            }
        }

        //Log Message for when scoreboard was clicked
        public void LogScoreBoardCheckMessage()
        {
            string message = String.Format(
                "Time : {0}, User clicked the \"View ScoreBoard\" button. Ellapsed Time: {1}",
                DateTime.Now.TimeOfDay, DateTime.Now.TimeOfDay - this.lastEventTrigger);

            this.lastEventTrigger = DateTime.Now.TimeOfDay;

            try
            {
                using (StreamWriter sw = File.AppendText(this.targetLogLocation))
                {
                    sw.WriteLine(xorEncryption.EncryptDecrypt(message, 7919));
                }
            }
            catch
            {
                //Nothing will log.
            }
        }

        //Log Message for when user history was clicked
        public void LogUserHistoryCheckMessage()
        {
            string message = String.Format(
                "Time : {0}, User clicked the \"View User History\" button. Ellapsed Time: {1}",
                DateTime.Now.TimeOfDay, DateTime.Now.TimeOfDay - this.lastEventTrigger);

            this.lastEventTrigger = DateTime.Now.TimeOfDay;

            try
            {
                using (StreamWriter sw = File.AppendText(this.targetLogLocation))
                {
                    sw.WriteLine(xorEncryption.EncryptDecrypt(message, 7919));
                }
            }
            catch
            {
                //Nothing will log.
            }
            
        }

        //Log Message for when user starts a new sequence
        public void LogSequenceStartMessage()
        {
            string message = String.Format(
                "Time : {0}, User has started a new sequence. Ellapsed Time: {1}",
                DateTime.Now.TimeOfDay, DateTime.Now.TimeOfDay - this.lastEventTrigger);

            this.lastEventTrigger = DateTime.Now.TimeOfDay;

            try
            {
                using (StreamWriter sw = File.AppendText(this.targetLogLocation))
                {
                    sw.WriteLine(xorEncryption.EncryptDecrypt(message, 7919));
                }
            }
            catch
            {
                //nothing will log.
            }
        }

        //Log Message when User correctly guesses a sequence... include gameMode
        public void LogCorrectSequenceMessage(string gameMode)
        {
            string message = String.Format(
                "Time : {0}, Player replicated sequence CORRECTLY in MODE: {1}. Ellapsed Time since last event: {2}",
                DateTime.Now.TimeOfDay, gameMode, DateTime.Now.TimeOfDay - this.lastEventTrigger);

            this.lastEventTrigger = DateTime.Now.TimeOfDay;
            try
            {
                using (StreamWriter sw = File.AppendText(this.targetLogLocation))
                {
                    sw.WriteLine(xorEncryption.EncryptDecrypt(message, 7919));
                }
            }
            catch
            {
                //Nothing will log.
            }
            
        }

        //Log Message when User does not get sequence correct... include gameMode
        public void LogIncorrectSequenceMessage(string gameMode)
        {
            string message = String.Format(
                "Time : {0}, Player DID NOT replicated sequence CORRECTLY in MODE: {1}. Ellapsed Time: {2}",
                DateTime.Now.TimeOfDay, gameMode, DateTime.Now.TimeOfDay - this.lastEventTrigger);

            this.lastEventTrigger = DateTime.Now.TimeOfDay;

            try
            {
                using (StreamWriter sw = File.AppendText(this.targetLogLocation))
                {
                    sw.WriteLine(xorEncryption.EncryptDecrypt(message, 7919));
                }
            }
            catch
            {
                //Nothing will log.
            }
        }

        //Log Message for the different block clicks

        //Log Message when User logs out... include total time spent during session.
        public void LogEndMessage()
        {
            this.totalSessionTime = DateTime.Now.TimeOfDay - this.totalSessionTime;
            string beginningMessage = String.Format(
                "========== END SESSION || Total Time: {0} ==========", this.totalSessionTime);

            try
            {
                using (StreamWriter sw = File.AppendText(this.targetLogLocation))
                {
                    sw.WriteLine(xorEncryption.EncryptDecrypt(beginningMessage, 7919));
                }
                this.lastEventTrigger = DateTime.Now.TimeOfDay;
            }
            catch
            {
                //Nothing will log.
            }
        }
    }
}
