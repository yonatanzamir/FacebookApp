using 
    FacebookWrapper;
using FacebookWrapper.ObjectModel;
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms.DataVisualization.Charting;
using FacebookLogic;

namespace BasicFacebookFeatures
{
    public partial class FacebookForm : Form
    {
        private LoginResult m_LoginResult;
        private User m_LoggedInUser;
        private IChart m_ChartStatistics;
        private IChart m_ChartFriendsTags;
        private IChart m_ChartPostActivity;
        private const bool k_IsComponentsVisible = true;
        private const bool k_IsEnableOptions = true;
        private ThemeSubject m_ThemeSubject;
        private MainPanelObserver m_MainPanelObserver;
        private MenuPanelObserver m_MenuPanelObserver;

        public FacebookForm()
        {
            InitializeComponent();
            FacebookWrapper.FacebookService.s_CollectionLimit = 100;
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            FacebookService.LogoutWithUI();
            buttonLogin.Text = "Login";
            m_LoginResult = null;
            buttonLogin.Enabled = true;
            labelUserName.Text = String.Empty;
            pictureBoxMain.Visible = true;
            pictureBoxProfilePhoto.Visible = false;
            setEnableAppOptions(!k_IsEnableOptions);
            setLoginEnable(k_IsEnableOptions);
            deleteUserInfoLabels();
            changeCurrentTab(eComponentType.UserInfo);
            Close();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            Clipboard.SetText("design.patterns22aa"); /// the current password for Desig Patter
            m_LoginResult = FacebookService.Login(
                            "2944152095915134",
                            "email",
                            "public_profile",
                            "user_age_range",
                            "user_birthday",
                            "user_events",
                            "user_friends",
                            "user_gender",
                            "user_hometown",
                            "user_likes",
                            "user_link",
                            "user_location",
                            "user_photos",
                            "user_posts",
                            "user_videos",
                            "groups_access_member_info");
                   
            if (!string.IsNullOrEmpty(m_LoginResult.AccessToken)) // if acess token exist
            {
                m_LoggedInUser = m_LoginResult.LoggedInUser;
                labelUserName.Text = $"Logged in as" + Environment.NewLine + $"{m_LoginResult.LoggedInUser.Name}";
                buttonLogin.Enabled = false;
                pictureBoxMain.Visible = false;
                pictureBoxProfilePhoto.Visible = true;
                pictureBoxProfilePhoto.LoadAsync(m_LoggedInUser.PictureLargeURL);
                showUserInfo();
                setEnableAppOptions(k_IsEnableOptions);
                setLoginEnable(!k_IsEnableOptions);
            }
            else
            {
                MessageBox.Show("Error occurred while trying to login " + m_LoginResult.ErrorMessage, "Login Failed");
            }

        }

        private void setLoginEnable(bool i_IsEnable)
        {
            buttonLogout.Enabled = !i_IsEnable;
            buttonLogin.Enabled = i_IsEnable;
        }


        private void setEnableAppOptions(bool i_IsEnable)
        {
            buttonAlbum.Enabled = i_IsEnable;
            buttonEvents.Enabled = i_IsEnable;
            buttonGroups.Enabled = i_IsEnable;
            buttonLikedPages.Enabled = i_IsEnable;
            buttonUserInfo.Enabled = i_IsEnable;
            buttonPost.Enabled = i_IsEnable;
            buttonUserStats.Enabled = i_IsEnable;
            buttonPostByCategory.Enabled = i_IsEnable;
        }

        private void setLikedPagesComponentsVisibility(bool i_IsShow)
        {
            buttonFetchPages.Visible = i_IsShow;
            labelPagePicture.Visible = i_IsShow;
            pictureBoxPages.Visible = i_IsShow;
            listBoxPages.Visible = i_IsShow;
            if(i_IsShow)
            {
                buttonLikedPages.BackColor = this.BackColor;
            }


        }

        private void setUserInfoComponentsVisibility(bool i_IsShow)
        {
            labelUserInfoHeadLine.Visible = i_IsShow;
            panelUserInfo.Visible = i_IsShow;
            if(i_IsShow)
            {
                buttonUserInfo.BackColor = this.BackColor;
            }

        }

