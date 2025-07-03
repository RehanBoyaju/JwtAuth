using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shared
{
    public static class ResultExtensions
    {
            //To validate a condition on a successful result's value.
            //If the result is already a failure → return it as-is.
            //If it's a success, apply the predicate (e.g. check something like user.IsActive)
            //If it passes → return original result.
            //If it fails → return a failure with the provided error.
        public static Result<T> Ensure<T>(
            this Result<T> result,
            Func<T, bool> predicate,
            Error error)
        {
            if (result.IsFailure)
            {
                return result;
            }

            return predicate(result.Value) ?
                result :
                Result.Failure<T>(error);
        }



        //To transform a successful result’s value from one type to another(TIn → TOut).
        //If the result is a failure → pass it through unchanged(but typed as Result<TOut>)
        //If it's a success → transform the value using mappingFunc, and return a new success result
        public static Result<TOut> Map<TIn, TOut>(
            this Result<TIn> result,
            Func<TIn, TOut> mappingFunc)
        {
            return result.IsSuccess ?
                Result.Success(mappingFunc(result.Value)) :
                Result.Failure<TOut>(result.Error);
        }
    }
}
