using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaintForTheWin.Ecosystem.ToolComponents
{
    public class Tool
    {
        private eTool _type;
        private PaintingColor _color = PaintingColor.CreateDefault();

        public Tool()
        {
            _type = eTool.Pencil;
        }

        public Tool(eTool toolType)
        {
            _type = toolType;
        }

        public void ChangeColor(String colorInHex)
        {
            _color = PaintingColor.CreateFromHex(colorInHex);
        }

        public PaintingColor GetColor()
        {
            return _color;
        }

        public void ChangeType(eTool toolType)
        {
            _type = toolType;
        }

        public override bool Equals(object obj)
        {
            Tool toolToCompare = (Tool) obj;

            if (this._type.Equals(toolToCompare._type) && this._color.Equals(toolToCompare._color))
                return true;

            return false;
        }
    }
}
