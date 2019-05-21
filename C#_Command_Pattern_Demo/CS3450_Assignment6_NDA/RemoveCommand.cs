using System.Collections.Generic;

namespace Command_Pattern_Demo
{
    internal class RemoveCommand : ICommand
    {
        private DBReceiver receiver;
        private Stack<Entry> previousEntries = new Stack<Entry>();

        public RemoveCommand(DBReceiver receiver)
        {
            this.receiver = receiver;
        }

        public void Execute(Entry entry)
        {
            previousEntries.Push(entry);
            receiver.GetDatabase(entry).Remove(entry);
        }

        public void UnExecute()
        {
            Entry previousEntry = previousEntries.Pop();
            receiver.GetDatabase(previousEntry).Add(previousEntry);
        }
    }
}