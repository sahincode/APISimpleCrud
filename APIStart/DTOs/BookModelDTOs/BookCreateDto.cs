namespace APIStart.DTOs.BookModelDTOs
{
    public class BookCreateDto
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }
}
