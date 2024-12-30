using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace SqlAccountRestAPI.Helpers;
public static class UserSessionHelper
{
    [DllImport("wtsapi32.dll", SetLastError = true)]
    private static extern bool WTSEnumerateSessions(
        IntPtr hServer,
        int Reserved,
        int Version,
        out IntPtr ppSessionInfo,
        out int pCount);

    [DllImport("wtsapi32.dll")]
    private static extern void WTSFreeMemory(IntPtr pMemory);

    [DllImport("wtsapi32.dll", SetLastError = true)]
    private static extern bool WTSQueryUserToken(uint SessionId, out IntPtr Token);

    [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    private static extern bool DuplicateTokenEx(
        IntPtr ExistingToken,
        uint dwDesiredAccess,
        IntPtr lpTokenAttributes,
        int ImpersonationLevel,
        int TokenType,
        out IntPtr DuplicateToken);

    [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    private static extern bool CreateProcessAsUser(
        IntPtr hToken,
        string lpApplicationName,
        string lpCommandLine,
        IntPtr lpProcessAttributes,
        IntPtr lpThreadAttributes,
        bool bInheritHandles,
        uint dwCreationFlags,
        IntPtr lpEnvironment,
        string lpCurrentDirectory,
        ref STARTUPINFO lpStartupInfo,
        out PROCESS_INFORMATION lpProcessInformation);

    private const int WTS_CURRENT_SERVER_HANDLE = 0;
    private const uint TOKEN_DUPLICATE = 0x0002;
    private const uint TOKEN_QUERY = 0x0008;
    private const uint TOKEN_ASSIGN_PRIMARY = 0x0001;
    private const uint TOKEN_ADJUST_PRIVILEGES = 0x0020;
    private const uint GENERIC_ALL_ACCESS = 0x10000000;
    private const int SecurityImpersonation = 2;
    private const int TokenPrimary = 1;

    [StructLayout(LayoutKind.Sequential)]
    private struct WTS_SESSION_INFO
    {
        public uint SessionID;
        public IntPtr pWinStationName;
        public int State;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct STARTUPINFO
    {
        public int cb;
        public string lpReserved;
        public string lpDesktop;
        public string lpTitle;
        public uint dwX;
        public uint dwY;
        public uint dwXSize;
        public uint dwYSize;
        public uint dwXCountChars;
        public uint dwYCountChars;
        public uint dwFillAttribute;
        public uint dwFlags;
        public ushort wShowWindow;
        public ushort cbReserved2;
        public IntPtr lpReserved2;
        public IntPtr hStdInput;
        public IntPtr hStdOutput;
        public IntPtr hStdError;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct PROCESS_INFORMATION
    {
        public IntPtr hProcess;
        public IntPtr hThread;
        public uint dwProcessId;
        public uint dwThreadId;
    }

    public static bool StartApplicationInUserSession(string applicationPath)
    {
        IntPtr ppSessionInfo = IntPtr.Zero;
        IntPtr userToken = IntPtr.Zero;
        IntPtr duplicatedToken = IntPtr.Zero;

        try
        {
            // Enumerate active sessions
            if (!WTSEnumerateSessions(IntPtr.Zero, 0, 1, out ppSessionInfo, out int count))
                throw new Exception("Failed to enumerate sessions.");

            int sessionInfoSize = Marshal.SizeOf(typeof(WTS_SESSION_INFO));
            for (int i = 0; i < count; i++)
            {
                IntPtr sessionPtr = new IntPtr(ppSessionInfo.ToInt64() + i * sessionInfoSize);
                var sessionInfo = Marshal.PtrToStructure<WTS_SESSION_INFO>(sessionPtr);

                // Check if the session is active
                if (sessionInfo.State == 0 /* Active */)
                {
                    // Get the user token
                    if (WTSQueryUserToken(sessionInfo.SessionID, out userToken))
                    {
                        // Duplicate the token
                        if (!DuplicateTokenEx(userToken, GENERIC_ALL_ACCESS, IntPtr.Zero, SecurityImpersonation, TokenPrimary, out duplicatedToken))
                            throw new Exception("Failed to duplicate user token.");

                        // Start the application in the user's session
                        var startupInfo = new STARTUPINFO
                        {
                            cb = Marshal.SizeOf(typeof(STARTUPINFO)),
                            lpDesktop = "winsta0\\default" // Run on the user's desktop
                        };
                        var processInfo = new PROCESS_INFORMATION();

                        if (!CreateProcessAsUser(
                            duplicatedToken,
                            applicationPath,
                            null,
                            IntPtr.Zero,
                            IntPtr.Zero,
                            false,
                            0,
                            IntPtr.Zero,
                            null,
                            ref startupInfo,
                            out processInfo))
                            throw new Exception("Failed to start process in user's session.");

                        return true;
                    }
                }
            }

            return false;
        }
        finally
        {
            if (userToken != IntPtr.Zero) Marshal.Release(userToken);
            if (duplicatedToken != IntPtr.Zero) Marshal.Release(duplicatedToken);
            if (ppSessionInfo != IntPtr.Zero) WTSFreeMemory(ppSessionInfo);
        }
    }
}
