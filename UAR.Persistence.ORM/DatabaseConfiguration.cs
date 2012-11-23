using System;
using System.Data.EntityClient;

using UAR.Persistence.Contracts;

namespace UAR.Persistence.ORM
{
    public class DatabaseConfiguration : IConfigureDatabase
    {
        public string EntityConnectionString(Type contextType)
        {
            if (contextType == null)
                throw new ArgumentNullException("contextType");

            var contextName = contextType.Name;

            if (String.IsNullOrWhiteSpace(contextName))
                throw new ArgumentOutOfRangeException("contextType");


            var modelName = ApplyModelNamingConvention(contextName);

            return String.Format(@"metadata=res://*/{0}.csdl|res://*/{0}.ssdl|res://*/{0}.msl;provider=System.Data.SqlClient;provider connection string=""data source=.\comwork;initial catalog=AdventureWorks;integrated security=True;multipleactiveresultsets=True;App=EntityFramework""", modelName);

            
            //var entityBuilder = new EntityConnectionStringBuilder
            //{
            //    Provider = "System.Data.SqlClient",
            //    ProviderConnectionString = "hello world",
            //    Metadata = "hello world"
            //};
            //return entityBuilder.ConnectionString;
        }

        static string ApplyModelNamingConvention(string contextName)
        {
            return contextName.Replace("DbContext", "Model");
        }
    }
}