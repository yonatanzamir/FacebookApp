using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacebookWrapper.ObjectModel;

namespace FacebookLogic
{
    public class EducationComparator:ComparatorByCategory
    {
        private readonly User r_CurrentUser;
        private readonly List<User> r_FriendsWithSameEducation;

        public EducationComparator(User i_CurrentUser)
        {
            r_CurrentUser = i_CurrentUser;
            r_FriendsWithSameEducation = new List<User>();
        }

        public List<User> FriendsWithSameEducation => r_FriendsWithSameEducation;

        protected override void NeedCompare(User i_Friend, ref bool i_IsEqualCategory)
        {
            if (r_CurrentUser.Educations != null && i_Friend.Educations != null)
            {
                Education myEducation = r_CurrentUser.Educations[r_CurrentUser.Educations.Length - 1];
                Education friendEducation = i_Friend.Educations[i_Friend.Educations.Length - 1];

                if (myEducation.School.Name == friendEducation.School.Name)
                {
                    r_FriendsWithSameEducation.Add(i_Friend);
                    i_IsEqualCategory = true;
                }
            }
        }
    }
}
