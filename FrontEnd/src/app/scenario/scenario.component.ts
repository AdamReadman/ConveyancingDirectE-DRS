import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup, FormBuilder, Validators, FormGroupDirective } from '@angular/forms';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ConfirmRegistrationComponent } from '../angular-dialogs/confirm-registration/confirm-registration.component';
import { ApplicationForm, Document } from '../models/application-form';
import { DocumentReference } from '../models/document-reference';
import { Address, Party } from '../models/party';
import { SupportingDocuments } from '../models/supporting-documents';
import { TitleNumber } from '../models/title-number';
import { RegistrationService } from '../services/registration.service';
import * as FileSaver from 'file-saver';
import { CommonUtils } from 'src/environments/common-utils';
import Swal from 'sweetalert2';
import { RequestLogs } from '../models/request-logs';
import { Representation } from '../models/representation';
import { Outstanding } from '../models/outstanding';
import { AttachmentService } from '../services/attachment.service';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { environment } from 'src/environments/environment';
import { ProgressComponent } from '../angular-dialogs/progress/progress.component';
import { finalize } from 'rxjs/operators';

@Component({
  selector: 'app-scenario',
  templateUrl: './scenario.component.html',
  styleUrls: ['./scenario.component.css']
})
export class ScenarioComponent implements OnInit {

  rolesList: string[] = ["Borrower", "Lender", "PersonalRepresentative", "Proprietor", "Third Party", "Transferee", "Transferor"];
  appTypeList: string[] = [
    "Adverse possession of registered land",
    "Notification of adverse possession",
    "Agreed Notice",
    "Alteration of Register",
    "Appointment of New Trustee",
    "Assent"
  ];
  supDocNameList: string[] = [
    "Abstract",
    "Agreement",
    "Assignment",
    "Conveyance",
    "Correspondence",
    "Court Order",
    "Deed",
    "Form DI",
    "Document List",
    "Evidence",
    "EX1A",
    "Identity Evidence",
    "Identity Form",
    "Indenture",
    "Lease",
    "Licence",
    "LR Correspondence",
    "Power of Attorney",
    "Stamp Duty Land Tax",
    "Statement Of Truth",
    "Statutory Declaration",
    "Witness Statement",
  ];

  titleList: TitleNumber[] = [];
  applicationList: ApplicationForm[] = [];
  supportingDocList: SupportingDocuments[] = [];
  partyList: Party[] = [];
  representationList: Representation[] = [];

  logsList: RequestLogs[] = [];
  outstandingList: Outstanding[] = [];

  documentReferenceGroup!: FormGroup;

  txtTitle: FormControl = new FormControl();
  applicationGroup!: FormGroup;
  supportingDocGroup!: FormGroup;
  partyGroup!: FormGroup;
  representationGroup!: FormGroup;
  mainPostalGroup!: FormGroup;
  address1Group!: FormGroup;
  address2Group!: FormGroup;


  selectedTitleNumber: number | undefined;
  selectedApplicationId: number | undefined;
  selectedsupportingDocId: number | undefined;
  selectedPartyId: number | undefined;
  selectedNotesId: number | undefined;
  selectedRepId: number | undefined;


  @ViewChild('txtCustomEmail') txtCustomEmail!: ElementRef;

  regType!: number;
  docRefId!: any;
  @ViewChild("file") file: ElementRef | undefined;
  @ViewChild("supportingDocumentfile") supportingDocumentfile: ElementRef | undefined;

  supportingDocumentFileObject: any = {};

  titleSaveBtn = "Add";
  appSaveBtn = "Add";
  supDocSaveBtn = "Add";
  partSaveBtn = "Add";
  notesSaveBtn = "Add";
  repSaveBtn = "Add";

  appType = 'other';
  repType = 'LodgingConveyancer';
  addressType = 'DXAddress';

  supDocType = 'supDoc';

  isOtherApplication = true;

  private hubConnection!: HubConnection;
  private connectionUrl = environment.apiURL + 'attachment/';
  regTypeComponent = "document-registration";
  regTypeComponentName = "";

  showProgress!: MatDialogRef<ProgressComponent, any>;

