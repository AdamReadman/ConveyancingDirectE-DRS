﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LrApiManager.SOAPManager;

using LrApiManager.SOAPManager.TranferAndCharge;
using LrApiManager.XMLClases;
using LrApiManager.XMLClases.PollResponse;
using LrApiManager.XMLClases.TransferAndChargeApplicationRequest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Address = LrApiManager.XMLClases.TransferAndChargeApplicationRequest.Address;
using Document = LrApiManager.XMLClases.TransferAndChargeApplicationRequest.Document;

namespace eDRSUnitTest
{
    [TestClass]
    public class TransferAndChangeTest
    {


        [TestMethod]
        public void ApplicationRequest()
        {
            TransferAndChargeApplicationManager restrictionServiceManager = new TransferAndChargeApplicationManager();

            TitleNumber[] TitleNumbers = { new TitleNumber { TitleString = "123334"}, new TitleNumber { TitleString = "56789" } };


            Dealingtitles Dealingtitles = new Dealingtitles
            {
                TitleNumber = TitleNumbers
            };                  
                    
            Dealing dealing = new Dealing {

                DealingTitles = Dealingtitles
            };            

            List<Dealing> Titles = new List<Dealing>();
            Titles.Add(dealing);


            //APPLICATIONS
            OtherapplicationObject otherapplication = new OtherapplicationObject
            {
                Priority = 1,
                Value = 0,
                FeeInPence = 500,
                Document = new Document
                {

                    CertifiedCopy = "Original"
                },
                Type = "RX1"

            };

            List<OtherapplicationObject> otherapplications = new List<OtherapplicationObject>();
            otherapplications.Add(otherapplication);

            ChargeapplicationObject chargeapplicationObject = new ChargeapplicationObject {

                Priority = 1,
                Value = 0,
                FeeInPence = 500,
                Document = new Document
                {

                    CertifiedCopy = "Original"
                },
                ChargeDate = "ChargeDate",
                MDRef= "MDRef"

            };

            List<ChargeapplicationObject> chargeapplications = new List<ChargeapplicationObject>();
            chargeapplications.Add(chargeapplicationObject);

            ApplicationsObject applications = new ApplicationsObject
            {
                OtherApplication= otherapplications,
                ChargeApplication= chargeapplications
            };
            
            
            

             //SUPPORTING DOCUMENTS
             Supportingdocument Supportingdocument = new Supportingdocument
            {


                CertifiedCopy = "Original",
                DocumentId = 2,
                DocumentName = "Evidence"
            };

            List<Supportingdocument> supportingdocuments = new List<Supportingdocument>();
            supportingdocuments.Add(Supportingdocument);           


            //LODGINGCONVENYANCER
            LodgingconveyancerObject lodgingconveyancer = new LodgingconveyancerObject
            {

                RepresentativeId = 1
            };



            RepresentingConveyancerObject RepresentingConveyancer = new RepresentingConveyancerObject
            {

                RepresentativeId = 2,
                ConveyancerName = "Parretts Conveyancers",
                Reference = "GHK / Youngblood ",

                DXAddress = new DXAddress
                {
                    DXNumber = "12456",
                    DXExchange = "Peterborough 4"

                }


            };
           

            RepresentationsObject representations = new RepresentationsObject
            {                

                LodgingConveyancer = lodgingconveyancer,
                RepresentingConveyancer = RepresentingConveyancer

            };
       
            // PARTY
            Role role = new Role
            {
                RoleType = "ThirdParty",
                Priority = 1
            };

            List<Role> roles1 = new List<Role>();
            roles1.Add(role);         

            //Parties
            Party party = new Party
            {

                IsApplicant = true,
                Company = new Company
                {
                    CompanyName = "Abbey National PLC"
                },
                Roles = roles1
            };


            List<Party> parties = new List<Party>();
            parties.Add(party);

            // AdditionalPartyNotifications

            Additionalpartynotification additionalPartyNotification = new Additionalpartynotification
            {

                Name = "Parrets",
                Reference = "Reference",
                Address = new Address
                {

                    DXAddress = new DXAddress
                    {

                        DXNumber = "12345",
                        DXExchange = "Peterborough 4"
                    }

                }

            };

            List<Additionalpartynotification> Additionalpartynotifications = new List<Additionalpartynotification>();
            Additionalpartynotifications.Add(additionalPartyNotification);


            TransferAndChargeApplicationRequest restrictionApplicationRequest = new TransferAndChargeApplicationRequest
            {

                AdditionalProviderFilter = "Solsdotcom",
                MessageId = "scenario4",
                ExternalReference = "CP/Barclaycard/Murphy",

                Product = new Product
                {
                    Reference = "CP/Barclaycard/Murphy",
                    TotalFeeInPence = 5000,
                    Email = "carolparker@cozyconveynacers.com",
                    TelephoneNumber = 1780299299,
                    AP1WarningUnderstood = true,
                    ApplicationDate = "2012-02-08",
                    DisclosableOveridingInterests = false,
                    Titles = Titles,
                    Applications = applications,
                    SupportingDocuments = supportingdocuments,
                    Representations = representations,
                    Parties = parties,
                    AdditionalPartyNotifications= Additionalpartynotifications,
                    ApplicationAffects = "WHOLE"
                }

            };

            ApplicationResponse applicationResponse = restrictionServiceManager.RequestTransferAndChargeApplication(restrictionApplicationRequest);
        }

        [TestMethod]
        public void AttachmentRequest()
        {

            AttachmentRequestManager restrictionServiceManager = new AttachmentRequestManager();

            var filearray = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 };

            AttachmentRequest AttachmentRequest = new AttachmentRequest
            {
                AdditionalProviderFilter = "Solsdotcom",
                MessageId = "MessageId",
                ExternalReference = "CP/Parrett/Jenkins",
                ApplicationMessageId = "ApplicationMessageId",
                ApplicationService = "104",
                AttachmentId = 0,
                CertifiedCopy = "Original",
                Attachment = filearray

            };


            AttachmentResponse attachmentResponse = restrictionServiceManager.RequestAttachment(AttachmentRequest);

        }

        [TestMethod]
        public void PoolRequest()
        {
            PollRequestManager restrictionPoolRequest = new PollRequestManager();

            PollResponse restrictionPoolResponse = restrictionPoolRequest.PoolRequest("test msg id");
        }

    }
}
