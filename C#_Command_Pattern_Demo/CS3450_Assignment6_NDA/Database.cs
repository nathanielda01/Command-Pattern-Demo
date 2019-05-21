using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Command_Pattern_Demo
{
    public struct Entry
    {
        private string _dbID;
        private string _dbKey;
        private string _dbValue;

        public string DbID { get => _dbID; set => _dbID = value; }
        public string DbKey { get => _dbKey; set => _dbKey = value; }
        public string DbValue { get => _dbValue; set => _dbValue = value; }
    }

    public class Database
    {
        private SortedDictionary<string, string> entries = new SortedDictionary<string, string>();
        private SortedDictionary<string, string> previousState = new SortedDictionary<string, string>();
        private string databaseID;
        
        public Database(string databaseID)
        {
            this.databaseID = databaseID;
        }

        public string DatabaseID { get => databaseID; set => databaseID = value; }

        internal void Add(Entry entry)
        {
            if (entries.ContainsKey(entry.DbKey))
            {
                Console.WriteLine("Database {0} already contains this key.", entry.DbID);
            }
            else
            {
                entries.Add(entry.DbKey, entry.DbValue);
                Console.WriteLine("Entry Added");
            }
        }

        public string GetValue(string key)
        {
            return entries[key];
        }

        public void Update(Entry entry)
        {
            if (!entries.ContainsKey(entry.DbKey))
            {
                Console.WriteLine("{0} does not exist is database {1}", entry.DbKey, entry.DbID);
            }
            else
            {
                entries[entry.DbKey] = entry.DbValue;
                Console.WriteLine("Entry Updated");
            }
        }

        public void Remove(Entry entry)
        {
            entries.Remove(entry.DbKey);
            Console.WriteLine("Entry Removed");
        }

        public void Display()  
        {
            string[] keyArr = entries.Keys.ToArray();
            Console.WriteLine("{0}", this.databaseID);
            for (int i = 0; i < entries.Count; i++)
            {
                Console.WriteLine("{0}|{1}", keyArr[i], entries[keyArr[i]]);
            }
        }
    }
}