using System;
using System.Linq;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Auth.Core.Interfaces;

namespace Auth.Core
{
    public class Password 
    {
        private const int Iterations = 5000;
        private const int HashBytes = 32;

        private string _hash;
        private Salt _salt;        
        private string _originalForm;        

        public Password(Password other)
        {
            _hash = other._hash;
            _salt = other._salt;
            _originalForm = other._originalForm;
            ProtectedForm = other.ProtectedForm;
        }

        public Password(string originalForm, Salt salt)
        {
            _originalForm = originalForm;
            _salt = salt;
            
            BuildProtectedForm();
        }

        public Password(byte[] protectedForm, Salt salt)
        {
            ProtectedForm = protectedForm;
            _salt = salt;
        }

        public byte[] ProtectedForm { get; private set; }


        public bool Check(string originalForm)
        {
            Password otherPw = new Password(originalForm, _salt);
            return ProtectedForm.SequenceEqual(otherPw.ProtectedForm);
        }

        private void BuildProtectedForm()
        {
            if (string.IsNullOrWhiteSpace(_originalForm))
            {
                throw new AuthenticationException("Can't build password without original form.");
            }

            ProtectedForm = KeyDerivation.Pbkdf2(
               password: _originalForm,
               salt: _salt.Data,
               prf: KeyDerivationPrf.HMACSHA1,
               iterationCount: Iterations,
               numBytesRequested: HashBytes
           );
        }        
    }
}