using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace SqlAccountRestAPI.Helpers;
public static class ComHelper
{
    public static object? GetActiveObject(string progID)
    {
        IRunningObjectTable rot = null!;
        IEnumMoniker enumMoniker = null!;
        IntPtr fetched = IntPtr.Zero;

        try
        {
            // Get the running object table
            GetRunningObjectTable(0, out rot);
            if (rot == null) return null;

            // Enumerate the monikers
            rot.EnumRunning(out enumMoniker);
            enumMoniker.Reset();

            // Create the ProgID lookup
            IMoniker[] monikers = new IMoniker[1];
            while (enumMoniker.Next(1, monikers, fetched) == 0)
            {
                rot.GetObject(monikers[0], out object obj);

                if (obj != null)
                {
                    string? displayName = GetDisplayName(monikers[0]);
                    if (!string.IsNullOrEmpty(displayName) && displayName.Contains(progID))
                    {
                        return obj;
                    }
                }
            }
        }
        finally
        {
            if (fetched != IntPtr.Zero)
                Marshal.Release(fetched);
            if (enumMoniker != null)
                Marshal.ReleaseComObject(enumMoniker);
            if (rot != null)
                Marshal.ReleaseComObject(rot);
        }

        return null;
    }

    private static string GetDisplayName(IMoniker moniker)
    {
        moniker.GetDisplayName(null, null, out string displayName);
        return displayName;
    }

    [DllImport("ole32.dll")]
    private static extern int GetRunningObjectTable(int reserved, out IRunningObjectTable pprot);
}
