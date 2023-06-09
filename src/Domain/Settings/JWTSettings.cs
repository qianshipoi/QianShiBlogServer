﻿namespace Domain.Settings;

public class JWTSettings
{
    public string Key { get; set; } = default!;
    public string Issuer { get; set; } = default!;
    public string Audience { get; set; } = default!;
    public int Duration { get; set; }
}
