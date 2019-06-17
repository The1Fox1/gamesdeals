using Sabio.Data.Providers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Data
{
    public sealed class DataProvider
    {
        private DataProvider() { }

        public static IDataProvider Instance
        {
            get
            {
                return SqlDataProvider.Instance;
            }
        }

    }
}
