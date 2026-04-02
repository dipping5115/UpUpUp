namespace UpUpUp.Models
{
    public class CardModel
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Damage { get; set; }
        public int Block { get; set; }
        public int Cost { get; set; }
    }
}
