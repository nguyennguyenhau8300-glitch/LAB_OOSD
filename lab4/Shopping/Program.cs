using System;
using System.IO;
using System.Windows.Forms;
using Shopping.Data;
using Shopping.Forms;

namespace Shopping
{
    internal static class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            ApplicationConfiguration.Initialize();
            try
            {
                Db.Initialize();var context=new LabContext();context.Accounts.SeedDemo();
                if(args.Length>0&&args[0]=="--verify") {Verification.Run(context,args.Length>1?args[1]:"verification.txt");return;}
                Application.Run(new FrmTrangChu(context));
            }
            catch(Exception ex)
            {
                if(args.Length>0&&args[0]=="--verify"){File.WriteAllText(args.Length>1?args[1]:"verification.txt","FAIL\n"+ex);Environment.ExitCode=1;return;}
                MessageBox.Show("Không thể khởi động bài lab.\n\n"+ex.Message+"\n\nKiểm tra SQL Server LocalDB và Connection.txt.","e-SHOPPING",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
    }
}
