using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookLogic
{
    public interface IUserDataAnalyzer
    {

        Dictionary<eMonth, int> GetUserStatsAtSelectedYear(int i_SelectedYear);
        void SetStrategyAnalyzer(IStrategyAnalyzer i_StrategyAnalyzer);
        Dictionary<eMonth, int> GetUserTagsAtSelectedYear(int i_SelectedYear);
        Dictionary<string, int> CreateDictionaryOfFriendToTagsNumber();
        Dictionary<int, int> CreateDictionaryOfYearToNumberOfPost();
        void InitYearToPostsMap();
        int YearOfFirstPost { get; set; }
        int YearOfFirstPhotoTag { get; set; }

    }
}
