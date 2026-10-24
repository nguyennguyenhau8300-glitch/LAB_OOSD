using Microsoft.Data.SqlClient;
using System.Data;
using System.Xml.Linq;
using System.Text.RegularExpressions;
namespace QuanLyNhanSu.Data;
public static class Db
{
    public static string ConnectionString {get;set;} = LoadConnection();
    static string LoadConnection()
    {
        var path=Path.Combine(AppContext.BaseDirectory,"App.config");
        return File.Exists(path) ? XDocument.Load(path).Descendants("add").First(x=>(string?)x.Attribute("name")=="NhanSuDB").Attribute("connectionString")!.Value : @"Server=(localdb)\MSSQLLocalDB;Database=QuanLyNhanSuLAB7;Integrated Security=true;TrustServerCertificate=true";
    }
    public static SqlConnection Open()
    {
        var settings=new SqlConnectionStringBuilder(ConnectionString);
        // A running LocalDB can have a valid pipe while its instance lookup is stale.
        // Resolve only the configured user's own LocalDB instance; keep the database unchanged.
        if(settings.DataSource.StartsWith(@"(localdb)\",StringComparison.OrdinalIgnoreCase))
        {
            string instance=settings.DataSource.Split('\\').Last();
            string log=Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "Microsoft","Microsoft SQL Server Local DB","Instances",instance,"error.log");
            try
            {
                using var stream=new FileStream(log,FileMode.Open,FileAccess.Read,FileShare.ReadWrite);
                using var reader=new StreamReader(stream);
                var pipes=Regex.Matches(reader.ReadToEnd(),@"\\\\\.\\pipe\\LOCALDB#[^ \]\r\n]+");
                if(pipes.Count>0)
                {
                    var direct=new SqlConnectionStringBuilder(settings.ConnectionString)
                    {DataSource="np:"+pipes[^1].Value,ConnectTimeout=2};
                    var existing=new SqlConnection(direct.ConnectionString);
                    try { existing.Open(); return existing; }
                    catch(SqlException) { existing.Dispose(); }
                }
            }
            catch(IOException) { }
            catch(UnauthorizedAccessException) { }
        }
        var connection=new SqlConnection(settings.ConnectionString);
        try { connection.Open(); return connection; }
        catch { connection.Dispose(); throw; }
    }
    public static SqlCommand Command(SqlConnection c,SqlTransaction? tr,string sql,params object?[] values)
    {
        var cmd=c.CreateCommand();cmd.Transaction=tr;cmd.CommandText=sql;cmd.CommandTimeout=60;
        for(int i=0;i<values.Length;i++)cmd.Parameters.AddWithValue("@p"+i,values[i]??DBNull.Value);
        return cmd;
    }
    public static DataTable Query(string sql,params object?[] v){using var c=Open();return Query(c,null,sql,v);}
    public static DataTable Query(SqlConnection c,SqlTransaction? t,string sql,params object?[] v){using var cmd=Command(c,t,sql,v);using var a=new SqlDataAdapter(cmd);var dt=new DataTable();a.Fill(dt);return dt;}
    public static int Exec(SqlConnection c,SqlTransaction? t,string sql,params object?[] v){using var cmd=Command(c,t,sql,v);return cmd.ExecuteNonQuery();}
    public static object? Scalar(SqlConnection c,SqlTransaction? t,string sql,params object?[] v){using var cmd=Command(c,t,sql,v);return cmd.ExecuteScalar();}
    public static T Transaction<T>(Func<SqlConnection,SqlTransaction,T> action){using var c=Open();using var t=c.BeginTransaction(IsolationLevel.Serializable);try{T v=action(c,t);t.Commit();return v;}catch{t.Rollback();throw;}}
}
