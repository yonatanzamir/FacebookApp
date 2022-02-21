using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacebookWrapper.ObjectModel;

namespace FacebookLogic
{
    public class CityComparator:ComparatorByCategory
    {
        private readonly User r_CurrentUser;
        private readonly List<User> r_FriendsWithSameCity;

        public CityComparator(User i_CurrentUser)
        {
            r_CurrentUser = i_CurrentUser;
            r_FriendsWithSameCity = new List<User>();
        }

        public List<User> FriendsWithSameCity => r_FriendsWithSameCity;

        protected override void NeedCompare(User i_Friend, ref bool i_IsEqualCategory)
        {
            City myCity = r_CurrentUser.Hometown;
            City friendCity = i_Friend.Hometown;
            if (myCity != null && friendCity != null)
            {
                if (myCity.Name == friendCity.Name)
                {
                    r_FriendsWithSameCity.Add(i_Friend);
                    i_IsEqualCategory = true;
                }
            }

        }
    }
}
