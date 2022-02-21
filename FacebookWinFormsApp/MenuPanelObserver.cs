using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BasicFacebookFeatures
{
    public class MenuPanelObserver : IThemeObserver
    {
        private Panel MenuPanel { get; set; }
        private ThemeSubject ThemeSubject { get; set; }

        public MenuPanelObserver(ThemeSubject i_ThemeSubject, Panel i_MenuPanel)
        {
            ThemeSubject = i_ThemeSubject;
            MenuPanel = i_MenuPanel;
            ThemeSubject.AttachObserver(this as IThemeObserver);
        }
        public void UpdateThemeColor(Color i_Color)
        {
            MenuPanel.BackColor= Color.FromArgb((int)(i_Color.R * 1.6), (int)(i_Color.G * 1.6), (int)(i_Color.B * 1.6));
            foreach (Control control in MenuPanel.Controls)
            {
                control.BackColor = Color.FromArgb((int)(i_Color.R * 1.6), (int)(i_Color.G * 1.6), (int)(i_Color.B * 1.6));
            }
        }
    }
}