        private void setGroupsComponentsVisibility(bool i_IsShow)
        {
            buttonFetchGroups.Visible = i_IsShow;
            labelGroupPicture.Visible = i_IsShow;
            pictureBoxGroups.Visible = i_IsShow;
            listBoxGroups.Visible = i_IsShow;
            textBoxGroupDescription.Visible = i_IsShow;
            labelGroupDescription.Visible = i_IsShow;
            if(i_IsShow)
            {
                buttonGroups.BackColor = this.BackColor;
            }
       
        }

        private void setEventsComponentsVisibility(bool i_IsShow)
        {
            buttonFetchEvents.Visible = i_IsShow;
            labelEventPicture.Visible = i_IsShow;
            pictureBoxEvents.Visible = i_IsShow;
            listBoxEvents.Visible = i_IsShow;
            eventDescriptionTextBox.Visible = i_IsShow;
            labelEventDescription.Visible = i_IsShow;
            if(i_IsShow)
            {
                buttonEvents.BackColor = this.BackColor;
            }

        }

        private void setAlbumsComponentsVisibility(bool i_IsShow)
        {
            buttonFetchAlbums.Visible = i_IsShow;
            labelAlbumCoverPicture.Visible = i_IsShow;
            pictureBoxAlbumCoverPicture.Visible = i_IsShow;
            listBoxAlbums.Visible = i_IsShow;
            if(i_IsShow)
            {
                buttonAlbum.BackColor = this.BackColor;
            }


        }

        private void setPostByCategoryComponentsVisibility(bool i_IsShow)
        {
            labelPostStatusToFriendsCategory.Visible = i_IsShow;
            labelMessageFriendsCategory.Visible = i_IsShow;
            buttonBrowsePhoto.Visible = i_IsShow;
            buttonPostNow.Visible = i_IsShow;
            richTextBoxStatusToFriendsCategory.Visible = i_IsShow;
            pictureBoxPhotoToFriendsCategory.Visible = i_IsShow;
            comboBoxFriendsCategory.Visible = i_IsShow;
            listBoxFriendsCategory.Visible = i_IsShow;
            labelFriendsFromCurrentCategory.Visible = i_IsShow;
            if (!i_IsShow)
            {
                labePostByCategoryException.Visible = i_IsShow;
            }

            if(i_IsShow)
            {
                buttonPostByCategory.BackColor = this.BackColor;
            }

        }

        private void setUserStatsComponentsVisibility(bool i_IsShow)
        {
            comboBoxSelectYear.Visible = i_IsShow;
            chartStatistics.Visible = i_IsShow;
            labelSelectYear.Visible = i_IsShow;
            buttonFetchStatistics.Visible = i_IsShow;
            buttonFetchFriendsTags.Visible = i_IsShow;
            buttonFetchPostActivity.Visible = i_IsShow;
            chartFriendsTags.Visible = i_IsShow;
            chartPostActivity.Visible = i_IsShow;

            if (!i_IsShow)
            {
                labelCommentsExceptionInYear.Visible = i_IsShow;
                labelLikesExceptionMsg.Visible = i_IsShow;
                labelFriendsTaggingException.Visible = i_IsShow;
            }

            if(i_IsShow)
            {
                buttonUserStats.BackColor = this.BackColor;
            }

        }

        private void setPostsComponentsVisibility(bool i_IsShow)
        {
            buttonFetchPosts.Visible = i_IsShow;
            labelPostComments.Visible = i_IsShow;
            listBoxPostComments.Visible = i_IsShow;
            listBoxPosts.Visible = i_IsShow;
            if (!i_IsShow)
            {
                displaySelectedPostedItem(eComponentType.None);
            }
            if (i_IsShow)
            {
                buttonPost.BackColor = this.BackColor;
            }

        }

        private void disableButtonsColor()
        {
            buttonUserInfo.BackColor = panel1.BackColor;
            buttonEvents.BackColor = panel1.BackColor;
            buttonPost.BackColor = panel1.BackColor;
            buttonAlbum.BackColor = panel1.BackColor;
            buttonGroups.BackColor = panel1.BackColor;
            buttonLikedPages.BackColor = panel1.BackColor;
            buttonUserStats.BackColor = panel1.BackColor;
            buttonPostByCategory.BackColor = panel1.BackColor;

        }

