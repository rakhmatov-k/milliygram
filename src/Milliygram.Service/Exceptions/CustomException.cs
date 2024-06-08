﻿namespace Milliygram.Service.Exceptions;

public class CustomException : Exception
{
    public CustomException() { }
    public CustomException(int statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }

    public CustomException(string message, int statusCode, Exception exception)
    {
        StatusCode = statusCode;
    }

    public int StatusCode { get; set; }
}
