using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicFacebookFeatures
{
    public class ThemeSubject
    {
        private Color m_ThemeColor;
        private readonly List<IThemeObserver> r_ThemeObservers;

        public ThemeSubject(Color i_ThemeColor)
        {
            m_ThemeColor = i_ThemeColor;
            r_ThemeObservers = new List<IThemeObserver>();
        }

        public void AttachObserver(IThemeObserver i_ThemeObserver)
        {
            r_ThemeObservers.Add(i_ThemeObserver);
        }

        public void DetachObserver(IThemeObserver i_ThemeObserver)
        {
            r_ThemeObservers.Remove(i_ThemeObserver);
        }

        public Color ThemeColor
        {
            get
            {
                return m_ThemeColor;
            }

            set
            {
                if(m_ThemeColor != value)
                {
                    m_ThemeColor = value;
                    notifyThemeObservers();
                }
            }
        }

        private void notifyThemeObservers()
        {
            foreach (IThemeObserver observer in r_ThemeObservers)
            {
                observer.UpdateThemeColor(m_ThemeColor);
            }
        }
    }
}
