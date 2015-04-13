using System;
using System.Collections.Generic;
using System.Linq;

namespace MediaBrowser.Naming.Video
{
    public class FileStack
    {
        public string Name { get; set; }
        public List<string> Files { get; set; }
        public bool IsFolderStack { get; set; }
        public string Expression { get; set; }

        public FileStack()
        {
            Files = new List<string>();
        }

        public bool ContainsFile(string file, bool isFolder)
        {
            if (IsFolderStack == isFolder)
            {
                return Files.Contains(file, StringComparer.OrdinalIgnoreCase);
            }

            return false;
        }
    }
}
