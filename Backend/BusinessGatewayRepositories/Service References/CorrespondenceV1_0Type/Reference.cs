﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BusinessGatewayRepositories.CorrespondenceV1_0Type {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://poll.correspondencev2_1.ws.bg.lr.gov/", ConfigurationName="CorrespondenceV1_0Type.CorrespondenceV2_1PollRequestService")]
    public interface CorrespondenceV2_1PollRequestService {
        
        // CODEGEN: Parameter 'return' requires additional schema information that cannot be captured using the parameter mode. The specific attribute is 'System.Xml.Serialization.XmlElementAttribute'.
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.FaultContractAttribute(typeof(string), Action="", Name="SchemaException")]
        [System.ServiceModel.FaultContractAttribute(typeof(string), Action="", Name="SOAPEngineSystemException")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        BusinessGatewayRepositories.CorrespondenceV1_0Type.getResponseResponse getResponse(BusinessGatewayRepositories.CorrespondenceV1_0Type.getResponseRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        System.Threading.Tasks.Task<BusinessGatewayRepositories.CorrespondenceV1_0Type.getResponseResponse> getResponseAsync(BusinessGatewayRepositories.CorrespondenceV1_0Type.getResponseRequest request);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.oscre.org/ns/eReg/MR01-20090605/PollRequest")]
    public partial class PollRequestType : object, System.ComponentModel.INotifyPropertyChanged {
        
        private Q1IdentifierType idField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public Q1IdentifierType ID {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
                this.RaisePropertyChanged("ID");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.oscre.org/ns/eReg/MR01-20090605/PollRequest")]
    public partial class Q1IdentifierType : object, System.ComponentModel.INotifyPropertyChanged {
        
        private MessageIDTextType messageIDField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public MessageIDTextType MessageID {
            get {
                return this.messageIDField;
            }
            set {
                this.messageIDField = value;
                this.RaisePropertyChanged("MessageID");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.oscre.org/ns/eReg/MR01-20090605/PollRequest")]
    public partial class MessageIDTextType : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string valueField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value {
            get {
                return this.valueField;
            }
            set {
                this.valueField = value;
                this.RaisePropertyChanged("Value");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.oscre.org/ns/eReg-Final/2012/CorrespondenceV1_0")]
    public partial class AttachmentType : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string filenameField;
        
        private string formatField;
        
        private byte[] valueField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form=System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string filename {
            get {
                return this.filenameField;
            }
            set {
                this.filenameField = value;
                this.RaisePropertyChanged("filename");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form=System.Xml.Schema.XmlSchemaForm.Qualified)]
        public string format {
            get {
                return this.formatField;
            }
            set {
                this.formatField = value;
                this.RaisePropertyChanged("format");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute(DataType="base64Binary")]
        public byte[] Value {
            get {
                return this.valueField;
            }
            set {
                this.valueField = value;
                this.RaisePropertyChanged("Value");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.oscre.org/ns/eReg-Final/2012/CorrespondenceV1_0")]
    public partial class GatewayResponseType : object, System.ComponentModel.INotifyPropertyChanged {
        
        private ProductResponseCodeContentType typeCodeField;
        
        private string applicationMessageIdField;
        
        private string externalReferenceField;
        
        private string aBRField;
        
        private AttachmentType correspondenceField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public ProductResponseCodeContentType TypeCode {
            get {
                return this.typeCodeField;
            }
            set {
                this.typeCodeField = value;
                this.RaisePropertyChanged("TypeCode");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string ApplicationMessageId {
            get {
                return this.applicationMessageIdField;
            }
            set {
                this.applicationMessageIdField = value;
                this.RaisePropertyChanged("ApplicationMessageId");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string ExternalReference {
            get {
                return this.externalReferenceField;
            }
            set {
                this.externalReferenceField = value;
                this.RaisePropertyChanged("ExternalReference");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string ABR {
            get {
                return this.aBRField;
            }
            set {
                this.aBRField = value;
                this.RaisePropertyChanged("ABR");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public AttachmentType Correspondence {
            get {
                return this.correspondenceField;
            }
            set {
                this.correspondenceField = value;
                this.RaisePropertyChanged("Correspondence");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.oscre.org/ns/eReg-Final/2012/CorrespondenceV1_0")]
    public enum ProductResponseCodeContentType {
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("31")]
        Item31,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.oscre.org/ns/eReg-Final/2012/CorrespondenceV1_0")]
    public partial class CorrespondenceV1_0Type : object, System.ComponentModel.INotifyPropertyChanged {
        
        private GatewayResponseType gatewayResponseField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public GatewayResponseType GatewayResponse {
            get {
                return this.gatewayResponseField;
            }
            set {
                this.gatewayResponseField = value;
                this.RaisePropertyChanged("GatewayResponse");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="getResponse", WrapperNamespace="http://poll.correspondencev2_1.ws.bg.lr.gov/", IsWrapped=true)]
    public partial class getResponseRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://poll.correspondencev2_1.ws.bg.lr.gov/", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public BusinessGatewayRepositories.CorrespondenceV1_0Type.PollRequestType arg0;
        
        public getResponseRequest() {
        }
        
        public getResponseRequest(BusinessGatewayRepositories.CorrespondenceV1_0Type.PollRequestType arg0) {
            this.arg0 = arg0;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="getResponseResponse", WrapperNamespace="http://poll.correspondencev2_1.ws.bg.lr.gov/", IsWrapped=true)]
    public partial class getResponseResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://poll.correspondencev2_1.ws.bg.lr.gov/", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public BusinessGatewayRepositories.CorrespondenceV1_0Type.CorrespondenceV1_0Type @return;
        
        public getResponseResponse() {
        }
        
        public getResponseResponse(BusinessGatewayRepositories.CorrespondenceV1_0Type.CorrespondenceV1_0Type @return) {
            this.@return = @return;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface CorrespondenceV2_1PollRequestServiceChannel : BusinessGatewayRepositories.CorrespondenceV1_0Type.CorrespondenceV2_1PollRequestService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CorrespondenceV2_1PollRequestServiceClient : System.ServiceModel.ClientBase<BusinessGatewayRepositories.CorrespondenceV1_0Type.CorrespondenceV2_1PollRequestService>, BusinessGatewayRepositories.CorrespondenceV1_0Type.CorrespondenceV2_1PollRequestService {
        
        public CorrespondenceV2_1PollRequestServiceClient() {
        }
        
        public CorrespondenceV2_1PollRequestServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public CorrespondenceV2_1PollRequestServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CorrespondenceV2_1PollRequestServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CorrespondenceV2_1PollRequestServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        BusinessGatewayRepositories.CorrespondenceV1_0Type.getResponseResponse BusinessGatewayRepositories.CorrespondenceV1_0Type.CorrespondenceV2_1PollRequestService.getResponse(BusinessGatewayRepositories.CorrespondenceV1_0Type.getResponseRequest request) {
            return base.Channel.getResponse(request);
        }
        
        public BusinessGatewayRepositories.CorrespondenceV1_0Type.CorrespondenceV1_0Type getResponse(BusinessGatewayRepositories.CorrespondenceV1_0Type.PollRequestType arg0) {
            BusinessGatewayRepositories.CorrespondenceV1_0Type.getResponseRequest inValue = new BusinessGatewayRepositories.CorrespondenceV1_0Type.getResponseRequest();
            inValue.arg0 = arg0;
            BusinessGatewayRepositories.CorrespondenceV1_0Type.getResponseResponse retVal = ((BusinessGatewayRepositories.CorrespondenceV1_0Type.CorrespondenceV2_1PollRequestService)(this)).getResponse(inValue);
            return retVal.@return;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<BusinessGatewayRepositories.CorrespondenceV1_0Type.getResponseResponse> BusinessGatewayRepositories.CorrespondenceV1_0Type.CorrespondenceV2_1PollRequestService.getResponseAsync(BusinessGatewayRepositories.CorrespondenceV1_0Type.getResponseRequest request) {
            return base.Channel.getResponseAsync(request);
        }
        
        public System.Threading.Tasks.Task<BusinessGatewayRepositories.CorrespondenceV1_0Type.getResponseResponse> getResponseAsync(BusinessGatewayRepositories.CorrespondenceV1_0Type.PollRequestType arg0) {
            BusinessGatewayRepositories.CorrespondenceV1_0Type.getResponseRequest inValue = new BusinessGatewayRepositories.CorrespondenceV1_0Type.getResponseRequest();
            inValue.arg0 = arg0;
            return ((BusinessGatewayRepositories.CorrespondenceV1_0Type.CorrespondenceV2_1PollRequestService)(this)).getResponseAsync(inValue);
        }
    }
}
