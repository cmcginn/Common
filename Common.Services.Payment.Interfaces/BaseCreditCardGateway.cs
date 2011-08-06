//**************************************************************
//
// MoneyBaby Project - Open source payment processors for .NET
//
// Copyright 2007-2008 Marcus McConnell and BV Software
// www.CodePlex.com/MoneyBaby
//**************************************************************

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.Unity;
namespace Common.Services.Payment.Interfaces
{
    /// <summary>
    /// Base implementation of the ICreditCardGateway interface
    /// </summary>
    public abstract class BaseCreditCardGateway : ICreditCardGateway
    {
        // generate a unique ID for your processor for this method
        public abstract Guid GatewayId { get;}
        // a user friendly display name for your gateway
        public abstract string GatewayName { get; }


        // Use these properties to help out your users. Hide buttons
        // that your gateway doesn't support. No need to display a VOID
        // button to the user if they can't ever get a successful response.
        public abstract bool SupportsAuthorize { get; }
        public abstract bool SupportsCapture { get; }
        public abstract bool SupportsCharge { get; }
        public abstract bool SupportsRefund { get; }
        public abstract bool SupportsVoid { get; }
        public abstract bool SupportsProfile { get;}

        // These methods do the real work of communicating with 
        // outside services based on the 
        public abstract bool Authorize(IPaymentData data);
        public abstract bool Capture(IPaymentData data);
        public abstract bool Charge(IPaymentData data);
        public abstract bool Refund(IPaymentData data);
        public abstract bool Void(IPaymentData data);


        [Dependency]
        public IPaymentGatewaySettings GatewaySettings { get; set; }
       
    }
}
