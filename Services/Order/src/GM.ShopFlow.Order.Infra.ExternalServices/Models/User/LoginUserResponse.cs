namespace GM.ShopFlow.Order.Infra.ExternalServices.Models.User;

public record LoginUserResponse(
    string AccessToken,
    string RefreshToken,
    int AccessTokenExpiresIn,
    int RefreshTokenExpiresIn,
    string TokenType
);
