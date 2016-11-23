using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlCommander
{
    interface IRegulation
    {
        string GetCreateTableCommand(string line);
        string GetInsertTableCommand(string line);
        string TableName { get; }
    }
}
