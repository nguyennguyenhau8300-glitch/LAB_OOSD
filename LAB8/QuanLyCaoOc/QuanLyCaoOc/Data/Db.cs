using System.Data;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Microsoft.Data.SqlClient;

namespace QuanLyCaoOc.Data;

public static class Db
{
    public static string ConnectionString { get; set; } = XDocument.Load(Path.Combine(AppContext.BaseDirectory, "App.config"))
        .Descendants("add").Single(x => (string?)x.Attribute("name") == "CaoOcDB").Attribute("connectionString")!.Value;

    public static SqlConnection Open(string? database = null)
    {
        var settings = new SqlConnectionStringBuilder(ConnectionString);
        if (database != null) settings.InitialCatalog = database;
        // Resolve only this configured LocalDB user's own pipe if instance discovery is stale.
        if (settings.DataSource.StartsWith(@"(localdb)\", StringComparison.OrdinalIgnoreCase))
        {
            var instance = settings.DataSource.Split('\\').Last();
            var log = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Microsoft", "Microsoft SQL Server Local DB", "Instances", instance, "error.log");
            try
            {
                using var stream = new FileStream(log, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                using var reader = new StreamReader(stream);
                var pipes = Regex.Matches(reader.ReadToEnd(), @"\\\\\.\\pipe\\LOCALDB#[^ \]\r\n]+");
                if (pipes.Count > 0)
                {
                    var direct = new SqlConnectionStringBuilder(settings.ConnectionString) { DataSource = "np:" + pipes[^1].Value, ConnectTimeout = 3 };
                    var connection = new SqlConnection(direct.ConnectionString);
                    try { connection.Open(); return connection; } catch (SqlException) { connection.Dispose(); }
                }
            }
            catch (IOException) { }
            catch (UnauthorizedAccessException) { }
        }
        var result = new SqlConnection(settings.ConnectionString);
        try { result.Open(); return result; } catch { result.Dispose(); throw; }
    }

    public static SqlCommand Command(SqlConnection c, SqlTransaction? tx, string sql, params object?[] args)
    {
        var cmd = c.CreateCommand(); cmd.Transaction = tx; cmd.CommandText = sql; cmd.CommandTimeout = 60;
        for (int i = 0; i < args.Length; i++)
        {
            object? value = args[i];
            var parameter = cmd.Parameters.AddWithValue("@p" + i, value ?? DBNull.Value);
            if (value is decimal) { parameter.SqlDbType = SqlDbType.Decimal; parameter.Precision = 18; parameter.Scale = 2; }
            if (value is DateTime date) { parameter.SqlDbType = SqlDbType.DateTime2; parameter.Value = date; }
        }
        return cmd;
    }
    public static DataTable Query(string sql, params object?[] args) { using var c = Open(); return Query(c, null, sql, args); }
    public static DataTable Query(SqlConnection c, SqlTransaction? tx, string sql, params object?[] args)
    { using var cmd = Command(c, tx, sql, args); using var adapter = new SqlDataAdapter(cmd); var data = new DataTable(); adapter.Fill(data); return data; }
    public static int Exec(SqlConnection c, SqlTransaction? tx, string sql, params object?[] args)
    { using var cmd = Command(c, tx, sql, args); return cmd.ExecuteNonQuery(); }
    public static object? Scalar(SqlConnection c, SqlTransaction? tx, string sql, params object?[] args)
    { using var cmd = Command(c, tx, sql, args); return cmd.ExecuteScalar(); }
    public static T Transaction<T>(Func<SqlConnection, SqlTransaction, T> action)
    {
        using var c = Open(); using var tx = c.BeginTransaction(IsolationLevel.Serializable);
        try
        {
            // All domain writes share this transaction-owned lock. The lab favors
            // correctness and short atomic operations over parallel write throughput.
            var code = Convert.ToInt32(Scalar(c, tx, "DECLARE @r int; EXEC @r=sys.sp_getapplock @Resource=N'LAB8_WRITE',@LockMode='Exclusive',@LockOwner='Transaction',@LockTimeout=15000; SELECT @r;"));
            if (code < 0) throw new InvalidOperationException("Dữ liệu đang được cập nhật. Vui lòng thử lại.");
            var result = action(c, tx); tx.Commit(); return result;
        }
        catch { try { tx.Rollback(); } catch (InvalidOperationException) { } throw; }
    }
    public static void Initialize()
    {
        var name = new SqlConnectionStringBuilder(ConnectionString).InitialCatalog;
        using (var master = Open("master"))
        {
            if (Scalar(master, null, "SELECT DB_ID(@p0)", name) is DBNull or null)
                Exec(master, null, "CREATE DATABASE [" + name.Replace("]", "]]") + "]");
        }
        using var c = Open();
        string script = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "Data", "QuanLyCaoOc.sql"));
        foreach (string batch in Regex.Split(script, @"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase))
            if (!string.IsNullOrWhiteSpace(batch)) Exec(c, null, batch);
    }
}
