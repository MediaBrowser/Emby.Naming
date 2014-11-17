using System.Collections.Generic;

namespace MediaBrowser.Naming.Video
{
    public class FileStack
    {
        public string Name { get; set; }
        public List<string> Files { get; set; }

        public FileStack()
        {
            Files = new List<string>();
        }
    }
}
