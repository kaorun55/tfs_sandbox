using System;
using System.IO;
using System.Net;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using TfsAdventCalendar;

namespace AddAttachments
{
    class Program
    {
        static void Main( string[] args )
        {
            Uri uri = new Uri( "http://localhost:8080/tfs/" );
            NetworkCredential credential = new NetworkCredential( "hoge", "foo" );
            string collenctionName = "localhost\\DefaultCollection";
            string projectName = "TFS_API_SAMPLE";

            TfsClient tfs = new TfsClient( uri, credential,
                                collenctionName, projectName );

            // 添付ファイル付の作業項目を作成する
            WorkItem newItem = tfs.GetNewWorkItem( "タスク" );
            newItem.Title = "作業項目の概要です";
            newItem.Description = "作業項目の詳細です";
            newItem.Attachments.Add( new Attachment(
                @"Z:\kaorun\work\tfs_sandbox\TFS_AdventCalendar\AddAttachments\Program.cs" 
                ) );
            newItem.Save();

            // 添付ファイルをダウンロードする
            WebClient request = new WebClient();
            request.Credentials = CredentialCache.DefaultCredentials;

            foreach ( Attachment attachment in newItem.Attachments ) {
                Console.WriteLine( attachment.Name );

                request.DownloadFile( attachment.Uri,
                    Path.Combine( @"C:\Users\kaorun55\Desktop", attachment.Name ) );
            }
        }
    }
}
