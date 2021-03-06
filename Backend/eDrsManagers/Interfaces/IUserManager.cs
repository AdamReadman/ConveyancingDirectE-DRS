﻿using eDrsManagers.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace eDrsManagers.Interfaces
{
    public interface IUserManager
    {
        UserViewModel Login(LoginViewModel viewModel);

        List<UserViewModel> Get();
        bool Update(UserViewModel viewModel);
    }
}
