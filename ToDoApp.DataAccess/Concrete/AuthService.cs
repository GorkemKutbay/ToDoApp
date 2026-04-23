using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Core.DTOs.Auth;
using ToDoApp.Core.Entities;
using ToDoApp.DataAccess.Abstract;

namespace ToDoApp.DataAccess.Concrete
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _config;

        public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;

        }

        public async Task RegisterAsync(RegisterDto dto)
        {
            var existingUser = await _userManager.FindByEmailAsync(dto.Email);
            if (existingUser != null)
                throw new Exception("This Email already exists.");

            var user = new AppUser
            {
                Name = dto.Name,
                Surname = dto.Surname,
                Email = dto.Email,
                UserName = ConvertToSafeUserName(dto.Name + dto.Surname)
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(",", result.Errors.Select(e => e.Description));
                throw new Exception(errors);
            }

            // Kayıt sonrası otomatik giriş yaptır
            await _signInManager.SignInAsync(user, isPersistent: false);
        }

        async Task<TokenResponseDto> IAuthService.LoginAsync(LoginDto dto)
        {
            var existingUser = await _userManager.FindByEmailAsync(dto.Email);
            if (existingUser == null)
                throw new Exception("Email or password is incorrect.");

            var result = await _signInManager.CheckPasswordSignInAsync(existingUser, dto.Password, lockoutOnFailure: false);
            if (!result.Succeeded)
                throw new Exception("Email or password is incorrect.");

            await _signInManager.SignInAsync(existingUser, isPersistent: false);
            // Token döndürmeye gerek yok, cookie otomatik set edilir
            return new TokenResponseDto
            {
                Token = null,
                Expiration = DateTime.UtcNow.AddHours(1)
            };
        }

        

        private string ConvertToSafeUserName(string input)
        {
            
            string result = input.Replace("İ", "I").Replace("ı", "i")
                                 .Replace("Ş", "S").Replace("ş", "s")
                                 .Replace("Ğ", "G").Replace("ğ", "g")
                                 .Replace("Ü", "U").Replace("ü", "u")
                                 .Replace("Ç", "C").Replace("ç", "c")
                                 .Replace("Ö", "O").Replace("ö", "o");

            
            var chars = result.Where(c => char.IsLetterOrDigit(c)).ToArray();
            return new string(chars).ToLower();
        }


    }

}

