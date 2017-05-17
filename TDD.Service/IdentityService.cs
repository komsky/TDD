using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TDD.Repository.Interfaces;

namespace TDD.Service
{
    public class IdentityService
    {
        IRepository _userRepository;
        public IdentityService(IRepository repository)
        {
            _userRepository = repository;
        }
        public bool ValidatePassword(string password)
        {
            if (password.Length < 6) return false;

            var regex = new Regex("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\\$%\\^&\\*])");
            if (regex.IsMatch(password))
            {
                return !_userRepository.IsUsedPassword(password);
            }
            return false;
        }
    }
}
