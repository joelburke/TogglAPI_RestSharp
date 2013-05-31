using System;
using System.Collections.Generic;

namespace TogglAPI_RestSharp.Classes.Toggl
{

    public class TimeEntry
    {
        public int duration { get; set; }
        public WorkSpace workspace { get; set; }
        public DateTime stop { get; set; }
        public int id { get; set; }
        public Boolean billable { get; set; }
        public DateTime start { get; set; }
        public List<string> tag_names { get; set; }
        public string description { get; set; }
        public Boolean ignore_start_and_stop { get; set; }
        public Project project { get; set; }
    }

    public class TimeEntries
    {
        public List<TimeEntry> data { get; set; }
        public DateTime related_data_updated_at { get; set; }
    }
}