  constructor(
    private router: Router,
    private dialog: MatDialog,
    private formBuilder: FormBuilder,
    private registrationService: RegistrationService,
    private attachmentServices: AttachmentService,
    private route: ActivatedRoute,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this.regType = +this.route.snapshot.paramMap.get("regTypeId")!;
    this.docRefId = +this.route.snapshot.paramMap.get("docRefId")!;

    this.registrationService.GetRegistrationType(this.regType.toString()).subscribe(res => {
      this.regTypeComponent = res.Url;
      this.regTypeComponentName = res.TypeName;
      console.log(res)
    })

    this.hubConnection = new HubConnectionBuilder()
      .withUrl(this.connectionUrl)
      .withAutomaticReconnect()
      .build();
    this.hubConnection.start()
    this.documentReferenceGroup = this.formBuilder.group({
      AdditionalProviderFilter: ['', Validators.required],
      MessageID: [''],
      ExternalReference: ['', Validators.required],
      UserID: [+localStorage.getItem("userId")!],
      Reference: ['', Validators.required],
      TotalFeeInPence: ['', Validators.required],
      Email: ['', [Validators.required, Validators.email]],
      TelephoneNumber: ['', Validators.required],
      AP1WarningUnderstood: [true],
      ApplicationDate: [new Date().toISOString().substring(0, 10), Validators.required],
      DisclosableOveridingInterests: [true],
      ApplicationAffects: ['', Validators.required],
      RegistrationTypeId: [this.regType],
      PostcodeOfProperty: [''],
      LocalAuthority: [''],
      DocumentReferenceId: 0
    })

    // const _today = CommonUtils.formatDate();
    // this.documentReferenceGroup.controls.ApplicationDate.setValue(_today.toISOString().substring(0, 10););

    this.applicationGroup = this.formBuilder.group({
      Priority: [],
      Value: [],
      FeeInPence: [],
      Type: [''],
      LocalId: [0],
      IsSelected: [false],
      ApplicationFormId: 0,
      DocumentReferenceId: 0,
      CertifiedCopy: ['', Validators.required],
      ExternalReference: ['', Validators.required],
      Document: {},
      Variety: [this.appType],
      ChargeDate: [new Date().toISOString().substring(0, 10)],
      IsMdRef: ['yes'],
      MdRef: [''],
      SortCode: [''],
      IsChecked: true
    });

    this.supportingDocGroup = this.formBuilder.group({
      CertifiedCopy: [],
      DocumentName: '',
      AdditionalProviderFilter: ['', Validators.required],
      MessageId: 1,
      ExternalReference: ['', Validators.required],
      ApplicationMessageId: ['', Validators.required],
      //ApplicationType: ['', Validators.required],

      DocumentId: [0],
      DocumentType: [this.supDocType, Validators.required],

      FileName: '',
      Base64: '',
      FileExtension: '',

      Notes: '',
      IsChecked: true,
      LocalId: [0],
      IsSelected: [false],
      SupportingDocumentId: 0,
      DocumentReferenceId: 0
    });

    this.representationGroup = this.formBuilder.group({
      RepresentationId: 0,
      Type: ['LodgingConveyancer'],
      RepresentativeId: [1],
      Name: [''],
      Reference: [''],
      AddressType: ['DXAddress'],

      LocalId: [0],
      IsSelected: [false],
      DocumentReferenceId: 0,

      CareOfName: [''],
      CareOfReference: [''],

      DxNumber: [''],
      DxExchange: [''],

      AddressLine1: [''],
      AddressLine2: [''],
      AddressLine3: [''],
      AddressLine4: [''],
      City: [''],
      County: [''],
      Country: [''],
      PostCode: [''],

    });
    this.mainPostalGroup = this.formBuilder.group({
      AddressId: [0],
      PartyId: [0],

      Type: ['main'],
      SubType: ['post'],
      AddressLine1: ['', Validators.required],
      AddressLine2: [''],
      AddressLine3: [''],
      AddressLine4: [''],
      City: [''],
      County: [''],
      Country: [''],
      PostCode: [''],

      CareOfName: [''],
      CareOfReference: [''],
      DxNumber: [''],
      DxExchange: [''],

      EmailAddress: ['', Validators.email]
    })

    this.address1Group = this.formBuilder.group({
      AddressId: [0],
      PartyId: [0],

      Type: ['ad1'],
      SubType: [''],
      AddressLine1: ['', Validators.required],
      AddressLine2: [''],
      AddressLine3: [''],
      AddressLine4: [''],
      City: [''],
      County: [''],
      Country: [''],
      PostCode: [''],

      CareOfName: [''],
      CareOfReference: [''],
      DxNumber: [''],
      DxExchange: [''],

      EmailAddress: ['', Validators.email]
    })
    this.address2Group = this.formBuilder.group({
      AddressId: [0],
      PartyId: [0],

      Type: ['ad2'],
      SubType: [''],
      AddressLine1: ['', Validators.required],
      AddressLine2: [''],
      AddressLine3: [''],
      AddressLine4: [''],
      City: [''],
      County: [''],
      Country: [''],
      PostCode: [''],

      CareOfName: [''],
      CareOfReference: [''],
      DxNumber: [''],
      DxExchange: [''],

      EmailAddress: ['', Validators.email]
    })

    this.partyGroup = this.formBuilder.group({
      PartyType: [true, Validators.required],
      IsApplicant: [true],
      CompanyOrForeName: ['', Validators.required],
      Surname: [''],
      Roles: [''],
      ViewModelRoles: [[], Validators.required],
      LocalId: [0],
      IsSelected: [false],
      PartyId: 0,
      AddressForService: [''],
      DocumentReferenceId: 0,
      Addresses: [],

    });

    this.PopulateAllFields();

    this.applicationGroup.get('Variety')?.valueChanges.subscribe(res => {
      this.appType = res

    })

    this.supportingDocGroup.get('DocumentType')?.valueChanges.subscribe(res => {
      this.supDocType = res

    })

    this.applicationGroup.get('IsMdRef')?.valueChanges.subscribe(res => {

      if (res == 'yes') {
        this.applicationGroup.controls.MdRef.enable();
      } else {
        this.applicationGroup.controls.MdRef.disable();
        this.applicationGroup.controls.MdRef.setValue("");
      }
    })

    this.partyGroup.get('PartyType')?.valueChanges.subscribe(res => {
      this.partyType = res
      if (this.partyType == "company") {

      } else if (this.partyType == "person") {

      }

    })

    this.representationGroup.get('Type')?.valueChanges.subscribe(res => {

      console.log("RES:", res);
      this.repType = res

      this.representationGroup.controls['Name'].clearValidators();
      this.representationGroup.controls['Name'].updateValueAndValidity();

      this.representationGroup.controls['Reference'].clearValidators();
      this.representationGroup.controls['Reference'].updateValueAndValidity();
      this.representationGroup.controls['CareOfName'].clearValidators();
      this.representationGroup.controls['CareOfName'].updateValueAndValidity();
      this.representationGroup.controls['CareOfReference'].clearValidators();
      this.representationGroup.controls['CareOfReference'].updateValueAndValidity();
      this.representationGroup.controls['DxNumber'].clearValidators();
      this.representationGroup.controls['DxNumber'].updateValueAndValidity();
      this.representationGroup.controls['DxExchange'].clearValidators();
      this.representationGroup.controls['DxExchange'].updateValueAndValidity();
      this.representationGroup.controls['AddressLine1'].clearValidators();
      this.representationGroup.controls['AddressLine1'].updateValueAndValidity();

      if (res != 'LodgingConveyancer') {
        this.representationGroup.controls['Name'].setValidators([Validators.required]);
        this.representationGroup.controls['Reference'].setValidators([Validators.required]);
        //this.representationGroup.controls['CareOfName'].setValidators([Validators.required]);
        //this.representationGroup.controls['CareOfReference'].setValidators([Validators.required]);

        if (this.representationGroup.controls['AddressType'].value == 'DXAddress') {
          this.representationGroup.controls['DxNumber'].setValidators([Validators.required]);
          this.representationGroup.controls['DxExchange'].setValidators([Validators.required]);
        } else {

          this.representationGroup.controls['AddressLine1'].setValidators([Validators.required]);
        }
      }

    })

    this.representationGroup.get('AddressType')?.valueChanges.subscribe(res => {
      this.addressType = res

      this.representationGroup.controls['DxNumber'].clearValidators();
      this.representationGroup.controls['DxNumber'].updateValueAndValidity();
      this.representationGroup.controls['DxExchange'].clearValidators();
      this.representationGroup.controls['DxExchange'].updateValueAndValidity();
      this.representationGroup.controls['AddressLine1'].clearValidators();
      this.representationGroup.controls['AddressLine1'].updateValueAndValidity();

      if (res == 'DXAddress') {
        this.representationGroup.controls['DxNumber'].setValidators([Validators.required]);
        this.representationGroup.controls['DxExchange'].setValidators([Validators.required]);
      } else if (res == 'PostalAddress') {
        this.representationGroup.controls['AddressLine1'].setValidators([Validators.required]);
      }
    })


    this.address1Group.get('SubType')?.valueChanges.subscribe(res => {
      this.address1Type = res
    })

    this.address2Group.get('SubType')?.valueChanges.subscribe(res => {
      this.address2Type = res
    })

    // set validations in Support documents form based on selected Attachment Type
    this.onAttcmntTypeChange();
  }

