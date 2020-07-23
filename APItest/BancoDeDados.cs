using System.Text;

namespace APItest
{
    public static class BancoDeDados
    {
        public static string StringDeConexao()
        {
            StringBuilder stringDeConexao = new StringBuilder();

            string bdServer = "DESKTOP-BG41172\\SQLEXPRESS";
            string bdDataBase = "bd_dev";
            string bdUserName = "sa";
            string bdPassword = "Ie^oWpRoj5uD";

            stringDeConexao.Append("Data Source=");
            stringDeConexao.Append(bdServer);
            stringDeConexao.Append(";");

            stringDeConexao.Append("Initial Catalog=");
            stringDeConexao.Append(bdDataBase);
            stringDeConexao.Append(";");

            stringDeConexao.Append("User ID=");
            stringDeConexao.Append(bdUserName);
            stringDeConexao.Append(";");

            stringDeConexao.Append("Password=");
            stringDeConexao.Append(bdPassword);
            stringDeConexao.Append(";");

            stringDeConexao.Append("Connect Timeout=600");
            stringDeConexao.Append(";");

            return stringDeConexao.ToString();
        }

    }
}
