using QuanLyNhanSu.Forms;
using QuanLyNhanSu.Services;
namespace QuanLyNhanSu;
internal static class Program
{
    [STAThread]
    static void Main(string[] args)
    {
        ApplicationConfiguration.Initialize();
        if(args.Contains("--verify")){Environment.ExitCode=Verification.Run();return;}
        if(args.Contains("--verify-ui")){Environment.ExitCode=Verification.Ui();return;}
        Application.ThreadException+=(_,e)=>Forms.FormHelper.Error(e.Exception);
        Application.Run(new FrmMain());
    }
}
