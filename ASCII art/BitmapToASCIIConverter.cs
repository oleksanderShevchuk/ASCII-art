using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCII_art
{
    public class BitmapToASCIIConverter
    {
        private readonly char[] _asciiTable = { '.', ',', ':', '+', '*', '?', '%', 'S', '#', '@' };
        private readonly char[] _asciiTableNegative = { '@', '#', 'S', '%', '?', '*', '+', ':', ',', '.' };
        private readonly Bitmap _bitmap;
        public BitmapToASCIIConverter(Bitmap bitmap)
        {
            _bitmap = bitmap;
        }
        public char[][] Convert()
        {
            return Convert(_asciiTable);
        }
        public char[][] ConvertAsNegative()
        {
            return Convert(_asciiTableNegative);
        }
        private char[][] Convert(char[] asciiTable)
        {
            var result = new char[_bitmap.Height][];
            for (int i = 0; i < _bitmap.Height; i++)
            {
                result[i] = new char[_bitmap.Width];
                for (int j = 0; j < _bitmap.Width; j++)
                {
                    int mapIndex = (int)Map(_bitmap.GetPixel(j, i).R, 0, 0, 255, asciiTable.Length - 1);
                    result[i][j] = asciiTable[mapIndex];
                }
            }
            return result;
        }
        private float Map(float valueToMap, float start1, float start2, float stop1, float stop2)
        {
            return ((valueToMap - start1) / (stop1 - start1)) * (stop2 - start2) + start2;
        }
    }
}