  async PopulateAllFields() {
    if (this.docRefId != 0) {
      await new Promise(resolve => setTimeout(resolve, 100));
      this.showProgress = CommonUtils.showProgress(this.dialog);

      this.registrationService.GetRegistration(this.docRefId).subscribe(res => {

        console.log("CASE:", res);

        this.showProgress.close();
        this.documentReferenceGroup = this.formBuilder.group(res);

        this.titleList = res.Titles ?? [];

        this.titleList.forEach(s => {
          s.LocalId = this.titleId++;
        })

        this.applicationList = res.Applications ?? [];

        this.applicationList.forEach(s => {
          s.LocalId = this.appId++;
        })

        this.supportingDocList = res.SupportingDocuments ?? [];

        this.supportingDocList.forEach(s => {
          s.LocalId = this.appId++;
        })

        this.partyList = res.Parties ?? [];

        this.partyList.forEach(s => {
          s.LocalId = this.appId++;
          s.ViewModelRoles = s.Roles!.split(',');
        })

        this.logsList = res.RequestLogs ?? [];
        this.outstandingList = res.Outstanding ?? [];

        this.representationList = res.Representations ?? [];

        this.representationList.forEach(s => {
          s.LocalId = this.appId++;
        })

      }, err => {
        this.showProgress.close();
      })
    }
  }

  partyType = 'company';
  address1Type = 'post';
  address2Type = 'post';

  // Title Numbers

  titleId = 1;
  PushTitleToGrid() {

    var insertObj: TitleNumber = {
      LocalId: this.titleId++,
      TitleNumberCode: this.txtTitle.value,
      IsSelected: false,
    }

    if (this.txtTitle.valid) {
      if (this.titleList.find(s => s.LocalId == this.selectedTitleNumber) == null) {
        this.titleList.push(Object.assign({}, insertObj));
      } else {
        insertObj = this.titleList.find(s => s.LocalId == this.selectedTitleNumber)!;
        this.titleList = this.titleList.filter(s => s.LocalId != this.selectedTitleNumber);

        insertObj.TitleNumberCode = this.txtTitle.value;

        this.titleList.push(Object.assign({}, insertObj));
        this.titleList = this.titleList.sort((a, b) => {
          return a.LocalId! - b.LocalId!;
        });
      }
      this.ClearTitleFields();
    }
  }


