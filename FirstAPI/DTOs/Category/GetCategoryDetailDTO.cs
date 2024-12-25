using FirstAPI.DTOs.Product;

namespace FirstAPI.DTOs
{
    public record GetCategoryDetailDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<GetProductDTO> ProductDTOs { get; set; }
    }
}