﻿using System;

namespace Infrastructure.Exceptions
{
    public class AuthorizationException : Exception
    {
        public AuthorizationException(string message) : base(message)
        {
        }
    }
}