  SelectTitleRow(id: any) {
    this.titleSaveBtn = "Update";
    this.selectedTitleNumber = id
    this.titleList.filter(x => x.LocalId == id).forEach(x => x.IsSelected = true);
    this.titleList.filter(x => x.LocalId != id).forEach(x => x.IsSelected = false);

    var selectedTitleNumber: any = this.titleList?.find(s => s.LocalId == id);
    this.selectedTitleNumber = selectedTitleNumber.LocalId;
    this.txtTitle?.setValue(selectedTitleNumber.TitleNumberCode)
  }

  ClearTitleFields() {
    this.titleSaveBtn = "Add";
    this.selectedTitleNumber = 0;
    this.txtTitle.setValue([]);
    this.txtTitle.reset();
    this.titleList?.forEach(s => s.IsSelected = false);
  }

  RemoveTitle(id: any) {
    this.titleList = this.titleList.filter(x => x.LocalId != id);
    if (this.selectedTitleNumber == id) {
      this.selectedTitleNumber = undefined;
    }
  }

  // For Applications

  appId = 1;
  fileName: any = "Choose files";
  PushApplicationToGrid(formDirective: FormGroupDirective) {

    var insertObj: ApplicationForm = {

    }

    if (this.applicationGroup.valid) {

      if (this.file?.nativeElement.files[0] != null) {

        var documents: Document = {};
        let fileToUpload = this.file?.nativeElement.files[0];

        var that = this;

        insertObj = that.applicationGroup.value;

        if (fileToUpload != undefined) {
          let fileName = fileToUpload.name;
          let fileString: any = "";
          let reader = new FileReader();
          reader.readAsDataURL(fileToUpload);
          reader.onload = function () {
            //me.modelvalue = reader.result;  
            fileString = reader.result;
            let fileExtension = fileToUpload.name.substr(
              fileToUpload.name.lastIndexOf(".") + 1
            );

            insertObj.LocalId = that.appId++
            insertObj.IsSelected = false;


            documents = {
              DocumentId: insertObj.Document?.DocumentId == undefined ? 0 : insertObj.Document.DocumentId,
              ApplicationFormId: insertObj.Document?.ApplicationFormId == undefined ? 0 : insertObj.Document.ApplicationFormId,
              FileName: fileName, Base64: fileString, FileExtension: fileExtension
            };
            that.InsertDataToAppList(insertObj, documents, formDirective);


          };
          reader.onerror = function (error) {
            console.log('Error: ', error);
          };
        } else if (insertObj.Document?.DocumentId != null) {
          this.InsertDataToAppList(insertObj, insertObj.Document, formDirective);
        }

      } else {

        this.toastr.warning("Please attach the Application Documnet")

      }


    }
  }

  InsertDataToAppList(insertObj: ApplicationForm, documents: Document, formDirective: FormGroupDirective) {
    insertObj.Document = documents;
    if (this.applicationList.find(s => s.LocalId == this.selectedApplicationId) == null) {

      if (this.applicationList.length == 0) {

        insertObj.Priority = 1;

      } else {

        insertObj.Priority = this.applicationList[this.applicationList.length - 1].Priority! + 1;
      }

      this.applicationList.push(Object.assign({}, insertObj));

    } else {
      this.applicationList = this.applicationList.filter(s => s.LocalId != this.selectedApplicationId);
      insertObj.LocalId = this.selectedApplicationId;
      this.applicationList.push(Object.assign({}, insertObj));
      this.applicationList = this.applicationList.sort((a, b) => {
        return a.LocalId! - b.LocalId!;
      });
    }
    this.ClearAppFields(formDirective);
  }

  SelectAppRow(id: any) {
    this.appSaveBtn = "Update"
    this.selectedApplicationId = id
    this.applicationList.filter(x => x.LocalId == id).forEach(x => x.IsSelected = true);
    this.applicationList.filter(x => x.LocalId != id).forEach(x => x.IsSelected = false);

    var selectedObj: ApplicationForm = this.applicationList.find(s => s.LocalId == id)!;
    this.selectedApplicationId = selectedObj.LocalId;
    this.applicationGroup.setValue(selectedObj);
    this.fileName = ""
    this.fileName = selectedObj.Document!.FileName;

  }

  ClearAppFields(formDirective: FormGroupDirective) {
    this.appSaveBtn = "Add"

    this.applicationList.forEach(x => x.IsSelected = false);
    this.selectedApplicationId = 0;
    this.fileName = "Choose File";


    formDirective.resetForm();
    this.applicationGroup.reset();


    this.appType = 'other';
    this.applicationGroup.controls.Variety.setValue(this.appType);
    this.file = undefined;
  }

  RemoveApp(id: any) {
    this.applicationList = this.applicationList.filter(x => x.LocalId != id);
    if (this.selectedApplicationId == id) {
      this.selectedApplicationId = undefined;
    }
  }

  uploadFile() {
    this.fileName = "";
    var files: any = this.file!.nativeElement.files;

    for (let i = 0; i < files.length; i++) {
      this.fileName += files[i].name + (i + 1 != files.length ? ", " : "");
    }
  }


  //handle application varity change event
  VarietyChange(value: any) {

    console.log('change value:', value);

    if (value == 'other') {
      this.isOtherApplication = true;
      this.applicationGroup.controls['Type'].setValidators([Validators.required]);
    } else {
      this.isOtherApplication = false;
      this.applicationGroup.controls['Type'].clearValidators();
      this.applicationGroup.controls['Type'].updateValueAndValidity();
    }

  }

