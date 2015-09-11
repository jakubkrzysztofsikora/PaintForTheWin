using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PaintForTheWin.Ecosystem
{
    public class PaintingColor
    {
        private Color _color;

        public PaintingColor(Color color)
        {
            _color = color;
        }

        public override bool Equals(object o)
        {
            PaintingColor colorToCompare = (PaintingColor)o;

            return colorToCompare != null && this._color.Equals(colorToCompare._color);
        }

        public override string ToString()
        {
            return _color.ToString();
        }

        public static PaintingColor CreateFromHex(String newColorInHex)
        {
            Color color = (Color) ColorConverter.ConvertFromString(newColorInHex);

            return new PaintingColor(color);
        }

        public static PaintingColor CreateDefault()
        {
            Color color = new Color();

            return new PaintingColor(color);
        }

        public Color GetNativeColorObject()
        {
            return _color;
        }
    }
}
