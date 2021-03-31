using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IAuth
    {
        Task<string> Login(LoginModel model);
        Task<object> Register(RegisterModel userModel);
        Task<Object> Update(string id, User model);
        Task<Object> Delete(string id);
    }
}
