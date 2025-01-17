using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System;
using SqlAccountRestAPI.Helpers;
using System.Reflection;
namespace SqlAccountRestAPI.Core;

public class SqlAccountFactory : IDisposable
{
    private dynamic? _app = null;
    public dynamic GetInstance(bool autoLogin = true)
    {
        if (_app != null)
        {
            try
            {
                var isLoginTask = Task.Run(() => _app.IsLogin());

                if (!isLoginTask.Wait(TimeSpan.FromSeconds(10)))
                {
                    throw new TimeoutException("The login process took too long.");
                }

                // The app must be logined
                if (!isLoginTask.Result)
                {
                    throw new InvalidOperationException("Login failed: _app.IsLogin() returned false.");
                }

                return _app;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Release();
            }
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {

            var lBizType = Type.GetTypeFromProgID("SQLAcc.BizApp");

            if (lBizType == null)
                throw new Exception("Cannot load SQLAcc.BizApp Assembly.");

            EndProcess("SQLACC");
            _app = Activator.CreateInstance(lBizType);
            if(autoLogin)
            {
                var loginInfo = SqlAccountLoginHelper.ReLogin();
                if(loginInfo.Count == 0) return _app!;
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

    public void EndProcess(string processName)
    {

        var shellScript = $@"
        $processName = '{processName}'
        $process = Get-Process -Name $processName -ErrorAction SilentlyContinue
        if ($process) {{ 
            Stop-Process -Name $processName -Force
            Write-Host 'Process $processName has stopped.'
        }}";
        _ = SystemHelper.RunPowerShellCommand(shellScript);
        Thread.Sleep(1000);
    }
}
