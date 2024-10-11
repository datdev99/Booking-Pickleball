﻿using System.ComponentModel.DataAnnotations;

namespace BookingApi.Models.DTO.UserDto
{
    public class RegisterRequestDto
    {
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
