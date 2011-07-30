using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Types;
using Common.Types.Interfaces;
using System.Xml.Schema;
namespace Common.Services.Fax.Interfaces
{
    public interface IFaxService:IValidatedType,IMessage<CreativeWork>
    {
        /// <summary>
        /// Gets or sets the fax job.
        /// </summary>
        /// <value>
        /// The fax job.
        /// </value>
        /// <remarks>
        /// Public property exposing the structure of a fax job. 
        /// </remarks>
        FaxJob FaxJob { get; set; }

        
    }
}
