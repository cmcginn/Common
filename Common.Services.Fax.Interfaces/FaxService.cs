using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Schema;
using System.Diagnostics.Contracts;
using Common.Types;
using Common.Utils.Extensions;
using Common.Types.Interfaces;
namespace Common.Services.Fax.Interfaces
{
    /// <summary>
    /// An abstract class that performs uniform tasks and allows concrete FaxService to implement transmit and update methods
    /// </summary>
    ///<remarks>
    ///This is here to insure that validation is perfromed on all concrete instances before attempting any process, and to force proper 
    ///processing sequence that is uniform and well known  to service
    /// </remarks>


    public abstract class FaxService : IMessageService<IFaxMessage>
    {

        #region Abstract IFaxService Members
        protected abstract IResponseStatusType PerformTransmitRequest();
        protected abstract IResponseStatusType PerformUpdateResponse();
        #endregion     


        #region IMessageService Implementation
        public event EventHandler Transmitted;
        protected virtual void OnTransmitted()
        {
            if (null != Transmitted)
                Transmitted(this, EventArgs.Empty);
        }
        public MessageStatuses MessageStatus
        {
            get;
            protected set;
        }

        public IMessage<IFaxMessage> Message { get; set; }
        #endregion


        public IResponseStatusType Transmit()
        {
            Contract.Ensures(!String.IsNullOrWhiteSpace(Message.TraceId));
            return this.PerformTransmitRequest();
        }

        public IResponseStatusType QueryResponse()
        {
            Contract.Requires(!String.IsNullOrWhiteSpace(Message.TraceId));
            //TODO:Validation on update
            return this.PerformUpdateResponse();
        }
    }
}
