﻿using BusinessGatewayRepositories.EDRSApplication;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eDrsManagers.ViewModels
{
    
        public class ApplicationPollRequest
        {
            public string MessageId { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }


        }

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
            public ResponseApplicationToChangeRegisterV1_0Type GatewayResponse { get; set; }

        }
    
}
