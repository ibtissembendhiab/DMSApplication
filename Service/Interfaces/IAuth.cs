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
        Task<object> Login(LoginModel model);
        Task<object> Register(Register userModel);
        Task<Object> Update(string id, UserUpdate model);
        Task<Object> Delete(string id);
        List<User> GetAll();
    }
}
