using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System;
using SqlAccountRestAPI.Helpers;
using System.Reflection;
namespace SqlAccountRestAPI.Core;

public class SqlAccountFactory : IDisposable
{
    private dynamic? _app = null;
    public dynamic GetInstance()
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
            catch (TimeoutException)
            {
                Console.WriteLine("Timeout: Login process exceeded 10 seconds.");
                _app = null;
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
                _app = null;
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

            EndProcess("SQLACC");
            _app = Activator.CreateInstance(lBizType);

            var loginInfo = SqlAccountLoginHelper.ReLogin();
            var username = loginInfo[0];
            var password = loginInfo[1];
            _app!.Login(username, password);

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
