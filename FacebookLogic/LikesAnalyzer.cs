using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacebookWrapper;
using FacebookWrapper.ObjectModel;

namespace FacebookLogic
{
    public class LikesAnalyzer : IStrategyAnalyzer
    {
        public int AddPostStats(Post i_Post)
        {
            return i_Post.LikedBy.Count;
        }
    }
}
