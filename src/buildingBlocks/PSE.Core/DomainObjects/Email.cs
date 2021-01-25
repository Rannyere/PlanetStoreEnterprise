using System;
using System.Text.RegularExpressions;

namespace PSE.Core.DomainObjects
{
    public class Email
    {
        public const int AddressWebMaxLength = 254;

        public const int AddressWebMinLength = 5;

        public string AddressWeb { get; private set; }

        //Constructor EntityFramework
        protected Email() { }

        public Email(string addressWeb)
        {
            if (!ValidateEmail(addressWeb)) throw new DomainException("E-mail invalid");
            AddressWeb = addressWeb;
        }

        public static bool ValidateEmail(string email)
        {
            var regexEmail = new Regex(@"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
            return regexEmail.IsMatch(email);
        }
    }
}
