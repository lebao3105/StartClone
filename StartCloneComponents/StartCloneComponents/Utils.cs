using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;

namespace StartCloneComponents
{
    public sealed class Utils
    {
        [DllImport("shell32.dll", EntryPoint = "#261",
            CharSet = CharSet.Unicode, PreserveSig = false)]
        internal static extern void GetUserTilePath(
            string username, UInt32 flags,
            StringBuilder picpath, int maxLength
        );

        public static IAsyncOperation<string> getWallpaperPath()
        {
            return getWallpaperPath_Impl().AsAsyncOperation();
        }

        private static async Task<string> getWallpaperPath_Impl()
        {
            //try
            //{
            //    StorageFolder themesFolder = await GetFolderAsync("ms-appdata:///roaming/Microsoft\\Windows\\Themes");
            //    var itemsList = await themesFolder.GetItemsAsync();
            //    foreach (var item in itemsList)
            //    {
            //        if (item.Name == "CachedFiles" && item.IsOfType(StorageItemTypes.Folder))
            //        {
            //            return (await (item as StorageFolder).GetFilesAsync()).First(
            //                it => item.Name.Contains("Cached")
            //            ).Path;
            //        }

            //        if (item.Name == "TranscodedWallpaper" && item.IsOfType(StorageItemTypes.File))
            //        {
            //            return item.Path;
            //        }
            //    }
            //    return "";
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            return "ms-appdata:///roaming/Microsoft/Windows/Themes/TranscodedWallpaper";
        }

        public static BitmapImage GetUserImage()
        {
            var sb = new StringBuilder(1000);
            GetUserTilePath(null, 0x80000000, sb, sb.Capacity);
            return new BitmapImage(new Uri(sb.ToString(), UriKind.Absolute));
        }
    }
}
