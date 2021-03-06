﻿using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BusinessGatewayModels;
using BusinessGatewayRepositories.EDRSApplication;
using eDrsDB.Data;
using eDrsDB.Models;
using eDrsManagers.Http;
using eDrsManagers.Interfaces;
using eDrsManagers.ViewModels;
using LrApiManager.SOAPManager;
using LrApiManager.XMLClases;
using Microsoft.EntityFrameworkCore;
using Party = eDrsDB.Models.Party;

namespace eDrsManagers.Managers
{
    public class Registration : IRegistration
    {
        private readonly RestrictionRequestManager _restrictionServiceManager = new RestrictionRequestManager();
        private readonly IHttpEdrsCall _httpInterceptor;
        private readonly PollRequestManager _pollRequestManager = new PollRequestManager();
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public Registration(AppDbContext context, IMapper mapper, IHttpEdrsCall httpInterceptor)
        {
            _context = context;
            _mapper = mapper;
            _httpInterceptor = httpInterceptor;
        }


        public List<RegistrationType> GetRegistrationTypes()
        {
            return _context.RegistrationTypes.Where(s => s.Status).ToList();
        }

        public RequestLog CreateRegistration(DocumentReferenceViewModel viewModel)
        {

            viewModel.Parties.ToList().ForEach(party =>
            {
                party.Roles = string.Join(",", party.ViewModelRoles);

            });

            var count = viewModel.Applications.Count();

            viewModel.SupportingDocuments.ToList().ForEach(supDoc =>
            {
                supDoc.DocumentId = ++count;
            });

            viewModel.MessageID = Guid.NewGuid().ToString();

            viewModel.User = _context.Users.FirstOrDefault(x => x.UserId == viewModel.UserId);

            if (viewModel.Representations == null)
            {
                viewModel.Representations = new List<Representation>
                {
                    new Representation { RepresentativeId = 1 }
                };
            }

            var model = _mapper.Map<DocumentReferenceViewModel, DocumentReference>(viewModel);

            _context.DocumentReferences.Add(model);

            _context.SaveChanges();


            /********** Calling LR Api backend ***********/
            var requestLog = _httpInterceptor.CallRegistrationApi(viewModel);
            /********** Calling LR Api backend ***********/

            if (requestLog == null)
            {
                model.IsApiSuccess = false;
            }
            else
            {
                model.IsApiSuccess = true;

                //Update case overall status 1=>Inprogress 
                model.OverallStatus = 1;

                requestLog.DocumentReferenceId = model.DocumentReferenceId;
                var requestLogList = requestLog.AttachmentResponse;
                if (requestLogList != null)
                {
                    if (model.RequestLogs == null)
                    {
                        model.RequestLogs = new List<RequestLog>();
                    }

                    requestLogList.ForEach(s => { model.RequestLogs.Add(s); });
                    model.RequestLogs.Add(requestLog);
                }
            }

            _context.SaveChanges();


            return requestLog;

        }

        public RequestLog UpdateRegistration(DocumentReferenceViewModel viewModel)
        {
            viewModel.Parties.ToList().ForEach(party =>
            {
                party.Roles = string.Join(",", party.ViewModelRoles);
            });

            var count = 1;

            viewModel.Applications.ToList().ForEach(x => x.Document.AttachmentId = count++);

            viewModel.SupportingDocuments.ToList().ForEach(supDoc =>
            {
                supDoc.DocumentId = count++;
            });

            var deletingTitle = _context.TitleNumbers
                .Where(x => !viewModel.Titles.Select(s => s.TitleNumberId).ToList().Contains(x.TitleNumberId) && x.DocumentReferenceId == viewModel.DocumentReferenceId).ToList();

            var deletingApplications = _context.ApplicationForms
                .Where(x => !viewModel.Applications.Select(s => s.ApplicationFormId).ToList().Contains(x.ApplicationFormId) && x.DocumentReferenceId == viewModel.DocumentReferenceId).ToList();

            var deletingSupportingDocuments = _context.SupportingDocuments
                .Where(x => !viewModel.SupportingDocuments.Select(s => s.SupportingDocumentId).ToList().Contains(x.SupportingDocumentId) && x.DocumentReferenceId == viewModel.DocumentReferenceId).ToList();

            var deletingParties = _context.Parties
                .Where(x => !viewModel.Parties.Select(s => s.PartyId).ToList().Contains(x.PartyId) && x.DocumentReferenceId == viewModel.DocumentReferenceId).ToList();

            if (viewModel.Representations != null)
            {
                var representations = _context.Representations
                    .Where(x => !viewModel.Representations.Select(s => s.RepresentationId).ToList().Contains(x.RepresentationId) && x.DocumentReferenceId == viewModel.DocumentReferenceId).ToList();
                _context.Representations.RemoveRange(representations);
            }

            _context.TitleNumbers.RemoveRange(deletingTitle);
            _context.ApplicationForms.RemoveRange(deletingApplications);
            _context.SupportingDocuments.RemoveRange(deletingSupportingDocuments);
            _context.Parties.RemoveRange(deletingParties);

            if (string.IsNullOrEmpty(viewModel.MessageID))
                viewModel.MessageID = Guid.NewGuid().ToString();

            viewModel.User = _context.Users.FirstOrDefault(x => x.UserId == viewModel.UserId);
            var model = _mapper.Map<DocumentReferenceViewModel, DocumentReference>(viewModel);

            _context.DocumentReferences.Update(model);

            var requestLog = _httpInterceptor.CallRegistrationApi(viewModel);

            if (requestLog == null)
            {
                model.IsApiSuccess = false;
            }
            else
            {
                requestLog.DocumentReferenceId = viewModel.DocumentReferenceId;

                var requestLogList = requestLog.AttachmentResponse;
                requestLogList.ForEach(s =>
                {
                    model.RequestLogs.Add(s);
                });
                model.RequestLogs.Add(requestLog);
            }
            _context.SaveChanges();
            return requestLog;

        }