        private void changeCurrentTab(eComponentType i_ComponentType)
        {
            setPostsComponentsVisibility(!k_IsComponentsVisible);
            setEventsComponentsVisibility(!k_IsComponentsVisible);
            setAlbumsComponentsVisibility(!k_IsComponentsVisible);
            setGroupsComponentsVisibility(!k_IsComponentsVisible);
            setLikedPagesComponentsVisibility(!k_IsComponentsVisible);
            setUserInfoComponentsVisibility(!k_IsComponentsVisible);
            setUserStatsComponentsVisibility(!k_IsComponentsVisible);
            setPostByCategoryComponentsVisibility(!k_IsComponentsVisible);
            disableButtonsColor();
            switch (i_ComponentType)
            {
                case eComponentType.Album:
                    setAlbumsComponentsVisibility(k_IsComponentsVisible);
                    break;
                case eComponentType.Event:
                    setEventsComponentsVisibility(k_IsComponentsVisible);
                    break;
                case eComponentType.Group:
                    setGroupsComponentsVisibility(k_IsComponentsVisible);
                    break;
                case eComponentType.Page:
                    setLikedPagesComponentsVisibility(k_IsComponentsVisible);
                    break;
                case eComponentType.Post:
                    setPostsComponentsVisibility(k_IsComponentsVisible);
                    break;
                case eComponentType.UserInfo:
                    setUserInfoComponentsVisibility(k_IsComponentsVisible);
                    break;
                case eComponentType.UserStatistics:
                    setUserStatsComponentsVisibility(k_IsComponentsVisible);
                    break;
                case eComponentType.PostByCategory:
                    setPostByCategoryComponentsVisibility(k_IsComponentsVisible);
                    break;
            }
        }

        private void buttonPost_Click(object sender, EventArgs e)
        {
            changeCurrentTab(eComponentType.Post);
        }

        private void buttonAlbum_Click(object sender, EventArgs e)
        {
            changeCurrentTab(eComponentType.Album);
        }

        private void buttonEvents_Click(object sender, EventArgs e)
        {
            changeCurrentTab(eComponentType.Event);
        }

        private void buttonGroups_Click(object sender, EventArgs e)
        {
            changeCurrentTab(eComponentType.Group);
        }

        private void buttonLikedPages_Click(object sender, EventArgs e)
        {
            changeCurrentTab(eComponentType.Page);

        }

        private void fetchEvents()
        {
            var allEvents = m_LoggedInUser.Events;
            if (!listBoxEvents.InvokeRequired)
            {
                eventBindingSource.DataSource = allEvents;
            }
            else
            {
                listBoxEvents.Invoke(new Action(() => eventBindingSource.DataSource = allEvents));
            }

            listBoxEvents.Invoke(new Action(() => {
                    if (listBoxEvents.Items.Count == 0)
                    {
                        MessageBox.Show("No Events to fetch");
                    }
                }));
        }

        private void buttonFetchEvents_Click(object sender, EventArgs e)
        {
            new Thread(fetchEvents).Start();
        }

        private void listBoxEvents_SelectedValueChanged(object sender, EventArgs e)
        {
            if (listBoxEvents.SelectedItems.Count != 0)
            {
                pictureBoxEvents.LoadAsync((listBoxEvents.SelectedItem as Event).Cover.SourceURL);
            }
        }

        private void fetchGroups()
        {
            listBoxGroups.Invoke(new Action(() =>
                {
                    listBoxGroups.Items.Clear();
                    listBoxGroups.DisplayMember = "Name";
                }));

            foreach (Group fbGroup in m_LoggedInUser.Groups)
            {
                listBoxGroups.Invoke(new Action(() => listBoxGroups.Items.Add(fbGroup)));
            }

            listBoxGroups.Invoke(new Action(() => {
                    if (listBoxGroups.Items.Count == 0)
                    {
                        MessageBox.Show("No groups to fetch");
                    }
                }));
        }

