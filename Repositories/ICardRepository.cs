using ApiProdutos.Models;

namespace ApiProdutos.Repositories
{
    public interface ICardRepository
    {
        Task<IEnumerable<Card>> GetAllAsync();
        Task<Card> GetByIdAsync(int id);
        Task<int> AddAsync(Card card);
        Task<bool> UpdateAsync(Card card);
        Task<bool> DeleteAsync(int id);
    }
}
