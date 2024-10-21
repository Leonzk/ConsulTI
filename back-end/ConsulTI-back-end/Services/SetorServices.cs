using ConsulTI_back_end.Entities;
using MySql.Data.MySqlClient;

namespace ConsulTI_back_end.Services
{
    public class SetorServices
    {
        private readonly BD _bd;

        public SetorServices(BD bd) {  _bd = bd; }

        public bool Salvar(Entities.Setor setor)
        {
            bool sucesso = false;
            var conexao = _bd.CriarConexao();

            MySqlCommand cmd = conexao.CreateCommand();

            cmd.CommandText = $@"INSERT INTO Setor (descicao)
                                    VALUES(@descicao)";

            cmd.Parameters.AddWithValue("@descricao", setor.descricao);

            try
            {
                if (conexao.State != System.Data.ConnectionState.Open)
                    conexao.Open();

                int qtdeLinhasAfetadas = cmd.ExecuteNonQuery();
                //int id = Convert.ToInt32(cmd.LastInsertedId);
                cmd.CommandText = $@"SELECT LAST_INSERT_ID()";
                int id = Convert.ToInt32(cmd.ExecuteScalar());
                setor.id = id;
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

        public IEnumerable<Setor> ObterTodos()
        {
            var conn = _bd.CriarConexao();

            MySqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = $@"select * from Setor";

            if (conn.State != System.Data.ConnectionState.Open)
            {
                conn.Open();
            }

            MySqlDataReader dr = cmd.ExecuteReader();
            List<Setor> setors = new List<Setor>();
            while (dr.Read())
            {

                setors.Add(new Setor()
                {
                    id = Convert.ToInt32(dr["id"]),
                    descricao = dr["descricao"].ToString()
                });
            }
            conn.Close();

            return setors;
        }

        public Setor? Obter(int id)
        {

            var conn = _bd.CriarConexao();


            MySqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = $@"select id, descricao
                                 from Setor
                                 where id = @id";

            cmd.Parameters.AddWithValue("@id", id);

            if (conn.State != System.Data.ConnectionState.Open)
            {
                conn.Open();
            }

            MySqlDataReader dr = cmd.ExecuteReader();
            Setor? setor = null;
            if (dr.Read())
            {
                setor = new Setor()
                {
                    id = Convert.ToInt32(dr["id"]),
                    descricao = dr["descricao"].ToString()
                };
            }
            conn.Close();
            return setor;
        }

        public bool Atualizar(Entities.Setor setor)
        {
            bool sucesso = false;
            var conexao = _bd.CriarConexao();

            MySqlCommand cmd = conexao.CreateCommand();

            cmd.CommandText = $@"UPDATE Setor SET id = @id, descricao = @descricao WHERE (id = @id)";

            cmd.Parameters.AddWithValue("@id", setor.id);
            cmd.Parameters.AddWithValue("@descricao", setor.descricao);

            try
            {
                if (conexao.State != System.Data.ConnectionState.Open)
                    conexao.Open();
                int qtdeLinhasAfetadas = cmd.ExecuteNonQuery();
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

        public bool Deletar(int id)
        {

            bool sucesso = false;
            var conexao = _bd.CriarConexao();

            MySqlCommand cmd = conexao.CreateCommand();

            cmd.CommandText = $@"DELETE FROM Setor WHERE id = {id}";

            try
            {
                if (conexao.State != System.Data.ConnectionState.Open)
                {
                    conexao.Open();
                }
                int qtdeLinhasAfetadas = cmd.ExecuteNonQuery();
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
    }
}
