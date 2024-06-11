﻿using System.ComponentModel.DataAnnotations;

namespace Milliygram.Service.DTOs.Users;

public class LoginModel
{
    [Required(ErrorMessage = "Username is required.")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}