        private void buttonFetchGroups_Click(object sender, EventArgs e)
        {
            new Thread(fetchGroups).Start();
        }

        private void listBoxGroups_SelectedValueChanged(object sender, EventArgs e)
        {
            if (listBoxGroups.SelectedItems.Count != 0)
            {
                pictureBoxGroups.LoadAsync((listBoxGroups.SelectedItem as Group).PictureLargeURL);
                textBoxGroupDescription.Text = (listBoxGroups.SelectedItem as Group).Description;
            }
        }

        private void fetchPages()
        {
            listBoxPages.Invoke(new Action(() =>
                {
                    listBoxPages.Items.Clear();
                    listBoxPages.DisplayMember = "Name";
                }));


            foreach (Page fbPage in m_LoggedInUser.LikedPages)
            {
                listBoxPages.Invoke(new Action(() => listBoxPages.Items.Add(fbPage)));
            }

            listBoxPages.Invoke(new Action(
                () =>
                    {
                        if(listBoxPages.Items.Count == 0)
                        {
                            MessageBox.Show("No pages to fetch.");
                        }
                    }));
        }

        private void buttonFetchPages_Click(object sender, EventArgs e)
        {
            new Thread(fetchPages).Start();
        }

        private void listBoxPages_SelectedValueChanged(object sender, EventArgs e)
        {
            if (listBoxPages.SelectedItems.Count != 0)
            {
                pictureBoxPages.LoadAsync((listBoxPages.SelectedItem as Page).PictureNormalURL);
            }
        }

        private void fetchAlbums()
        {
            listBoxAlbums.Invoke(new Action(() =>
                {
                    listBoxAlbums.Items.Clear();
                    listBoxAlbums.DisplayMember = "Name";
                }));

            foreach (Album fbAlbum in m_LoggedInUser.Albums)
            {
                listBoxAlbums.Invoke(new Action(() => listBoxAlbums.Items.Add(fbAlbum)));
            }

            listBoxAlbums.Invoke(
                new Action(
                    () =>
                        {
                            if(listBoxAlbums.Items.Count == 0)
                            {
                                MessageBox.Show("No album to fetch.");
                            }
                        }));
        }

        private void buttonFetchAlbums_Click(object sender, EventArgs e)
        {
            new Thread(fetchAlbums).Start();
        }

        private void listBoxAlbums_SelectedValueChanged(object sender, EventArgs e)
        {
            if (listBoxAlbums.SelectedItems.Count != 0)
            {
                pictureBoxAlbumCoverPicture.LoadAsync((listBoxAlbums.SelectedItem as Album).PictureAlbumURL);
            }
        }

        private void buttonFetchPosts_Click(object sender, EventArgs e)
        {
            new Thread(fetchPosts).Start();
        }

        private void fetchPosts()
        {
            listBoxPosts.Invoke(new Action(() => listBoxPosts.Items.Clear()));

            foreach (Post fbPost in m_LoggedInUser.Posts)
            {
                listBoxPosts.Invoke(new Action(() => listBoxPosts.Items.Add(fbPost)));
            }

            listBoxPosts.Invoke(
                new Action(
                    () =>
                        {
                            if(listBoxPosts.Items.Count == 0)
                            {
                                MessageBox.Show("No posts to fetch.");
                            }
                        }));
        }

        private void listBoxPosts_SelectedValueChanged(object sender, EventArgs e)
        {
            if (listBoxPosts.SelectedItems.Count != 0)
            {
                Post selectedPost = listBoxPosts.SelectedItem as Post;
                checkPostedItemType(selectedPost);
                showPostComments(selectedPost);
            }
        }

        private void showPostComments(Post i_SelectedPost)
        {
            listBoxPostComments.Items.Clear();
            foreach (Comment comment in i_SelectedPost.Comments)
            {
                listBoxPostComments.Items.Add(comment.Message);
            }
        }

