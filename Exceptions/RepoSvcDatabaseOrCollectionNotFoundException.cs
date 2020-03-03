using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DOG.RepoNook.Exceptions
{
    public class RepoSvcDatabaseOrCollectionNotFound: ApplicationException
    {
        public RepoSvcDatabaseOrCollectionNotFound() {  }              //ctor1
        public RepoSvcDatabaseOrCollectionNotFound(string message) :   //ctor2
        base(message)
        { }
    }
}
