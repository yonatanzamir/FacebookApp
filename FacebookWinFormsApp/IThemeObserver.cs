using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicFacebookFeatures
{
    public interface IThemeObserver
    {
        void UpdateThemeColor(Color i_Color);
    }
}
