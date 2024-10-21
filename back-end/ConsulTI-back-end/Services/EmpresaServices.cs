using ConsulTI_back_end.Entities;
using MySql.Data.MySqlClient;

namespace ConsulTI_back_end.Services
{
    public class EmpresaServices
    {
        private readonly BD _bd;
        
        public EmpresaServices(BD bd) {  _bd = bd; }

        public bool Salvar(Entities.Empresa empresa)
        {
            bool sucesso = false;
            var conexao = _bd.CriarConexao();

            MySqlCommand cmd = conexao.CreateCommand();

            cmd.CommandText = $@"INSERT INTO Empresa (razao_social, nome_fantasia, cnpj)
                                    VALUES(@razao_social, @nome_fantasia, @cnpj)";

            cmd.Parameters.AddWithValue("@razao_social", empresa.razao_social);
            cmd.Parameters.AddWithValue("@nome_fantasia", empresa.nome_fantasia);
            cmd.Parameters.AddWithValue("@cnpj", empresa.cnpj);

            try
            {
                if (conexao.State != System.Data.ConnectionState.Open)
                    conexao.Open();

                int qtdeLinhasAfetadas = cmd.ExecuteNonQuery();
                //int id = Convert.ToInt32(cmd.LastInsertedId);
                cmd.CommandText = $@"SELECT LAST_INSERT_ID()";
                int id = Convert.ToInt32(cmd.ExecuteScalar());
                empresa.id = id;
                sucesso = true;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conexao.Close();
            }


            return sucesso;
        }

        public IEnumerable<Empresa> ObterTodos()
        {
            var conn = _bd.CriarConexao();

            MySqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = $@"select * from Empresa";

            conn.Open();

            MySqlDataReader dr = cmd.ExecuteReader();
            List<Empresa> empresas = new List<Empresa>();
            while (dr.Read())
            {
                
                empresas.Add(new Empresa()
                {
                    id = Convert.ToInt32(dr["id"]),
                    razao_social = dr["razao_social"].ToString(),
                    nome_fantasia = dr["nome_fantasia"].ToString(),
                    cnpj = dr["cnpj"].ToString()
                });
            }
            conn.Close();

            return empresas;
        }

        public Empresa? Obter(int id)
        {

            var conn = _bd.CriarConexao();


            MySqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = $@"select id, razao_social, nome_fantasia, cnpj
                                 from Empresa
                                 where id = @id";

            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();

            MySqlDataReader dr = cmd.ExecuteReader();
            Empresa? empresa = null;
            if (dr.Read())
            {
                empresa = new Empresa()
                {
                    id = Convert.ToInt32(dr["id"]),
                    razao_social = dr["razao_social"].ToString(),
                    nome_fantasia = dr["nome_fantasia"].ToString(),
                    cnpj = dr["cnpj"].ToString()
                };
            }
            conn.Close();

            return empresa;
        }

    }
}
