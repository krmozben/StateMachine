﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Boyner.ReturnSave
{
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://boynerortak.com/MASTERDATA/EKOL", ConfigurationName="Boyner.ReturnSave.SI_ECOM_EKOL_WSC_GOODS_IN_PHY_RECEIVED_OUT_SYN")]
    public interface SI_ECOM_EKOL_WSC_GOODS_IN_PHY_RECEIVED_OUT_SYN
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://sap.com/xi/WebService/soap1.1", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<Boyner.ReturnSave.SI_ECOM_EKOL_WSC_GOODS_IN_PHY_RECEIVED_OUT_SYNResponse> SI_ECOM_EKOL_WSC_GOODS_IN_PHY_RECEIVED_OUT_SYNAsync(Boyner.ReturnSave.SI_ECOM_EKOL_WSC_GOODS_IN_PHY_RECEIVED_OUT_SYNRequest request);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://boynerortak.com/MASTERDATA/EKOL")]
    public partial class DT_3RD_EKOL_WSC_GOODS_IN_PHY_RECEIVED_REQ
    {
        
        private DT_3RD_EKOL_WSC_GOODS_IN_PHY_RECEIVED_REQWSC_GOODS_IN_PHY_RECEIVEDInput wSC_GOODS_IN_PHY_RECEIVEDInputField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public DT_3RD_EKOL_WSC_GOODS_IN_PHY_RECEIVED_REQWSC_GOODS_IN_PHY_RECEIVEDInput WSC_GOODS_IN_PHY_RECEIVEDInput
        {
            get
            {
                return this.wSC_GOODS_IN_PHY_RECEIVEDInputField;
            }
            set
            {
                this.wSC_GOODS_IN_PHY_RECEIVEDInputField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://boynerortak.com/MASTERDATA/EKOL")]
    public partial class DT_3RD_EKOL_WSC_GOODS_IN_PHY_RECEIVED_REQWSC_GOODS_IN_PHY_RECEIVEDInput
    {
        
        private DT_3RD_EKOL_WSC_GOODS_IN_PHY_RECEIVED_REQWSC_GOODS_IN_PHY_RECEIVEDInputI_HEADER_WSC_STATUS_W_CIN i_HEADER_WSC_STATUS_W_CINField;
        
        private string o_RESULT_WSC_RESULT_W_COUTField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public DT_3RD_EKOL_WSC_GOODS_IN_PHY_RECEIVED_REQWSC_GOODS_IN_PHY_RECEIVEDInputI_HEADER_WSC_STATUS_W_CIN I_HEADER_WSC_STATUS_W_CIN
        {
            get
            {
                return this.i_HEADER_WSC_STATUS_W_CINField;
            }
            set
            {
                this.i_HEADER_WSC_STATUS_W_CINField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        public string O_RESULT_WSC_RESULT_W_COUT
        {
            get
            {
                return this.o_RESULT_WSC_RESULT_W_COUTField;
            }
            set
            {
                this.o_RESULT_WSC_RESULT_W_COUTField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://boynerortak.com/MASTERDATA/EKOL")]
    public partial class DT_3RD_EKOL_WSC_GOODS_IN_PHY_RECEIVED_REQWSC_GOODS_IN_PHY_RECEIVEDInputI_HEADER_WSC_STATUS_W_CIN
    {
        
        private DT_3RD_EKOL_WSC_GOODS_IN_PHY_RECEIVED_REQWSC_GOODS_IN_PHY_RECEIVEDInputI_HEADER_WSC_STATUS_W_CINWSC_STATUS_W wSC_STATUS_WField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public DT_3RD_EKOL_WSC_GOODS_IN_PHY_RECEIVED_REQWSC_GOODS_IN_PHY_RECEIVEDInputI_HEADER_WSC_STATUS_W_CINWSC_STATUS_W WSC_STATUS_W
        {
            get
            {
                return this.wSC_STATUS_WField;
            }
            set
            {
                this.wSC_STATUS_WField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://boynerortak.com/MASTERDATA/EKOL")]
    public partial class DT_3RD_EKOL_WSC_GOODS_IN_PHY_RECEIVED_REQWSC_GOODS_IN_PHY_RECEIVEDInputI_HEADER_WSC_STATUS_W_CINWSC_STATUS_W
    {
        
        private DT_3RD_EKOL_WSC_GOODS_IN_PHY_RECEIVED_REQWSC_GOODS_IN_PHY_RECEIVEDInputI_HEADER_WSC_STATUS_W_CINWSC_STATUS_WWSC_STATUS_TYPE[] rECORDSField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        [System.Xml.Serialization.XmlArrayItemAttribute("WSC_STATUS_TYPE", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)]
        public DT_3RD_EKOL_WSC_GOODS_IN_PHY_RECEIVED_REQWSC_GOODS_IN_PHY_RECEIVEDInputI_HEADER_WSC_STATUS_W_CINWSC_STATUS_WWSC_STATUS_TYPE[] RECORDS
        {
            get
            {
                return this.rECORDSField;
            }
            set
            {
                this.rECORDSField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://boynerortak.com/MASTERDATA/EKOL")]
    public partial class DT_3RD_EKOL_WSC_GOODS_IN_PHY_RECEIVED_REQWSC_GOODS_IN_PHY_RECEIVEDInputI_HEADER_WSC_STATUS_W_CINWSC_STATUS_WWSC_STATUS_TYPE
    {
        
        private string oRG_CODEField;
        
        private string rECORD_NOField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public string ORG_CODE
        {
            get
            {
                return this.oRG_CODEField;
            }
            set
            {
                this.oRG_CODEField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        public string RECORD_NO
        {
            get
            {
                return this.rECORD_NOField;
            }
            set
            {
                this.rECORD_NOField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://boynerortak.com/MASTERDATA/EKOL")]
    public partial class DT_3RD_EKOL_WSC_GOODS_IN_PHY_RECEIVED_RES
    {
        
        private DT_3RD_EKOL_WSC_GOODS_IN_PHY_RECEIVED_RESWSC_GOODS_IN_PHY_RECEIVEDOutput wSC_GOODS_IN_PHY_RECEIVEDOutputField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public DT_3RD_EKOL_WSC_GOODS_IN_PHY_RECEIVED_RESWSC_GOODS_IN_PHY_RECEIVEDOutput WSC_GOODS_IN_PHY_RECEIVEDOutput
        {
            get
            {
                return this.wSC_GOODS_IN_PHY_RECEIVEDOutputField;
            }
            set
            {
                this.wSC_GOODS_IN_PHY_RECEIVEDOutputField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://boynerortak.com/MASTERDATA/EKOL")]
    public partial class DT_3RD_EKOL_WSC_GOODS_IN_PHY_RECEIVED_RESWSC_GOODS_IN_PHY_RECEIVEDOutput
    {
        
        private DT_3RD_EKOL_WSC_GOODS_IN_PHY_RECEIVED_RESWSC_GOODS_IN_PHY_RECEIVEDOutputO_RESULT o_RESULTField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public DT_3RD_EKOL_WSC_GOODS_IN_PHY_RECEIVED_RESWSC_GOODS_IN_PHY_RECEIVEDOutputO_RESULT O_RESULT
        {
            get
            {
                return this.o_RESULTField;
            }
            set
            {
                this.o_RESULTField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://boynerortak.com/MASTERDATA/EKOL")]
    public partial class DT_3RD_EKOL_WSC_GOODS_IN_PHY_RECEIVED_RESWSC_GOODS_IN_PHY_RECEIVEDOutputO_RESULT
    {
        
        private DT_3RD_EKOL_WSC_GOODS_IN_PHY_RECEIVED_RESWSC_GOODS_IN_PHY_RECEIVEDOutputO_RESULTWSC_RESULT_W wSC_RESULT_WField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public DT_3RD_EKOL_WSC_GOODS_IN_PHY_RECEIVED_RESWSC_GOODS_IN_PHY_RECEIVEDOutputO_RESULTWSC_RESULT_W WSC_RESULT_W
        {
            get
            {
                return this.wSC_RESULT_WField;
            }
            set
            {
                this.wSC_RESULT_WField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://boynerortak.com/MASTERDATA/EKOL")]
    public partial class DT_3RD_EKOL_WSC_GOODS_IN_PHY_RECEIVED_RESWSC_GOODS_IN_PHY_RECEIVEDOutputO_RESULTWSC_RESULT_W
    {
        
        private DT_3RD_EKOL_WSC_GOODS_IN_PHY_RECEIVED_RESWSC_GOODS_IN_PHY_RECEIVEDOutputO_RESULTWSC_RESULT_WWSC_RESULT_TYPE[] rSETField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        [System.Xml.Serialization.XmlArrayItemAttribute("WSC_RESULT_TYPE", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)]
        public DT_3RD_EKOL_WSC_GOODS_IN_PHY_RECEIVED_RESWSC_GOODS_IN_PHY_RECEIVEDOutputO_RESULTWSC_RESULT_WWSC_RESULT_TYPE[] RSET
        {
            get
            {
                return this.rSETField;
            }
            set
            {
                this.rSETField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://boynerortak.com/MASTERDATA/EKOL")]
    public partial class DT_3RD_EKOL_WSC_GOODS_IN_PHY_RECEIVED_RESWSC_GOODS_IN_PHY_RECEIVEDOutputO_RESULTWSC_RESULT_WWSC_RESULT_TYPE
    {
        
        private string oRG_CODEField;
        
        private string kEY_VALUEField;
        
        private string rESULTField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public string ORG_CODE
        {
            get
            {
                return this.oRG_CODEField;
            }
            set
            {
                this.oRG_CODEField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        public string KEY_VALUE
        {
            get
            {
                return this.kEY_VALUEField;
            }
            set
            {
                this.kEY_VALUEField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=2)]
        public string RESULT
        {
            get
            {
                return this.rESULTField;
            }
            set
            {
                this.rESULTField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class SI_ECOM_EKOL_WSC_GOODS_IN_PHY_RECEIVED_OUT_SYNRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://boynerortak.com/MASTERDATA/EKOL", Order=0)]
        public Boyner.ReturnSave.DT_3RD_EKOL_WSC_GOODS_IN_PHY_RECEIVED_REQ MT_3RD_EKOL_WSC_GOODS_IN_PHY_RECEIVED_REQ;
        
        public SI_ECOM_EKOL_WSC_GOODS_IN_PHY_RECEIVED_OUT_SYNRequest()
        {
        }
        
        public SI_ECOM_EKOL_WSC_GOODS_IN_PHY_RECEIVED_OUT_SYNRequest(Boyner.ReturnSave.DT_3RD_EKOL_WSC_GOODS_IN_PHY_RECEIVED_REQ MT_3RD_EKOL_WSC_GOODS_IN_PHY_RECEIVED_REQ)
        {
            this.MT_3RD_EKOL_WSC_GOODS_IN_PHY_RECEIVED_REQ = MT_3RD_EKOL_WSC_GOODS_IN_PHY_RECEIVED_REQ;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class SI_ECOM_EKOL_WSC_GOODS_IN_PHY_RECEIVED_OUT_SYNResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://boynerortak.com/MASTERDATA/EKOL", Order=0)]
        public Boyner.ReturnSave.DT_3RD_EKOL_WSC_GOODS_IN_PHY_RECEIVED_RES MT_3RD_EKOL_WSC_GOODS_IN_PHY_RECEIVED_RES;
        
        public SI_ECOM_EKOL_WSC_GOODS_IN_PHY_RECEIVED_OUT_SYNResponse()
        {
        }
        
        public SI_ECOM_EKOL_WSC_GOODS_IN_PHY_RECEIVED_OUT_SYNResponse(Boyner.ReturnSave.DT_3RD_EKOL_WSC_GOODS_IN_PHY_RECEIVED_RES MT_3RD_EKOL_WSC_GOODS_IN_PHY_RECEIVED_RES)
        {
            this.MT_3RD_EKOL_WSC_GOODS_IN_PHY_RECEIVED_RES = MT_3RD_EKOL_WSC_GOODS_IN_PHY_RECEIVED_RES;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    public interface SI_ECOM_EKOL_WSC_GOODS_IN_PHY_RECEIVED_OUT_SYNChannel : Boyner.ReturnSave.SI_ECOM_EKOL_WSC_GOODS_IN_PHY_RECEIVED_OUT_SYN, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    public partial class SI_ECOM_EKOL_WSC_GOODS_IN_PHY_RECEIVED_OUT_SYNClient : System.ServiceModel.ClientBase<Boyner.ReturnSave.SI_ECOM_EKOL_WSC_GOODS_IN_PHY_RECEIVED_OUT_SYN>, Boyner.ReturnSave.SI_ECOM_EKOL_WSC_GOODS_IN_PHY_RECEIVED_OUT_SYN
    {
        
        /// <summary>
        /// Implement this partial method to configure the service endpoint.
        /// </summary>
        /// <param name="serviceEndpoint">The endpoint to configure</param>
        /// <param name="clientCredentials">The client credentials</param>
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public SI_ECOM_EKOL_WSC_GOODS_IN_PHY_RECEIVED_OUT_SYNClient(EndpointConfiguration endpointConfiguration) : 
                base(SI_ECOM_EKOL_WSC_GOODS_IN_PHY_RECEIVED_OUT_SYNClient.GetBindingForEndpoint(endpointConfiguration), SI_ECOM_EKOL_WSC_GOODS_IN_PHY_RECEIVED_OUT_SYNClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public SI_ECOM_EKOL_WSC_GOODS_IN_PHY_RECEIVED_OUT_SYNClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(SI_ECOM_EKOL_WSC_GOODS_IN_PHY_RECEIVED_OUT_SYNClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public SI_ECOM_EKOL_WSC_GOODS_IN_PHY_RECEIVED_OUT_SYNClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(SI_ECOM_EKOL_WSC_GOODS_IN_PHY_RECEIVED_OUT_SYNClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public SI_ECOM_EKOL_WSC_GOODS_IN_PHY_RECEIVED_OUT_SYNClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<Boyner.ReturnSave.SI_ECOM_EKOL_WSC_GOODS_IN_PHY_RECEIVED_OUT_SYNResponse> Boyner.ReturnSave.SI_ECOM_EKOL_WSC_GOODS_IN_PHY_RECEIVED_OUT_SYN.SI_ECOM_EKOL_WSC_GOODS_IN_PHY_RECEIVED_OUT_SYNAsync(Boyner.ReturnSave.SI_ECOM_EKOL_WSC_GOODS_IN_PHY_RECEIVED_OUT_SYNRequest request)
        {
            return base.Channel.SI_ECOM_EKOL_WSC_GOODS_IN_PHY_RECEIVED_OUT_SYNAsync(request);
        }
        
        public System.Threading.Tasks.Task<Boyner.ReturnSave.SI_ECOM_EKOL_WSC_GOODS_IN_PHY_RECEIVED_OUT_SYNResponse> SI_ECOM_EKOL_WSC_GOODS_IN_PHY_RECEIVED_OUT_SYNAsync(Boyner.ReturnSave.DT_3RD_EKOL_WSC_GOODS_IN_PHY_RECEIVED_REQ MT_3RD_EKOL_WSC_GOODS_IN_PHY_RECEIVED_REQ)
        {
            Boyner.ReturnSave.SI_ECOM_EKOL_WSC_GOODS_IN_PHY_RECEIVED_OUT_SYNRequest inValue = new Boyner.ReturnSave.SI_ECOM_EKOL_WSC_GOODS_IN_PHY_RECEIVED_OUT_SYNRequest();
            inValue.MT_3RD_EKOL_WSC_GOODS_IN_PHY_RECEIVED_REQ = MT_3RD_EKOL_WSC_GOODS_IN_PHY_RECEIVED_REQ;
            return ((Boyner.ReturnSave.SI_ECOM_EKOL_WSC_GOODS_IN_PHY_RECEIVED_OUT_SYN)(this)).SI_ECOM_EKOL_WSC_GOODS_IN_PHY_RECEIVED_OUT_SYNAsync(inValue);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.HTTP_Port))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                return result;
            }
            if ((endpointConfiguration == EndpointConfiguration.HTTPS_Port))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                result.Security.Mode = System.ServiceModel.BasicHttpSecurityMode.Transport;
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.HTTP_Port))
            {
                return new System.ServiceModel.EndpointAddress(@"http://bbmpoappdev:50000/XISOAPAdapter/MessageServlet?senderParty=BBM&senderService=BC_3RD_BBM_ECOM&receiverParty=&receiverService=&interface=SI_ECOM_EKOL_WSC_GOODS_IN_PHY_RECEIVED_OUT_SYN&interfaceNamespace=http%3A%2F%2Fboynerortak.com%2FMASTERDATA%2FEKOL");
            }
            if ((endpointConfiguration == EndpointConfiguration.HTTPS_Port))
            {
                return new System.ServiceModel.EndpointAddress(@"https://bbmpoappdev:50001/XISOAPAdapter/MessageServlet?senderParty=BBM&senderService=BC_3RD_BBM_ECOM&receiverParty=&receiverService=&interface=SI_ECOM_EKOL_WSC_GOODS_IN_PHY_RECEIVED_OUT_SYN&interfaceNamespace=http%3A%2F%2Fboynerortak.com%2FMASTERDATA%2FEKOL");
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        public enum EndpointConfiguration
        {
            
            HTTP_Port,
            
            HTTPS_Port,
        }
    }
}
