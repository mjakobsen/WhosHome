using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WhosHome.Logic
{
    public static class LocalListBackup
    {
        static string _filePath = @"WhosHome.txt";
        private static object _lockObject = new object();

        public static void SaveToFile()
        {
            lock (_lockObject)
            {
                try
                {
                    if (File.Exists(_filePath))
                    {
                        File.Delete(_filePath);
                    }
                    SerializeObjectToXML(MainWindow.Instance.Vehicles);
                }
                catch (Exception)
                {                    
                }
            }

        }

        private static void SerializeObjectToXML<T>(T item)
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            using (StreamWriter wr = new StreamWriter(_filePath))
            {
                xs.Serialize(wr, item);
            }
        }

        public static ObservableCollection<Vehicle> LoadFromFile()
        {
            ObservableCollection<Vehicle> result = new ObservableCollection<Vehicle>();

            try
            {
                lock (_lockObject)
                {
                    if (File.Exists(_filePath))
                    {
                        XmlSerializer xs = new XmlSerializer(typeof(ObservableCollection<Vehicle>));
                        using (StreamReader sr = new StreamReader(_filePath))
                        {
                            result = (ObservableCollection<Vehicle>)xs.Deserialize(sr);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result;
        }
    }
}
