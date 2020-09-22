using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LaptopShop.BLL.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LaptopShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaptchaImageController : ControllerBase
    {
        private const int ImageWidth = 200, ImageHight = 70;
        private const string FontFamily = "Arial";
        private readonly static Brush Foreground = Brushes.Navy;
        private readonly static Color Background = Color.Silver;
        //деформация текста
        private const int WarpFactory = 5;
        private const Double xAmp = WarpFactory * ImageWidth / 100;
        private const Double yAmp = WarpFactory * ImageHight / 60;
        private const Double xFreq = 2.0 * Math.PI / ImageWidth;
        private const Double yFreq = 2.0 * Math.PI / ImageHight;
        private GraphicsPath DeformPath(GraphicsPath path)
        {
            PointF[] deformed = new PointF[path.PathPoints.Length];
            Random rng = new Random();
            Double xSeed = rng.NextDouble() * 2 * Math.PI;
            Double ySeed = rng.NextDouble() * 2 * Math.PI;
            for (int i = 0; i < path.PathPoints.Length; i++)
            {
                PointF original = path.PathPoints[i];
                Double val = xFreq * original.X + yFreq * original.Y;
                int xOffset = (int)(xAmp * Math.Sin(val + xSeed));
                int yOffset = (int)(yAmp * Math.Sin(val + ySeed));
                deformed[i] = new PointF(original.X + xOffset, original.Y + yOffset);
            }
            return new GraphicsPath(deformed, path.PathTypes);
        }
        [HttpPost("post-guid-captcha")]
        public IActionResult GuidCaptcha()
        {
            string challengeGuid = Guid.NewGuid().ToString();
            string key = CaptchaHelper
                .SessionKeyPrefix + challengeGuid;
            this.HttpContext.Session.SetString(key, CaptchaHelper.MakeRandomSolution());
            return Ok(challengeGuid);
        }
        // GET: CaptchaImage
        [HttpGet("get-captcha/{challengeGuid}")]
        public IActionResult Render(string challengeGuid)
        {
            string key = CaptchaHelper
                .SessionKeyPrefix + challengeGuid;
            string solution = (string)this.HttpContext.Session.GetString(key);
            if (solution != null)
            {
                //Создать пустое полотно для отображения на ней CAPTCHA
                using (Bitmap bmp = new Bitmap(ImageWidth, ImageHight))
                using (Graphics g = Graphics.FromImage(bmp))
                using (Font font = new Font(FontFamily, 1f))
                {
                    g.Clear(Background);
                    //Выполныть пробную визуализацию для определения найлучшего размера шрифта
                    SizeF finalSize;
                    SizeF testSize = g.MeasureString(solution, font);
                    float bestFontSize = Math.Min(ImageWidth / testSize.Width, ImageHight / testSize.Height) * 0.95f;
                    using (Font finalFont = new Font(FontFamily, bestFontSize))
                    {
                        finalSize = g.MeasureString(solution, finalFont);
                    }
                    //получить путь который представляет текст, центрированый на полотне
                    g.PageUnit = GraphicsUnit.Point;
                    PointF textTopLeft = new PointF((ImageWidth - finalSize.Width) / 2, (ImageHight - finalSize.Height) / 2);
                    using (GraphicsPath path = new GraphicsPath())
                    {
                        path.AddString(solution, new FontFamily(FontFamily), 0,
                            bestFontSize, textTopLeft, StringFormat.GenericDefault);
                        g.SmoothingMode = SmoothingMode.HighQuality;
                        g.FillPath(Foreground, DeformPath(path));
                        g.Flush();
                        using (MemoryStream ms = new MemoryStream())
                        {
                            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                            var b = ms.ToArray();
                            return File(b, "image/jpeg");
                        }
                    }
                }
            }

            return Ok();
        }
    }
}