namespace GM.ShopFlow.Identity.Dtos.Login;

public record LoginResponse(string AccessToken, string RefreshToken, int AccessTokenExpiresIn, int RefreshTokenExpiresIn, string TokenType);
