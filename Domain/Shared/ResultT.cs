using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shared
{
    public class Result<TValue> : Result
    {
        private readonly TValue? _value;

        /// <summary>
        /// It calls the base Result(true, Error.None) to set IsSuccess and Error

        ///Then stores "Hello" into the _value field for later access
        /// </summary>
        /// <param name="value"></param>
        /// <param name="isSuccess"></param>
        /// <param name="error"></param>
        protected internal Result(TValue? value, bool isSuccess, Error error)
            : base(isSuccess, error) =>
            _value = value;



        //This property gives you the value — but only if the result was successful.
        //If it's a success, it returns _value!
        //If it's a failure, it throws an exception to stop incorrect usage
        public TValue Value => IsSuccess
            ? _value!
            : throw new InvalidOperationException("The value of a failure result can not be accessed.");

        //This line defines an implicit conversion from TValue to Result<TValue>.
        //It means you can write this:
        //Result<string> result = "hello";
        //and it will automatically call:
        //Result result = Result.Create("hello")
        public static implicit operator Result<TValue>(TValue? value) => Create(value);
    }
}
