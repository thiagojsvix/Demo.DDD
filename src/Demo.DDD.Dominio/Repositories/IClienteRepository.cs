using Demo.DDD.Domain.Entitys;

namespace Demo.DDD.Domain.Repositories
{
    public interface IClienteRepository: IRepository<Cliente>
    {
        bool ExisteDocumento(string documento);
        bool ExisteEmail(string email);
        long ObterNovoCodigoPessoa();

        void Salvar(Cliente entity);
    }
}
