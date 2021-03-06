﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using System.Configuration;
using BusinessGatewayRepositories;
using BusinessGatewayModels;
using BusinessGatewayRepositories.EarlyCompletion;
using BusinessGatewayRepositories.PollService;

namespace BusinessGatewayModels
{
    public class ResponsePollRequest
    {
        public ResponsePollRequest() { }
        public string Error { get; set; }
        public string UniqueReference { get; set; }
        public decimal ActualPrice { get; set; }
        public bool Successful { get; set; }
        public string MessageDetails { get; set; }
        public DateTime ExpiryDate { get; set; }       
        public string UserName { get; set; }
        public bool Attachment { get; set; }
        public ResponseApplicationToChangeRegisterV2_0Type GatewayResponse { get; set; }

    }
}
