using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Types.Interfaces
{
    public interface IMessageService<T>
    {
        /// <summary>
        /// Transmits the request.
        /// </summary>
        IResponseStatusType Transmit();
        /// <summary>
        /// Updates the response.
        /// </summary>
        /// <remarks>
        /// Used to query the status of a message which may be a long running process involving periods of disconnected state        
        /// </remarks>
        IResponseStatusType QueryResponse();
        /// <summary>
        /// Gets the message status. This property provides a common status to interrogate as opposed to a ResponseStatusType which is more specific to 
        /// the implementation.
        /// </summary>
        MessageStatuses MessageStatus { get; }

        IMessage<T> Message { get; set; }
        /// <summary>
        /// Occurs when [transmitted]. This event is here to notify caller that it can call QueryResponse to get the outcome or status of a message
        /// this is NOT an indicator of success or failure, just that the attempt to transmit has completed. This will facillitate async operations
        event EventHandler Transmitted;
    }
}
