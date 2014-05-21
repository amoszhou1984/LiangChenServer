﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using AustinHarris.JsonRpc;

namespace LCServer
{
    public class Global : System.Web.HttpApplication
    {
        static LCJsonRPCServer service;// = new LCJsonRPCServer();
        protected void Application_Start(object sender, EventArgs e)
        {
            service = new LCJsonRPCServer();
        }
    }

}