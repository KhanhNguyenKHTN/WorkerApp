﻿using System;
using System.Collections.Generic;
using System.Text;
using WorkerModel.Order;

namespace Global
{
    public class GlobalInfo
    {
        public static string URL = @"http://171.227.218.132:8081/";

        public static string UserName = "xarzdlrm";
        public static string Password = "Be9K-7C4C1sO9hiJTJwZSHITqm7NX6LS";
        public static string VirtualHost = "xarzdlrm";
        public static string HostName = "baboon.rmq.cloudamqp.com";

        public static Pic UserLogged { get; set; }
        public static string UserLogin { get; set; }
    }
}
