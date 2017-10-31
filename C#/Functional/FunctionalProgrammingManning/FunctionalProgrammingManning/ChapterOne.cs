using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace FunctionalProgrammingManning
{
    public static class ChapterOne
    {
        // 1.4 Higher-order functions

        //// 1.4.1 Functions that depend on other functions
        public static IEnumerable<T> Where<T>(this IEnumerable<T> ts, Func<T, bool> predicate)
        {
            foreach (T t in ts)
            {
                if (predicate(t))
                    yield return t;
            }
        }

        //// 1.4.2 Adapter functions
        private static readonly Func<int, int, int> divide = (x, y) => x / y;

        private static Func<T2, T1, TR> SwapArgs<T1, T2, TR>(this Func<T1, T2, TR> f)
            => (t2, t1) => f(t1, t2);

        public static int SwapDivide(int x, int y)
        {
            var divideBy = divide.SwapArgs();
            return divideBy(x, y);
        }

        //// 1.4.3 Functions that create other functions
        private static Func<int, bool> IsMod(int n) => i => i % n == 0;

        public static IEnumerable<int> GetMod(int x, int y, int mod)
        {
            return Enumerable.Range(x, y).Where(IsMod(mod));
        }
    }

    // 1.5 Using HOFs to avoid duplication

    //// 1.5.1 Encapsulating setup and teardown into a HOF
    public static class ConnectionHelper
    {
        public static TR Connect<TR>(string connString, Func<IDbConnection, TR> f)
        {
            using (var conn = new SqlConnection(connString))
            {
                conn.Open();
                return f(conn);
            }
        }
    }

    public class DbLogger
    {
        private readonly string connString;

        public DbLogger(string connString)
        {
            this.connString = connString;
        }

        public void Log(LogMessage message) => ConnectionHelper.Connect(connString,
            c => c.Execute("sp_create_log", message, commandType: CommandType.StoredProcedure));

        public IEnumerable<LogMessage> GetLogs(DateTime since) => ConnectionHelper.Connect(connString,
            c => c.Query<LogMessage>(@"SELECT * FROM [Logs] WHERE [Timestamp] > @since", new {since}));
    }

    // Temp type for above methods
    public class LogMessage
    {
    }

    //// 1.5.2 Turning the using statement into a HOF
    public static class UsingHof
    {
        public static TR Using<TDisp, TR>(TDisp disposable, Func<TDisp, TR> f) where TDisp : IDisposable
        {
            using (disposable) return f(disposable);
        }

        // More succinct ConnectionHelper Connect function
        public static TR Connect<TR>(string connStr, Func<IDbConnection, TR> f) => Using(new SqlConnection(connStr),
            conn =>
            {
                conn.Open();
                return f(conn);
            });

        // Exercise 5 Attempt
        public static TR Using<TR>(Func<IDisposable> disposable, Func<Func<IDisposable>, TR> f)
        {
            using (disposable.Invoke())
            {
                return f(disposable);
            }
        }
    }
}