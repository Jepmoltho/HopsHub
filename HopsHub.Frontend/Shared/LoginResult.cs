﻿using HopsHub.Shared;

namespace HopsHub.Api.Shared;

public class LoginResult : Result
{
    public string Token { get; set; } = "";
}