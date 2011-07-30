using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Common.Services.Fax.Interfaces;
using Common.Types.Interfaces;
using Common.Types;
using System.Xml.Schema;

namespace Common.Services.Fax.Tests.Mocks
{
    [Export(typeof(FaxService))]
    public class MockFaxService : FaxService
    {

        protected override IResponseStatusType PerformTransmitRequest()
        {
            Message.TraceId = Guid.NewGuid().ToString();
            IResponseStatusType result = new ResponseStatusType();
            result.Description = "The fax has been sent to the server";
            result.StatusType = StatusTypes.Success;
            return result;
        }

        protected override IResponseStatusType PerformUpdateResponse()
        {
            IResponseStatusType result = new ResponseStatusType();
            result.Description = "The job completed";
            result.StatusType = StatusTypes.Success;
            return result;
        }
    }
}
