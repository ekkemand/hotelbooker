namespace PublicApi.DTO.v1.Identity
{
    public class JwtResponseDTO
    {
        public string Token { get; set; } = default!;
        public string Status { get; set; } = default!;
    }
}