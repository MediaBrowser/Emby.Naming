using MediaBrowser.Naming.IO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MediaBrowser.Naming.Video
{
    public class FileStack
    {
        public string Name { get; set; }
        public List<string> Files { get; set; }
        public FileInfoType Type { get; set; }
        public string Expression { get; set; }

        public FileStack()
        {
            Files = new List<string>();
        }

        public bool ContainsFile(string file, FileInfoType type)
        {
            if (type == Type)
            {
                return Files.Contains(file, StringComparer.OrdinalIgnoreCase);
            }

            return false;
        }
    }
}
