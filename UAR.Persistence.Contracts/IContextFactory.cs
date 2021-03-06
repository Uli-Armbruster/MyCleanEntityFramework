﻿using System;
using System.Data.Entity;

namespace UAR.Persistence.Contracts
{
    public interface IContextFactory
    {
        DbContext Create(Type type);
    }
}