  DownloadAttached(item: ApplicationForm) {
    FileSaver.saveAs(item.Document?.Base64!, item.Document?.FileName);
  }

  // For Supporting Documents
  supDocfileName: any = "Choose files";
  supDocId = 1;

  async PushSupDocumentToGrid(formDirective: FormGroupDirective) {

    var insertObj: SupportingDocuments = {

    }

    //Add Supporting Documnet
    if (this.supportingDocGroup.valid && this.supDocType == "supDoc") {

      if (this.supportingDocumentFileObject.Base64) {

        insertObj = this.supportingDocGroup.value;
        insertObj.LocalId = this.supDocId++
        insertObj.IsSelected = false;


        if (insertObj.DocumentType == "supDoc") {
          insertObj.FileName = this.supportingDocumentFileObject.FileName;
          insertObj.Base64 = this.supportingDocumentFileObject.Base64;
          insertObj.FileExtension = this.supportingDocumentFileObject.FileExtension;
        }

        if (this.supportingDocList.find(s => s.LocalId == this.selectedsupportingDocId) == null) {
          // insertObj.MessageId = this.supportingDocList[this.supportingDocList.length - 1].MessageId! + 1;
          insertObj.MessageId = 1;
          this.supportingDocList.push(Object.assign({}, insertObj));
        } else {

          this.supportingDocList = this.supportingDocList.filter(s => s.LocalId != this.selectedsupportingDocId);
          insertObj.LocalId = this.selectedsupportingDocId;
          this.supportingDocList.push(Object.assign({}, insertObj));
          this.supportingDocList = this.supportingDocList.sort((a, b) => {
            return a.LocalId! - b.LocalId!;
          });
        }
        this.ClearSupDocFields(formDirective);

      } else {
        this.toastr.warning("Please attach a Documnet");
      }

    }
    //Add Note
    else if (this.supportingDocGroup.valid && this.supDocType == "notes") {

      insertObj = this.supportingDocGroup.value;
      insertObj.LocalId = this.supDocId++
      insertObj.IsSelected = false;

      if (this.supportingDocList.find(s => s.LocalId == this.selectedsupportingDocId) == null) {
        // insertObj.MessageId = this.supportingDocList[this.supportingDocList.length - 1].MessageId! + 1;
        insertObj.MessageId = 1;
        this.supportingDocList.push(Object.assign({}, insertObj));
      } else {

        this.supportingDocList = this.supportingDocList.filter(s => s.LocalId != this.selectedsupportingDocId);
        insertObj.LocalId = this.selectedsupportingDocId;
        this.supportingDocList.push(Object.assign({}, insertObj));
        this.supportingDocList = this.supportingDocList.sort((a, b) => {
          return a.LocalId! - b.LocalId!;
        });
      }
      this.ClearSupDocFields(formDirective);

    }
  }

  SelectSupDocRow(id: any) {

    this.supDocSaveBtn = "Update"
    this.selectedsupportingDocId = id
    this.supportingDocList.filter(x => x.LocalId == id).forEach(x => x.IsSelected = true);
    this.supportingDocList.filter(x => x.LocalId != id).forEach(x => x.IsSelected = false);

    var selectedObj: SupportingDocuments = this.supportingDocList?.find(s => s.LocalId == id)!;
    this.selectedsupportingDocId = selectedObj.LocalId;
    this.supportingDocGroup.setValue(selectedObj);
    this.supDocfileName = ""
    this.supDocfileName = selectedObj.FileName + "." + selectedObj.FileExtension;
  }

  // CLEAR SUPPORTING DOCUMENT FORM

  ClearSupDocFields(formDirective: FormGroupDirective) {
    this.supDocSaveBtn = "Add"
    this.supDocfileName = "Choose File";

    this.supportingDocList.forEach(x => x.IsSelected = false);

    this.selectedsupportingDocId = 0;

    formDirective.resetForm();
    this.supportingDocGroup.reset();

    this.supportingDocumentFileObject.Base64 = null;

    /*this.supportingDocGroup.patchValue({
      CertifiedCopy: [],
      DocumentName: '',
      LocalId: [0],
      IsSelected: [false],
      SupportingDocumentId: 0,
      DocumentReferenceId: 0,
  
      AdditionalProviderFilter: '',
      MessageId: 1,
      ExternalReference: '',
      ApplicationMessageId: '', 
      //ApplicationType: '',
  
      DocumentType: [this.supDocType],
      FileName: '',
      Base64: '',
      FileExtension: '',
      Notes: '',
  
    }) */


  }

  RemoveSupDoc(id: any) {
    this.supportingDocList = this.supportingDocList.filter(x => x.LocalId != id);
    if (this.selectedsupportingDocId == id) {
      this.selectedsupportingDocId = undefined;
    }
  }

  uploadSupDocFile() {

    this.supDocfileName = "";
    var fileToUpload: any = this.supportingDocumentfile!.nativeElement.files[0];

    /**** Uploading file if the type is Supporting Document */

    var that = this;
    if (fileToUpload != undefined) {
      this.supDocfileName = fileToUpload.name;
      let fileName = this.supDocfileName.split('.').slice(0, -1).join('.')
      let fileString: any = "";
      let reader = new FileReader();
      reader.readAsDataURL(fileToUpload);
      reader.onload = function () {
        fileString = reader.result;
        let fileExtension = fileToUpload.name.substr(
          fileToUpload.name.lastIndexOf(".") + 1
        );

        that.supportingDocumentFileObject.FileName = fileName;
        that.supportingDocumentFileObject.Base64 = fileString;
        that.supportingDocumentFileObject.FileExtension = fileExtension;

      };
      reader.onerror = function (error) {
        console.log('Error: ', error);
      };
    }

  }

