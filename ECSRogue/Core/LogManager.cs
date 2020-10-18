using ECSRogue.Managers.Events;
using ECSRogue.Partis;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECSRogue
{
    public class LogManager
    {
        public Dictionary<int, string> logMessages = new Dictionary<int, string>();

        public LogManager()
        {

        }
        public void Update(PartisInstance instance)
        {
            foreach (var logEvent in instance.GetEvents<LogEvent>())
            {
                logMessages.Add(logMessages.Count, logEvent.LogMessage);
            }          
        }
        public List<string> GetNewMessages(int previousFetchIndex)
        {
            List<string> newMessages = new List<string>();
            //should be wrapped in a try
            for (int newFetchIndex = previousFetchIndex; newFetchIndex < logMessages.Count; newFetchIndex++)
            {
                newMessages.Add(logMessages[newFetchIndex]);
            }

            return newMessages;
        }
    }
}