        private void checkPostedItemType(Post i_Post)
        {
            switch (i_Post.Type)
            {
                case Post.eType.photo:
                    displaySelectedPostedItem(eComponentType.Photo);
                    pictureBoxPostedPhoto.LoadAsync(i_Post.PictureURL);
                    break;

                case Post.eType.video:
                    displaySelectedPostedItem(eComponentType.Video);
                    linkLabelPostedVideo.Text = i_Post.Link;
                    break;

                case Post.eType.status:
                    displaySelectedPostedItem(eComponentType.Status);
                    if (i_Post.Message != null)
                    {
                        textBoxPostStatusContent.Text = i_Post.Message;
                    }
                    else
                    {
                        textBoxPostStatusContent.Text = "Textless Status";
                    }

                    break;

                default:
                    displaySelectedPostedItem(eComponentType.Post);
                    break;
            }
        }

        private void createYearsComboBox()
        {
            IUserDataAnalyzer userDataAnalyzer = FeaturesManager.GetInstance(m_LoggedInUser).UserDataAnalyzer;
            int firstYear = Math.Min(userDataAnalyzer.YearOfFirstPost, userDataAnalyzer.YearOfFirstPhotoTag);
            for (int i = firstYear; i <= DateTime.Now.Year; i++)
            {
                comboBoxSelectYear.Items.Add(i);
            }
        }

        private void createCategoriesComboBox()
        {
            comboBoxFriendsCategory.Items.Add("Friends With Same Birth Month");
            comboBoxFriendsCategory.Items.Add("Friends With Same Job");
            comboBoxFriendsCategory.Items.Add("Friends From My City");
            comboBoxFriendsCategory.Items.Add("Friends From My School");
            comboBoxFriendsCategory.Items.Add("Other Friends");
            comboBoxFriendsCategory.Items.Add("All Friends");
        }

        private void displaySelectedPostedItem(eComponentType i_ComponentType)
        {
            textBoxPostStatusContent.Visible = false;
            linkLabelPostedVideo.Visible = false;
            pictureBoxPostedPhoto.Visible = false;
            switch (i_ComponentType)
            {
                case eComponentType.Photo:
                    pictureBoxPostedPhoto.Visible = true;
                    break;

                case eComponentType.Status:
                    textBoxPostStatusContent.Visible = true;
                    break;

                case eComponentType.Video:
                    linkLabelPostedVideo.Visible = true;
                    break;
            }
        }

        private void linkLabelPostedVideo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start((sender as LinkLabel).Text);
        }

        public enum eComponentType
        {
            Post,
            Event,
            Album,
            Group,
            Page,
            Status,
            Photo,
            Video,
            UserInfo,
            UserStatistics,
            PostByCategory,
            None
        }

        private void buttonUserInfo_Click(object sender, EventArgs e)
        {
            showUserInfo();
        }

        private void showUserInfo()
        {
            labelUserNameInfo.Text = m_LoggedInUser.Name;
            labelUserEmailInfo.Text = m_LoggedInUser.Email;
            labelUserBirthdayInfo.Text = m_LoggedInUser.Birthday;
            labelUserGenderInfo.Text = m_LoggedInUser.Gender.ToString();
            pictureBoxProfilePictureUserInfo.LoadAsync(m_LoggedInUser.PictureNormalURL);
            changeCurrentTab(eComponentType.UserInfo);
        }

        private void deleteUserInfoLabels()
        {
            labelUserNameInfo.Text = String.Empty;
            labelUserEmailInfo.Text = String.Empty;
            labelUserBirthdayInfo.Text = String.Empty;
            labelUserGenderInfo.Text = String.Empty;
            pictureBoxProfilePictureUserInfo.Image = null;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            changeCurrentTab(eComponentType.UserInfo);
            setEnableAppOptions(!k_IsEnableOptions);
            setLoginEnable(k_IsEnableOptions);
            m_ChartStatistics = new ChartAdapter(chartStatistics);
            m_ChartFriendsTags = new ChartAdapter(chartFriendsTags);
            m_ChartPostActivity = new ChartAdapter(chartPostActivity);

            m_ThemeSubject = new ThemeSubject(this.BackColor);
            m_MainPanelObserver = new MainPanelObserver(m_ThemeSubject, this);
            m_MenuPanelObserver = new MenuPanelObserver(m_ThemeSubject,panel1);
            comboboxThemeColor.SelectedIndex = 1;
        }

