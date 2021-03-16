using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IAuth
    {
        Task<string> Login(LoginModel model);
        Task<object> Register(RegisterModel userModel);
    }
}
