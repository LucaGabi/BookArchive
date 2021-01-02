using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookArchive.Application
{
    public class CQRSValidationException : ValidationException
    {
        public CQRSResult<List<ValidationFailure>> Data { get; protected set; }

        public CQRSValidationException(string message)
            : this(message, Enumerable.Empty<ValidationFailure>())
        {
        }


        public CQRSValidationException(string message, IEnumerable<ValidationFailure> errors)
            : base(message, errors)
        {
            Data = new CQRSResult<List<ValidationFailure>>(errors.ToList(), 400, message, true);
        }

    }
}
