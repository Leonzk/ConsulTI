using ConsulTI_back_end.Entities;
using MySql.Data.MySqlClient;
using ConsulTI_back_end.Services;

namespace ConsulTI_back_end.Services
{
    public class EmpresaSetorServices
    {
        private readonly BD _bd;

        public EmpresaSetorServices(BD bd)
        {
            _bd = bd;
        }

        private bool EmpresaSetorVinculado(int empresaId, int setorId)
        {
            var conexao = _bd.CriarConexao();
            MySqlCommand cmd = conexao.CreateCommand();

            cmd.CommandText = $@"select count(*) 
                                 from empresa_has_setor 
                                 where empresa_id = {empresaId} and 
                                       setor_id = {setorId}";

            bool vinculado = false;

            conexao.Open();
            vinculado = Convert.ToBoolean(cmd.ExecuteScalar());
            conexao.Close();

            return vinculado;
        }

        public bool VincularEmpresaSetor(int empresaId, int setorId)
        {
            var conexao = _bd.CriarConexao();

            bool sucesso = false;
            try
            {
                if(EmpresaSetorVinculado(empresaId, setorId))
                {
                    return sucesso;
                }

                MySqlCommand cmd = conexao.CreateCommand();

                cmd.CommandText = $@"INSERT INTO empresa_has_setor (empresa_id, setor_id)
                                    values({empresaId}, {setorId})";

                conexao.Open();
                sucesso = cmd.ExecuteNonQuery() >= 1;
            }
            catch (Exception ex)
            {

            }
            finally { 
                conexao.Close();
            }
            return sucesso;
        }

        public IEnumerable<EmpresaSetor> ObterEmpresasId(int empresa_id)
        {
            var conexao = _bd.CriarConexao();

            MySqlCommand cmd = conexao.CreateCommand();

            cmd.CommandText = $@"select * from empresa_has_setor where empresa_id = @id";

            cmd.Parameters.AddWithValue("@id", empresa_id);
            conexao.Open();

            MySqlDataReader dr = cmd.ExecuteReader();
            List<EmpresaSetor> empresasetor = new List<EmpresaSetor>();
            while (dr.Read())
            {
                empresasetor.Add(new EmpresaSetor()
                {
                    empresa_id = Convert.ToInt32(dr["empresa_id"]),
                    setor_id = Convert.ToInt32(dr["setor_id"]),
                    empresa = new Empresa() { },
                    setor = new Setor() { }
                });
            }
            conexao.Close();

            return empresasetor;
        }

        public IEnumerable<EmpresaSetor> ObterSetorId(int setor_id)
        {
            var conexao = _bd.CriarConexao();

            MySqlCommand cmd = conexao.CreateCommand();

            cmd.CommandText = $@"select * from empresa_has_setor where setor_id = @id";

            cmd.Parameters.AddWithValue("@id", setor_id);
            conexao.Open();

            MySqlDataReader dr = cmd.ExecuteReader();
            List<EmpresaSetor> empresasetor = new List<EmpresaSetor>();
            while (dr.Read())
            {
                empresasetor.Add(new EmpresaSetor()
                {
                    empresa_id = Convert.ToInt32(dr["empresa_id"]),
                    setor_id = Convert.ToInt32(dr["setor_id"])
                });
            }
            conexao.Close();

            return empresasetor;
        }
    }
}
