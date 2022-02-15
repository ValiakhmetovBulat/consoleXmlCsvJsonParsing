using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using System.IO;
using System.Xml;

namespace Solution11
{
    internal class Parsing
    {
        private List<Item> items = new List<Item>();
        
        private void _getFromJson()
        {
            using (StreamReader r = new StreamReader(@".\src\participants.json"))
            {
                string json = r.ReadToEnd();
                items.AddRange(JsonConvert.DeserializeObject<List<Item>>(json));
            }
            foreach (var item in items)
            {
                item.Service = "Сервис №1";
            }
        }
        private void _getFromXml()
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(@".\src\participants.xml");

            foreach (XmlNode node in xml.DocumentElement)
            {
                Item item = new Item();
                item.FirstName = node["Name"].InnerText.Replace(" ", "");
                item.LastName = node["Surname"].InnerText.Replace(" ", "");
                item.RegistrationDate = DateTime.Parse(node["RegisterDate"].InnerText);
                item.Service = "Сервис №2";
                items.Add(item);
            }
        }
        private void _getFromCsv()
        {
            using (TextFieldParser tfp = new TextFieldParser(@".\src\participants.csv"))
            {
                tfp.TextFieldType = FieldType.Delimited;
                tfp.SetDelimiters(",");

                while (!tfp.EndOfData)
                {
                    Item item = new Item();

                    string[] fields = tfp.ReadFields();
                    item.FirstName = fields[0].Replace(" ", "");
                    item.LastName = fields[1].Replace(" ", "");
                    item.RegistrationDate = DateTime.Parse(fields[2]);
                    item.Service = "Сервис №3";
                    items.Add(item);
                }
            }
        }
        private void _removeDubs(List<Item> participants)
        {
            List<Item> items = new List<Item>();
            List<Item> toRemove = new List<Item>();

            foreach (Item eachItem in participants)
            {
                foreach (Item itemToFindDubs in participants)
                {
                    if (itemToFindDubs.FirstName == eachItem.FirstName && itemToFindDubs.LastName == eachItem.LastName)
                    {
                        items.Add(itemToFindDubs);
                    }
                }
                if (items.Count > 1)
                {
                    for (int i = 1; i < items.Count; i++)
                    {
                        toRemove.Add(items[i]);
                    }
                }
                items.Clear();
            }
            foreach (Item dub in toRemove)
            {
                participants.Remove(dub);
            }
        }
        public List<Item> parseParticipants()
        {
            _getFromJson();
            _getFromXml();
            _getFromCsv();

            items.Sort(delegate(Item i1, Item i2)
            { return i1.RegistrationDate.CompareTo(i2.RegistrationDate); });
            _removeDubs(items);

            return items;
        }
    }
}
