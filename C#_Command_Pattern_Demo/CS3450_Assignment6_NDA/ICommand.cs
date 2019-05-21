using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Command_Pattern_Demo
{
    public interface ICommand
    {
        void Execute(Entry entry);
        void UnExecute();
    }
}