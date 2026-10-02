using System;

using System.Data;
using Microsoft.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;

namespace Shopping.Data
{
    public static class Db
    {
        public static string ConnectionString { get { return File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Connection.txt")).Trim(); } }
        public static SqlConnection Open() { var c=new SqlConnection(ConnectionString); c.Open(); return c; }
        public static void Initialize()
        {
            var cs = new SqlConnectionStringBuilder(ConnectionString);
            string database = cs.InitialCatalog;
            if (database != "EShopping_Prototype") throw new InvalidOperationException("Bài lab dùng CSDL EShopping_Prototype; cập nhật script nếu đổi tên.");
            cs.InitialCatalog = "master";
            var script = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database", "CuaHangOnlineDB.sql"));
            using (var c = new SqlConnection(cs.ConnectionString))
            {
                c.Open();
                foreach (string batch in Regex.Split(script, @"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase))
                    if (!string.IsNullOrWhiteSpace(batch)) using (var cmd = new SqlCommand(batch,c)) { cmd.CommandTimeout=60; cmd.ExecuteNonQuery(); }
            }
        }
        public static SqlParameter P(string name, object value) { return new SqlParameter(name, value ?? DBNull.Value); }
        public static DataTable Query(string sql, params SqlParameter[] parameters)
        {
            using (var c=Open()) using (var cmd=new SqlCommand(sql,c)) using (var a=new SqlDataAdapter(cmd))
            { cmd.Parameters.AddRange(parameters); var table=new DataTable(); a.Fill(table); return table; }
        }
    }
}
