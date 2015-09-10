using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PaintForTheWin.Ecosystem.ToolComponents
{
    public class Tool
    {
        private eTool _type;
        private PaintingColor _color = PaintingColor.CreateDefault();
        private double _thickness = 1;

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

        public Color GetNativeColorObject()
        {
            return _color.GetNativeColorObject();
        }

        public void ChangeType(eTool toolType)
        {
            _type = toolType;
        }

        public eTool GetToolType()
        {
            return _type;
        }

        public override bool Equals(object obj)
        {
            Tool toolToCompare = (Tool) obj;

            if (this._type.Equals(toolToCompare._type) && this._color.Equals(toolToCompare._color))
                return true;

            return false;
        }

        public double GetThickness()
        {
            return _thickness;
        }
    }
}
