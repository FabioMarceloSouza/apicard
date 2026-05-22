using System.Data;

namespace ApiProdutos.Context
{
    public interface IDbContext
    {
        IDbConnection GetConnection();
    }
}
