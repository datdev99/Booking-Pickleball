﻿namespace BookingApi.Models.DTO.NewDto
{
    public class AddNewRequestDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
    }
}