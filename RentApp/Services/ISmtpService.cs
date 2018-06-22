using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentApp.Services
{
    public interface ISmtpService
    {
        void SendMail(string subject, string body, string emailTo);
    }
}
