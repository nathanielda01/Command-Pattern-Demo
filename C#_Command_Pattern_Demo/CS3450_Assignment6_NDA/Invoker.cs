using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Command_Pattern_Demo
{
    public class Invoker
    {
        private bool isMacro = false;
        private MacroCommand mc;

        public void ExecuteCommand(ICommand command, Entry entry)
        {
            if (isMacro)
            {
                mc.StoreCommand(command, entry);
            }
            command.Execute(entry);
        }

        public void UndoCommand(ICommand command)
        {
            command.UnExecute();
        }

        public void BeginMacro(MacroCommand macroCommand)
        {
            Console.WriteLine("Macro Beginning");
            isMacro = true;
            mc = macroCommand;
        }

        public void EndMacro()
        {
            Console.WriteLine("Macro Ending");
            isMacro = false;
        }
    }
}