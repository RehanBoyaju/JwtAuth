using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using MediatR;
using System.Threading.Tasks;
using Domain.Shared;

namespace Application.Abstractions.Messaging
{
    /// <summary>
    ///             IRequest<T> is a MediatR interface that represents a request that expects a response of type T
    ///             When you send an IRequest<T> via MediatR’s mediator, it routes the request to an appropriate handler that returns T
    /// </summary>
    public interface ICommand : IRequest<Result>
    {
    }
    public interface ICommand<TResponse> : IRequest<Result<TResponse>>
    {
    }

}
