﻿namespace CampBg.Web.Areas.Administration.Controllers
{
    using System.IO;
    using System.Web.Mvc;

    using ElFinder;

    public class FilesController : AdministrationBaseController
    {
        private Connector connector;

        /// <remarks>
        /// The connector that creates and manages the connection with the local filesystem.
        /// Using the elFinder library as an intermediary between the frontend and the filesystem.
        /// Official repository: https://github.com/Studio-42/elFinder/
        /// </remarks>
        public Connector Connector
        {
            get
            {
                if (this.connector == null)
                {
                    var driver = new FileSystemDriver();

                    // mapping the folder to display and manage
                    var thumbsStorage = new DirectoryInfo(this.Server.MapPath("~/SiteContent/Thumbnails"));

                    // adding the root directory to manage. An absolute path can be
                    // used instead of DirectoryInfo as well.
                    driver.AddRoot(new Root(new DirectoryInfo(this.Server.MapPath("~/SiteContent")), "/SiteContent/")
                    {
                        Alias = "Main files folder",
                        ThumbnailsStorage = thumbsStorage,
                        MaxUploadSizeInMb = 2.2,
                        ThumbnailsUrl = "Thumbnails/"
                    });

                    this.connector = new Connector(driver);
                }

                return this.connector;
            }
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult FileConnector()
        {
            return this.Connector.Process(this.HttpContext.Request);
        }

        public ActionResult SelectFile(string target)
        {
            return this.Json(this.Connector.GetFileByHash(target).FullName);
        }

        public ActionResult Thumbs(string tmb)
        {
            return this.Connector.GetThumbnail(this.Request, this.Response, tmb);
        }
    }
}