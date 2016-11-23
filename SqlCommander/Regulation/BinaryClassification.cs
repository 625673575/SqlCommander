using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace SqlCommander.Regulation
{
    public class BinaryClassification : IRegulation
    {
        public string TableName
        {
            get
            {
                return "binary_classification";
            }
        }

        public string GetCreateTableCommand(string line)
        {
            string[] sArray = line.Split();
            int length = sArray.Length;
            StringBuilder sb = new StringBuilder();
            sb.Append("Create Table ");
            sb.Append(TableName);
            sb.Append(" ");
            sb.Append("(");
            sb.Append("UID INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,");
            sb.Append("Probability float,");
            for(int i = 1; i < length-1; i++)
            {
                sb.Append("Param" + i + " float,");
            }
            sb.Append("Param" + (length-1) + " float ");
           
            sb.Append(")");
            return sb.ToString();
        }

        public string GetInsertTableCommand(string line)
        {
            string[] sArray = line.Split();
            int length = sArray.Length;
            StringBuilder sb = new StringBuilder();
            sb.Append("Insert into ");
            sb.Append(TableName);
            sb.Append(" Values");
            sb.Append("(");
            sb.Append(@"0,");
            for (int i = 0; i < length-1; i++)
            {
                sb.Append(sArray[i]+",");
            }
            sb.Append(sArray[length-1]);

            sb.Append(")");
            return sb.ToString();
        }
    }
}
