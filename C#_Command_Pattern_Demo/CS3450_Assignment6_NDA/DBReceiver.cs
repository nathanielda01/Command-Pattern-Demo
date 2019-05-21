using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Command_Pattern_Demo
{
    public class DBReceiver
    {
        private List<Database> databases = new List<Database>();
        private Database database;
        private int indexOfContained;

        public bool Contains(string dbID)
        {
            if (databases.Count == 0)
            {
                return false;
            }
            for (int i = 0; i < databases.Count; i++)
            {
                if (databases[i].DatabaseID == dbID)
                {
                    indexOfContained = i;
                    return true;
                }
            }
            return false;
        }

        public void Handler(Entry entry)
        {
            if (this.Contains(entry.DbID))
            {
                this.database = databases[indexOfContained];
            }
            else
            {
                this.database = new Database(entry.DbID);
                databases.Add(this.database);
            }
        }

        public Database GetDatabase(Entry entry)
        {
            this.Handler(entry);
            return database;
        }

        public List<Database> GetDatabases()
        {
            return databases;
        }
    }
}