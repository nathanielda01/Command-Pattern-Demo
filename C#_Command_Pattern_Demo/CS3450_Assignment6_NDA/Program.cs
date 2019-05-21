/*
 * Purpose: This program demonstrates the Command Pattern
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Command_Pattern_Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            DBReceiver receiver = new DBReceiver();
            Invoker invoker = new Invoker();
            AddCommand addCommand = new AddCommand(receiver);
            UpdateCommand updateCommand = new UpdateCommand(receiver);
            RemoveCommand removeCommand = new RemoveCommand(receiver);
            MacroCommand macroCommand = new MacroCommand(receiver);

            int operations = 0;
            bool partOfMacro = false;
            Stack<string> cmdStack = new Stack<string>();

            // IMPORTANT!!! This line supposes that the filename is 
            // the first argument on the command line per instruction.
            // If you are not testing this from the command line, please
            // insert file name below.

            string fileName = "file.txt";
            // string fileName = args[1];


            System.IO.StreamReader file = new System.IO.StreamReader(fileName);
            string line;
            while ((line = file.ReadLine()) != null)
            {
                Entry entry = new Entry();
                if (line != "")
                {
                    string operation = line;

                    if (operation != "E" && operation != "B")
                    {
                        operation = line.Substring(0, line.IndexOf(' '));
                        line = line.Substring(line.IndexOf(' ')).TrimStart();
                        entry.DbID = line.Substring(0, line.IndexOf(' '));
                        line = line.Substring(line.IndexOf(' ')).TrimStart();
                        entry.DbKey = line.Substring(0, line.IndexOf(' '));
                        line = line.Substring(line.IndexOf(' ')).TrimStart();
                        entry.DbValue = line.Substring(0, line.Length);
                    }
                    
                    switch (operation)
                    {
                        default:
                            break;
                        case "A":
                            if (!partOfMacro)
                            {
                                operations++;
                                cmdStack.Push("A");
                            }                            
                            invoker.ExecuteCommand(addCommand, entry);
                            break;
                        case "U":
                            if (!partOfMacro)
                            {
                                operations++;
                                cmdStack.Push("U");
                            }
                            invoker.ExecuteCommand(updateCommand, entry);
                            break;
                        case "R":
                            if (!partOfMacro)
                            {
                                operations++;
                                cmdStack.Push("R");
                            }
                            invoker.ExecuteCommand(removeCommand, entry);
                            break;
                        case "B":
                            cmdStack.Push("B");
                            invoker.BeginMacro(macroCommand);
                            partOfMacro = true;
                            break;
                        case "E":
                            cmdStack.Push("E");
                            invoker.EndMacro();
                            operations++;
                            partOfMacro = false;
                            break;
                    }

                   
                }
            }
            foreach (var item in receiver.GetDatabases())
            {
                item.Display();
            }
            while (operations != 0)
            {
                switch (cmdStack.Pop())
                {
                    default:
                        break;
                    case "A":
                        invoker.UndoCommand(addCommand);
                        operations--;
                        break;
                    case "U":
                        invoker.UndoCommand(updateCommand);
                        operations--;
                        break;
                    case "R":
                        invoker.UndoCommand(removeCommand);
                        operations--;
                        break;
                    case "B":
                        Console.WriteLine("Macro Unexecuted");
                        operations--;
                        break;
                    case "E":
                        invoker.UndoCommand(macroCommand);
                        break;
                }
            }
            Console.ReadKey();
        }
    }
}
