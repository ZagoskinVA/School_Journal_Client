using System;
using System.Collections.Generic;
using System.Text;
using Xamarin_Lesson.Interface;
using Xamarin_Lesson.Models;

namespace Xamarin_Lesson.ViewModel
{
    internal class UserPageViewModel
    {
        IUserManager _userManager;
        IRefreshTokenService _refreshTokenService;

        public UserPageViewModel(IUserManager userManager, IRefreshTokenService refreshTokenService)
        {
            _userManager = userManager;
            _refreshTokenService = refreshTokenService;
            _userManager.RefreshToken += UserManager_RefreshToken;
            _userManager.SuccsessfulGetUser += UserManager_SuccsessfulGetUser;
            _refreshTokenService.RefreshTokenSuccsessfuly += RefreshTokenService_RefreshTokenSuccsessfuly;
            _refreshTokenService.RefreshTokenFailed += RefreshTokenService_RefreshTokenFailed;
        }

        private void RefreshTokenService_RefreshTokenFailed(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void RefreshTokenService_RefreshTokenSuccsessfuly(object sender, User e)
        {
            throw new NotImplementedException();
        }

        private void UserManager_SuccsessfulGetUser(object sender, User e)
        {
            throw new NotImplementedException();
        }

        private void UserManager_RefreshToken(object sender, string e)
        {
            throw new NotImplementedException();
        }
    }
}
