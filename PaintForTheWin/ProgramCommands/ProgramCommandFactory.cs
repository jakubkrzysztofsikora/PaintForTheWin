using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaintForTheWin.Ecosystem;

namespace PaintForTheWin.ProgramCommands
{
    public class ProgramCommandFactory
    {
        public IProgramCommand CreateDrawCommand(object getCurrentTool)
        {
            throw new NotImplementedException();
        }

        public IProgramCommand CreateReverseCommand(eDirection direction)
        {
            throw new NotImplementedException();
        }

        public IProgramCommand CreateRotateCommand(int degrees)
        {
            throw new NotImplementedException();
        }
    }
}
