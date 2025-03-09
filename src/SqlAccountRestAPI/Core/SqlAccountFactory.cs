using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System;
using SqlAccountRestAPI.Helpers;
using System.Diagnostics;
using System.Reflection;
namespace SqlAccountRestAPI.Core;

public class SqlAccountFactory : IDisposable
{
    private dynamic? _app = null;
    public dynamic GetInstance(bool autoLogin = true)
    {
        if (_app != null)
        {
            try {
                var comResponsiveChecker = SystemHelper.IsComObjectResponsive(() => _app.IsLogin, TimeSpan.FromSeconds(5));
                if (!comResponsiveChecker || !_app.IsLogin) {
                    throw new Exception();
                }
                return _app;
            }
            catch {
                SystemHelper.EndProcess("SQLACC");
                Thread.Sleep(5000);
                _app = null;
            }
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {

            var lBizType = Type.GetTypeFromProgID("SQLAcc.BizApp");

            if (lBizType == null)
                throw new Exception("Cannot load SQLAcc.BizApp Assembly.");

            _app = Activator.CreateInstance(lBizType);
            if (autoLogin)
            {
                var loginInfo = SqlAccountLoginHelper.ReLogin();
                if (loginInfo.Count == 0)
                    throw new Exception("The login information in the credentials file is invalid or outdated. Please log in again to refresh your credentials.");
                var username = loginInfo[0];
                var password = loginInfo[1];
                _app!.Login(username, password);
            }

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
