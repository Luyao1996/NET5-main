using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net5.Toolbox.SqlSugar
{
    public class ClientHelper
    {
        private static SqlSugarClient Client { get; set; }

        /// <summary>
        /// 初始化客户端帮助类
        /// </summary>
        public ClientHelper()
        {
            Client= new SqlClient().GetInstance();
        }

        /// <summary>
        /// 初始化客户端帮助类2
        /// </summary>
        public ClientHelper(SqlSugarClient _client)
        {
            Client = _client;
        }

        #region 事件

        /// <summary>
        /// 执行sql事件
        /// SQL执行前触发
        /// </summary>
        /// <param name="act">string:sql SugarParameter[]:sql执行中的参数</param>
        public static void OnLogExecuting(Action<string, SugarParameter[]> act)
        {
            var client = new SqlClient().GetInstance();
            //添加Sql打印事件，开发中可以删掉这个代码
            client.Aop.OnLogExecuting = (sql, pars) =>
            {
                act.Invoke(sql, pars);
            };
        }

        /// <summary>
        /// 执行sql事件
        /// SQL执行后前触发
        /// db.Ado.SqlExecutionTime.ToString() sql执行耗时
        /// </summary>
        /// <param name="act">string:sql SugarParameter[]:sql执行中的参数</param>
        public static void OnLogExecuted(Action<string, SugarParameter[]> act)
        {
            var client = new SqlClient().GetInstance();
            client.Aop.OnLogExecuted = (sql, pars) =>
            {
                act.Invoke(sql, pars);
            };
        }

        /// <summary>
        /// sql中事件
        /// SQL执行时前触发
        /// 可以修改sql 和 参数
        /// </summary>
        /// <param name="act">string:sql SugarParameter[]:sql执行中的参数 KeyValuePair<sql,参数></param>
        public static void OnExecutingChangeSql(Func<string, SugarParameter[], KeyValuePair<string, SugarParameter[]>> func)
        {
            var client = new SqlClient().GetInstance();
            //添加Sql打印事件，开发中可以删掉这个代码
            client.Aop.OnExecutingChangeSql = (sql, pars) =>
            {
                return func.Invoke(sql, pars);
            };
        }

        /// <summary>
        /// Sql执行完后会进该事件，该事件可以拿到更改前记录和更改后记录，执行时间等参数
        /// </summary>
        /// <param name="act"></param>
        public static void OnDiffLogEvent(Action<DiffLogModel> act)
        {
            var client = new SqlClient().GetInstance();
            client.Aop.OnDiffLogEvent = it =>
            {
                act(it);
            };
        }

        #endregion

        #region 过滤器

        /// <summary>
        /// 过滤指定的sql
        /// </summary>
        /// <param name="func"></param>
        public static void SqlFilter(Func<SqlFilterItem> func)
        {
            var client = new SqlClient().GetInstance();
            client.QueryFilter.Add(func.Invoke());
        }

        #endregion

        #region 客户端操作

        /// <summary>
        /// 自定义客户端
        /// </summary>
        /// <param name="act"></param>
        public void DebugSql(Action<SqlSugarClient> act)
        {
            act.Invoke(Client);
        }

        /// <summary>
        /// 释放客户端
        /// </summary>
        public void DisposeClient()
        {
            Client?.Dispose();
            Client = null;
        }

        #endregion
    }
}
