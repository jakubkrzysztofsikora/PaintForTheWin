using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaintForTheWin.ProgramCommands;

namespace PaintForTheWin.CanvasComponents.ChangeManagement
{
    public class ChangeStack
    {
        List<Change> _changes = new List<Change>();

        public void Push(IProgramCommand action)
        {
            Change lastChange = this.Pop();
            Change newChange;
            int actionToChange = action.GetActionToChangeId();

            if (lastChange.GetId() == 0)
            {
                newChange = Change.CreateChange(actionToChange, action);
            }
            else if (lastChange.GetId() == actionToChange)
            {
                lastChange.AddCommand(action);
                newChange = lastChange;
            }
            else
            {
                _changes.Add(lastChange);
                newChange = Change.CreateChange(actionToChange, action);
            }

            _changes.Add(newChange);
        }

        public Change Pop()
        {
            Change lastChange;

            if (_changes.Count == 0)
            {
                lastChange = GetNullChange();
            }
            else
            {
                lastChange = _changes[_changes.Count - 1];
                _changes.Remove(lastChange);
            }

            return lastChange;
        }

        public int GetCount()
        {
            return _changes.Count;
        }

        private Change GetNullChange()
        {
            return Change.CreateNullChange();
        }
    }
}
