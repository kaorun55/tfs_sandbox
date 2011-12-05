using System;
using System.Net;
using TfsAdventCalendar;

namespace AddWorkItem
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
                tfs.AddWorkItem();

                Console.WriteLine( "Success" );
            }
            catch ( Exception ex ) {
                Console.WriteLine( "Error : " + ex.Message );
            }
        }
    }
}
