using System;
using System.Collections.Generic;

namespace TogglAPI_RestSharp.Classes.Toggl
{
    public class WorkSpace
    {
        public WorkSpace() { }
        public string name { get; set; }
        public int id { get; set; }
    }


    public class WorkSpaces
    {
        public WorkSpaces() { }
        public List<WorkSpace> data { get; set; }
        public DateTime related_data_updated_at { get; set; }
    }
}
