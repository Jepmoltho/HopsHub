﻿using HopsHub.Api.Shared;

namespace HopsHub.Api.Services.Interfaces;

public interface IEmailService
{
    Task SendEmailAsync(string toEmail, string subject, string body);
}