  /********* For Supporting Documents End ************/

  // Push Party to Grid
  partyId = 1;
  PushPartyToGrid(formDirective: FormGroupDirective) {

    var insertObj: Party = {

    }
    if (this.partyGroup.valid && this.mainPostalGroup.valid) {

      var isValid = true;

      if (this.address1Group.controls.SubType.value == 'post' || this.address1Group.controls.SubType.value == 'dx' || this.address1Group.controls.SubType.value == 'email') {
        if (this.address1Type == 'post') {

          if (this.address1Group.controls.AddressLine1.status == 'INVALID') {

            isValid = false;
            this.toastr.warning('Please fill Address Line 1 in Additional Adddress 1')

          }

        } else if (this.address1Type == 'dx') {

          if (this.address1Group.controls.DxNumber.status == 'INVALID') {
            isValid = false;
            this.toastr.warning('Please fill DXNumber in Additional Adddress 1')

          }

          if (this.address1Group.controls.DxExchange.status == 'INVALID') {

            isValid = false;
            this.toastr.warning('Please fill DxExchange in Additional Adddress 1')

          }

        } else if (this.address1Type == 'email') {


          if (this.address1Group.controls.EmailAddress.status == 'INVALID') {

            isValid = false;
            this.toastr.warning('Please fill EmailAddress in Additional Adddress 1')

          }
        }
      }

      if (this.address2Group.controls.SubType.value == 'post' || this.address2Group.controls.SubType.value == 'dx' || this.address2Group.controls.SubType.value == 'email') {
        if (this.address2Type == 'post') {

          if (this.address2Group.controls.AddressLine1.status == 'INVALID') {

            isValid = false;
            this.toastr.warning('Please fill Address Line 1 in Additional Adddress 2')

          }

        } else if (this.address2Type == 'dx') {

          if (this.address2Group.controls.DxNumber.status == 'INVALID') {

            isValid = false;
            this.toastr.warning('Please fill DXNumber in Additional Adddress 2')

          }

          if (this.address2Group.controls.DxExchange.status == 'INVALID') {

            isValid = false;
            this.toastr.warning('Please fill DxExchange in Additional Adddress 2')

          }

        } else if (this.address2Type == 'email') {


          if (this.address2Group.controls.EmailAddress.status == 'INVALID') {

            isValid = false;
            this.toastr.warning('Please fill EmailAddress in Additional Adddress 2')

          }
        }
      }

      if (isValid) {

        insertObj = this.partyGroup.value;
        insertObj.LocalId = this.supDocId++
        insertObj.IsSelected = false;
        insertObj.Addresses = [];

        let postalAddress: Address = this.mainPostalGroup.value;
        let additionalAddress1: Address = this.address1Group.value;
        let additionalAddress2: Address = this.address2Group.value;

        insertObj.Addresses?.push(postalAddress);
        insertObj.Addresses?.push(additionalAddress1);
        insertObj.Addresses?.push(additionalAddress2);

        if (this.partyList.find(s => s.LocalId == this.selectedPartyId) == null) {
          this.partyList.push(Object.assign({}, insertObj));
        } else {

          this.partyList = this.partyList.filter(s => s.LocalId != this.selectedPartyId);
          insertObj.LocalId = this.selectedPartyId;
          this.partyList.push(Object.assign({}, insertObj));
          this.partyList = this.partyList.sort((a, b) => {
            return a.LocalId! - b.LocalId!;
          });
        }
        this.ClearPartyFields(formDirective);

      }

    }
  }

  SelectPartyRow(id: any) {
    this.partSaveBtn = "Update"
    this.selectedPartyId = id
    this.partyList.filter(x => x.LocalId == id).forEach(x => x.IsSelected = true);
    this.partyList.filter(x => x.LocalId != id).forEach(x => x.IsSelected = false);

    var selectedObj: Party = this.partyList?.find(s => s.LocalId == id)!;
    this.selectedPartyId = selectedObj.LocalId;
    this.partyGroup.setValue(selectedObj);
    this.mainPostalGroup.setValue(selectedObj.Addresses![0]);
    this.address1Group.setValue(selectedObj.Addresses![1]);
    this.address2Group.setValue(selectedObj.Addresses![2]);
  }

  ClearPartyFields(formDirective: FormGroupDirective) {
    this.partSaveBtn = "Add"
    this.partyList.forEach(x => x.IsSelected = false);

    this.selectedPartyId = 0;

    formDirective.resetForm();
    this.partyGroup.reset();
    this.mainPostalGroup.reset();
    this.address1Group.reset();
    this.address2Group.reset();
    /*this.partyGroup.setValue({
      PartyType: true,
      IsApplicant: true,
      CompanyOrForeName: '',
      Surname: '',
      Roles: '',
      ViewModelRoles: [],
      LocalId: [0],
      IsSelected: [false],
      PartyId: 0,
      DocumentReferenceId: 0,
  
    })*/

    this.partyGroup.controls.PartyId.setValue(0);
    this.partyGroup.controls.DocumentReferenceId.setValue(0);
    this.partyGroup.controls.ViewModelRoles.setValue([]);
  }

