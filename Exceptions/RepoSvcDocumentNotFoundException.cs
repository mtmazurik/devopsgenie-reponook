﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevopsGenie.Reponook.Exceptions
{
    public class RepoSvcDocumentNotFoundException : ApplicationException
    {
        public RepoSvcDocumentNotFoundException() {  }              //ctor1
        public RepoSvcDocumentNotFoundException(string message) :   //ctor2
        base(message)
        { }
    }
}
