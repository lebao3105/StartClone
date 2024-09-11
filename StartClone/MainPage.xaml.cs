using StartClone.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.System.UserProfile;
using StartCloneComponents;
using System.Collections.Specialized;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace StartClone
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private TilesSource tiles = new TilesSource();
        private NavigationHelper navigationHelper;

        public ObservableCollection<TilesGroup> DefaultViewModel
        {
            get { return tiles.Groups; }
        }

        /// <summary>
        /// NavigationHelper is used on each page to aid in navigation and 
        /// process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        public MainPage()
        {
            tiles.LoadData();
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            InitializeItems();
        }

        private async void InitializeItems()
        {
            /// TODO:
            /// Windows.System.UserProfile is deprecated on Windows 10.
            /// External API calls needed.
            username.Text = await Utils.getUserName();
            profilePicture.Fill = new ImageBrush()
            {
                ImageSource = Utils.GetUserImage()
            };
        }

        private void GridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var itemCommand = ((Tile)e.ClickedItem).command;
            switch (itemCommand)
            {
                case "start[hide]":
                    this.Visibility = Visibility.Collapsed;
                    break;

                case "":
                    break;
            }
        }

        /// <summary>
        /// Invoked when a group header is clicked.
        /// </summary>
        /// <param name="sender">The Button used as a group header for the selected group.</param>
        /// <param name="e">Event data that describes how the click was initiated.</param>
        void Header_Click(object sender, RoutedEventArgs e)
        {
            // Determine what group the Button instance represents
            var group = (sender as FrameworkElement).DataContext;

            // Navigate to the appropriate destination page, configuring the new page
            // by passing required information as a navigation parameter
            this.Frame.Navigate(typeof(GroupDetails), (group as TilesGroup));
        }

        private void groupedItemsViewSource_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {

        }
        #region NavigationHelper registration

        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// 
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="Common.NavigationHelper.LoadState"/>
        /// and <see cref="Common.NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion
    }
}
