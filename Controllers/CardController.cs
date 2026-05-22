using Microsoft.AspNetCore.Mvc;
using ApiProdutos.Models;
using ApiProdutos.Repositories;

namespace ApiProdutos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardController : ControllerBase
    {
        private readonly ICardRepository _cardRepository;
        private readonly IConfiguration _configuration;

        public CardController(ICardRepository cardRepository, IConfiguration configuration)
        {
            _cardRepository = cardRepository;
            _configuration = configuration;
        }

        // GET: api/card
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Card>>> GetAll()
        {
            var cards = await _cardRepository.GetAllAsync();
            return Ok(cards);
        }

        [HttpGet("diretor")]
        public ActionResult<string> GetDiretor()
        {
            var diretor = _configuration["Diretor"];
            return Ok(diretor);
        }

        // GET: api/card/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Card>> GetById(int id)
        {
            var card = await _cardRepository.GetByIdAsync(id);
            if (card == null)
            {
                return NotFound();
            }
            return Ok(card);
        }

        // POST: api/card
        [HttpPost]
        public async Task<ActionResult<Card>> Create(Card card)
        {
            if (string.IsNullOrWhiteSpace(card.NmCard) || string.IsNullOrWhiteSpace(card.DsTranslation))
            {
                return BadRequest("NmCard e DsTranslation são obrigatórios.");
            }

            var id = await _cardRepository.AddAsync(card);
            card.IdCard = id;
            return CreatedAtAction(nameof(GetById), new { id = card.IdCard }, card);
        }

        // PUT: api/card/1
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Card card)
        {
            if (id != card.IdCard)
            {
                return BadRequest("ID mismatch");
            }

            var success = await _cardRepository.UpdateAsync(card);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/card/1
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var success = await _cardRepository.DeleteAsync(id);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