  RemoveParty(id: any) {
    this.partyList = this.partyList.filter(x => x.LocalId != id);
    if (this.selectedPartyId == id) {
      this.selectedPartyId = undefined;
    }
  }

  // For Representation and Additional Parties

  repId = 1;
  PushRepToGrid(formDirective: FormGroupDirective) {

    var insertObj: Representation = {

    }
    if (this.representationGroup.valid) {
      insertObj = this.representationGroup.value;
      insertObj.LocalId = this.repId++
      insertObj.IsSelected = false;

      if (this.representationList.find(s => s.LocalId == this.selectedRepId) == null) {
        try {

          insertObj.RepresentativeId = this.representationList[this.representationList.length - 1].RepresentativeId! + 1;

        } catch (error) { }

        this.representationList.push(Object.assign({}, insertObj));
      } else {

        this.representationList = this.representationList.filter(s => s.LocalId != this.selectedRepId);
        insertObj.LocalId = this.selectedRepId;
        this.representationList.push(Object.assign({}, insertObj));
        this.representationList = this.representationList.sort((a, b) => {
          return a.LocalId! - b.LocalId!;
        });
      }
      this.ClearRepFields(formDirective);

    }
  }

  SelectRepRow(id: any) {
    this.repSaveBtn = "Update"
    this.selectedRepId = id
    this.representationList.filter(x => x.LocalId == id).forEach(x => x.IsSelected = true);
    this.representationList.filter(x => x.LocalId != id).forEach(x => x.IsSelected = false);

    var selectedObj: any = this.representationList?.find(s => s.LocalId == id);
    this.selectedRepId = selectedObj.LocalId;
    this.representationGroup.setValue(selectedObj);
  }

  // CLEAR REPRESENTATION FORM
  ClearRepFields(formDirective: FormGroupDirective) {
    this.repSaveBtn = "Add"

    this.representationList.forEach(x => x.IsSelected = false);

    this.selectedRepId = 0;

    formDirective.resetForm();
    this.representationGroup.reset();

    /* this.representationGroup.patchValue({
       RepresentationId: 0,
       Type: 'LodgingConveyancer',
       RepresentativeId: 0,
       Name: '',
       Reference: '',
       AddressType: 'DXAddress',
       LocalId: [0],
       IsSelected: [false],
       DocumentReferenceId: 0,
   
       CareOfName: '',
       CareOfReference: '',
   
       DxNumber: 0,
       DxExchange: '',
   
       AddressLine1: '',
       AddressLine2: '',
       AddressLine3: '',
       AddressLine4: '',
       City: '',
       County: '',
       Country: '',
       PostCode: '',
     }) */

    this.representationGroup.controls.Type.setValue("LodgingConveyancer");
    this.representationGroup.controls.AddressType.setValue("DXAddress");

    this.representationGroup.controls.RepresentationId.setValue(0);
    this.representationGroup.controls.DocumentReferenceId.setValue(0);

  }

  RemoveRep(id: any) {
    this.representationList = this.representationList.filter(x => x.LocalId != id);
    if (this.selectedRepId == id) {
      this.selectedRepId = undefined;
    }
  }

  UpdateDatabase() {
    let documentRef: DocumentReference = this.documentReferenceGroup.value;
    documentRef.Titles = JSON.parse(JSON.stringify(this.titleList));
    documentRef.Applications = JSON.parse(JSON.stringify(this.applicationList));
    documentRef.SupportingDocuments = JSON.parse(JSON.stringify(this.supportingDocList));
    documentRef.Representations = JSON.parse(JSON.stringify(this.representationList));
    documentRef.Parties = JSON.parse(JSON.stringify(this.partyList));
    documentRef.RequestLogs = JSON.parse(JSON.stringify(this.logsList));
    documentRef.UserId = parseInt(localStorage.getItem("userId")!);
    documentRef.Outstanding = undefined;

    if (documentRef.Titles?.length! < 1) {
      this.toastr.warning("Please add at least one Title", "Fields missing")
    } else if (documentRef.Applications?.length! < 1) {
      this.toastr.warning("Please add at least one Application", "Fields missing")
    } else if (documentRef.SupportingDocuments?.length! < 1) {
      this.toastr.warning("Please add at least one Supporting Document", "Fields missing")
    } else if (documentRef.Representations?.length! < 1) {
      this.toastr.warning("Please add at least one Representation", "Fields missing")
    } else if (documentRef.Parties?.length! < 1) {
      this.toastr.warning("Please add at least one Party", "Fields missing")
    } else if (documentRef.Representations?.filter(s => s.Type == "LodgingConveyancer").length! < 1) {
      this.toastr.warning("Please add at least one Lodging Conveyancer", "Fields missing")
    } else if (this.documentReferenceGroup.valid) {

      this.showProgress = CommonUtils.showProgress(this.dialog);

      if (this.docRefId == 0) {
        this.registrationService.CreateRegistration(documentRef).pipe(
          finalize(() => this.showProgress.close())
        ).subscribe((res) => {
          this.ShowResponse(res);
        }, () => {
          this.toastr.error(this.regTypeComponentName + " has not successfully updated", "Changes failed");
        });
      } else {
        this.registrationService.UpdateRegistration(documentRef).pipe(
          finalize(() => this.showProgress.close())
        ).subscribe((res) => {
          this.ShowResponse(res);
        }, () => {
          this.toastr.error(this.regTypeComponentName + " has not successfully updated", "Changes failed");
        });
      }
    } else {
      this.toastr.warning("Please fill all fields in request header", "Fields missing")
    }
  }

