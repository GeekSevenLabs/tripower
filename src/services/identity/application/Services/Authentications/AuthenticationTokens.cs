﻿namespace TriPower.Identity.Application.Services.Authentications;

public record AuthenticationTokens(AuthenticationToken AccessToken, AuthenticationToken RefreshToken);