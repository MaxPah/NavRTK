using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace NavRTK.ModuleGPS.Model
{
    [Serializable()]
    public class ObjectsPorts : List<ObjectPort>
    {
        /// <summary>
        /// Save SerialPorts to XML format.
        /// </summary>
        /// <param name="chemin"></param>
        public void SavePort(string link)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ObjectsPorts));
            StreamWriter writer = new StreamWriter(link);
            serializer.Serialize(writer, this);
            writer.Close();
        }

        /// <summary>
        /// Load SerialPort from XML format.
        /// </summary>
        /// <param name="chemin"></param>
        public static ObjectsPorts LoadPort(string link)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(ObjectsPorts));
            StreamReader reader = new StreamReader(link);
            ObjectsPorts p = (ObjectsPorts)deserializer.Deserialize(reader);
            reader.Close();
            return p;
        }

        /// <summary>
        /// Return the higger Id from the list of SerialPort
        /// </summary>
        /// <returns></returns>
        public int MaxId()
        {
            int max = 0;
            foreach (ObjectPort o in this)
            {
                if (max < o.Id)
                    max = o.Id;
            }
            return max;
        }
        /// <summary>
        /// Swap the Id between old default Id and new default Id
        /// </summary>
        /// <param name="i"></param>
        public void DefaultSwap(int i)
        {
            foreach (ObjectPort o in this)
            {
                if (o.Id == 0)
                    o.Id = i;
                else if (o.Id == i)
                    o.Id = 0;
            }
        }
    }
}
