using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace ICP.WF.Activities
{
    [Serializable, SuppressMessage("Microsoft.Design", "CA1064:ExceptionsShouldBePublic")]
    internal class RuleSyntaxException : SystemException
    {
        private int errorNumber;
        private int position;

        internal RuleSyntaxException()
        {
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        private RuleSyntaxException(SerializationInfo serializeInfo, StreamingContext context)
            : base(serializeInfo, context)
        {
        }

        internal RuleSyntaxException(int errorNumber, string message, int position)
            : base(message)
        {
            this.errorNumber = errorNumber;
            this.position = position;
        }

        internal int ErrorNumber
        {
            get
            {
                return this.errorNumber;
            }
        }

        internal int Position
        {
            get
            {
                return this.position;
            }
        }
    }
}
