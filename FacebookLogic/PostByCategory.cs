
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FacebookWrapper.ObjectModel;

namespace FacebookLogic
{
    public class PostByCategory
    {
        private readonly User r_CurrentUser;
        private readonly List<ComparatorByCategory> r_ComparatorsByCategory;
        private readonly List<User> r_FriendsWithoutSpecialCategory;
        List<Thread> m_PostToCategoriesThreads;
        private readonly object r_PostByCategoryLock = new object();
        private bool m_IsThreadThrewException = false;

        public PostByCategory(User i_CurrentUser)
        {
           r_CurrentUser = i_CurrentUser;
           r_ComparatorsByCategory = new List<ComparatorByCategory>();
           initComparatorsByCategory();
           r_FriendsWithoutSpecialCategory = new List<User>();
           initFriendsCategoryLists();
        }

        private void initComparatorsByCategory()
        {
            r_ComparatorsByCategory.Add(new BirthdayComparator(r_CurrentUser));
            r_ComparatorsByCategory.Add(new EducationComparator(r_CurrentUser));
            r_ComparatorsByCategory.Add(new JobComparator(r_CurrentUser));
            r_ComparatorsByCategory.Add(new CityComparator(r_CurrentUser));
        }

        private void initFriendsCategoryLists()
        {
            foreach (User friend in r_CurrentUser.Friends)
            {
                if(r_ComparatorsByCategory.All(i_Category => !i_Category.CompareByCategory(friend)))
                {
                    r_FriendsWithoutSpecialCategory.Add(friend);
                }
            }
        }

        private void postToFriendsByCategory(List<User> i_FriendsFromCategory,string i_Photo, string i_Status)
        {
            foreach (User friend in i_FriendsFromCategory)
            {
                r_CurrentUser.PostStatus(i_Status, null, i_Photo, friend.Id);
            }
        }

        public void CheckSelectedCategory(eCategory i_Category,string i_Photo,string i_Status)
        {
            if(i_Category == eCategory.AllFriends)
            {
                m_PostToCategoriesThreads = new List<Thread>();
                createPostingCategoryOnThread(eCategory.SameJob, i_Photo, i_Status);
                createPostingCategoryOnThread(eCategory.SameEducation, i_Photo, i_Status);
                createPostingCategoryOnThread(eCategory.SameBirthMonth, i_Photo, i_Status);
                createPostingCategoryOnThread(eCategory.SameCity, i_Photo, i_Status);
                createPostingCategoryOnThread(eCategory.Others, i_Photo, i_Status);
                foreach(Thread thread in m_PostToCategoriesThreads)
                {
                    thread.Start();
                }

                foreach (Thread thread in m_PostToCategoriesThreads)
                {
                    thread.Join();
                }

                if (m_IsThreadThrewException == true)
                {
                    m_IsThreadThrewException = false;
                    throw new Exception();
                }

            }
            else
            {
                postToASpecificCategory(i_Category, i_Photo, i_Status);
            }
        }

        private void createPostingCategoryOnThread(eCategory i_Category, string i_Photo, string i_Status)
        {
            m_PostToCategoriesThreads.Add(createThread(i_Category,i_Photo,i_Status));
        }

        private Thread createThread(eCategory i_Category, string i_Photo, string i_Status)
        {
            Thread categoryThread = new Thread(
                () =>
                    {
                        try
                        {
                            postToASpecificCategory(i_Category, i_Photo, i_Status);
                        }
                        catch (Exception e)
                        {
                            lock (r_PostByCategoryLock)
                            {
                                m_IsThreadThrewException = true;
                            }
                        }
                    });

            return categoryThread;
        }

        private void postToASpecificCategory(eCategory i_Category, string i_Photo, string i_Status)
        {
            switch(i_Category)
            {
                case eCategory.SameBirthMonth:
                    postToFriendsByCategory((r_ComparatorsByCategory.ElementAt(0) as BirthdayComparator).FriendsWithSameBirthMonth, i_Photo, i_Status);
                    break;

                case eCategory.SameEducation:
                    postToFriendsByCategory((r_ComparatorsByCategory.ElementAt(1) as EducationComparator).FriendsWithSameEducation, i_Photo, i_Status);
                    break;

                case eCategory.SameJob:
                    postToFriendsByCategory((r_ComparatorsByCategory.ElementAt(2) as JobComparator).FriendsWithSameJob, i_Photo, i_Status);
                    break;

                case eCategory.SameCity:
                    postToFriendsByCategory((r_ComparatorsByCategory.ElementAt(3) as CityComparator).FriendsWithSameCity, i_Photo, i_Status);
                    break;

          
                case eCategory.Others:
                    postToFriendsByCategory(r_FriendsWithoutSpecialCategory, i_Photo, i_Status);
                    break;
            }
        }

        public List<User> FriendsWithSameBirthMonth
        {
            get => (r_ComparatorsByCategory.ElementAt(0) as BirthdayComparator).FriendsWithSameBirthMonth;
        }
        public List<User> FriendsWithSameEducation
        {
            get => (r_ComparatorsByCategory.ElementAt(1) as EducationComparator).FriendsWithSameEducation;
        }
        public List<User> FriendsWithSameCity
        {
            get => (r_ComparatorsByCategory.ElementAt(3) as CityComparator).FriendsWithSameCity;
        }
        public List<User> FriendsWithSameJob
        {
            get => (r_ComparatorsByCategory.ElementAt(2) as JobComparator).FriendsWithSameJob;
        }
        public List<User> FriendsWithoutSpecialCategory
        {
            get => r_FriendsWithoutSpecialCategory;
        }

        public enum eCategory
        {
            SameBirthMonth,
            SameJob,
            SameCity,
            SameEducation,
            Others,
            AllFriends
        }
    }
}
