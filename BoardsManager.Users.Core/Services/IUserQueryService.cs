﻿using BoardsManager.Users.Core.DTO;

namespace BoardsManager.Users.Core.Abstractions
{
    public interface IUserQueryService
    {
        Task<UserDTO?> GetUserById(string id);

        IEnumerable<UserDTO> GetUsersByProjectId(string projectId);
    }
}