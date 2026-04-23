using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Core.DTOs.Auth;

namespace ToDoApp.DataAccess.Abstract
{
    public interface IAuthService
    {
        Task RegisterAsync(RegisterDto dto);
        Task<TokenResponseDto> LoginAsync(LoginDto dto);

    }
}
