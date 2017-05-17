using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDD.Repository.Interfaces
{
    public interface IRepository
    {
        bool IsUsedPassword(string newPassword);
    }
}
