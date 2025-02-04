using System;

namespace Auth.Db
{
    public class Environment
    {
        public enum EnvironmentType
        {
            Development,
            Staging,
            Release
        }
        

        public Environment()
        {
            var env = System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
                ?? "Development";
                
            Type = (EnvironmentType) Enum.Parse(typeof(EnvironmentType), env);
            Console.WriteLine(env);
        }

        public EnvironmentType Type { get; private set; }
    }
}