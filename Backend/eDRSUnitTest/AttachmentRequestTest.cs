﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LrApiManager.SOAPManager;
using LrApiManager.XMLClases;
using LrApiManager.XMLClases.PollResponse;
using LrApiManager.XMLClases.Restriction; 

namespace eDRSUnitTest
{
    [TestClass]
    public class AttachementRequestTest
    {
        [TestMethod]
        public void AttachementRequest()
        {
            BusinessGatewayServices.Services _services = new BusinessGatewayServices.Services();
            BusinessGatewayModels.Search[] _search_array = new BusinessGatewayModels.Search[1];

            AttachmentV2_0Type _request = new AttachmentV2_0Type();

            _request.MessageId = "messageid";
            _request.ExternalReference = "ExternalReference";
            _request.ApplicationMessageId = "ApplicationMessageId";
            _request.ApplicationService = "104";

            string pdfFilePath = "C:/Users/SACHITH/Documents/ggg.pdf";
            byte[] filearray = System.IO.File.ReadAllBytes(pdfFilePath);         

            BusinessGatewayRepositories.AttachmentServiceRequest.AttachmentType attachment = new BusinessGatewayRepositories.AttachmentServiceRequest.AttachmentType
            {
                filename = "filename",
                format = "pdf",
                Value = filearray,
            };

            var ItemsElementName = new BusinessGatewayRepositories.AttachmentServiceRequest.ItemsChoiceType[3];

           // ItemsElementName[0] = BusinessGatewayRepositories.AttachmentServiceRequest.ItemsChoiceType.ApplicationType;
            ItemsElementName[0] = BusinessGatewayRepositories.AttachmentServiceRequest.ItemsChoiceType.Attachment;
            ItemsElementName[1] = BusinessGatewayRepositories.AttachmentServiceRequest.ItemsChoiceType.AttachmentId;
            ItemsElementName[2] = BusinessGatewayRepositories.AttachmentServiceRequest.ItemsChoiceType.CertifiedCopy;
        

            Object[] Items = new object[] {
              attachment,
              "1",
              BusinessGatewayRepositories.AttachmentServiceRequest.CertifiedTypeContent.Scanned            

            };

            _request.Items = Items;
            _request.ItemsElementName = ItemsElementName;

            var _reponse = _services.AttachmentRequest("testoutofhours", "BGUser001", "landreg001", _request);

            Assert.AreEqual(true, true);
        }

    }
}
