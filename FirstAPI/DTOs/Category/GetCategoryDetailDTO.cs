namespace FirstAPI.DTOs
{
    public record GetCategoryDetailDTO
    {
        public string Name { get; set; }
        public int Id { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}