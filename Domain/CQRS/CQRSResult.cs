using BookArchive.Application;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookArchive.Application
{
    public class CQRSResult<T>
    {
        public int Code { get; }
        public bool HasError { get; internal set; }
        public bool WasHandledError { get; internal set; }
        public string Message { get; internal set; }


        public T Data { get; private set; }

        public CQRSResult(T data, int code = 200, string message = "", bool hasError = false, bool wasHandledError = true)
        {
            Data = data;
            Code = code;
            Message = message;
            HasError = code / 100 !=2 || hasError;
            WasHandledError = wasHandledError;
        }

        public static implicit operator CQRSResult<T>(T input)
        {
            return new CQRSResult<T>(input);
        }

    }


}

public static class CQRSResultExtentions
{
    public static CQRSResult<T> AsCQRSResult<T>(this T data, int code = 200, string message = "", bool hasError = false, bool wasHandledError = true)
    {
        return new CQRSResult<T>(data, code, message, hasError, wasHandledError);
    }
}