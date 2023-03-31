using Autofac;
using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace Study402Online.ContentService.Api
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // 为 Dapper 注册 DbConnection
            builder.Register<DbConnection>(context =>
            {
                var conf = context.Resolve<IConfiguration>();
                var connectionString = conf.GetConnectionString("default");
                var sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();

                return sqlConnection;
            });
        }
    }
}
