﻿namespace FirstAPI.DTOs
{
    public record GetCategoryDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ProductCount { get; set; }
    }
}
