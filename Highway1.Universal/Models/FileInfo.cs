namespace Highway1.Universal.Models
{

    using System;
    using System.Diagnostics.Contracts;
    using Windows.Storage;
    using Windows.Storage.FileProperties;
    using System.Diagnostics;

    /// <summary>File info class.</summary>
    public sealed class FileInfo : IDisposable
    {

        #region Fields

        private static readonly FileInfo _empty = Create();

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool _isDisposed;

        #endregion

        #region Properties

        /// <summary>Gets the attributes.</summary>
        /// <value>The attributes.</value>
        public FileAttributes Attributes { get; }

        /// <summary>Gets the type of the content.</summary>
        /// <value>The type of the content.</value>
        public string ContentType { get; }

        /// <summary>Gets the date created.</summary>
        /// <value>The date created.</value>
        public DateTimeOffset DateCreated { get; }

        /// <summary>Gets the empty.</summary>
        /// <value>The empty.</value>
        public static FileInfo Empty
        {
            get
            {
                Contract.Ensures(Contract.Result<FileInfo>() != null, nameof(Empty));
                return _empty;
            }
        }

        /// <summary>Gets the type of the file.</summary>
        /// <value>The type of the file.</value>
        public string FileType { get; }

        /// <summary>Gets the name.</summary>
        /// <value>The name.</value>
        public string Name { get; }

        /// <summary>Gets the path.</summary>
        /// <value>The path.</value>
        public string Path { get; }

        /// <summary>Gets the display name.</summary>
        /// <value>The display name.</value>
        public string DisplayName { get; }

        /// <summary>Gets the display type.</summary>
        /// <value>The display type.</value>
        public string DisplayType { get; }

        /// <summary>Gets the folder relative identifier.</summary>
        /// <value>The folder relative identifier.</value>
        public string FolderRelativeId { get; }

        /// <summary>Gets the future access list token.</summary>
        /// <value>The future access list token.</value>
        public string FutureAccessListToken { get; }

        /// <summary>Gets the properties.</summary>
        /// <value>The properties.</value>
        public StorageItemContentProperties Properties { get; }

        /// <summary>Gets the thumbnail.</summary>
        /// <value>The thumbnail.</value>
        public StorageItemThumbnail Thumbnail { get; }

        #endregion

        #region Methods

        private FileInfo(FileAttributes attributes, string contentType, DateTimeOffset dateCreated, string displayName, string displayType, string fileType, string folderRelativeId, string futureAccessListToken, string name, StorageItemContentProperties properties, string path, StorageItemThumbnail thumbnail)
        {
            Attributes = attributes;
            ContentType = contentType;
            DateCreated = dateCreated;
            FileType = fileType;
            Path = path;
            FutureAccessListToken = futureAccessListToken;
            Thumbnail = thumbnail;
            Name = name;
        }

        /// <summary>Creates the specified attributes.</summary>
        /// <param name="attributes">The attributes.</param>
        /// <param name="contentType">Type of the content.</param>
        /// <param name="dateCreated">The date created.</param>
        /// <param name="displayName">The display name.</param>
        /// <param name="displayType">The display type.</param>
        /// <param name="fileType">Type of the file.</param>
        /// <param name="folderRelativeId">The folder relative identifier.</param>
        /// <param name="futureAccessListToken">The future access list token.</param>
        /// <param name="path">The path.</param>
        /// <param name="name">The name.</param>
        /// <param name="properties">The properties.</param>
        /// <param name="thumbnail">The thumbnail.</param>
        /// <returns></returns>
        [Pure]
        public static FileInfo Create(FileAttributes attributes = FileAttributes.Normal, string contentType = null, DateTimeOffset dateCreated = default(DateTimeOffset), string displayName = null, string displayType = null, string fileType = null, string folderRelativeId = null, string futureAccessListToken = null, string path = null, string name = null, StorageItemContentProperties properties = null, StorageItemThumbnail thumbnail = null)
        {
            Contract.Ensures(Contract.Result<FileInfo>() != null, nameof(Create));
            return new FileInfo(attributes, contentType, dateCreated, displayName, displayType, fileType, folderRelativeId, futureAccessListToken, name, properties, path, thumbnail);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose() 
            => Dispose(true);

        private void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    if (Thumbnail != null)
                        ((IDisposable)Thumbnail).Dispose();
                }
                _isDisposed = true;
            }
        }

        /// <summary>Withes the attributes.</summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [Pure]
        public FileInfo WithAttributes(FileAttributes value)
        {
            Contract.Ensures(Contract.Result<FileInfo>() != null, nameof(WithAttributes));
            return value == Attributes ? this : new FileInfo(value, ContentType, DateCreated, DisplayName, DisplayType, FileType, FolderRelativeId, FutureAccessListToken, Name, Properties, Path, Thumbnail);
        }

        /// <summary>Withes the type of the content.</summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [Pure]
        public FileInfo WithContentType(string value)
        {
            Contract.Ensures(Contract.Result<FileInfo>() != null, nameof(WithContentType));
            return string.Equals(value, ContentType) ? this : new FileInfo(Attributes, value, DateCreated, DisplayName, DisplayType, FileType, FolderRelativeId, FutureAccessListToken, Name, Properties, Path, Thumbnail);
        }

        /// <summary>Withes the display name.</summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [Pure]
        public FileInfo WithDisplayName(string value)
        {
            Contract.Ensures(Contract.Result<FileInfo>() != null, nameof(WithDisplayName));
            return string.Equals(value, DisplayName) ? this : new FileInfo(Attributes, ContentType, DateCreated, value, DisplayType, FileType, FolderRelativeId, FutureAccessListToken, Name, Properties, Path, Thumbnail);
        }

        /// <summary>Withes the display type.</summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [Pure]
        public FileInfo WithDisplayType(string value)
        {
            Contract.Ensures(Contract.Result<FileInfo>() != null, nameof(WithDisplayType));
            return string.Equals(value, DisplayType) ? this : new FileInfo(Attributes, ContentType, DateCreated, DisplayName, value, FileType, FolderRelativeId, FutureAccessListToken, Name, Properties, Path, Thumbnail);
        }

        /// <summary>Withes the type of the file.</summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [Pure]
        public FileInfo WithFileType(string value)
        {
            Contract.Ensures(Contract.Result<FileInfo>() != null, nameof(WithFileType));
            return string.Equals(value, FileType) ? this : new FileInfo(Attributes, ContentType, DateCreated, DisplayName, DisplayType, value, FolderRelativeId, FutureAccessListToken, Name, Properties, Path, Thumbnail);
        }

        /// <summary>Withes the folder relative identifier.</summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [Pure]
        public FileInfo WithFolderRelativeId(string value)
        {
            Contract.Ensures(Contract.Result<FileInfo>() != null, nameof(WithFolderRelativeId));
            return string.Equals(value, FolderRelativeId) ? this : new FileInfo(Attributes, ContentType, DateCreated, DisplayName, DisplayType, FileType, value, FutureAccessListToken, Name, Properties, Path, Thumbnail);
        }

        /// <summary>Withes the future access list token.</summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [Pure]
        public FileInfo WithFutureAccessListToken(string value)
        {
            Contract.Ensures(Contract.Result<FileInfo>() != null, nameof(WithFutureAccessListToken));
            return string.Equals(value, FutureAccessListToken) ? this : new FileInfo(Attributes, ContentType, DateCreated, DisplayName, DisplayType, FileType, FolderRelativeId, value, Name, Properties, Path, Thumbnail);
        }

        /// <summary>Withes the name.</summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [Pure]
        public FileInfo WithName(string value)
        {
            Contract.Ensures(Contract.Result<FileInfo>() != null, nameof(WithName));
            return string.Equals(value, Name) ? this : new FileInfo(Attributes, ContentType, DateCreated, DisplayName, DisplayType, FileType, FolderRelativeId, FutureAccessListToken, value, Properties, Path, Thumbnail);
        }

        /// <summary>Withes the path.</summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [Pure]
        public FileInfo WithPath(string value)
        {
            Contract.Ensures(Contract.Result<FileInfo>() != null, nameof(WithPath));
            return string.Equals(value, Path) ? this : new FileInfo(Attributes, ContentType, DateCreated, DisplayName, DisplayType, FileType, FolderRelativeId, FutureAccessListToken, Name, Properties, value, Thumbnail);
        }

        /// <summary>Withes the properties.</summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [Pure]
        public FileInfo WithProperties(StorageItemContentProperties value)
        {
            Contract.Ensures(Contract.Result<FileInfo>() != null, nameof(WithProperties));
            return value == Properties ? this : new FileInfo(Attributes, ContentType, DateCreated, DisplayName, DisplayType, FileType, FolderRelativeId, FutureAccessListToken, Name, value, Path, Thumbnail);
        }

        /// <summary>Withes the date created.</summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [Pure]
        public FileInfo WithDateCreated(DateTimeOffset value)
        {
            Contract.Ensures(Contract.Result<FileInfo>() != null, nameof(WithDateCreated));
            return value == DateCreated ? this : new FileInfo(Attributes, ContentType, value, DisplayName, DisplayType, FileType, FolderRelativeId, FutureAccessListToken, Name, Properties, Path, Thumbnail);
        }

        /// <summary>Froms the storage file.</summary>
        /// <param name="storageFile">The storage file.</param>
        /// <returns></returns>
        [Pure]
        public static FileInfo FromStorageFile(IStorageFile storageFile)
        {
            if (storageFile != null)
                return Create(storageFile.Attributes, storageFile.ContentType, storageFile.DateCreated, fileType: storageFile.FileType, path: storageFile.Path, properties: null, name: storageFile.Name);
            return null;
        }

        /// <summary>Froms the storage file.</summary>
        /// <param name="storageFile">The storage file.</param>
        /// <returns></returns>
        [Pure]
        public static FileInfo FromStorageFile(StorageFile storageFile)
        {
            if (storageFile != null)
                return Create(storageFile.Attributes, storageFile.ContentType, storageFile.DateCreated, fileType: storageFile.FileType, path: storageFile.Path, properties: storageFile.Properties, name: storageFile.Name);
            return null;
        }

        /// <summary>Withes the thumbnail.</summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [Pure]
        public FileInfo WithThumbnail(StorageItemThumbnail value)
        {
            Contract.Ensures(Contract.Result<FileInfo>() != null, nameof(WithThumbnail));
            return value == Thumbnail ? this : new FileInfo(Attributes, ContentType, DateCreated, DisplayName, DisplayType, Path, FolderRelativeId, FutureAccessListToken, Name, Properties, FileType, value);
        }

        /// <summary>Clones this instance.</summary>
        /// <returns></returns>
        [Pure]
        public FileInfo Clone()
        {
            Contract.Ensures(Contract.Result<FileInfo>() != null, nameof(Clone));
            return Create(Attributes, ContentType, DateCreated, DisplayName, DisplayType, FileType, FolderRelativeId, FutureAccessListToken, Path, Name, Properties, Thumbnail);
        }

        /// <summary>Implements the operator explicit FileInfo.</summary>
        /// <param name="storageFile">The storage file.</param>
        /// <returns>The result of the operator.</returns>
        public static explicit operator FileInfo(StorageFile storageFile)
            => FromStorageFile(storageFile);

        #endregion

    }

}