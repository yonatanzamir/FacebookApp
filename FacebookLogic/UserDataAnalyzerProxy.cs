using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookLogic
{
   public class UserDataAnalyzerProxy: IUserDataAnalyzer
    {
        public UserDataAnalyzer UserDataAnalyzer { get; set; }

        public void SetStrategyAnalyzer(IStrategyAnalyzer i_StrategyAnalyzer) 
        {
            UserDataAnalyzer.SetStrategyAnalyzer(i_StrategyAnalyzer);
        }
        public Dictionary<eMonth, int> GetUserStatsAtSelectedYear(int i_SelectedYear)
        {
            Dictionary<eMonth, int> userCommentsPerMonth;
            try
            {
               userCommentsPerMonth = UserDataAnalyzer.GetUserStatsAtSelectedYear(i_SelectedYear);
            }

            catch (Exception exception)
            {
                userCommentsPerMonth = null;
            }
            return userCommentsPerMonth;
        }
        public int YearOfFirstPost
        {
            get => UserDataAnalyzer.YearOfFirstPost;
            set => UserDataAnalyzer.YearOfFirstPost = value;
        }
        public int YearOfFirstPhotoTag
        {
            get => UserDataAnalyzer.YearOfFirstPhotoTag;
            set => UserDataAnalyzer.YearOfFirstPhotoTag= value;
        }

        public Dictionary<eMonth, int> GetUserTagsAtSelectedYear(int i_SelectedYear)
        {
            Dictionary<eMonth, int> userTagsPerMonth;
            try
            {
                userTagsPerMonth = UserDataAnalyzer.GetUserTagsAtSelectedYear(i_SelectedYear);
            }

            catch (Exception exception)
            {
                userTagsPerMonth = null;
            }
            return userTagsPerMonth;
        }
        public Dictionary<string, int> CreateDictionaryOfFriendToTagsNumber()
        {
            Dictionary<string, int> friendToTagsNumber = UserDataAnalyzer.CreateDictionaryOfFriendToTagsNumber();

            return friendToTagsNumber;
        }
        public Dictionary<int, int> CreateDictionaryOfYearToNumberOfPost()
        {
            Dictionary<int, int> yearToPostsNumber = UserDataAnalyzer.CreateDictionaryOfYearToNumberOfPost();

            return yearToPostsNumber;
        }
        public void InitYearToPostsMap()
        {
            UserDataAnalyzer.InitYearToPostsMap();
        }
    }
}
