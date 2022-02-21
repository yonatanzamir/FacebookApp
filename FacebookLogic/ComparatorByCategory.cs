using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacebookWrapper.ObjectModel;

namespace FacebookLogic
{
    public abstract class ComparatorByCategory
    {
        public bool CompareByCategory(User i_Friend)
        {
            bool isEqualCategory = false;
            NeedCompare(i_Friend, ref isEqualCategory);
            return isEqualCategory;
        }

        protected abstract void NeedCompare(User i_Friend, ref bool i_IsEqualCategory);
    }
}
