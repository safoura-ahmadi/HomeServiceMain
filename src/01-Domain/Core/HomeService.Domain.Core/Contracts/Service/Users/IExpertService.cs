﻿using HomeService.Domain.Core.Entities;

namespace HomeService.Domain.Core.Contracts.Service.Users;

public interface IExpertService
{
    Task<Result> Create(int userId, string lName, CancellationToken cancellationToken);
    Task<int> GetTotalCount(CancellationToken cancellationToken);
}
