using System;
using System.Net;
using TfsAdventCalendar;

namespace GetUserList
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

                Console.WriteLine( "" );

                // コレクションのユーザー一覧
                var users = tfs.GetUserList( @"[DefaultCollection]\プロジェクト コレクション管理者" );
                foreach ( var user in users ) {
                    Console.WriteLine( user );
                }
                Console.WriteLine( "" );

                // プロジェクトのユーザー一覧
                users = tfs.GetUserList( @"[TFS_API_SAMPLE]\プロジェクト管理者" );
                foreach ( var user in users ) {
                    Console.WriteLine( user );
                }
                Console.WriteLine( "" );

                Console.WriteLine( "Success" );
            }
            catch ( Exception ex ) {
                Console.WriteLine( "Error : " + ex.Message );
            }
        }
    }
}
