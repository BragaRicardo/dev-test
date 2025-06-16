namespace DevTest_API.DTOs
{
    public class LoginResponseDto
    {
        public string Token { get; set; } = null!;
        public DateTime ExpiraEn { get; set; }
    }
}
