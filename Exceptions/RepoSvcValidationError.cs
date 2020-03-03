using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DOG.RepoNook.Exceptions
{
    public class RepoSvcValidationError : ApplicationException
    {
        public RepoSvcValidationError() {  }              //ctor1
        public RepoSvcValidationError(string message) :   //ctor2
        base(message)
        { }
    }
}