        private void buttonUserStats_Click(object sender, EventArgs e)
        {
            if (comboBoxSelectYear.Items.Count == 0)
            {
                createYearsComboBox();
            }

            changeCurrentTab(eComponentType.UserStatistics);
        }

        private void buttonFetchStatistics_Click(object sender, EventArgs e)
        {
            IUserDataAnalyzer userDataAnalyzer = FeaturesManager.GetInstance(m_LoggedInUser).UserDataAnalyzer;
            userDataAnalyzer.InitYearToPostsMap();
            m_ChartStatistics.ClearPoints("Comments Per Month");
            m_ChartStatistics.ClearPoints("Likes Per Month");
            m_ChartStatistics.ClearPoints("Tagged Photos Per Month");
            int selectedYear = int.Parse(comboBoxSelectYear.SelectedItem.ToString());
            userDataAnalyzer.SetStrategyAnalyzer(new CommentsAnalyzer());
            Dictionary<eMonth, int> userCommentsPerMonth =
                  userDataAnalyzer.GetUserStatsAtSelectedYear(selectedYear);
            if (userCommentsPerMonth != null)
            {
                m_ChartStatistics.AddPoints(userCommentsPerMonth, "Comments Per Month");
                labelCommentsExceptionInYear.Visible = false;
            }
       
            else
            {
                labelCommentsExceptionInYear.Visible = true;
            }
            userDataAnalyzer.SetStrategyAnalyzer(new LikesAnalyzer());
            Dictionary<eMonth, int> userLikePerMonth = userDataAnalyzer.GetUserStatsAtSelectedYear(selectedYear);
            if (userLikePerMonth != null)
            {
                m_ChartStatistics.AddPoints(userLikePerMonth, "Likes Per Month");
            }
          
            else
            {
                labelLikesExceptionMsg.Visible = true;
            }
        

            Dictionary<eMonth, int> userTagsPerMonth = userDataAnalyzer.GetUserTagsAtSelectedYear(selectedYear);
            m_ChartStatistics.AddPoints(userTagsPerMonth, "Tagged Photos Per Month");
        }

        private void buttonFetchFriendsTags_Click(object sender, EventArgs e)
        {
            IUserDataAnalyzer userDataAnalyzer = FeaturesManager.GetInstance(m_LoggedInUser).UserDataAnalyzer;
            m_ChartFriendsTags.ClearPoints("SeriesFriendsTags");
            Dictionary<string, int> friendToTagsNumber = userDataAnalyzer.CreateDictionaryOfFriendToTagsNumber();
            chartFriendsTags.Visible = true;
            labelFriendsTaggingException.Visible = true;
            m_ChartFriendsTags.AddPoints(friendToTagsNumber,"SeriesFriendsTags");
        }

        private void comboBoxSelectYear_SelectedValueChanged(object sender, EventArgs e)
        {
            buttonFetchStatistics.Enabled = true;
        }

        private void buttonFetchPostActivity_Click(object sender, EventArgs e)
        {
            IUserDataAnalyzer userDataAnalyzer = FeaturesManager.GetInstance(m_LoggedInUser).UserDataAnalyzer;
            m_ChartPostActivity.ClearPoints("SeriesPostActivity");
            Dictionary<int, int> yearToPostsNumber = userDataAnalyzer.CreateDictionaryOfYearToNumberOfPost();
            chartPostActivity.Visible = true;
            m_ChartPostActivity.AddPoints(yearToPostsNumber,"SeriesPostActivity");
        }

        private void buttonBrowsePhoto_Click(object sender, EventArgs e)
        {
            openFileDialogPhoto.Filter = "Image Files (JPG,PNG,GIF)|*.JPG;*.PNG;*.GIF";
            DialogResult openFileResult = openFileDialogPhoto.ShowDialog(); // Show the dialog.
            if (openFileResult == DialogResult.OK) // Test result.
            {
                string photoFileName = openFileDialogPhoto.FileName;
                pictureBoxPhotoToFriendsCategory.LoadAsync(photoFileName);
            }
        }

