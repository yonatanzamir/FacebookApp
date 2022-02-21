using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacebookWrapper;
using FacebookWrapper.ObjectModel;
namespace FacebookLogic
{
    public class UserDataAnalyzer : IUserDataAnalyzer
    {
        private const int k_MonthsNumber = 12;
        private int m_YearOfFirstPost;
        private int m_YearOfFirstPhotoTag;
        private readonly User r_CurrentUser;
        private IStrategyAnalyzer StrategyAnalyzer { get; set; }
        private readonly Dictionary<int, List<Post>> r_YearToPostsMap;
        private readonly Dictionary<int, List<Photo>> r_YearToTaggedPhotosMap;
        private readonly Dictionary<string, int> r_FriendToNumberOfTags;
        public int YearOfFirstPost
        {
            get => m_YearOfFirstPost;
            set => m_YearOfFirstPost = value;
        }
        public int YearOfFirstPhotoTag
        {
            get => m_YearOfFirstPhotoTag;
            set => m_YearOfFirstPhotoTag = value;
        }
        public void SetStrategyAnalyzer(IStrategyAnalyzer i_StrategyAnalyzer)
        {
            StrategyAnalyzer = i_StrategyAnalyzer;
        }
        public UserDataAnalyzer(User i_User)
        {
            r_CurrentUser = i_User;
            int firstPostIndex = r_CurrentUser.Posts.Count - 1;
            int firstPhotoTagIndex = findFirstYearOfPhotoTag();
            r_YearToPostsMap = new Dictionary<int, List<Post>>();
            r_YearToTaggedPhotosMap = new Dictionary<int, List<Photo>>();
            m_YearOfFirstPost = r_CurrentUser.Posts[firstPostIndex].CreatedTime.Value.Year;
            m_YearOfFirstPhotoTag = r_CurrentUser.PhotosTaggedIn[firstPhotoTagIndex].CreatedTime.Value.Year;
            r_FriendToNumberOfTags = new Dictionary<string, int>();
        }

        public void InitYearToPostsMap()
        {
            if (r_YearToPostsMap.Count == 0)
            {
                for (int i = m_YearOfFirstPost; i <= DateTime.Now.Year; i++)
                {
                    r_YearToPostsMap.Add(i, new List<Post>());
                }

                foreach (Post currentPost in r_CurrentUser.Posts)
                {
                    int currentPostYear = (currentPost.CreatedTime).Value.Year;
                    r_YearToPostsMap[currentPostYear].Add(currentPost);
                }
            }
        }

        public Dictionary<eMonth, int> GetUserStatsAtSelectedYear(int i_Year)
        {
            Dictionary<eMonth, int> userStatsAtSelectedYearMap = new Dictionary<eMonth, int>();
            initMonthsDictionary(userStatsAtSelectedYearMap);
            if (r_YearToPostsMap.ContainsKey(i_Year))
            {
                List<Post> userYearPosts = r_YearToPostsMap[i_Year];
                foreach (Post currentPost in userYearPosts)
                {
                    userStatsAtSelectedYearMap[(eMonth)currentPost.CreatedTime.Value.Month] += StrategyAnalyzer.AddPostStats(currentPost);
                }
            }

            return userStatsAtSelectedYearMap;
        }

        public Dictionary<eMonth, int> GetUserTagsAtSelectedYear(int i_Year)
        {
            initDictionaryOfYearToTaggedPhotos();
            Dictionary<eMonth, int> userTagsAtSelectedYearMap = new Dictionary<eMonth, int>();
            initMonthsDictionary(userTagsAtSelectedYearMap);
            if (r_YearToTaggedPhotosMap.ContainsKey(i_Year))
            {
                List<Photo> userYearPhotos = r_YearToTaggedPhotosMap[i_Year];
                foreach (Photo currentPhoto in userYearPhotos)
                {
                    userTagsAtSelectedYearMap[(eMonth)currentPhoto.CreatedTime.Value.Month]++;
                }
            }

            return userTagsAtSelectedYearMap;
        }

        private void initMonthsDictionary(Dictionary<eMonth, int> i_MonthsToStats)
        {
            for (int i = 1; i <= k_MonthsNumber; i++)
            {
                i_MonthsToStats.Add((eMonth)i, 0);
            }
        }

        public Dictionary<string, int> CreateDictionaryOfFriendToTagsNumber()
        {
            FacebookObjectCollection<Photo> taggedInPhotos = r_CurrentUser.PhotosTaggedIn;
            foreach (Photo photo in taggedInPhotos)
            {
                User photoOwner = photo.From;
                if (photoOwner != null)
                {
                    if (r_FriendToNumberOfTags.ContainsKey(photoOwner.Name))
                    {
                        r_FriendToNumberOfTags[photoOwner.Name]++;
                    }
                    else
                    {
                        r_FriendToNumberOfTags.Add(photoOwner.Name, 1);
                    }
                }
            }
            return r_FriendToNumberOfTags;
        }

        public Dictionary<int, int> CreateDictionaryOfYearToNumberOfPost()
        {
            Dictionary<int, int> yearToNumberOfPost = new Dictionary<int, int>();
            if (r_YearToPostsMap.Count == 0)
            {
                InitYearToPostsMap();
            }

            foreach (var item in r_YearToPostsMap)
            {
                yearToNumberOfPost.Add(item.Key, item.Value.Count());
            }

            for (int i = m_YearOfFirstPost; i < DateTime.Now.Year; i++)
            {
                if (yearToNumberOfPost[i] == 0)
                {
                    yearToNumberOfPost.Remove(i);
                }
            }

            return yearToNumberOfPost;
        }

        private void initDictionaryOfYearToTaggedPhotos()
        {
            if (r_YearToTaggedPhotosMap.Count == 0)
            {
                for (int i = m_YearOfFirstPhotoTag; i <= DateTime.Now.Year; i++)
                {
                    r_YearToTaggedPhotosMap.Add(i, new List<Photo>());
                }
                foreach (Photo currentPhoto in r_CurrentUser.PhotosTaggedIn)
                {
                    int currentPhotoYear = (currentPhoto.CreatedTime).Value.Year;
                    r_YearToTaggedPhotosMap[currentPhotoYear].Add(currentPhoto);
                }
            }
        }



        private int findFirstYearOfPhotoTag()
        {
            int minYear = r_CurrentUser.PhotosTaggedIn[0].CreatedTime.Value.Year;
            int indexMinYear = 0;
            int currentIndex = 0; ;
            foreach (var photo in r_CurrentUser.PhotosTaggedIn)
            {
                if (minYear > photo.CreatedTime.Value.Year)
                {
                    minYear = photo.CreatedTime.Value.Year;
                    indexMinYear = currentIndex;
                }

                currentIndex++;
            }
            return indexMinYear;
        }
    }
}
