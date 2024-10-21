using MySql.Data.MySqlClient;

namespace ConsulTI_back_end
{
    public class BD
    {
        public MySqlConnection CriarConexao()
        {
            string strCon = "Server=localhost;Database=mydb;Uid=root;Pwd=Tvtlnbks09";//Trocar conexão com o banco
            MySqlConnection conexao = new MySqlConnection(strCon);

            return conexao;
        }
    }
}
