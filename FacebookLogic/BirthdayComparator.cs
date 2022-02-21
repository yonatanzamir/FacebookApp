using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacebookWrapper.ObjectModel;

namespace FacebookLogic
{
    public class BirthdayComparator:ComparatorByCategory
    {
        private readonly User r_CurrentUser;
        private readonly List<User> r_FriendsWithSameBirthMonth;

        public BirthdayComparator(User i_CurrentUser)
        {
            r_CurrentUser = i_CurrentUser;
            r_FriendsWithSameBirthMonth = new List<User>();
        }

        public List<User> FriendsWithSameBirthMonth => r_FriendsWithSameBirthMonth;

        protected override void NeedCompare(User i_Friend, ref bool i_IsEqualCategory)
        {
            if (r_CurrentUser.Birthday != null && i_Friend.Birthday != null)
            {
                if (r_CurrentUser.Birthday.Substring(0, 2) == i_Friend.Birthday.Substring(0, 2))
                {
                    r_FriendsWithSameBirthMonth.Add(i_Friend);
                    i_IsEqualCategory = true;
                }
            }
        }
    }
}
