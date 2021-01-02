
using FluentValidation;
using MediatR;
using MediatR.Pipeline;
using System;


namespace BookArchive.Application
{

    public class CQRSExceptionHandler<TRequest, TResponse> : RequestExceptionHandler<TRequest, TResponse, Exception> where TRequest : IRequest<TResponse>
    {
        //protected override void Handle(TRequest request, Exception exception, RequestExceptionHandlerState<TResponse> state)
        //{
        //    if (exception is ValidationException)
        //    {
        //        state.SetHandled(state.Response);
        //    }
        //    else
        //    {
        //        // TODO: log the error 
        //        // then handle at midd
        //    }
        //}
        protected override void Handle(TRequest request, Exception exception, RequestExceptionHandlerState<TResponse> state)
        {
            throw new NotImplementedException();
        }
    }
}