using ApiProdutos.Context;
using ApiProdutos.Models;
using Dapper;

namespace ApiProdutos.Repositories
{
    public class CardRepository : ICardRepository
    {
        private readonly IDbContext _dbContext;

        public CardRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Card>> GetAllAsync()
        {
            using (var connection = _dbContext.GetConnection())
            {
                connection.Open();
                return await connection.QueryAsync<Card>("SELECT ID_CARD as IdCard, NM_CARD as NmCard, DS_TRANSLATION as DsTranslation, DT_CREATE as DtCreate, DT_UPDATE as DtUpdate FROM TB_CARD ORDER BY DT_CREATE DESC");
            }
        }

        public async Task<Card> GetByIdAsync(int id)
        {
            using (var connection = _dbContext.GetConnection())
            {
                connection.Open();
                return await connection.QueryFirstOrDefaultAsync<Card>(
                    "SELECT ID_CARD as IdCard, NM_CARD as NmCard, DS_TRANSLATION as DsTranslation, DT_CREATE as DtCreate, DT_UPDATE as DtUpdate FROM TB_CARD WHERE ID_CARD = @Id", 
                    new { Id = id });
            }
        }

        public async Task<int> AddAsync(Card card)
        {
            using (var connection = _dbContext.GetConnection())
            {
                connection.Open();
                var sql = @"INSERT INTO TB_CARD (NM_CARD, DS_TRANSLATION, DT_CREATE) 
                           VALUES (@NmCard, @DsTranslation, @DtCreate);
                           SELECT CAST(SCOPE_IDENTITY() as int)";
                
                var parameters = new
                {
                    NmCard = card.NmCard,
                    DsTranslation = card.DsTranslation,
                    DtCreate = DateTime.Now
                };
                
                return await connection.ExecuteScalarAsync<int>(sql, parameters);
            }
        }

        public async Task<bool> UpdateAsync(Card card)
        {
            using (var connection = _dbContext.GetConnection())
            {
                connection.Open();
                var sql = @"UPDATE TB_CARD 
                           SET NM_CARD = @NmCard, DS_TRANSLATION = @DsTranslation, DT_UPDATE = @DtUpdate 
                           WHERE ID_CARD = @IdCard";
                
                var parameters = new
                {
                    NmCard = card.NmCard,
                    DsTranslation = card.DsTranslation,
                    DtUpdate = DateTime.Now,
                    IdCard = card.IdCard
                };
                
                var result = await connection.ExecuteAsync(sql, parameters);
                return result > 0;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (var connection = _dbContext.GetConnection())
            {
                connection.Open();
                var result = await connection.ExecuteAsync(
                    "DELETE FROM TB_CARD WHERE ID_CARD = @Id", 
                    new { Id = id });
                
                return result > 0;
            }
        }
    }
}

