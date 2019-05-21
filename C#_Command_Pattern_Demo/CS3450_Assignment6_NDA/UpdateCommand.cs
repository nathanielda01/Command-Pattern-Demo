using System.Collections.Generic;

namespace Command_Pattern_Demo
{
    internal class UpdateCommand : ICommand
    {
        private DBReceiver receiver;
        private Stack<Entry> previousEntries = new Stack<Entry>();
        private Entry previousEntry;

        public UpdateCommand(DBReceiver receiver)
        {
            this.receiver = receiver;
        }

        public void Execute(Entry entry)
        {
            previousEntry.DbID = entry.DbID;
            previousEntry.DbKey = entry.DbKey;
            previousEntry.DbValue = receiver.GetDatabase(entry).GetValue(entry.DbKey);
            previousEntries.Push(previousEntry);
            receiver.GetDatabase(entry).Update(entry);
        }

        public void UnExecute()
        {
            Entry previousEntry = previousEntries.Pop();
            receiver.GetDatabase(previousEntry).Update(previousEntry);
        }
    }
}