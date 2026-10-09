namespace QuanLyCongTyDuLich
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.ThrowException);
            if (args.Length > 0 && args[0] == "--check-forms")
            {
                FormDiagnostics.Run(args.Length > 1 ? args[1] : Path.Combine(Path.GetTempPath(), "DuLichFormCheck"));
                return;
            }
            try
            {
                using (var cn = Data.Db.OpenConnection()) { }
                Application.Run(new Forms.FrmMain());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể chạy ứng dụng. Kiểm tra SQL Server và cấu hình kết nối.\n\n" + ex.Message,
                    "Quản lý công ty du lịch", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
