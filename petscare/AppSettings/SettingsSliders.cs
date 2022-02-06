using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Web;

namespace petscare.AppSettings
{
    public class SettingsSliders
    {
        public static Size SliderImageSize
        {
            get
            {
                Size size = new Size();
                size.Width = Convert.ToInt32(ConfigurationManager.AppSettings["SliderImageWidth"]);
                size.Height = Convert.ToInt32(ConfigurationManager.AppSettings["SliderImageHeight"]);
                return size;
            }
        }
    }
}