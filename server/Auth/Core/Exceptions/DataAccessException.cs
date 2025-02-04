using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Auth.Core.Exceptions
{
	class DataAccessException : Exception
	{
		public DataAccessException()
		{
		}

		public DataAccessException(string message) : base(message)
		{
		}

		public DataAccessException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected DataAccessException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