  ShowResponse(res: any) {
    if (res == null) {
      this.toastr.error("There was an error occured while trying to connect Land Registry API", "Error sending request")

    } else if (res.IsSuccess) {
      const dialogRef = this.dialog.open(ConfirmRegistrationComponent, {
        data: { res }
      });
      dialogRef.afterClosed().subscribe(() => {
        this.PopulateAllFields();
      });
    } else {
      this.toastr.error("There was an error occured while trying to connect, please check all fields again", "Error sending request")
    }
  }

  SendPoolRequest() {
    this.showProgress = CommonUtils.showProgress(this.dialog);

    this.registrationService.GetPoolResponse(this.docRefId).pipe(
      finalize(() => this.showProgress.close())
    ).subscribe((res: RequestLogs) => {
      // console.log()
      Swal.fire({
        title: 'Poll Response from Gateway',
        html: `
        ${res.Description}
        `,
        icon: 'success',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Download Zip'
      }).then((result) => {
        if (result.isConfirmed) {
          FileSaver.saveAs(res.File!, res.FileName + "." + res.FileExtension?.toLowerCase());
          this.PopulateAllFields();
        }
      })
    });
  }

  DownloadAttachedPoll(item: RequestLogs) {
    this.showProgress = CommonUtils.showProgress(this.dialog);

    this.attachmentServices.GetAttachment(item.RequestLogId!).pipe(
      finalize(() => this.showProgress.close())
    ).subscribe(res => {

      FileSaver.saveAs(res!, item.FileName + "." + item.FileExtension?.toLowerCase());
      //this.PopulateAllFields();
    })
  }

  CollectAttachmentResult() {
    this.showProgress = CommonUtils.showProgress(this.dialog);
    this.registrationService.CollectAttachmentResult(this.docRefId, "70").pipe(
      finalize(() => this.showProgress.close())
    ).subscribe(res => {
      if (res.Successful) {
        this.PopulateAllFields();
        this.toastr.success("Please refresh the page to view the results", "Attachment Results collected")
      }
      else
        this.toastr.error("Something went wrong while collecting results", "Attachment Results Error")

    });
  }

  FindRequisitions() {
    this.showProgress = CommonUtils.showProgress(this.dialog);
    this.registrationService.GetRequisition(this.docRefId, "70").subscribe(res => {

      this.showProgress.close();

      if (res != false) {
        if (res.IsSuccess) {
          this.PopulateAllFields();
          this.toastr.success("Requisition Results collected", "Successe");

        }
        else
          this.toastr.error("Something went wrong while collecting results", "Requisition Results Error");

      } else {
        this.toastr.error("Something went wrong while collecting results", "Requisition Results Error");
      }

    });
  }

  RespondToRequisition() {
    this.showProgress = CommonUtils.showProgress(this.dialog);
    this.attachmentServices.RespondToRequisition(this.docRefId).pipe(
      finalize(() => this.showProgress.close())
    ).subscribe(res => {

      if (res != false) {

        this.PopulateAllFields();
        this.toastr.success("Success", "Replied to Requisition")


      } else {

        this.toastr.error("Something went wrong while replying to Requisition", "Requisition Reply Error")

      }

    });
  }

  CollectFinalResults() {
    this.showProgress = CommonUtils.showProgress(this.dialog);
    this.registrationService.CollectFinalResults(this.docRefId, "70").pipe(
      finalize(() => this.showProgress.close())
    ).subscribe(res => {

      if (res != false) {
        this.PopulateAllFields();

        if (res.IsSuccess)
          this.toastr.success("Please refresh the page to view the results", "Requisition Results collected")
        else
          this.toastr.error("Something went wrong while collecting results", "Requisition Results Error")

      } else {
        this.toastr.error("Something went wrong while collecting results", "Requisition Results Error")

      }

    });
  }

  onAttcmntTypeChange() {

    if (this.supDocType == "supDoc") {

      this.supportingDocGroup.get('DocumentName')?.setValidators([Validators.required]);

      this.supportingDocGroup.get('CertifiedCopy')?.setValidators([Validators.required]);

      this.supportingDocGroup.get('Notes')?.clearValidators();
      this.supportingDocGroup.controls['Notes'].updateValueAndValidity();


    } else {

      this.supportingDocGroup.get('DocumentName')?.clearValidators();
      this.supportingDocGroup.controls['DocumentName'].updateValueAndValidity();

      this.supportingDocGroup.get('CertifiedCopy')?.clearValidators();
      this.supportingDocGroup.controls['CertifiedCopy'].updateValueAndValidity();

      this.supportingDocGroup.get('Notes')?.setValidators([Validators.required]);

    }
  }

  ChangeCheckEvent(item: any) {
    if (item.ApplicationFormId != null) {
      this.hubConnection.invoke("ChangeAttachmentState", item.ApplicationFormId, "app", item.IsChecked).then(res => {

      })
    } else if (item.SupportingDocumentId != null) {
      this.hubConnection.invoke("ChangeAttachmentState", item.SupportingDocumentId, "sup", item.IsChecked).then(res => {

      })
    }
  }
}
