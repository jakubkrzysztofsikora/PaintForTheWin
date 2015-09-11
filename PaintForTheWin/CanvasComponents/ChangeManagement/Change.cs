using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaintForTheWin.ProgramCommands;

namespace PaintForTheWin.CanvasComponents.ChangeManagement
{
    public class Change
    {
        private readonly List<IProgramCommand> _commands;
        private readonly int _changeId;

        public void Retract()
        {
            foreach (IProgramCommand command in _commands)
            {
                command.Retract();
            }
        }

        public int GetId()
        {
            return _changeId;
        }

        public void AddCommand(IProgramCommand action)
        {
            _commands.Add(action);
        }

        public static Change CreateChange(int id, IProgramCommand action)
        {
            return new Change(id, action);
        }

        public static Change CreateNullChange()
        {
            return new Change();
        }

        private Change()
        {
            _commands = new List<IProgramCommand>();
            _changeId = 0;
        }

        private Change(int id, IProgramCommand action)
        {
            _commands = new List<IProgramCommand>();
            _commands.Add(action);
            _changeId = id;
        }
    }
}