        public bool DeleteRegistration(long regId)
        {
            var deleteObject = _context.DocumentReferences.FirstOrDefault(s => s.DocumentReferenceId == regId);
            if (deleteObject != null) deleteObject.Status = false;
            return _context.SaveChanges() > 0;
        }

        public RegistrationType GetRegistrationType(long regType)
        {
            return _context.RegistrationTypes.FirstOrDefault(s => s.RegistrationTypeId == regType);
        }

        public List<DocumentReference> GetRegistrations(string regType)
        {
            return _context.DocumentReferences.Where(s => s.Status && s.RegistrationTypeId == int.Parse(regType)).ToList();
        }

        public dynamic GetPollResponse(long docRefId)
        {
            try
            {
                var docRef = _context.DocumentReferences.FirstOrDefault(x => x.DocumentReferenceId == docRefId);

                OutstaningRequestViewModel outstaningRequest = new OutstaningRequestViewModel();
                outstaningRequest.Username = "BGUser001";
                if (docRef != null)
                {
                    outstaningRequest.Service = 70;
                    outstaningRequest.MessageId = docRef.MessageID;
                }

                var response = _httpInterceptor.CallOutstandingApi(outstaningRequest);
                if (response != null)
                    if (response.Successful)
                    {
                        var outResponse = response.Requests.FirstOrDefault();

                        ApplicationPollRequest applicationPollRequest = new ApplicationPollRequest();
                        applicationPollRequest.MessageId = outResponse.Id;

                        var responseEarlyCompletionApi = _httpInterceptor.CallApplicationPollRequestApi(applicationPollRequest);

                        if (responseEarlyCompletionApi.IsSuccess)
                        {
                            if (!string.IsNullOrEmpty(responseEarlyCompletionApi.File))
                            {
                                docRef.OverallStatus = 10; // Overall Process is completed
                            }

                            responseEarlyCompletionApi.DocumentReferenceId = docRef.DocumentReferenceId;
                            _context.RequestLogs.Add(responseEarlyCompletionApi);
                            _context.SaveChanges();
                            return responseEarlyCompletionApi;
                        }

                    }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool AutomatePollRequest()
        {
            try
            {
                var messageIds = _context.DocumentReferences.Where(x => x.Status && x.OverallStatus == 1).Select(x => x.DocumentReferenceId).ToList();
                messageIds.ForEach(x =>
                {
                    GetPollResponse(x);
                });

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public dynamic GetOutStandingPollRequest(long docRefId, int serviceId)
        {
            var docRef = _context.DocumentReferences.FirstOrDefault(x => x.DocumentReferenceId == docRefId);

            OutstaningRequestViewModel outstaningRequest = new OutstaningRequestViewModel();
            outstaningRequest.Username = "BGUser001";
            if (docRef != null)
            {
                outstaningRequest.Service = serviceId;
                outstaningRequest.MessageId = docRef.MessageID;
            }

            var response = _httpInterceptor.CallOutstandingApi(outstaningRequest);

            var outstanding = new List<Outstanding>();

            response.Requests.ForEach(x =>
            {
                outstanding.Add(new Outstanding
                {
                    LandRegistryId = x.Id,
                    NewResponse = x.NewResponse,
                    Type = "attachment_outstanding",
                    DocumentReferenceId = docRef.DocumentReferenceId,
                    TypeCode = x.TypeCode,
                    ServiceType = x.ServiceType
                });
            });

            var requestLogList = new List<RequestLog>();
            outstanding.ForEach(x =>
            {
                AttachmentPollRequestViewModel attachmentPoll = new AttachmentPollRequestViewModel();
                attachmentPoll.Username = "BGUser001";
                if (docRef != null)
                {
                    attachmentPoll.MessageId = docRef.MessageID;
                }

                var pollResponse = _httpInterceptor.CallAttachmentPollApi(attachmentPoll);

                pollResponse.DocumentReferenceId = docRef.DocumentReferenceId;

                requestLogList.Add(pollResponse);

            });

            _context.Outstanding.AddRange(outstanding);
            _context.RequestLogs.AddRange(requestLogList);
            _context.SaveChanges();

            return response;

        }

        public dynamic GetRequisition(long docRefId, int serviceId)
        {
            var docRef = _context.DocumentReferences.FirstOrDefault(x => x.DocumentReferenceId == docRefId);

            OutstaningRequestViewModel outstaningRequest = new OutstaningRequestViewModel();
            outstaningRequest.Username = "BGUser001";
            if (docRef != null)
            {
                outstaningRequest.Service = serviceId;
                outstaningRequest.MessageId = docRef.MessageID;
            }

            var response = _httpInterceptor.CallOutstandingApi(outstaningRequest);

            if (response != null && response.Successful)
            {
                var outResponse = response.Requests.FirstOrDefault();

                CorrospondanceRequestViewModel corrospondanceRequestViewModel = new CorrospondanceRequestViewModel();
                corrospondanceRequestViewModel.Username = "BGUser001";
                if (outResponse != null) corrospondanceRequestViewModel.MessageId = outResponse.Id;

                var correspondenceResponse = _httpInterceptor.CallCorrespondenceRequestApi(corrospondanceRequestViewModel);

                if (correspondenceResponse.IsSuccess)
                {
                    correspondenceResponse.DocumentReferenceId = docRef.DocumentReferenceId;
                    _context.RequestLogs.Add(correspondenceResponse);
                    _context.SaveChanges();
                    return correspondenceResponse;
                }

                return false;
            }
            else
            {
                return false;
            }
        }


        public dynamic GetFinalResult(long docRefId, int serviceId)
        {
            var docRef = _context.DocumentReferences.FirstOrDefault(x => x.DocumentReferenceId == docRefId);

            OutstaningRequestViewModel outstaningRequest = new OutstaningRequestViewModel();
            outstaningRequest.Username = "BGUser001";
            if (docRef != null)
            {
                outstaningRequest.Service = serviceId;
                outstaningRequest.MessageId = docRef.MessageID;
            }

            var response = _httpInterceptor.CallOutstandingApi(outstaningRequest);

            if (response.Successful)
            {
                var outResponse = response.Requests.FirstOrDefault();

                if (outResponse != null && outResponse.TypeCode == 30)
                {
                    var earlyCompletionRequest = new EarlyCompletionRequest
                    {
                        Username = "BGUser001",
                        MessageId = outResponse.Id
                    };

                    var responseEarlyCompletionApi = _httpInterceptor.CallEarlyCompletionApi(earlyCompletionRequest);

                    if (responseEarlyCompletionApi.IsSuccess)
                    {
                        if (!string.IsNullOrEmpty(responseEarlyCompletionApi.File))
                        {
                            if (docRef != null) docRef.OverallStatus = 10; // Overall Process is completed
                        }
                        responseEarlyCompletionApi.DocumentReferenceId = docRef.DocumentReferenceId;
                        _context.RequestLogs.Add(responseEarlyCompletionApi);
                        _context.SaveChanges();
                        return responseEarlyCompletionApi;
                    }
                }
                else if (outResponse != null && outResponse.TypeCode == 20)
                {
                    var applicationPollRequest = new ApplicationPollRequest
                    {
                        Username = "BGUser001",
                        MessageId = outResponse.Id
                    };

                    var responseEarlyCompletionApi = _httpInterceptor.CallApplicationPollRequestApi(applicationPollRequest);

                    if (responseEarlyCompletionApi.IsSuccess)
                    {
                        if (!string.IsNullOrEmpty(responseEarlyCompletionApi.File))
                        {
                            if (docRef != null) docRef.OverallStatus = 10; // Overall Process is completed
                        }
                        responseEarlyCompletionApi.DocumentReferenceId = docRef.DocumentReferenceId;
                        _context.RequestLogs.Add(responseEarlyCompletionApi);
                        _context.SaveChanges();
                        return responseEarlyCompletionApi;
                    }
                }

                return false;
            }
            else
            {
                return false;
            }
        }


        public DocumentReference GetRegistration(long regId)
        {
            var documentReference =
                _context.DocumentReferences.Include(x => x.SupportingDocuments)
                    .Include(x => x.Applications)
                    .Include(x => x.Parties)
                    .Include(x => x.Titles)
                    .Select(sel => new DocumentReference
                    {
                        DocumentReferenceId = sel.DocumentReferenceId,
                        Email = sel.Email,
                        AP1WarningUnderstood = sel.AP1WarningUnderstood,
                        Titles = sel.Titles.Select(s => new TitleNumber
                        {
                            UpdatedDate = s.UpdatedDate,
                            Status = s.Status,
                            DocumentReferenceId = s.DocumentReferenceId,
                            TitleNumberId = s.TitleNumberId,
                            CreatedDate = s.CreatedDate,
                            TitleNumberCode = s.TitleNumberCode,
                        }).ToList(),
                        RegistrationTypeId = sel.RegistrationTypeId,
                        ApplicationAffects = sel.ApplicationAffects,
                        ApplicationDate = sel.ApplicationDate,
                        Applications = sel.Applications.Select(app => new ApplicationForm
                        {
                            ApplicationFormId = app.ApplicationFormId,
                            DocumentReferenceId = app.DocumentReferenceId,
                            FeeInPence = app.FeeInPence,
                            Priority = app.Priority,
                            Type = app.Type,
                            Value = app.Value,
                            ExternalReference = app.ExternalReference,
                            Document = app.Document,
                            CertifiedCopy = app.CertifiedCopy,
                            IsMdRef = app.IsMdRef,
                            MdRef = app.MdRef,
                            SortCode = app.SortCode,
                            ChargeDate = app.ChargeDate,
                            Variety = app.Variety,
                            IsChecked = app.IsChecked
                        }).ToList(),
                        DisclosableOveridingInterests = sel.DisclosableOveridingInterests,
                        LocalAuthority = sel.LocalAuthority,
                        TotalFeeInPence = sel.TotalFeeInPence,
                        TelephoneNumber = sel.TelephoneNumber,
                        SupportingDocuments = sel.SupportingDocuments.Select(sup => new SupportingDocuments
                        {
                            CertifiedCopy = sup.CertifiedCopy,
                            SupportingDocumentId = sup.SupportingDocumentId,
                            DocumentReferenceId = sup.DocumentReferenceId,
                            DocumentName = sup.DocumentName,
                            DocumentId = sup.DocumentId,
                            IsChecked = sup.IsChecked,
                            Notes = sup.Notes,
                            AdditionalProviderFilter = sup.AdditionalProviderFilter,
                            ApplicationMessageId = sup.ApplicationMessageId,
                            Base64 = sup.Base64,
                            DocumentType = sup.DocumentType,
                            ExternalReference = sup.ExternalReference,
                            FileExtension = sup.FileExtension,
                            FileName = sup.FileName,
                            MessageId = sup.MessageId
                        }).ToList(),
                        PostcodeOfProperty = sel.PostcodeOfProperty,
                        Reference = sel.Reference,
                        MessageID = sel.MessageID,
                        Parties = sel.Parties.Select(party => new Party
                        {
                            Surname = party.Surname,
                            PartyType = party.PartyType,
                            IsApplicant = party.IsApplicant,
                            DocumentReferenceId = party.DocumentReferenceId,
                            CompanyOrForeName = party.CompanyOrForeName,
                            PartyId = party.PartyId,
                            Roles = party.Roles,
                            AddressForService = party.AddressForService,
                            Addresses = party.Addresses
                        }).ToList(),
                        Status = sel.Status,
                        AdditionalProviderFilter = sel.AdditionalProviderFilter,
                        ExternalReference = sel.ExternalReference,
                        RequestLogs = sel.RequestLogs,
                        Representations = sel.Representations,
                        Outstanding = sel.Outstanding,
                        OverallStatus = sel.OverallStatus
                    })
                    .FirstOrDefault(s => s.Status && s.DocumentReferenceId == regId);

            return documentReference;

        }

    }
}
