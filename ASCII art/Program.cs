using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASCII_art
{
    public class Program
    {
        private const double WIDTH_OFFSET = 2.2;
        private const int MAX_WIDTH = 550;

        [STAThread]
        static void Main(string[] args)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Images | *.bmp; *.png; *.jpg; *.JPEG"
            };
            while (true)
            {
                Console.ReadLine();
                if (openFileDialog.ShowDialog() != DialogResult.OK)
                {
                    continue;
                }
                var bitmap = new Bitmap(openFileDialog.FileName);
                bitmap = ResizeBitmap(bitmap);
                bitmap.ToGrayScale();

                var converter = new BitmapToASCIIConverter(bitmap);
                var rows = converter.Convert();

                foreach ( var row in rows )
                {
                    Console.WriteLine(row);
                }

                var rowAsNegative = converter.ConvertAsNegative();
                File.WriteAllLines("image.txt", rowAsNegative.Select(r => new string(r)));

                Console.SetCursorPosition(0,0);
            }
        }
        private static Bitmap ResizeBitmap(Bitmap bitmap)
        {
            var newHeight = bitmap.Height / WIDTH_OFFSET * MAX_WIDTH / bitmap.Width;
            if (bitmap.Width > MAX_WIDTH || bitmap.Height > newHeight)
            {
                bitmap = new Bitmap(bitmap, new Size(MAX_WIDTH, (int)newHeight));
            }
            return bitmap;
        }
    }
}
