using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net5.Toolbox.SqlSugar
{
    public class SqlClient
    {
        private static SqlSugarClient  Client {get;set;}

        private static object ObjLock { get; set; }

        /// <summary>
        /// 获取数据库实例
        /// 单例模式
        /// </summary>
        /// <param name="exDb">从库 默认null</param>
        /// <returns></returns>
        public SqlSugarClient GetInstance(List<SlaveConnectionConfig> exDb = null)
        {
            if (Client == null)
            {
                lock (ObjLock)
                {
                    if (Client == null)
                    {
                        ConnectionConfig cc = new ConnectionConfig()
                        {
                            ConnectionString = Config.ConnectionString,
                            DbType = Config.dbType,
                            IsAutoCloseConnection = Config.IsAutoCloseConnection,
                            InitKeyType = Config._InitKeyType
                        };
                        if (exDb != null && exDb.Count > 0)
                        {
                            cc.SlaveConnectionConfigs = exDb;
                        }

                        Client = new SqlSugarClient(cc);
                    }
                }
            }

            return Client;
        }

        /// <summary>
        /// 释放之前的客户端并自定义客户端
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public SqlSugarClient DiyInstance(Func<ConnectionConfig> func)
        {
            Client?.Dispose();
            Client = null;
            Client = new SqlSugarClient(func.Invoke());
            return Client;
        }
    }
}
