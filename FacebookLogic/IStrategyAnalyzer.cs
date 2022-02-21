using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacebookWrapper;
using FacebookWrapper.ObjectModel;

namespace FacebookLogic
{
   public interface IStrategyAnalyzer
    {
        int AddPostStats(Post i_Post);
    }
}
