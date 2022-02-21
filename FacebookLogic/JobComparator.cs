using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacebookWrapper.ObjectModel;

namespace FacebookLogic
{
    public class JobComparator:ComparatorByCategory
    {
        private readonly User r_CurrentUser;
        private readonly List<User> r_FriendsWithSameJob;

        public JobComparator(User i_CurrentUser)
        {
            r_CurrentUser = i_CurrentUser;
            r_FriendsWithSameJob = new List<User>();
        }

        public List<User> FriendsWithSameJob => r_FriendsWithSameJob;

        protected override void NeedCompare(User i_Friend, ref bool i_IsEqualCategory)
        {
            if (r_CurrentUser.WorkExperiences != null && i_Friend.WorkExperiences != null)
            {
                WorkExperience meLastExperience = r_CurrentUser.WorkExperiences[r_CurrentUser.WorkExperiences.Length - 1];
                WorkExperience friendLastExperience = i_Friend.WorkExperiences[i_Friend.WorkExperiences.Length - 1];

                if (meLastExperience.Name == friendLastExperience.Name)
                {
                    r_FriendsWithSameJob.Add(i_Friend);
                    i_IsEqualCategory = true;
                }
            }
        }
    }
}
