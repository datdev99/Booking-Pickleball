namespace BookingApi.Models.DTO.UserDto
{
    public class GetUserDto
    {
        public Guid id { get; set; }
        public string userName { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public bool emailConfirmed { get; set; }
    }
}
