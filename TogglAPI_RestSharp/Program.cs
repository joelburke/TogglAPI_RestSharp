using System;
using System.Collections.Generic;
using System.Linq;
using TogglAPI_RestSharp.Classes.Toggl;
using TogglAPI_RestSharp.Services.Toggl;

namespace TogglAPI_RestSharp
{
    class Program
    {
        static void Main(string[] args)
        {

            if (args.Count() != 1)
            {
                Console.WriteLine("Please provide your Toggle API token from your account");
            }
            else
            {
                //create a new togglAPI passing in the first argument as the API token
                TogglApi api = new TogglApi(args[0]);

                //grab all tasks from the beginning of the day until now
                string start = DateTime.Today.ToString("o");// "2013-04-25";
                string end = DateTime.Now.ToString("o");// "2013-04-26";
                TimeEntries Result = api.GetTimeEntries(start, end);

                Output(Result);
            }
        }
        private static void Output(TimeEntries entries){
            foreach (TimeEntry entry in entries.data)
            {
                string output = string.Format("Desc: {0} Start: {1} Stop: {2} ID: {3} Duration: {4}",
                                  entry.description,
                                  entry.start,
                                  entry.stop,
                                  entry.id,
                                  entry.duration);
                Console.WriteLine(output);
            }
        }
    }
}

 

 
