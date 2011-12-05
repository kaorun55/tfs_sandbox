using System;
using System.Collections.ObjectModel;
using System.Net;

using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Framework.Client;
using Microsoft.TeamFoundation.Framework.Common;
using Microsoft.TeamFoundation.WorkItemTracking.Client;


namespace TfsAdventCalendar
{
    public class TfsClient
    {
        TfsConfigurationServer configurationServer;
        WorkItemStore workItemStore;
        Project teamProject;

        public TfsClient( Uri tfsUri, NetworkCredential credential,
                                string collenctionName, string projectName )
        {
            configurationServer =
                new TfsConfigurationServer( tfsUri, credential );
            GetTfsProject( collenctionName, projectName );
        }

        // TFSの情報を取得する
        void GetTfsProject( string collenctionName, string projectName )
        {
            // TFS内の、コレクションのリストを取得する
            ReadOnlyCollection<CatalogNode> collectionNodes =
                configurationServer.CatalogNode.QueryChildren(
                    new[] { CatalogResourceTypes.ProjectCollection },
                    false, CatalogQueryOptions.None );

            // コレクションのリストから、必要なコレクションを取得する
            foreach ( CatalogNode collectionNode in collectionNodes ) {
                Guid collectionId =
                    new Guid( collectionNode.Resource.Properties["InstanceId"] );
                TfsTeamProjectCollection teamProjectCollection =
                    configurationServer.GetTeamProjectCollection( collectionId );
                Console.WriteLine( "Collection: " + teamProjectCollection.Name );

                // 登録するチームコレクションでフィルタする
                if ( teamProjectCollection.Name != collenctionName ) {
                    continue;
                }

                // コレクション内の、チームプロジェクトのリストを取得する
                ReadOnlyCollection<CatalogNode> projectNodes =
                    collectionNode.QueryChildren(
                        new[] { CatalogResourceTypes.TeamProject },
                        false, CatalogQueryOptions.None );

                // チームプロジェクトのリストから、必要なプロジェクトを取得する
                foreach ( CatalogNode projectNode in projectNodes ) {
                    Console.WriteLine( " Team Project: " +
                        projectNode.Resource.DisplayName );

                    // 登録するプロジェクトでフィルタする
                    if ( projectNode.Resource.DisplayName != projectName ) {
                        continue;
                    }

                    // チームプロジェクトを取得する
                    workItemStore = teamProjectCollection.GetService<WorkItemStore>();
                    teamProject = workItemStore.Projects[projectNode.Resource.DisplayName];

                    return;
                }
            }

            throw new Exception( "プロジェクトがありません" );
        }

        public int AddWorkItem()
        {
            WorkItem newItem = new WorkItem( teamProject.WorkItemTypes["タスク"] );
            newItem.Title = "作業項目の概要です";
            newItem.Description = "作業項目の詳細です";
            newItem.Save();

            return newItem.Id;
        }

        public WorkItem GetWorkItem( int id )
        {
            return workItemStore.GetWorkItem( id );
        }
    }
}
