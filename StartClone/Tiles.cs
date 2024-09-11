using StartCloneComponents;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace StartClone
{

    /// <summary>
    /// Class representing Start screen tile.
    /// </summary>
    public sealed class Tile
    {
        public string command { get; set; }
        public ImageSource image { get; set; }
        public string name { get; set; }
        public TileSize size { get; set; } = TileSize.Normal;
    }

    public sealed class TilesGroup
    {
        public string name { get; set; }
        public ObservableCollection<Tile> tiles { get; set; }
    }

    public sealed class TilesSource
    {
        public ObservableCollection<TilesGroup> Groups
        { get; set; }

        public async void addDefaultTiles()
        {
            Groups.Insert(0, new TilesGroup {
                name = "Test",
                tiles = new ObservableCollection<Tile>
                {
                    new Tile()
                    {
                        command = "start[hide]",
                        name = "Desktop",
                        image = new BitmapImage( new Uri(await Utils.getWallpaperPath(), UriKind.Absolute) ),
                        size = TileSize.Wide
                    },
                    new Tile()
                    {
                        command = "",
                        name = "Placeholder",
                        image = new BitmapImage()
                    }
                }
            });
        }

        public void SaveData()
        {
            try
            {
                ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
                localSettings.Values["Groups"] = Groups.ToArray();
            }
            catch
            {

            }
        }

        public void LoadData()
        {
            try
            {
                ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
                Groups = localSettings.Values["Groups"] as ObservableCollection<TilesGroup>;
                if (Groups == null)
                {
                    Groups = new ObservableCollection<TilesGroup>();
                    addDefaultTiles();
                }
            }
            catch
            {
                addDefaultTiles();
            }
        }
    }

    public enum TileSize
    {
        Small, // 1x1
        Normal, // 2x2
        Wide, // 4x2
        Large // 4x4
    }
}