        private void buttonPostByCategory_Click(object sender, EventArgs e)
        {
            if (comboBoxFriendsCategory.Items.Count == 0)
            {
                createCategoriesComboBox();
            }

            changeCurrentTab(eComponentType.PostByCategory);
        }

        private void buttonPostNow_Click(object sender, EventArgs e)
        {
            PostByCategory postByCategory = FeaturesManager.GetInstance(m_LoggedInUser).PostByCategory;
            PostByCategory.eCategory selectedCategory;
            switch (comboBoxFriendsCategory.SelectedItem)
            {
                case "Friends With Same Birth Month":
                    selectedCategory = PostByCategory.eCategory.SameBirthMonth;
                    break;
                case "Friends With Same Job":
                    selectedCategory = PostByCategory.eCategory.SameJob;
                    break;
                case "Friends From My City":
                    selectedCategory = PostByCategory.eCategory.SameCity;
                    break;
                case "Friends From My School":
                    selectedCategory = PostByCategory.eCategory.SameEducation;
                    break;
                case "Other Friends":
                    selectedCategory = PostByCategory.eCategory.Others;
                    break;
                case "All Friends":
                    selectedCategory = PostByCategory.eCategory.AllFriends;
                    break;
                default:
                    selectedCategory = PostByCategory.eCategory.Others;
                    break;
            }

            try
            {
                postByCategory.CheckSelectedCategory(
                    selectedCategory,
                    pictureBoxPhotoToFriendsCategory.ImageLocation,
                    richTextBoxStatusToFriendsCategory.Text);
                labePostByCategoryException.Visible = true;
            }
            catch (Exception exception)
            {
                labePostByCategoryException.Visible = true;
            }
        }

        private void comboBoxFriendsCategory_SelectedValueChanged(object sender, EventArgs e)
        {
            PostByCategory postByCategory = FeaturesManager.GetInstance(m_LoggedInUser).PostByCategory;
            List<User> usersFromCategory;
            switch (comboBoxFriendsCategory.SelectedItem)
            {
                case "Friends With Same Birth Month":
                    usersFromCategory = postByCategory.FriendsWithSameBirthMonth;
                    break;
                case "Friends With Same Job":
                    usersFromCategory = postByCategory.FriendsWithSameJob;
                    break;
                case "Friends From My City":
                    usersFromCategory = postByCategory.FriendsWithSameCity;
                    break;
                case "Friends From My School":
                    usersFromCategory = postByCategory.FriendsWithSameEducation;
                    break;
                case "Other Friends":
                    usersFromCategory = postByCategory.FriendsWithoutSpecialCategory;
                    break;
                default:
                    usersFromCategory = postByCategory.FriendsWithoutSpecialCategory;
                    break;
            }

            listBoxFriendsCategory.Items.Clear();
            if (comboBoxFriendsCategory.SelectedItem.Equals("All Friends"))
            {
                foreach (User currentUser in m_LoggedInUser.Friends)
                {
                    listBoxFriendsCategory.Items.Add(currentUser.Name);
                }
            }

            else
            {
                foreach(User currentUser in usersFromCategory)
                {
                    listBoxFriendsCategory.Items.Add(currentUser.Name);
                }
            }
        }

        private void textBoxGroupDescription_TextChanged(object sender, EventArgs e)
        {
            textBoxGroupDescription.SelectionStart = textBoxGroupDescription.Text.Length;
            textBoxGroupDescription.ScrollToCaret();
        }

        private void textBoxPostStatusContent_TextChanged(object sender, EventArgs e)
        {
            textBoxPostStatusContent.SelectionStart = textBoxGroupDescription.Text.Length;
            textBoxPostStatusContent.ScrollToCaret();
        }

        private void comboboxThemeColor_SelectedValueChanged(object sender, EventArgs e)
        {
            if(comboboxThemeColor.SelectedItem.ToString() == "Dark")
            {
                m_ThemeSubject.ThemeColor= System.Drawing.Color.FromArgb(74, 74, 74);
            }
            else
            {
                m_ThemeSubject.ThemeColor = System.Drawing.Color.FromArgb(128, 128, 128);
            }
        }
    }
}
