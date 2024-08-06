namespace PortalRentCar.Shared.Response;

public class LoginDtoResponse : BaseResponse
{
    public string Token { get; set; } = default!;
    public string RazonSocial { get; set; } = default!;
    public List<string> Perfiles { get; set; } = default!;
}