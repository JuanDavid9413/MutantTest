using BackEnd.Mutants.Entities.Response;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace BackEnd.Mutants.Utilities
{
    public static class GenericUtilities
    {
        public static void ResponseBaseCatch<T>(this ResponseBase<T> data, bool validation, string message, HttpStatusCode status)
        {
            ResponseBase<T> result = data;
            if (validation)
            {
                result.Code = (int)status;
                result.Message = message;
            }
        }
    }
}
