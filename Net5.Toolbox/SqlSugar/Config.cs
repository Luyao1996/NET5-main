using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net5.Toolbox.SqlSugar
{
    public static class Config
    {
        /// <summary>
        /// 数据库链接字符串
        /// </summary>
        public static string ConnectionString { get; set; }

        /// <summary>
        /// 数据库类型 默认sqlserver
        /// </summary>
        public static DbType dbType { get; set; } = DbType.SqlServer;

        /// <summary>
        /// 是否自动关闭数据库链接  默认true
        /// </summary>
        public static bool IsAutoCloseConnection { get; set; } = true;

        /// <summary>
        /// 从哪儿读取主键自增信息  默认Attribute
        /// </summary>
        public static InitKeyType _InitKeyType { get; set; } = InitKeyType.Attribute;
    }
}
