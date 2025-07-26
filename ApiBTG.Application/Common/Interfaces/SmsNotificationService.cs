using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace ApiBTG.Application.Common.Interfaces
{
    public class SmsNotificationService : INotificationService
    {
        private readonly TwilioSettings _settings;

        public SmsNotificationService(IOptions<TwilioSettings> options)
        {
            _settings = options.Value;
            TwilioClient.Init(_settings.AccountSid, _settings.AuthToken);
        }

        public async Task SendAsync(string to, string subject, string message)
        {
            await MessageResource.CreateAsync(
                to: new PhoneNumber(to),
                from: new PhoneNumber(_settings.FromNumber),
                body: message
            );
        }
    }
} 