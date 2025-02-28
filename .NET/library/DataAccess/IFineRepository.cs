﻿using OneBeyondApi.Model;

namespace OneBeyondApi.DataAccess;

public interface IFineRepository
{
    public List<Fine> GetFines();

    public Task<Fine> AddFine(Fine fine);
}