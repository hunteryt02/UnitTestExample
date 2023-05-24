using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnitTestExample.Abstractions;
using UnitTestExample.Entities;
using UnitTestExample.Services;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace UnitTestExample.Controllers
{
    public class AccountController
    {        
        public IAccountManager AccountManager { get; set; }

        public AccountController()
        {
            AccountManager = new AccountManager();
        }

        public Account Register(string email, string password)
        {
            if(!ValidateEmail(email))
                throw new ValidationException(
                    "A megadott e-mail cím nem megfelelő!");
            if(!ValidatePassword(password))
                throw new ValidationException(
                    "A megadottt jelszó nem megfelelő!\n" +
                    "A jelszó legalább 8 karakter hosszú kell legyen, csak az angol ABC betűiből és számokból állhat, és tartalmaznia kell legalább egy kisbetűt, egy nagybetűt és egy számot.");

            var account = new Account()
            {
                Email = email,
                Password = password
            };

            var newAccount = AccountManager.CreateAccount(account);

            return newAccount;
        }

        public bool ValidateEmail(string email)
        {            
            return Regex.IsMatch(
                email, 
                @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?");
        }

        public bool ValidatePassword(string password)
        {
            // Check if the text is at least eight characters long and consists only of letters and numbers
            if (!Regex.IsMatch(password, @"^[a-zA-Z0-9]{8,}$"))
            {
                return false;
            }

            // Check if the text contains a lowercase letter
            if (!Regex.IsMatch(password, @"[a-z]"))
            {
                return false;
            }

            // Check if the text contains an uppercase letter
            if (!Regex.IsMatch(password, @"[A-Z]"))
            {
                return false;
            }

            // Check if the text contains a digit
            if (!Regex.IsMatch(password, @"\d"))
            {
                return false;
            }

            // All conditions passed, password is valid
            return true;
        }
    }
}
