using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using RestSharp;
using TogglAPI_RestSharp.Classes.Toggl;

namespace TogglAPI_RestSharp.Services.Toggl
{
    /// <summary>
    /// Api to handle the integration with Toggl
    /// </summary>
    class TogglApi
    {
        private const string TogglUrl = "https://www.toggl.com/api/v6";
        private const string ApiTokenString = "api_token";
        private string ApiToken { get; set; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="apiToken">The api token for the user</param>
        public TogglApi(string apiToken)
        {
            this.ApiToken = apiToken;
        }

        /// <summary>
        /// Gets the time entries in the date range provided
        /// </summary>
        /// <param name="start">The start date range</param>
        /// <param name="stop">The stop date range</param>
        /// <returns>A list of time entries</returns>
        public TimeEntries GetTimeEntries(string start, string stop)
        {
            //Create a new GET Request for the update, Get is the default
            RestRequest Req = new RestRequest();

            //Set the resource
            Req.Resource = "time_entries.json";
            
            //Add the start_date & end_Date parameters
            Req.AddParameter("start_date", start);
            Req.AddParameter("end_date", stop);

            //Execute the request
            return this.Execute<TimeEntries>(Req);
        }

        /// <summary>
        /// Gets the latest time entries
        /// </summary>
        /// <returns>A list of the latest TimeEntries</returns>
        public TimeEntries GetLatestTimeEntries()
        {
            //Create a new GET Request for the update, Get is the default
            RestRequest Req = new RestRequest();

            //Set the resource
            Req.Resource = "time_entries.json";

            //Execute the request
            return this.Execute<TimeEntries>(Req);
        }

        /// <summary>
        /// Updates all of the time entries provided
        /// </summary>
        /// <param name="val">the time entries you want to update</param>
        public void UpdateTimeEntries(TimeEntries val)
        {
            foreach (TimeEntry entry in val.data)
            {
                UpdateTimeEntry(entry);
            }
        }

        /// <summary>
        /// Updates the TimeEntry provided
        /// </summary>
        /// <param name="val">The timeEntry you want to update</param>
        /// <remarks>Currently does not work</remarks>
        public void UpdateTimeEntry(TimeEntry val)
        {
            //Create a new PUT Request for the update 
            RestRequest Req = new RestRequest(Method.PUT);
            
            //Set the resource, allowing for EntryID to be set based on the request
            Req.Resource = "time_entries/{EntryID}.json";

            //Set the RequestFormat to JSON, as required by Toggl
            Req.RequestFormat = DataFormat.Json;
            
            //Add the entryID of the entry to be updated to the URL
            Req.AddParameter("EntryID", val.id, ParameterType.UrlSegment);

            //Add the entry to be updated to the body
            Req.AddBody(val);

            //Execute the put request
            this.Execute<TimeEntry>(Req);
        }

        /// <summary>
        /// Executes the request provided
        /// </summary>
        /// <typeparam name="T">The type of the data to serialize for the request</typeparam>
        /// <param name="request">The request to be sent</param>
        /// <returns>Deserialized response of type T</returns>
        private T Execute<T>(RestRequest request) where T : new()
        {
            //create a new RestClient with the default Toggl url
            var client = new RestClient(TogglUrl);

            //create a new basic authenticator using the ApiToken for the user as the username
            //and 'api_token' as the password per the "HTTP Basic Auth with API token" located
            //https://www.toggl.com/public/api#api_token
            client.Authenticator = new HttpBasicAuthenticator(ApiToken,  ApiTokenString);
            
            //execute the request
            var response = client.Execute<T>(request);
            
            //if there was an exception raised, throw an exception
            if (response.ErrorException != null)
            {
                throw new Exception(string.Format("RestSharp could not execute the API call. API Response Content: {0}",
                                    response.Content),
                                    response.ErrorException);
            }

            //return the data from the response
            return response.Data;
        }
    }
}
