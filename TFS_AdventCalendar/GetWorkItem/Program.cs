﻿using System;
using System.Net;
using TfsAdventCalendar;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace GetWorkItem
{
    class Program
    {
        static void Main( string[] args )
        {
            try {
                Uri uri = new Uri( "http://win7-vm:8080/tfs/" );
                NetworkCredential credential = new NetworkCredential( "hoge", "foo" );
                string collenctionName = "win7-vm\\DefaultCollection";
                string projectName = "TFS_API_SAMPLE";

                TfsClient tfs = new TfsClient( uri, credential,
                                    collenctionName, projectName );
                int id = tfs.AddWorkItem();
                WorkItem item = tfs.GetWorkItem( id );
                Console.WriteLine( item.Title + ":" + item.Description );
            }
            catch ( Exception ex ) {
                Console.WriteLine( "Error : " + ex.Message );
            }
        }
    }
}
