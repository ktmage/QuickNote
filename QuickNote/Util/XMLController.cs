using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace QuickNote.Util
{
    public static class XMLController
    {
        /// <summary>
        /// return the data.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static object Load(Type type , string path)
        {
            if(!File.Exists(path)) return MessageBox.Show("setting.xml is not found.");

            XmlSerializer serializer = new XmlSerializer(type);
            TextReader reader = new StreamReader(path); 
            var data = serializer.Deserialize(reader);
            if (data != null)
            {
                return data;
            }
            return MessageBox.Show("data is null.");
        }

        
        public static void Save(object target ,Type type , string path)
        {
            XmlSerializer serializer = new XmlSerializer(type);
            TextWriter writer = new StreamWriter(path);
            serializer.Serialize(writer, target);
        }


    }
}
