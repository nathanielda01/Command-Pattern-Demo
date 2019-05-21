using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Command_Pattern_Demo
{
    public class AddCommand : ICommand
    {
        private DBReceiver receiver;
        private Stack<Entry> previousEntries = new Stack<Entry>();

        public AddCommand(DBReceiver receiver)
        {
            this.receiver = receiver;
        }

        public void Execute(Entry entry)
        {
            previousEntries.Push(entry);
            receiver.GetDatabase(entry).Add(entry);
        }

        public void UnExecute()
        {
            Entry previousEntry = previousEntries.Pop();
            receiver.GetDatabase(previousEntry).Remove(previousEntry);
        }                           
    }
}
