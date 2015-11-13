using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;

namespace EVTracker
{
    class SaveLoader : ISaveLoader
    {
        private readonly DataContractSerializer _deserializer = new DataContractSerializer(typeof(List<Pokemon>));

        public IList<Pokemon> LoadSaveFile(string filePath)
        {
            try
            { 
                using (var stream = File.OpenRead(filePath))
                {
                    return (IList<Pokemon>) _deserializer.ReadObject(stream);
                }
            }
            catch
            {
                //This is when the  file is invalid
                throw new Exception("Failed to load save.");
            }
        }
    }
}