﻿namespace BookStoreMvc.Services
{
    public interface IUserService
    {
        string GetUserId();
        bool IsAuthenticated();
    }
}