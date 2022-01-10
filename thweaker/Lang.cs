using System.Collections.Generic;
using System.Xml;
using System.IO;
using System;

namespace thweaker
{
    public class Lang
    {
        private static string activeLang;
        static Dictionary<string, XmlDocument> langMaps=new Dictionary<string, XmlDocument>();
        static string[] languageList;

        public delegate void LanguageChangeHookCallback(string lang);

        public static string ActiveLang
        {
            get => activeLang;
            set
            {
                activeLang = value;
                Config.writeItem("language", value);
            }
        }

        public static void Init()
        {
            Reload();
        }

        public static void Reload()
        {
            languageList = Config.getItem("languageList", "").Split(',');

            for(int i=0;i<languageList.Length;i++)
            {
                languageList[i]=languageList[i].Trim();
                LoadSingleMap(languageList[i]);
            }
        }

        public static string[] GetLanguageList()
        {
            return languageList;
        }

        static void LoadSingleMap(string lang)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(File.ReadAllText("./lang/" + lang + ".xml"));
                langMaps.Add(lang, doc);
            }
            catch (XmlException)
            {
                Console.WriteLine("Warning: Failed to load language map './" + lang + ".xml'!");
            }
        }

        public static string GetLocalizedString(string lang, string id)
        {
            string[] namespaces = id.Split('.');
            XmlNode currentNode;

            try
            {
                currentNode = langMaps[lang];
            }
            catch(KeyNotFoundException)
            {
                return id;
            }

            foreach(var i in namespaces)
            {
                currentNode = currentNode[i];
                if (currentNode == null)
                    return id;
            }

            return currentNode.FirstChild.Value ?? id;
        }
    }
}
