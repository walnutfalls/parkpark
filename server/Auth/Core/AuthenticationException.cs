namespace Auth.Core
{    
    public class AuthenticationException : System.Exception
    {
        public AuthenticationException() { }
        public AuthenticationException(string message) : base(message) { }
        public AuthenticationException(string message, System.Exception inner) : base(message, inner) { }
        protected AuthenticationException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}