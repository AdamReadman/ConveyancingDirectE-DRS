﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LrApiManager.SOAPManager;
using LrApiManager.XMLClases;
using LrApiManager.XMLClases.PollResponse;
using LrApiManager.XMLClases.Restriction;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessGatewayServices;
using BusinessGatewayRepositories.EDRSApplication;

namespace eDRSUnitTest
{
    [TestClass]
    public class eDRSAppRequestTest
    {


        [TestMethod]
        public void eDRSAppRequest()
        {
            BusinessGatewayServices.Services _services = new BusinessGatewayServices.Services();
            BusinessGatewayModels.Search[] _search_array = new BusinessGatewayModels.Search[1];

            BusinessGatewayRepositories.EDRSApplication.RequestApplicationToChangeRegisterV1_0Type _request = new BusinessGatewayRepositories.EDRSApplication.RequestApplicationToChangeRegisterV1_0Type();
            BusinessGatewayRepositories.EDRSApplication.ProductType _product = new BusinessGatewayRepositories.EDRSApplication.ProductType();


            _request.ExternalReference = "EXTERREF";
            _request.MessageId = "scenario1";

            _product.Reference = "Reference";
            _product.TotalFeeInPence = 50000.ToString();
            _product.Email = "test@dhd.com";
            _product.TelephoneNumber = "7979778787";
            _product.AP1WarningUnderstood = true;
            _product.ApplicationDate = DateTime.Now;
            _product.PostcodeOfProperty = "POSTCODE";
            _product.DisclosableOveridingInterests = true;


            #region TitleNumbers

            string[] _titlesArray = { "GR518195" };
            BusinessGatewayRepositories.EDRSApplication.TitlesType _titles = new BusinessGatewayRepositories.EDRSApplication.TitlesType();
            _titles = new BusinessGatewayRepositories.EDRSApplication.TitlesType { TitleNumber = _titlesArray };

            //   BusinessGatewayRepositories.EDRSApplication.

            _product.Titles = _titles;
            #endregion

            BusinessGatewayRepositories.EDRSApplication.OtherApplicationType[] applications = new BusinessGatewayRepositories.EDRSApplication.OtherApplicationType[1];

            applications[0] = new BusinessGatewayRepositories.EDRSApplication.OtherApplicationType
            {

                Document = new BusinessGatewayRepositories.EDRSApplication.DocumentType { CertifiedCopy = CertifiedTypeContent.Certified },
                Priority = 1.ToString(),
                Value = 1000.ToString(),
                FeeInPence = 1000.ToString()

            };
            _product.Applications = applications;

            //supporting documnets
            BusinessGatewayRepositories.EDRSApplication.SupportingDocumentsType supportingDocuments = new SupportingDocumentsType();

            supportingDocuments.SupportingDocument = new SupportingDocumentType[1];
            supportingDocuments.SupportingDocument[0] = new SupportingDocumentType
            {
                CertifiedCopy = CertifiedTypeContent.Certified,
                DocumentId = "2",
                DocumentName = DocumentNameContent.Evidence
            };

            _product.SupportingDocuments = supportingDocuments;

            //Representations

            BusinessGatewayRepositories.EDRSApplication.RepresentationsType Representations = new RepresentationsType();

            Representations.LodgingConveyancer = new LodgingConveyancerType
            {
                RepresentativeId = "1"
            };

            _product.Representations = Representations;

            //Parties
            BusinessGatewayRepositories.EDRSApplication.PartiesType parties = new PartiesType();

            parties.Party = new PartyType[1];
            PartyRoleType[] partyRoleTypes = new PartyRoleType[1];
            partyRoleTypes[0] = new PartyRoleType { RoleType = RoleTypeContent.Lender, Priority = "1" };

            parties.Party[0] = new PartyType
            {
                representativeId = "1",
                IsApplicant = true,
                Item = new CompanyType { CompanyName = "company" },
                Roles = partyRoleTypes

            };

            _product.Parties = parties;

            _request.Product = _product;

            var _reponse = _services.eDRSApplicationRequest("tes1234", "BGUser001", "landreg001", _request);

            Assert.AreEqual(true, true);
        }

    }
}
