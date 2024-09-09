using StartCloneComponents;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace StartClone
{
    public sealed class TilesGroup
    {
        public string name;
        public List<Tile> tiles;
    }

    public sealed class TilesSource
    {
        //public static TilesSource Instance { get; private set; }

        public ObservableCollection<TilesGroup> Groups =
            new ObservableCollection<TilesGroup>();

        public TilesSource()
        {
            //Instance = this;
        }

        public async void addDefaultTiles()
        {
            Groups.Insert(0, new TilesGroup {
                name = "Test",
                tiles = new List<Tile>
                {
                    new Tile()
                    {
                        command = "start[hide]",
                        name = "Desktop",
                        image = new BitmapImage(new Uri(await Utils.getWallpaperPath())),
                        //image = new BitmapImage(),
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
    }

    /// <summary>
    /// Class representing Start screen tile.
    /// </summary>
    public sealed class Tile
    {
        public string command;
        public ImageSource image;
        public string name;
        public TileSize size = TileSize.Normal;
    }

    public enum TileSize
    {
        Small, // 1x1
        Normal, // 2x2
        Wide, // 4x2
        Large // 4x4
    }
}
