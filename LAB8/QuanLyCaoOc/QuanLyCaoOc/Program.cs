using QuanLyCaoOc.Data;
using QuanLyCaoOc.Forms;
using QuanLyCaoOc.Services;

namespace QuanLyCaoOc;

internal static class Program
{
    [STAThread]
    static int Main(string[] args)
    {
        ApplicationConfiguration.Initialize();
        try
        {
            if(args.Contains("--verify"))return Verification.Run();
            Db.Initialize();
            if(args.Contains("--verify-ui"))return Verification.RunUi();
            Application.Run(new FrmMain());return 0;
        }
        catch(Exception ex)
        {
            var text=ex.ToString();File.WriteAllText(Path.Combine(AppContext.BaseDirectory,"startup-error.txt"),text);
            MessageBox.Show(ex.Message+"\n\nKiểm tra LocalDB và App.config. Chi tiết: startup-error.txt","Không thể khởi động",MessageBoxButtons.OK,MessageBoxIcon.Error);return 1;
        }
    }
}
