﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.18408
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace ZIT.AppRouteServer.AppServerAPI.CarStepManagerService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://ws.firstAid.com/", ConfigurationName="CarStepManagerService.CarStepManagerDelegate")]
    public interface CarStepManagerDelegate {
        
        // CODEGEN: 命名空间  的元素名称 arg0 以后生成的消息协定未标记为 nillable
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        ZIT.AppRouteServer.AppServerAPI.CarStepManagerService.addCarStepResponse addCarStep(ZIT.AppRouteServer.AppServerAPI.CarStepManagerService.addCarStepRequest request);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="", ReplyAction="*")]
        System.IAsyncResult BeginaddCarStep(ZIT.AppRouteServer.AppServerAPI.CarStepManagerService.addCarStepRequest request, System.AsyncCallback callback, object asyncState);
        
        ZIT.AppRouteServer.AppServerAPI.CarStepManagerService.addCarStepResponse EndaddCarStep(System.IAsyncResult result);
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18408")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "")]
    public partial class CredentialSoapHeader
    {
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string Username;

        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public string Password;
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class addCarStepRequest {

        [System.ServiceModel.MessageHeaderAttribute(Namespace = "")]
        public ZIT.AppRouteServer.AppServerAPI.CarStepManagerService.CredentialSoapHeader CredentialSoapHeader;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="addCarStep", Namespace="http://ws.firstAid.com/", Order=0)]
        public ZIT.AppRouteServer.AppServerAPI.CarStepManagerService.addCarStepRequestBody Body;
        
        public addCarStepRequest() {
        }

        public addCarStepRequest(ZIT.AppRouteServer.AppServerAPI.CarStepManagerService.CredentialSoapHeader CredentialSoapHeader, ZIT.AppRouteServer.AppServerAPI.CarStepManagerService.addCarStepRequestBody Body)
        {
            this.CredentialSoapHeader = CredentialSoapHeader;
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="")]
    public partial class addCarStepRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string arg0;
        
        public addCarStepRequestBody() {
        }
        
        public addCarStepRequestBody(string arg0) {
            this.arg0 = arg0;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class addCarStepResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="addCarStepResponse", Namespace="http://ws.firstAid.com/", Order=0)]
        public ZIT.AppRouteServer.AppServerAPI.CarStepManagerService.addCarStepResponseBody Body;
        
        public addCarStepResponse() {
        }
        
        public addCarStepResponse(ZIT.AppRouteServer.AppServerAPI.CarStepManagerService.addCarStepResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="")]
    public partial class addCarStepResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string @return;
        
        public addCarStepResponseBody() {
        }
        
        public addCarStepResponseBody(string @return) {
            this.@return = @return;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface CarStepManagerDelegateChannel : ZIT.AppRouteServer.AppServerAPI.CarStepManagerService.CarStepManagerDelegate, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class addCarStepCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public addCarStepCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public string Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CarStepManagerDelegateClient : System.ServiceModel.ClientBase<ZIT.AppRouteServer.AppServerAPI.CarStepManagerService.CarStepManagerDelegate>, ZIT.AppRouteServer.AppServerAPI.CarStepManagerService.CarStepManagerDelegate {
        
        private BeginOperationDelegate onBeginaddCarStepDelegate;
        
        private EndOperationDelegate onEndaddCarStepDelegate;
        
        private System.Threading.SendOrPostCallback onaddCarStepCompletedDelegate;
        
        public CarStepManagerDelegateClient() {
        }
        
        public CarStepManagerDelegateClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public CarStepManagerDelegateClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CarStepManagerDelegateClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CarStepManagerDelegateClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public event System.EventHandler<addCarStepCompletedEventArgs> addCarStepCompleted;
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        ZIT.AppRouteServer.AppServerAPI.CarStepManagerService.addCarStepResponse ZIT.AppRouteServer.AppServerAPI.CarStepManagerService.CarStepManagerDelegate.addCarStep(ZIT.AppRouteServer.AppServerAPI.CarStepManagerService.addCarStepRequest request) {
            return base.Channel.addCarStep(request);
        }

        public string addCarStep(ZIT.AppRouteServer.AppServerAPI.CarStepManagerService.CredentialSoapHeader CredentialSoapHeader, string arg0)
        {
            ZIT.AppRouteServer.AppServerAPI.CarStepManagerService.addCarStepRequest inValue = new ZIT.AppRouteServer.AppServerAPI.CarStepManagerService.addCarStepRequest();
            inValue.Body = new ZIT.AppRouteServer.AppServerAPI.CarStepManagerService.addCarStepRequestBody();
            inValue.CredentialSoapHeader = CredentialSoapHeader;
            inValue.Body.arg0 = arg0;
            ZIT.AppRouteServer.AppServerAPI.CarStepManagerService.addCarStepResponse retVal = ((ZIT.AppRouteServer.AppServerAPI.CarStepManagerService.CarStepManagerDelegate)(this)).addCarStep(inValue);
            return retVal.Body.@return;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult ZIT.AppRouteServer.AppServerAPI.CarStepManagerService.CarStepManagerDelegate.BeginaddCarStep(ZIT.AppRouteServer.AppServerAPI.CarStepManagerService.addCarStepRequest request, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginaddCarStep(request, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BeginaddCarStep(ZIT.AppRouteServer.AppServerAPI.CarStepManagerService.CredentialSoapHeader CredentialSoapHeader, string arg0, System.AsyncCallback callback, object asyncState)
        {
            ZIT.AppRouteServer.AppServerAPI.CarStepManagerService.addCarStepRequest inValue = new ZIT.AppRouteServer.AppServerAPI.CarStepManagerService.addCarStepRequest();
            inValue.Body = new ZIT.AppRouteServer.AppServerAPI.CarStepManagerService.addCarStepRequestBody();
            inValue.CredentialSoapHeader = CredentialSoapHeader;
            inValue.Body.arg0 = arg0;
            return ((ZIT.AppRouteServer.AppServerAPI.CarStepManagerService.CarStepManagerDelegate)(this)).BeginaddCarStep(inValue, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        ZIT.AppRouteServer.AppServerAPI.CarStepManagerService.addCarStepResponse ZIT.AppRouteServer.AppServerAPI.CarStepManagerService.CarStepManagerDelegate.EndaddCarStep(System.IAsyncResult result) {
            return base.Channel.EndaddCarStep(result);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public string EndaddCarStep(System.IAsyncResult result) {
            ZIT.AppRouteServer.AppServerAPI.CarStepManagerService.addCarStepResponse retVal = ((ZIT.AppRouteServer.AppServerAPI.CarStepManagerService.CarStepManagerDelegate)(this)).EndaddCarStep(result);
            return retVal.Body.@return;
        }
        
        private System.IAsyncResult OnBeginaddCarStep(object[] inValues, System.AsyncCallback callback, object asyncState) {
            ZIT.AppRouteServer.AppServerAPI.CarStepManagerService.CredentialSoapHeader CredentialSoapHeader = ((ZIT.AppRouteServer.AppServerAPI.CarStepManagerService.CredentialSoapHeader)(inValues[0]));
            string arg0 = ((string)(inValues[1]));
            return this.BeginaddCarStep(CredentialSoapHeader, arg0, callback, asyncState);
        }
        
        private object[] OnEndaddCarStep(System.IAsyncResult result) {
            string retVal = this.EndaddCarStep(result);
            return new object[] {
                    retVal};
        }
        
        private void OnaddCarStepCompleted(object state) {
            if ((this.addCarStepCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.addCarStepCompleted(this, new addCarStepCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }

        public void addCarStepAsync(ZIT.AppRouteServer.AppServerAPI.CarStepManagerService.CredentialSoapHeader CredentialSoapHeader, string arg0)
        {
            this.addCarStepAsync(CredentialSoapHeader, arg0, null);
        }

        public void addCarStepAsync(ZIT.AppRouteServer.AppServerAPI.CarStepManagerService.CredentialSoapHeader CredentialSoapHeader, string arg0, object userState)
        {
            if ((this.onBeginaddCarStepDelegate == null)) {
                this.onBeginaddCarStepDelegate = new BeginOperationDelegate(this.OnBeginaddCarStep);
            }
            if ((this.onEndaddCarStepDelegate == null)) {
                this.onEndaddCarStepDelegate = new EndOperationDelegate(this.OnEndaddCarStep);
            }
            if ((this.onaddCarStepCompletedDelegate == null)) {
                this.onaddCarStepCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnaddCarStepCompleted);
            }
            base.InvokeAsync(this.onBeginaddCarStepDelegate, new object[] { 
                       CredentialSoapHeader, 
                        arg0}, this.onEndaddCarStepDelegate, this.onaddCarStepCompletedDelegate, userState);
        }
    }
}
