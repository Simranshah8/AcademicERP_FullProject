using Dynamic.BusinessEntity.Account;
using Dynamic.DataAccess.Global;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
//using System.Web.Http.Results;

namespace AcademicLib.BE.Infirmary
{
    public class SQLColumn : Attribute
    {
        public string Name { get; set; }
        public string Type { get; set; } = "column";
        public string JoinType { get; set; } = "left outer join";
        public string PKTable { get; set; } = "";
        public string PKColumn { get; set; }
        public string FKTable { get; set; } = "";
        public string FKColumn { get; set; }
        public string Query { get; set; } = "";
        public bool pk { get; set; } = false;
        public bool auto { get; set; } = false;
        public bool ignoreUpdate { get; set; } = false;
    }
    public class SQLTableProperty : Attribute
    {
        public string TableName { get; set; }
        public string Name { get; set; }
        public string PKTable { get; set; } = "";
        public string PKColumn { get; set; }
        public string FKTable { get; set; } = "";
        public string FKColumn { get; set; }
    }
    public class JoinQuery
    {
        public string joinOnNameSpace;
        public string joinOnColumn;
        public string joiningTable;
        public string joiningNamespace;
        public string joiningColumn;
        public string type;
        public JoinQuery(string joinOnNameSpace, string joinOnColumn, string joiningTable, string joiningNamespace, string joiningColumn, string type = "left outer join")
        {
            this.joinOnNameSpace = joinOnNameSpace;
            this.joinOnColumn = joinOnColumn;
            this.joiningTable = joiningTable;
            this.joiningNamespace = joinOnNameSpace + "_" + joiningNamespace;
            this.joiningColumn = joiningColumn;
            this.type = type;
        }
        public string toString()
        {
            if (type == "cross join") return type + " " + joiningTable + " " + joiningNamespace + " \n";
            return type + " " + joiningTable + " " + joiningNamespace + " on " + joiningNamespace + ".[" + joiningColumn + "] = " + joinOnNameSpace + ".[" + joinOnColumn + "]\n";
        }
    }
    public class SelectQuery
    {
        public string selfTable = "";
        public List<string> columnNames = new List<string>();
        public List<string> customColumns = new List<string>();
        public List<JoinQuery> joins = new List<JoinQuery>();
        int top = -1;
        int skip = -1;
        string orderBy = "";
        public SelectQuery(int top, int skip, string orderBy)
        {
            this.top = top;
            this.skip = skip;
            this.orderBy = orderBy;
        }
        public void merge(SelectQuery t)
        {
            this.columnNames.AddRange(t.columnNames);
            this.customColumns.AddRange(t.customColumns);
            this.joins.AddRange(t.joins);
        }
        public void addJoinQuery(string joinOnNameSpace, string joinOnColumn, string joiningTable, string joiningNamespace, string joiningColumn, string type = "left outer join")
        {
            joins.Add(new JoinQuery(joinOnNameSpace, joinOnColumn, joiningTable, joiningNamespace, joiningColumn, type));
        }
        public void addColumn(string nameSpace, string name)
        {
            if (skip == -1)
            {
                columnNames.Add(nameSpace + ".[" + name + "] as " + nameSpace + "_" + name);
                customColumns.Add("");
            } else
            {
                columnNames.Add(nameSpace + ".[" + name + "] as " + nameSpace + "_" + name);
                customColumns.Add("");
            }
        }
        public void addCustomColumn(string nameSpace, string query, string customName)
        {
            columnNames.Add(nameSpace + "_" + customName + "");
            customColumns.Add(query + " ");
        }
        public string getQueryString(bool count = false)
        {
            if (skip > -1)
            {
                string ret = "select " + (count ? "count(*)" : "*") + " ";
                if (this.top > -1) ret = $"select top({top}) {(count ? "count(*)" : "*")} FROM (SELECT ROW_NUMBER() OVER(ORDER BY {orderBy}) AS RoNum, \n";
                for (var i = 0; i < columnNames.Count; i++)
                {
                    if (i == 0) ret = ret + customColumns[i] + columnNames[i] + " \n";
                    else ret = ret + "," + customColumns[i] + columnNames[i] + " \n";
                }
                ret = ret + " from " + selfTable + " self \n";
                for (var i = 0; i < joins.Count; i++)
                {
                    ret = ret + joins[i].toString() + " \n";
                }
                ret = ret + $")AS tbl WHERE {skip} < RoNum";
                return ret;
            }
            else
            {
                string ret = "select " + (count ? "count(*)" : "");
                if (this.top > -1) ret = $"select top({top}) " + (count ? "count(*)" : "");
                if (!count)
                {
                    for (var i = 0; i < columnNames.Count; i++)
                    {
                        if (i == 0) ret = ret + customColumns[i] + columnNames[i] + " \n";
                        else ret = ret + "," + customColumns[i] + columnNames[i] + " \n";
                    }
                }
                ret = ret + " from " + selfTable + " self \n";
                for (var i = 0; i < joins.Count; i++)
                {
                    ret = ret + joins[i].toString() + " \n";
                }
                return ret;
            }
        }
    }
    public class SQLResponse
    {
        public dynamic Data { get; set; }
        public int Count { get; set; }
        public int CountAll { get; set; }
        public bool IsSuccess { get; set; }
        public string ResponseMSG { get; set; }
        public int ErrorNumber { get; set; }
        public int RId { get; set; }
        public string query { get; set; }
        public SelectQuery sq { get; set; }
        public AddUpdateQuery auq { get; set; }
        public WhereQuery wq { get; set; }
    }
    public class AddUpdateQuery
    {
        public string selfTable = "";
        public string pidColumnName = "";
        public List<string> columnNames = new List<string>();
        public List<string> columnParameters = new List<string>();
        public List<dynamic> columnValues = new List<dynamic>();
        public void addValue(string columnName, dynamic value)
        {
            columnNames.Add($"[{columnName}]");
            columnParameters.Add($"@{columnName}");
            columnValues.Add(value);
        }
        public string getAddQueryString(bool isGetId = false)
        {
            string ret = "";
            ret = $"INSERT INTO {selfTable} \n(";
            for (var i = 0; i < columnNames.Count; i++)
            {
                if (i == 0) ret = ret + $"{columnNames[i]} \n";
                else ret = ret + $", {columnNames[i]} \n";
            }
            if (isGetId) ret = ret + $") output (INSERTED.[{pidColumnName}]) values (";
            else ret = ret + $") values (";
            for (var i = 0; i < columnNames.Count; i++)
            {
                if (i == 0) ret = ret + $"{columnParameters[i]}";
                else ret = ret + $", {columnParameters[i]}";
            }
            ret = ret + $");";
            return ret;
        }
        public string getUpdateQueryString(WhereQuery where)
        {
            string ret = "";
            ret = $"update {selfTable} set \n";
            for (var i = 0; i < columnNames.Count; i++)
            {
                if (i == 0) ret = ret + $"{columnNames[i]} = {columnParameters[i]} \n";
                else ret = ret + $", {columnNames[i]} = {columnParameters[i]} \n";
            }
            ret = ret + $" where {where.getQueryString()}";
            return ret;
        }
    }
    public class WhereQuery
    {
        string whereType = "";
        public List<string> columnNames = new List<string>();
        public List<string> columnConditions = new List<string>();
        public List<string> columnParameters = new List<string>();
        public List<dynamic> columnValues = new List<dynamic>();
        public WhereQuery(string WhereType)
        {
            whereType = WhereType;
        }
        public void addValue(string columnName, string condition, dynamic value, string parameter)
        {
            columnNames.Add($"{columnName}");
            columnConditions.Add($"{condition}");
            columnParameters.Add($"@{parameter}");
            columnValues.Add(value);
        }
        public string getQueryString(string orderBy = "")
        {
            string ret = " ";
            for (var i = 0; i < columnNames.Count; i++)
            {
                if (i == 0) ret = ret + $"{columnNames[i]} {columnConditions[i]} {columnParameters[i]} \n";
                else ret = ret + $" {whereType} {columnNames[i]} {columnConditions[i]} {columnParameters[i]} \n";
            }
            if (orderBy != String.Empty) ret = ret + $"ORDER BY {orderBy}";
            return ret;
        }
    }
    public class SQLTable
    {
        public SelectQuery getSelectQuery(int top = -1, int skip = -1, string orderBy = "", string prevNameSpace = "", string myName = "self")
        {
            SelectQuery sq = new SelectQuery(top, skip, orderBy);
            sq.selfTable = ((SQLTableProperty)this.GetType().UnderlyingSystemType.GetCustomAttribute(typeof(SQLTableProperty))).TableName;
            var myNameSpace = (prevNameSpace == String.Empty ? "" : prevNameSpace + "_") + myName;
            foreach (PropertyInfo propertyInfo in this.GetType().UnderlyingSystemType.GetProperties())
            {
                object[] attributeColumn = propertyInfo.GetCustomAttributes(typeof(SQLColumn), true);
                object[] attributeTable = propertyInfo.GetCustomAttributes(typeof(SQLTableProperty), true);
                if (attributeColumn.Length > 0)
                {
                    SQLColumn myAttribute = (SQLColumn)attributeColumn[0];
                    if (myAttribute.Type == "join")
                    {
                        var newChildTable = Activator.CreateInstance(propertyInfo.PropertyType);
                        propertyInfo.SetValue(this, newChildTable);
                        var childTable = ((SQLTable)propertyInfo.GetValue(this));

                        var tsq = childTable.getSelectQuery(top, skip, orderBy, myNameSpace, myAttribute.Name);
                        if (myAttribute.FKTable == "this")
                            sq.addJoinQuery(myNameSpace, myAttribute.FKColumn, ((SQLTableProperty)childTable.GetType().GetCustomAttribute(typeof(SQLTableProperty))).TableName, myAttribute.Name, myAttribute.PKColumn, myAttribute.JoinType);
                        else
                            sq.addJoinQuery(myNameSpace, myAttribute.PKColumn, ((SQLTableProperty)childTable.GetType().GetCustomAttribute(typeof(SQLTableProperty))).TableName, myAttribute.Name, myAttribute.FKColumn, myAttribute.JoinType);
                        sq.merge(tsq);
                    }
                    else if (myAttribute.Type == "column")
                    {
                        string propertyValue = myAttribute.Name;
                        sq.addColumn(myNameSpace, propertyValue);
                    }
                    else if (myAttribute.Type == "custom")
                    {
                        string propertyValue = myAttribute.Name;
                        sq.addCustomColumn(myNameSpace, myAttribute.Query, propertyValue);
                    }
                }
                /*else if (attributeTable.Length > 0)
                {
                    var newChildTable = Activator.CreateInstance(propertyInfo.PropertyType);
                    propertyInfo.SetValue(this, newChildTable);
                    var childTable = ((SQLTable)propertyInfo.GetValue(this));
                    // ret += childTable.getAll() + " ";
                }*/
            }
            return sq;
        }
        public void fillColumn(string tableName, string columnName, System.Data.SqlClient.SqlDataReader reader, int index)
        {
            columnName = columnName.Substring(tableName.Length);
            var selfTableName = ((SQLTableProperty)this.GetType().UnderlyingSystemType.GetCustomAttribute(typeof(SQLTableProperty))).TableName;
            foreach (PropertyInfo propertyInfo in this.GetType().UnderlyingSystemType.GetProperties())
            {
                object[] attributeColumn = propertyInfo.GetCustomAttributes(typeof(SQLColumn), true);
                object[] attributeTable = propertyInfo.GetCustomAttributes(typeof(SQLTableProperty), true);
                if (attributeColumn.Length > 0)
                {
                    SQLColumn myAttribute = (SQLColumn)attributeColumn[0];
                    if (myAttribute.Type == "join")
                    {
                        if (columnName.IndexOf("_" + myAttribute.Name) == 0)
                        {
                            SQLTable childTable;
                            if (((SQLTable)propertyInfo.GetValue(this)) == null)
                            {
                                var newChildTable = Activator.CreateInstance(propertyInfo.PropertyType);
                                propertyInfo.SetValue(this, newChildTable);
                                childTable = ((SQLTable)propertyInfo.GetValue(this));
                            }
                            else childTable = ((SQLTable)propertyInfo.GetValue(this));
                            var nextTableName = myAttribute.Name;
                            childTable.fillColumn("_" + nextTableName, columnName, reader, index);
                        }
                    }
                    else if ((myAttribute.Type == "column" && columnName.IndexOf(".[" + myAttribute.Name + "]") == 0) || (myAttribute.Type == "custom" && columnName.IndexOf("_" + myAttribute.Name + "") == 0))
                    {
                        var typeName = propertyInfo.PropertyType.FullName;
                        if (propertyInfo.PropertyType.IsGenericType) typeName = propertyInfo.PropertyType.GetGenericArguments().Single().FullName;
                        if (typeName == "System.Int32" && !(reader[index] is DBNull))
                        {
                            propertyInfo.SetValue(this, reader.GetInt32(index));
                        }
                        else if (typeName == "System.String" && !(reader[index] is DBNull))
                        {
                            propertyInfo.SetValue(this, reader.GetString(index));
                        }
                        else if (typeName == "System.DateTime" && !(reader[index] is DBNull))
                        {
                            propertyInfo.SetValue(this, reader.GetDateTime(index));
                        }
                        else if (typeName == "System.Byte" && !(reader[index] is DBNull))
                        {
                            propertyInfo.SetValue(this, reader.GetByte(index));
                        }
                        break;
                    }
                }
            }
        }
        public void fillOneToMany(DataAccessLayer1 dal)
        {
            foreach (PropertyInfo propertyInfo in this.GetType().UnderlyingSystemType.GetProperties())
            {
                object[] attributeColumn = propertyInfo.GetCustomAttributes(typeof(SQLColumn), true);
                object[] attributeTable = propertyInfo.GetCustomAttributes(typeof(SQLTableProperty), true);
                if (attributeColumn.Length > 0)
                {
                    SQLColumn myAttribute = (SQLColumn)attributeColumn[0];
                    if (myAttribute.Type == "join")
                    {
                        SQLTable childTable;
                        if (((SQLTable)propertyInfo.GetValue(this)) != null)
                        {
                            childTable = ((SQLTable)propertyInfo.GetValue(this));
                            childTable.fillOneToMany(dal);
                        }
                    }
                }
                else if (attributeTable.Length > 0)
                {
                    SQLTableProperty myAttribute = (SQLTableProperty)attributeTable[0];
                    var tType = propertyInfo.PropertyType.GetGenericArguments().Single();
                    WhereQuery where = new WhereQuery("and");
                    foreach (PropertyInfo propertyInfo2 in this.GetType().UnderlyingSystemType.GetProperties())
                    {
                        object[] attributeColumn2 = propertyInfo2.GetCustomAttributes(typeof(SQLColumn), true);
                        if (attributeColumn2.Length > 0)
                        {
                            SQLColumn myAttribute2 = (SQLColumn)attributeColumn2[0];
                            if (myAttribute2.Type != "join" && myAttribute2.Name == myAttribute.PKColumn)
                            {
                                where.addValue(myAttribute.FKColumn, "=", propertyInfo2.GetValue(this), myAttribute.FKColumn);
                            }
                        }
                    }
                    propertyInfo.SetValue(this, getAll(dal, tType, where).Data);
                }
            }
        }
        public SQLResponse save(DataAccessLayer1 dal, bool isGetId = false)
        {
            if (this.IsExists(dal))
            {
                return this.update(dal, isGetId);
            }
            return this.add(dal, isGetId);
        }
        public bool IsExists(DataAccessLayer1 dal)
        {
            bool ret = false;
            WhereQuery where = new WhereQuery("and");
            var selfTableName = ((SQLTableProperty)this.GetType().UnderlyingSystemType.GetCustomAttribute(typeof(SQLTableProperty))).TableName;
            foreach (PropertyInfo propertyInfo in this.GetType().UnderlyingSystemType.GetProperties())
            {
                object[] attributeColumn = propertyInfo.GetCustomAttributes(typeof(SQLColumn), true);
                if (attributeColumn.Length > 0)
                {
                    SQLColumn myAttribute = (SQLColumn)attributeColumn[0];
                    if (myAttribute.Type != "join" && myAttribute.pk && (propertyInfo.GetValue(this)) != null)
                    {
                        SQLTable childTable;
                        where.addValue(myAttribute.Name, "=", propertyInfo.GetValue(this), myAttribute.Name);
                    }
                }
            }
            string query = $"select 1 from {selfTableName} where {where.getQueryString()}";
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = query;
            for (var i = 0; i < where.columnValues.Count; i++) cmd.Parameters.AddWithValue(where.columnParameters[i], where.columnValues[i]);
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ret = true;
                }
                reader.Close();
            }
            catch (Exception ee)
            {
            }
            finally
            {
                dal.CloseConnection();
            }
            return ret;
        }
        public SQLResponse add(DataAccessLayer1 dal, bool isGetId = false)
        {
            SQLResponse res = new SQLResponse();
            AddUpdateQuery adder = new AddUpdateQuery();
            var selfTableName = ((SQLTableProperty)this.GetType().UnderlyingSystemType.GetCustomAttribute(typeof(SQLTableProperty))).TableName;
            adder.selfTable = selfTableName;
            foreach (PropertyInfo propertyInfo in this.GetType().UnderlyingSystemType.GetProperties())
            {
                object[] attributeColumn = propertyInfo.GetCustomAttributes(typeof(SQLColumn), true);
                if (attributeColumn.Length > 0)
                {
                    SQLColumn myAttribute = (SQLColumn)attributeColumn[0];
                    if (myAttribute.Type != "join" && !myAttribute.auto && (propertyInfo.GetValue(this)) != null)
                    {
                        SQLTable childTable;
                        adder.addValue(myAttribute.Name, propertyInfo.GetValue(this));
                    }
                    else if (myAttribute.Type != "join" && myAttribute.auto && isGetId)
                    {
                        SQLTable childTable;
                        adder.pidColumnName = myAttribute.Name;
                    }
                }
            }
            res.query = adder.getAddQueryString(isGetId);
            res.auq = adder;
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = res.query;
            for (var i = 0; i < adder.columnValues.Count; i++) cmd.Parameters.AddWithValue(adder.columnParameters[i], adder.columnValues[i]);
            try
            {
                if (isGetId)
                {
                    int myId = (int)cmd.ExecuteScalar();
                    res.RId = myId;
                }
                else cmd.ExecuteScalar();
                res.IsSuccess = true;
                res.ResponseMSG = "added row";
            }
            catch (Exception ee)
            {
                res.IsSuccess = false;
                res.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            return res;
        }
        public SQLResponse update(DataAccessLayer1 dal, bool isGetId = false)
        {
            SQLResponse res = new SQLResponse();
            AddUpdateQuery updater = new AddUpdateQuery();
            WhereQuery where = new WhereQuery("and");
            var selfTableName = ((SQLTableProperty)this.GetType().UnderlyingSystemType.GetCustomAttribute(typeof(SQLTableProperty))).TableName;
            updater.selfTable = selfTableName;
            foreach (PropertyInfo propertyInfo in this.GetType().UnderlyingSystemType.GetProperties())
            {
                object[] attributeColumn = propertyInfo.GetCustomAttributes(typeof(SQLColumn), true);
                if (attributeColumn.Length > 0)
                {
                    SQLColumn myAttribute = (SQLColumn)attributeColumn[0];
                    if (myAttribute.Type != "join" && !myAttribute.auto && (propertyInfo.GetValue(this)) != null)
                    {
                        SQLTable childTable;
                        updater.addValue(myAttribute.Name, propertyInfo.GetValue(this));
                    }
                    else if (myAttribute.Type != "join" && myAttribute.auto && isGetId && (propertyInfo.GetValue(this)) != null)
                    {
                        SQLTable childTable;
                        res.RId = (int)propertyInfo.GetValue(this);
                    }
                    if (myAttribute.pk && (propertyInfo.GetValue(this)) != null)
                    {
                        where.addValue(myAttribute.Name, "=", propertyInfo.GetValue(this), myAttribute.Name);
                    }
                }
            }
            res.query = updater.getUpdateQueryString(where);
            res.auq = updater;
            res.wq = where;
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = res.query;
            for (var i = 0; i < updater.columnValues.Count; i++) cmd.Parameters.AddWithValue(updater.columnParameters[i], updater.columnValues[i]);
            for (var i = 0; i < where.columnValues.Count; i++) cmd.Parameters.AddWithValue(where.columnParameters[i], where.columnValues[i]);
            try
            {
                cmd.ExecuteScalar();
                res.IsSuccess = true;
                res.ResponseMSG = "added row";
            }
            catch (Exception ee)
            {
                res.IsSuccess = false;
                res.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            return res;
        }
        public SQLResponse remove(DataAccessLayer1 dal)
        {
            SQLResponse res = new SQLResponse();
            if (this.IsExists(dal))
            {
                WhereQuery where = new WhereQuery("and");
                var selfTableName = ((SQLTableProperty)this.GetType().UnderlyingSystemType.GetCustomAttribute(typeof(SQLTableProperty))).TableName;
                foreach (PropertyInfo propertyInfo in this.GetType().UnderlyingSystemType.GetProperties())
                {
                    object[] attributeColumn = propertyInfo.GetCustomAttributes(typeof(SQLColumn), true);
                    if (attributeColumn.Length > 0)
                    {
                        SQLColumn myAttribute = (SQLColumn)attributeColumn[0];
                        if (myAttribute.pk && (propertyInfo.GetValue(this)) != null)
                        {
                            where.addValue(myAttribute.Name, "=", propertyInfo.GetValue(this), myAttribute.Name);
                        }
                    }
                }
                res.query = $"delete from {selfTableName} where {where.getQueryString()}";
                res.wq = where;
                dal.OpenConnection();
                System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = res.query;
                for (var i = 0; i < where.columnValues.Count; i++) cmd.Parameters.AddWithValue(where.columnParameters[i], where.columnValues[i]);
                try
                {
                    cmd.ExecuteScalar();
                    res.IsSuccess = true;
                    res.ResponseMSG = "row deleted";
                }
                catch (Exception ee)
                {
                    res.IsSuccess = false;
                    res.ResponseMSG = ee.Message;
                }
                finally
                {
                    dal.CloseConnection();
                }
                return res;
            }
            res.IsSuccess = true;
            res.ResponseMSG = "row does not exist";
            return res;
        }
        public void getSample()
        {
            foreach (PropertyInfo propertyInfo in this.GetType().UnderlyingSystemType.GetProperties())
            {
                object[] attributeColumn = propertyInfo.GetCustomAttributes(typeof(SQLColumn), true);
                object[] attributeTable = propertyInfo.GetCustomAttributes(typeof(SQLTableProperty), true);
                if (attributeColumn.Length > 0)
                {
                    SQLColumn myAttribute = (SQLColumn)attributeColumn[0];
                    if (myAttribute.Type == "join")
                    {
                        SQLTable childTable;
                        if (((SQLTable)propertyInfo.GetValue(this)) == null)
                        {
                            var newChildTable = Activator.CreateInstance(propertyInfo.PropertyType);
                            propertyInfo.SetValue(this, newChildTable);
                            childTable = ((SQLTable)propertyInfo.GetValue(this));
                        }
                        else childTable = ((SQLTable)propertyInfo.GetValue(this));
                        var nextTableName = myAttribute.Name;
                        childTable.getSample();
                    }
                }
                else if (attributeTable.Length > 0)
                {
                    object rows = Activator.CreateInstance(propertyInfo.PropertyType);
                    dynamic Rows = Convert.ChangeType(rows, propertyInfo.PropertyType);
                    propertyInfo.SetValue(this, rows);
                    var columns = Activator.CreateInstance(propertyInfo.PropertyType.GetGenericArguments().Single());
                    dynamic Columns = Convert.ChangeType(columns, propertyInfo.PropertyType.GetGenericArguments().Single());
                    Rows.Add(Columns);
                    Rows[0].getSample();
                }
            }
        }
        public static SQLResponse getAll(DataAccessLayer1 dal, Type TableType, string where = "", int top = -1, int skip = -1, string orderBy = "")
        {
            SQLResponse res = new SQLResponse();
            Type genericType = typeof(List<>);
            Type specificType = genericType.MakeGenericType(TableType);
            object rows = Activator.CreateInstance(specificType);
            dynamic Rows = Convert.ChangeType(rows, specificType);
            object tTable = Activator.CreateInstance(TableType);
            dynamic TTable = Convert.ChangeType(tTable, TableType);
            var sq = TTable.getSelectQuery(top, skip, orderBy);
            var query = sq.getQueryString();
            if (where != String.Empty && skip < 0) query = query + " where " + where;
            if (where != String.Empty && skip >= 0) query = query + " and " + where;
            if (orderBy != string.Empty && skip == -1) query = query + $" ORDER BY {orderBy}";
            res.query = query;
            res.sq = sq;
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = query;
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    object row = Activator.CreateInstance(TableType);
                    dynamic Row = Convert.ChangeType(row, TableType);
                    for (var i = (skip > -1 ? 1 : 0); i < sq.columnNames.Count + (skip > -1 ? 1 : 0); i++)
                    {
                        var j = i - (skip > -1 ? 1 : 0);
                        Row.fillColumn("self", sq.columnNames[j], reader, i);
                    }
                    Rows.Add(Row);
                }
                reader.Close();
                for (var i = 0; i < Rows.Count; i++) Rows[i].fillOneToMany(dal);
                res.IsSuccess = true;
                res.ResponseMSG = "got all rows";
            }
            catch (Exception ee)
            {
                res.IsSuccess = false;
                res.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            res.Data = Rows;
            return res;
        }
        public static SQLResponse getAll(DataAccessLayer1 dal, Type TableType, WhereQuery where = null, int top = -1, int skip = -1, string orderBy = "")
        {
            SQLResponse res = new SQLResponse();
            Type genericType = typeof(List<>);
            Type specificType = genericType.MakeGenericType(TableType);
            object rows = Activator.CreateInstance(specificType);
            dynamic Rows = Convert.ChangeType(rows, specificType);
            object tTable = Activator.CreateInstance(TableType);
            dynamic TTable = Convert.ChangeType(tTable, TableType);
            var sq = TTable.getSelectQuery(top, skip, orderBy);
            var query = sq.getQueryString();
            if (where != null && where.columnNames.Count > 0 && skip < 0) query = query + " where " + where.getQueryString(orderBy);
            if (where != null && where.columnNames.Count > 0 && skip >= 0) query = query + " and " + where.getQueryString();
            res.query = query;
            res.sq = sq;
            res.wq = where;
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = query;
            for (var i = 0; i < where.columnValues.Count; i++) cmd.Parameters.AddWithValue(where.columnParameters[i], where.columnValues[i]);
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    object row = Activator.CreateInstance(TableType);
                    dynamic Row = Convert.ChangeType(row, TableType);
                    for (var i = (skip > -1 ? 1 : 0); i < sq.columnNames.Count + (skip > -1 ? 1 : 0); i++)
                    {
                        Row.fillColumn("self", sq.columnNames[i - (skip > -1 ? 1 : 0)], reader, i);
                    }
                    Rows.Add(Row);
                }
                reader.Close();
                for (var i = 0; i < Rows.Count; i++) Rows[i].fillOneToMany(dal);
                res.IsSuccess = true;
                res.ResponseMSG = "got all rows";
            }
            catch (Exception ee)
            {
                res.IsSuccess = false;
                res.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            res.Data = Rows;
            return res;
        }
        public static SQLResponse get(DataAccessLayer1 dal, Type TableType, string where = "", int top = -1, int skip = -1, string orderBy = "")
        {
            SQLResponse res = new SQLResponse();
            object row = Activator.CreateInstance(TableType);
            dynamic Row = Convert.ChangeType(row, TableType);
            object tTable = Activator.CreateInstance(TableType);
            dynamic TTable = Convert.ChangeType(tTable, TableType);
            var sq = TTable.getSelectQuery(top, skip, orderBy);
            var query = sq.getQueryString();
            if (where != String.Empty && skip < 0) query = query + " where " + where;
            if (where != String.Empty && skip >= 0) query = query + " and " + where;
            if (orderBy != string.Empty && skip == -1) query = query + $" ORDER BY {orderBy}";
            res.query = query;
            res.sq = sq;
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = query;
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    for (var i = (skip > -1 ? 1 : 0); i < sq.columnNames.Count + (skip > -1 ? 1 : 0); i++)
                    {
                        Row.fillColumn("self", sq.columnNames[i - (skip > -1 ? 1 : 0)], reader, i);
                    }
                }
                reader.Close();
                Row.fillOneToMany(dal);
                res.IsSuccess = true;
                res.ResponseMSG = "got all rows";
            }
            catch (Exception ee)
            {
                res.IsSuccess = false;
                res.ResponseMSG = ee.Message;
            }
            finally
            {
                dal.CloseConnection();
            }
            res.Data = Row;
            return res;
        }
        public static int count(DataAccessLayer1 dal, Type TableType, string where = "", int top = -1, int skip = -1, string orderBy = "")
        {
            int ret = -1;
            object tTable = Activator.CreateInstance(TableType);
            dynamic TTable = Convert.ChangeType(tTable, TableType);
            var sq = TTable.getSelectQuery(top, skip, orderBy);
            var query = sq.getQueryString(true);
            if (where != String.Empty && skip < 0) query = query + " where " + where;
            if (where != String.Empty && skip >= 0) query = query + " and " + where;
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = query;
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (!(reader[0] is DBNull)) ret = reader.GetInt32(0);
                }
                reader.Close();
            }
            catch (Exception ee)
            {
            }
            finally
            {
                dal.CloseConnection();
            }
            return ret;
        }
        public static int count(DataAccessLayer1 dal, Type TableType, WhereQuery where = null, int top = -1, int skip = -1, string orderBy = "")
        {
            int ret = -1;
            object tTable = Activator.CreateInstance(TableType);
            dynamic TTable = Convert.ChangeType(tTable, TableType);
            var sq = TTable.getSelectQuery(top, skip, orderBy);
            var query = sq.getQueryString(true);
            if (where != null && where.columnNames.Count > 0 && where.columnNames.Count > 0 && skip < 0) query = query + " where " + where.getQueryString();
            if (where != null && where.columnNames.Count > 0 && skip >= 0) query = query + " and " + where.getQueryString();
            dal.OpenConnection();
            System.Data.SqlClient.SqlCommand cmd = dal.GetCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = query;
            for (var i = 0; i < where.columnValues.Count; i++) cmd.Parameters.AddWithValue(where.columnParameters[i], where.columnValues[i]);
            try
            {
                System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (!(reader[0] is DBNull)) ret = reader.GetInt32(0);
                }
                reader.Close();
            }
            catch (Exception ee)
            {
            }
            finally
            {
                dal.CloseConnection();
            }
            return ret;
        }
    }
}