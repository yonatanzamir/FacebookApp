using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BasicFacebookFeatures
{
    public class MainPanelObserver : IThemeObserver
    {
        private Form MainPanel { get; set; }
        private ThemeSubject ThemeSubject { get; set; }

        public MainPanelObserver(ThemeSubject i_ThemeSubject, Form i_MainPanel)
        {
            ThemeSubject = i_ThemeSubject;
            MainPanel = i_MainPanel;
            ThemeSubject.AttachObserver(this as IThemeObserver);
        }
        public void UpdateThemeColor(Color i_Color)
        {
            MainPanel.BackColor = Color.FromArgb((int)(i_Color.R * 0.8), (int)(i_Color.G * 0.8), (int)(i_Color.B * 0.8));
        }
    }
}
