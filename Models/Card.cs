namespace ApiProdutos.Models
{
    public class Card
    {
        public int IdCard { get; set; }
        public string NmCard { get; set; }
        public string DsTranslation { get; set; }
        public DateTime DtCreate { get; set; }
        public DateTime? DtUpdate { get; set; }
    }
}
