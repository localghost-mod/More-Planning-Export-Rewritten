using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Verse;

namespace MorePlanningExportRewritten
{
    public static class Plannings
    {
        static string saveLocation = Path.Combine(
            GenFilePaths.SaveDataFolderPath,
            "MorePlanningExportRewritten/savedPlannings.xml"
        );

        static void Serialize()
        {
            var serializer = new XmlSerializer(typeof(List<Entry>));
            Directory.CreateDirectory(Path.GetDirectoryName(saveLocation));
            var filestream = new FileStream(saveLocation, FileMode.Create);
            serializer.Serialize(
                filestream,
                _plannings.Select(planning => new Entry(planning.Key, planning.Value)).ToList()
            );
            filestream.Close();
        }

        static void Deserialize()
        {
            _plannings = new Dictionary<string, string>();
            if (!File.Exists(saveLocation))
                return;
            var serializer = new XmlSerializer(typeof(List<Entry>));
            var filestream = new FileStream(saveLocation, FileMode.Open);
            _plannings = ((List<Entry>)serializer.Deserialize(filestream)).ToDictionary(
                entry => entry.Name,
                entry => entry.Value
            );
            filestream.Close();
        }

        static Dictionary<string, string> _plannings;
        static Dictionary<string, string> plannings
        {
            get
            {
                if (_plannings == null)
                    Deserialize();
                return _plannings;
            }
            set
            {
                _plannings = value;
                Serialize();
            }
        }

        public static void Add(string name, string value)
        {
            _plannings.Add(name, value);
            Serialize();
        }

        public static void Del(string name)
        {
            _plannings.Remove(name);
            Serialize();
        }

        public static bool Empty() => plannings.Count == 0;

        public static bool HasName(string name) => plannings.ContainsKey(name);

        public static Dictionary<string, string> GetPlannings() => plannings;

        public class Entry
        {
            public string Name { get; set; }
            public string Value { get; set; }

            public Entry() { }

            public Entry(string name, string value)
            {
                Name = name;
                Value = value;
            }
        }
    }
}
