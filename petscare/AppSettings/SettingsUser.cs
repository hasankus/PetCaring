using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Web;

namespace petscare.AppSettings
{
    public class SettingsUser
    {
        public static Size UsersImageSize
        {
            get
            {
                Size size = new Size();
                size.Width = Convert.ToInt32(ConfigurationManager.AppSettings["UsersImageWidth"]);
                size.Height = Convert.ToInt32(ConfigurationManager.AppSettings["UsersImageHeight"]);
                return size;
            }
        }
    }
}