// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime;
using System.ServiceModel.Channels;
using System.ServiceModel.Diagnostics;
using System.Runtime.Diagnostics;

namespace System.ServiceModel.Dispatcher
{
    internal struct ProxyRpc
    {
        internal readonly string Action;
        internal ServiceModelActivity Activity;
        internal Guid ActivityId;
        internal readonly ServiceChannel Channel;
        internal object[] Correlation;
        internal readonly object[] InputParameters;
        internal readonly ProxyOperationRuntime Operation;
        internal object[] OutputParameters;
        internal Message Request;
        internal Message Reply;
        internal object ReturnValue;
        internal MessageVersion MessageVersion;
        internal readonly TimeoutHelper TimeoutHelper;
        private EventTraceActivity _eventTraceActivity;

        internal ProxyRpc(ServiceChannel channel, ProxyOperationRuntime operation, string action, object[] inputs, TimeSpan timeout)
        {
            this.Action = action;
            this.Activity = null;
            _eventTraceActivity = null;
            this.Channel = channel;
            this.Correlation = EmptyArray<object>.Allocate(operation.Parent.CorrelationCount);
            this.InputParameters = inputs;
            this.Operation = operation;
            this.OutputParameters = null;
            this.Request = null;
            this.Reply = null;
            this.ActivityId = Guid.Empty;
            this.ReturnValue = null;
            this.MessageVersion = channel.MessageVersion;
            this.TimeoutHelper = new TimeoutHelper(timeout);
        }

        internal EventTraceActivity EventTraceActivity
        {
            get
            {
                if (_eventTraceActivity == null)
                {
                    _eventTraceActivity = new EventTraceActivity();
                }
                return _eventTraceActivity;
            }

            set
            {
                _eventTraceActivity = value;
            }
        }
    }
}
