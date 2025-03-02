﻿using HomeService.Domain.Core.Dtos.Users;
using HomeService.Domain.Core.Entities;

namespace HomeService.Domain.Core.Contracts.Repository.Users;

public interface IExpertRepository
{
    Task<Result> Create(int userId, string lName,CancellationToken cancellationToken);
    Task<int> GetTotalCount(CancellationToken cancellationToken);
}
