using System.Collections.Generic;

namespace EVTracker
{
    public interface ISaveLoader
    {
        IList<Pokemon> LoadSaveFile(string filePath);
    }
}