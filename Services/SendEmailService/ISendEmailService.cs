using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogPlatform.Data.DTOs.AuthenticateUser;

namespace BlogPlatform.Services.SendEmailService
{
    public interface ISendEmailService
    {
        string SendEmail(MessageDto message);
    }
}