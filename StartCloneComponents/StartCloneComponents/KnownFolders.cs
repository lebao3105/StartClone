using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace StartCloneComponents
{
    [Flags]
    public enum KnownFolderFlag : uint
    {
        None = 0x0,
        CREATE = 0x8000,
        DONT_VERFIY = 0x4000,
        DONT_UNEXPAND = 0x2000,
        NO_ALIAS = 0x1000,
        INIT = 0x800,
        DEFAULT_PATH = 0x400,
        NOT_PARENT_RELATIVE = 0x200,
        SIMPLE_IDLIST = 0x100,
        ALIAS_ONLY = 0x80000000
    }

    // https://stackoverflow.com/a/39532717
    // https://learn.microsoft.com/en-us/dotnet/desktop/winforms/controls/known-folder-guids-for-file-dialog-custom-places?view=netframeworkdesktop-4.8
    public static class KnownFolderFinder
    {
        public static Guid ChangeReplaceProg
        {
            get { return new Guid("{DF7266AC-9274-4867-8D55-3BD661DE872D}"); }
        }

        public static Guid AllPrograms
        {
            get { return new Guid("{1e87508d-89c2-42f0-8a7e-645a0f50ca58}"); }
        }

        public static Guid UserRoaming
        {
            get { return new Guid("{3EB685DB-65F9-4CF6-A03A-E3EF65729F3D}"); }
        }

        [DllImport("shell32.dll")]
        static extern int SHGetKnownFolderPath([MarshalAs(UnmanagedType.LPStruct)] Guid rfid, uint dwFlags, IntPtr hToken, out IntPtr pszPath);

        public static string GetFolderFromKnownFolderGUID(Guid guid)
        {
            return pinvokePath(guid, KnownFolderFlag.DEFAULT_PATH);
        }

        private static string pinvokePath(Guid guid, KnownFolderFlag flags)
        {
            IntPtr pPath;
            SHGetKnownFolderPath(guid, (uint)flags, IntPtr.Zero, out pPath); // public documents

            string path = Marshal.PtrToStringUni(pPath);
            Marshal.FreeCoTaskMem(pPath);
            return path;
        }
    }
}
