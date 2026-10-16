namespace QuanLyHoSoBenhAn;

internal static class Program
{
    [STAThread]
    static void Main(string[] args)
    {
        ApplicationConfiguration.Initialize(); try
        {
            if (args.Contains("--verify")) { Verification.Run(); return; }
            if (args.Contains("--verify-catalog")) { DanhMucVerification.Run(); return; }
            if (args.Contains("--screenshots")) { Verification.Screenshots(args.Last()); return; }
            using (var c = Data.Db.OpenConnection()) { }
            Application.Run(new Forms.FrmMain());
        }
        catch (Exception e) { if (args.Contains("--verify") || args.Contains("--verify-catalog")) { Console.Error.WriteLine(e); var logDir=Path.Combine(AppContext.BaseDirectory,"Verification");Directory.CreateDirectory(logDir);File.AppendAllText(Path.Combine(logDir,"results.txt"),Environment.NewLine+"FAIL Exception: "+e.Message+Environment.NewLine); Environment.ExitCode = 1; return; } MessageBox.Show("Kiểm tra SQL Server và chạy QuanLyHoSoBenhAn.sql trước.\n" + e.Message, "LAB6"); Environment.ExitCode = 1; }
    }
}
