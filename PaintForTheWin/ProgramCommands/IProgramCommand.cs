using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using PaintForTheWin.Ecosystem.ToolComponents;

namespace PaintForTheWin.ProgramCommands
{
    public interface IProgramCommand
    {
        void Execute(Canvas element);
        void Retract();
        int GetActionToChangeId();
    }
}
