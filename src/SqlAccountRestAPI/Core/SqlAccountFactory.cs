using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System;
namespace SqlAccountRestAPI.Core;

public class SqlAccountingFactory : IDisposable
{
    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern uint WTSGetActiveConsoleSessionId();

    [DllImport("user32.dll", SetLastError = true)]
    public static extern bool SetThreadDesktop(uint hDesktop);
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
            {
                throw new Exception("Cannot load SQLAcc.BizApp Assembly");
            }
            
            uint sessionId = WTSGetActiveConsoleSessionId();

            // Set the desktop of the current thread to the user's session
            SetThreadDesktop(sessionId);

            _app = Activator.CreateInstance(lBizType);

            if (_app == null)
            {
                throw new Exception("Cannot create instance of SQLAcc.BizApp");
            }

            return _app!;
        }
        else
        {
            throw new NotSupportedException("SQLAcc.BizApp is not supported on this platform");
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
