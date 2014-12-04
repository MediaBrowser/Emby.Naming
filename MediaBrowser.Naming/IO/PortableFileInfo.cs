using System.IO;

namespace MediaBrowser.Naming.IO
{
    public class PortableFileInfo
    {
        /// <summary>
        /// Gets or sets the full name.
        /// </summary>
        /// <value>The full name.</value>
        public string FullName { get; set; }
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public FileInfoType Type { get; set; }

        /// <summary>
        /// Gets the file name without extension.
        /// </summary>
        /// <value>The file name without extension.</value>
        public string FileNameWithoutExtension
        {
            get { return Type == FileInfoType.File ? Path.GetFileNameWithoutExtension(FullName) : Path.GetFileName(FullName); }
        }
    }
}
