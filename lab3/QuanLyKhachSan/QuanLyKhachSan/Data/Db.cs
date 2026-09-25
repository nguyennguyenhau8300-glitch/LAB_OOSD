using Microsoft.Data.SqlClient;
using System.Data;

namespace QuanLyKhachSan.Data
{
    public static class Db
    {
        private static readonly string connectionString =
            @"Server=LAPTOP-8NKHKTEP\SQLEXPRESS01;
              Database=QuanLyKhachSan;
              User Id=sa;
              Password=123;
              TrustServerCertificate=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        public static DataTable Query(
            string sql,
            params SqlParameter[] parameters)
        {
            DataTable dt = new DataTable();

            using SqlConnection conn = GetConnection();
            using SqlCommand cmd = new SqlCommand(sql, conn);

            if (parameters != null && parameters.Length > 0)
                cmd.Parameters.AddRange(parameters);

            conn.Open();

            using SqlDataAdapter adapter =
                new SqlDataAdapter(cmd);

            adapter.Fill(dt);

            return dt;
        }

        public static int Execute(
            string sql,
            params SqlParameter[] parameters)
        {
            using SqlConnection conn = GetConnection();
            using SqlCommand cmd = new SqlCommand(sql, conn);

            if (parameters != null && parameters.Length > 0)
                cmd.Parameters.AddRange(parameters);

            conn.Open();

            return cmd.ExecuteNonQuery();
        }

        public static object? Scalar(
            string sql,
            params SqlParameter[] parameters)
        {
            using SqlConnection conn = GetConnection();
            using SqlCommand cmd = new SqlCommand(sql, conn);

            if (parameters != null && parameters.Length > 0)
                cmd.Parameters.AddRange(parameters);

            conn.Open();

            return cmd.ExecuteScalar();
        }
    }
}