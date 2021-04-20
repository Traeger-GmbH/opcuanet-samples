// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace CustomFile
{
    using System;
    using System.IO;
    using System.Net;

    using Opc.UaFx;

    /// <summary>
    /// Provides a simple implementation of the <see cref="IOpcFileInfo"/> interface to access FTP files
    /// as read-only.
    /// </summary>
    internal class FtpFileInfo : IOpcFileInfo, IDisposable
    {
        #region ---------- Private readonly fields ----------

        private readonly ICredentials credentials;
        private readonly object syncRoot;

        #endregion

        #region ---------- Private fields ----------

        private Uri path;
        private FtpWebResponse response;

        #endregion

        #region ---------- Public constructors ----------

        public FtpFileInfo(string path, ICredentials credentials)
            : this(new Uri(path), credentials)
        {
        }

        public FtpFileInfo(Uri path, ICredentials credentials)
            : base()
        {
            if (path is null)
                throw new ArgumentNullException();

            if (!path.Scheme.Equals(Uri.UriSchemeFtp, StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException();

            this.path = path;
            this.credentials = credentials;

            this.syncRoot = new object();
        }

        #endregion

        #region ---------- Public properties ----------

        public bool Exists
        {
            get
            {
                lock (this.syncRoot) {
                    this.Refresh();
                    return this.response != null;
                }
            }
        }

        public bool IsReadOnly
        {
            get => true;
        }

        public long Length
        {
            get
            {
                lock (this.syncRoot) {
                    if (!this.Exists)
                        throw new IOException();

                    var length = this.response.ContentLength;

                    if (length < 0)
                        length = 0;

                    return length;
                }
            }
        }

        #endregion

        #region ---------- Public methods ----------

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public string GetMimeType()
        {
            lock (this.syncRoot) {
                if (!this.Exists)
                    throw new IOException();

                // FtpWebResponse throws NotImplementedException.
                ////return this.response.ContentType;

                // Therefore we do not know the type of content and just use the mime type
                // for binary documents. This might be enhanced on-demand.
                return "application/octet-stream";
            }
        }

        public Stream Open(OpcFileMode mode)
        {
            if (mode != OpcFileMode.Read)
                throw new ArgumentException();

            lock (this.syncRoot) {
                if (!this.Exists)
                    throw new IOException();

                return this.response?.GetResponseStream();
            }
        }

        public void Refresh()
        {
            lock (this.syncRoot) {
                if (this.path is null)
                    throw new ObjectDisposedException(this.GetType().Name);

                this.response?.Dispose();
                this.response = null;

                if (WebRequest.Create(this.path) is FtpWebRequest request) {
                    request.Credentials = this.credentials;
                    request.Method = WebRequestMethods.Ftp.DownloadFile;

                    this.response = request.GetResponse() as FtpWebResponse;
                }
            }
        }

        #endregion

        #region ---------- Protected methods ----------

        protected virtual void Dispose(bool disposing)
        {
            if (disposing) {
                lock (this.syncRoot) {
                    this.response?.Dispose();
                    this.response = null;

                    this.path = null;
                }
            }
        }

        #endregion
    }
}
