using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacebookWrapper.ObjectModel;

namespace FacebookLogic
{
    public sealed class FeaturesManager
    {
        private static FeaturesManager s_Instance = null;
        private readonly IUserDataAnalyzer r_UserDataAnalyzer;
        private readonly PostByCategory r_PostByCategory;

        private FeaturesManager(User i_LoggedInUser)
        {
            r_UserDataAnalyzer = new UserDataAnalyzerProxy() { UserDataAnalyzer = new UserDataAnalyzer(i_LoggedInUser) };
            r_PostByCategory = new PostByCategory(i_LoggedInUser);
        }

        public static FeaturesManager GetInstance(User i_LoggedInUser)
        {
            if (s_Instance == null)
            {
                s_Instance = new FeaturesManager(i_LoggedInUser);
            }

            return s_Instance;
        }

        public IUserDataAnalyzer UserDataAnalyzer
        {
            get => r_UserDataAnalyzer;
        }
        public PostByCategory PostByCategory
        {
            get => r_PostByCategory;
        }
    }

    public enum eMonth
    {
        Jan = 1,
        Feb = 2,
        Mar = 3,
        Apr = 4,
        May = 5,
        Jun = 6,
        Jul = 7,
        Aug = 8,
        Sept = 9,
        Oct = 10,
        Nov = 11,
        Dec = 12
    }
}
