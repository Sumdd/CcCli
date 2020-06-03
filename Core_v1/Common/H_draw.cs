using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace Core_v1 {
    public class H_Draw {

        #region 注释
        public static Image CreateImage(object _imageStr) {
            string imageStr = _imageStr.ToString();
            Graphics g = Graphics.FromImage(new Bitmap(1, 1));
            Font font = new Font("Microsoft YaHei UI", 20);
            SizeF sizeF = g.MeasureString(imageStr, font);
            Brush brush = Brushes.Red;
            PointF pf = new PointF(1, 1);
            Bitmap img = new Bitmap(Convert.ToInt32(sizeF.Width + 2), Convert.ToInt32(sizeF.Height + 2));
            g = Graphics.FromImage(img);
            g.Clear(Color.Transparent);
            g.DrawString(imageStr, font, brush, pf);
            g.Dispose();
            return img;
        }
        #endregion

        #region 注释

        /// <summary>
        /// 生成文字图片
        /// </summary>
        /// <param name="text"></param>
        /// <param name="isBold"></param>
        /// <param name="fontSize"></param>
        public static Image CreateImage(string text, bool isBold = true, int fontSize = 24) {
            int wid = 20;
            int high = 20;
            Font font;
            if(isBold) {
                font = new Font("Arial", fontSize, FontStyle.Bold);

            } else {
                font = new Font("Arial", fontSize, FontStyle.Regular);
            }
            //绘笔颜色
            SolidBrush brush = new SolidBrush(Color.Red);
            StringFormat format = new StringFormat(StringFormatFlags.NoClip);
            Bitmap image = new Bitmap(wid, high);
            Graphics g = Graphics.FromImage(image);
            SizeF sizef = g.MeasureString(text, font, PointF.Empty, format);//得到文本的宽高
            int width = (int)(sizef.Width + 1);
            int height = (int)(sizef.Height + 1);
            image.Dispose();
            image = new Bitmap(width, height);
            g = Graphics.FromImage(image);
            //g.Clear(Color.White);//透明

            RectangleF rect = new RectangleF(0, 0, width, height);
            //绘制图片
            g.DrawString(text, font, brush, rect);
            //释放对象
            g.Dispose();
            return image;
        }
        #endregion

        #region 注释
        /// <summary>
        /// 把文字转换才Bitmap
        /// </summary>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <param name="rect">用于输出的矩形，文字在这个矩形内显示，为空时自动计算</param>
        /// <param name="fontcolor">字体颜色</param>
        /// <param name="backColor">背景颜色</param>
        /// <returns></returns>
        public static Bitmap TextToBitmap(string text, Font font, Rectangle rect, Color fontcolor, Color backColor) {
            Graphics g;
            Bitmap bmp;
            StringFormat format = new StringFormat(StringFormatFlags.NoClip);
            if(rect == Rectangle.Empty) {
                bmp = new Bitmap(1, 1);
                g = Graphics.FromImage(bmp);
                //计算绘制文字所需的区域大小（根据宽度计算长度），重新创建矩形区域绘图
                SizeF sizef = g.MeasureString(text, font, PointF.Empty, format);

                int width = (int)(sizef.Width + 1);
                int height = (int)(sizef.Height + 1);
                rect = new Rectangle(0, 0, width, height);
                bmp.Dispose();

                bmp = new Bitmap(width, height);
            } else {
                bmp = new Bitmap(rect.Width, rect.Height);
            }

            g = Graphics.FromImage(bmp);

            //使用ClearType字体功能
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            g.FillRectangle(new SolidBrush(backColor), rect);
            g.DrawString(text, font, Brushes.Black, rect, format);
            return bmp;
        }
        #endregion
    }
}
