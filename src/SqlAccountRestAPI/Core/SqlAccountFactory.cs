using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System;
using SqlAccountRestAPI.Helpers;
using System.Reflection;
namespace SqlAccountRestAPI.Core;

public class SqlAccountingFactory : IDisposable
{
    //     [DllImport("kernel32.dll", SetLastError = true)]
    //     public static extern uint WTSGetActiveConsoleSessionId();

    //     [DllImport("wtsapi32.dll", SetLastError = true)]
    //     public static extern bool WTSQuerySessionInformation(IntPtr hServer, uint sessionId, WTS_INFO_CLASS wtsInfoClass, out IntPtr ppBuffer, out uint pBytesReturned);

    //     [DllImport("user32.dll", SetLastError = true)]
    //     public static extern bool SetThreadDesktop(uint hDesktop);

    //     public enum WTS_INFO_CLASS
    // {
    //     WTSUserName = 5,
    //     WTSDomainName = 7,
    //     WTSConnectState = 8,
    //     WTSClientName = 10,
    //     WTSClientAddress = 14,
    //     WTSIdleTime = 15,
    //     WTSLogonTime = 16,
    //     WTSIncomingBytes = 17,
    //     WTSOutgoingBytes = 18,
    //     WTSIncomingFrames = 19,
    //     WTSOutgoingFrames = 20,
    //     WTSClientProtocolType = 22,
    //     WTSClientDirectory = 23,
    //     WTSClientBuildNumber = 24,
    //     WTSClientNameLength = 25,
    //     WTSClientAddressLength = 26,
    //     WTSClientDirectoryLength = 27,
    //     WTSClientBuildNumberLength = 28,
    //     WTSSessionId = 29,
    // }

    private dynamic? _app = null;
    public dynamic GetInstance()
    {
        if (_app != null)
        {
            try
            {
                _app.IsLogin();
                return _app;
            }
            catch (COMException)
            {
                _app = null;
            }
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {

            var lBizType = Type.GetTypeFromProgID("SQLAcc.BizApp");

            if (lBizType == null)
                throw new Exception("Cannot load SQLAcc.BizApp Assembly.");

            _app = Activator.CreateInstance(lBizType);

            if (_app == null)
                throw new Exception("Cannot create instance of SQLAcc.BizApp.");

            return _app!;
        }
        else
        {
            throw new NotSupportedException("SQLAcc.BizApp is not supported on this platform.");
        }
    }

    public void Dispose()
    {
        if (_app != null)
        {
            System.Runtime.InteropServices.Marshal.ReleaseComObject(_app);
        }
    }

    public void Release()
    {
        System.Runtime.InteropServices.Marshal.ReleaseComObject(_app);
        _app = null;
    }
}
