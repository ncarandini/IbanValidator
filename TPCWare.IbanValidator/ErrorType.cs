using System;
using System.Collections.Generic;
using System.Text;

namespace TPCWare.IbanValidator
{
    public enum ErrorType
    {
        None,
        NotSupportedCountry,
        WrongCountryFormat,
        FailedChecksum,
        InternalError
    }
}
