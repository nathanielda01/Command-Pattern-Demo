using System;
using System.Collections.Generic;
using CustomExtensions;

namespace Command_Pattern_Demo
{
    public class MacroCommand : ICommand
    {
        private DBReceiver receiver;
        private List<ICommand> macroCommands = new List<ICommand>();
        private List<Entry> macroEntries = new List<Entry>();

        public MacroCommand(DBReceiver receiver)
        {
            this.receiver = receiver;
        }

        public void Execute(Entry entry)
        {
            System.Console.WriteLine("Executing Macro");
            for (int i = 0; i < macroCommands.Count; i++)
            {
                macroCommands[i].Execute(macroEntries[i]);
            }
        }

        public void UnExecute()
        {
            Stack<ICommand> cmdStack = macroCommands.ToStack();
            Stack<Entry> entriesStack = macroEntries.ToStack();
            System.Console.WriteLine("Unexecuting Macro");
            for (int i = 0; i < macroCommands.Count; i++)
            {
                cmdStack.Pop().UnExecute();
                entriesStack.Pop();
            }
        }

        public void StoreCommand(ICommand command, Entry entry)
        {
            macroCommands.Add(command);
            macroEntries.Add(entry);
        }        
    }
}

namespace CustomExtensions
{
    // Extension methods must be defined in a static class.
    public static class ListExtension
    {
        // This is the extension method.
        // The first parameter takes the "this" modifier
        // and specifies the type for which the method is defined.
        public static Stack<Command_Pattern_Demo.ICommand> ToStack(this List<Command_Pattern_Demo.ICommand> list)
        {
            Stack<Command_Pattern_Demo.ICommand> cmdStack = new Stack<Command_Pattern_Demo.ICommand>();
            for (int i = 0; i < list.Count; i++)
            {
                cmdStack.Push(list[i]);
            }
            return cmdStack;
        }

        public static Stack<Command_Pattern_Demo.Entry> ToStack(this List<Command_Pattern_Demo.Entry> list)
        {
            Stack<Command_Pattern_Demo.Entry> entryStack = new Stack<Command_Pattern_Demo.Entry>();
            for (int i = 0; i < list.Count; i++)
            {
                entryStack.Push(list[i]);
            }
            return entryStack;
        }
    }
}