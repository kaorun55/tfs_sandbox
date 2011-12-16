using System;
using System.Net;
using TfsAdventCalendar;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace ListWorkItemFields
{
    class Program
    {
        static void Main( string[] args )
        {
            try {
                Uri uri = new Uri( "http://localhost:8080/tfs/" );
                NetworkCredential credential = new NetworkCredential( "hoge", "foo" );
                string collenctionName = "localhost\\DefaultCollection";
                string projectName = "TFS_API_SAMPLE";

                TfsClient tfs = new TfsClient( uri, credential,
                                    collenctionName, projectName );
                int id = tfs.AddWorkItem();
                WorkItem item = tfs.GetWorkItem( id );

                foreach ( Field field in item.Fields ) {
                    Console.WriteLine( field.Name + ":" + item[field.Name] );
                }
            }
            catch ( Exception ex ) {
                Console.WriteLine( "Error : " + ex.Message );
            }
        }
    }
}
