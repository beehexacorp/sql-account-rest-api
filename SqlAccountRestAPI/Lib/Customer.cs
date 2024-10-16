using static System.Runtime.InteropServices.JavaScript.JSType;
using System;
using System.Text.Json.Nodes;
using SqlAccountRestAPI.Models;
using System.Xml;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using System.ComponentModel.DataAnnotations;

namespace SqlAccountRestAPI.Lib
{
    public class Customer
    {
        private SqlComServer app;
        public Customer(SqlComServer comServer)
        {
            if (comServer == null) throw new Exception("Sql Accounting is not running");
            app = comServer;
        }
        public string LoadAllByDaysToNow(int days)
        {
            long todayTimeStamp = new DateTimeOffset(DateTime.UtcNow.Date).ToUnixTimeSeconds();
            long searchDayTimeStamp = todayTimeStamp - days * 24 * 3600;
            dynamic lSQL = "SELECT * FROM "
                + "AR_CUSTOMER JOIN AR_CUSTOMERBRANCH "
                + "ON AR_CUSTOMER.CODE = AR_CUSTOMERBRANCH.CODE "
                + "WHERE AR_CUSTOMER.LASTMODIFIED > " + searchDayTimeStamp.ToString()
                + " ORDER BY AR_CUSTOMER.LASTMODIFIED";
            dynamic lJoinDataset = app.ComServer.DBManager.NewDataSet(lSQL);

            dynamic lMainFields = app.ComServer.DBManager.NewDataSet("SELECT * FROM AR_CUSTOMER").Fields;
            dynamic lSubDataSetFields = app.ComServer.DBManager.NewDataSet("SELECT * FROM AR_CUSTOMERBRANCH").Fields;

            List<string> mainFieldsArray = new List<string> { };
            for (int i = 0; i < lMainFields.Count; i++)
            {
                mainFieldsArray.Add(lMainFields.Items(i).FieldName);
            }
            List<string> subDataSetFieldsArray = new List<string> { };
            for (int i = 0; i < lSubDataSetFields.Count; i++)
            {
                subDataSetFieldsArray.Add(lSubDataSetFields.Items(i).FieldName);
            }

            JArray rows = new JArray();
            lJoinDataset.First();
            var mark = "";
            JObject row = new JObject();
            JArray subRowArray = new JArray();
            while (!lJoinDataset.eof)
            {
                var fields = lJoinDataset.Fields;

                if (mark != fields.FindField("CODE").value.ToString())
                {
                    subRowArray = new JArray();
                    row = new JObject();
                    rows.Add(row);

                    mark = fields.FindField("CODE").value.ToString();

                    foreach (string mainField in mainFieldsArray)
                    {
                        if (fields.FindField(mainField).value is string)
                            row[mainField] = fields.FindField(mainField).value;
                    }
                    row["cdsBranch"] = subRowArray;
                }

                JObject subRow = new JObject();
                foreach (string subDataSetField in subDataSetFieldsArray)
                {
                    if (fields.FindField(subDataSetField).value is string)
                        subRow[subDataSetField] = fields.FindField(subDataSetField).value;
                }
                subRowArray.Add(subRow);
                lJoinDataset.Next();

            }
            return rows.ToString(Newtonsoft.Json.Formatting.Indented);

        }
        public string LoadByEmail(string email)
        {
            dynamic lSQL = "SELECT * FROM "
                + "AR_CUSTOMER JOIN AR_CUSTOMERBRANCH "
                + "ON AR_CUSTOMER.CODE = AR_CUSTOMERBRANCH.CODE WHERE AR_CUSTOMER.CODE IN (SELECT CODE FROM AR_CUSTOMERBRANCH WHERE AR_CUSTOMERBRANCH.EMAIL='" + email + "')";

            dynamic lJoinDataset = app.ComServer.DBManager.NewDataSet(lSQL);

            dynamic lMainFields = app.ComServer.DBManager.NewDataSet("SELECT * FROM AR_CUSTOMER").Fields;
            dynamic lSubDataSetFields = app.ComServer.DBManager.NewDataSet("SELECT * FROM AR_CUSTOMERBRANCH").Fields;

            List<string> mainFieldsArray = new List<string> { };
            for (int i = 0; i < lMainFields.Count; i++)
            {
                mainFieldsArray.Add(lMainFields.Items(i).FieldName);
            }
            List<string> subDataSetFieldsArray = new List<string> { };
            for (int i = 0; i < lSubDataSetFields.Count; i++)
            {
                subDataSetFieldsArray.Add(lSubDataSetFields.Items(i).FieldName);
            }

            JArray rows = new JArray();
            lJoinDataset.First();
            var mark = "";
            JObject row = new JObject();
            JArray subRowArray = new JArray();
            while (!lJoinDataset.eof)
            {
                var fields = lJoinDataset.Fields;

                // new customer
                if (mark != fields.FindField("CODE").value.ToString())
                {
                    subRowArray = new JArray();
                    row = new JObject();
                    rows.Add(row);

                    mark = fields.FindField("CODE").value.ToString();
                    foreach (string mainField in mainFieldsArray)
                    {
                        if (fields.FindField(mainField).value is string)
                        {
                            row[mainField] = fields.FindField(mainField).value;
                        }
                    }
                    row["cdsBranch"] = subRowArray;

                }

                JObject subRow = new JObject();
                foreach (string subDataSetField in subDataSetFieldsArray)
                {
                    if (fields.FindField(subDataSetField).value is string)
                        subRow[subDataSetField] = fields.FindField(subDataSetField).value;
                }
                subRowArray.Add(subRow);
                lJoinDataset.Next();

            }
            return rows.ToString(Newtonsoft.Json.Formatting.Indented);
        }
        public JObject Payment(JObject jsonBody)
        {
            dynamic lDockey, lSQL, lMain, IvBizObj, lKnockOff, Fields, objectType, lDocAmt;
            
            lSQL = "SELECT FROMDOCTYPE, DOCAMT FROM AR_IV WHERE DOCNO='" + jsonBody["DOCNO"] + "'";
            lMain = app.ComServer.DBManager.NewDataSet(lSQL);
            objectType = lMain.FindField("FROMDOCTYPE").value;
            lDocAmt = lMain.FindField("DOCAMT").value;

            // ADD
            IvBizObj = app.ComServer.BizObjects.Find("AR_PM");
            var lMainDataSet = IvBizObj.DataSets.Find("MainDataSet");

            IvBizObj.New();


            lMainDataSet.FindField("CODE").value = jsonBody["CODE"];
            lMainDataSet.FindField("PAYMENTMETHOD").value = jsonBody["PAYMENTMETHOD"];
            lMainDataSet.FindField("DOCAMT").value = lDocAmt;
            lMainDataSet.FindField("LOCALDOCAMT").value = lDocAmt;
            lMainDataSet.FindField("PROJECT").value = jsonBody["PROJECT"];
            lMainDataSet.FindField("PAYMENTPROJECT").value = jsonBody["PROJECT"];        

            var lDetail = IvBizObj.DataSets.Find("cdsKnockOff");
            //Step 5: Knock Off IV
            dynamic lIVNO = jsonBody["DOCNO"];
            object[] V = new object[2];
            V[0] = "IV";
            V[1] = lIVNO;
            Console.WriteLine(V);

            if (lDetail.Locate("DocType;DocNo", V, false, false))
            {
                lDetail.Edit();
                lDetail.FindField("KOAmt").AsFloat = lDocAmt; //Partial Knock off
                // lDetail.FindField("KnockOff").AsString = "T";
                lDetail.Post();
            }



            IvBizObj.Save();
            if (lMainDataSet.FindField("DOCNO") != null)
                return new JObject { { "DOCNO", lMainDataSet.FindField("DOCNO").value.ToString() } };
            return new JObject { { "CODE", lMainDataSet.FindField("CODE").value.ToString() } };
        }
        public void Test(JObject jsonBody){
            dynamic lDockey, lSQL, lMain, IvBizObj, lKnockOff, Fields, objectType, lDocAmt;
            
            IvBizObj = app.ComServer.BizObjects.Find("AR_PM");

            //Step 3: Set Dataset
            lMain = IvBizObj.DataSets.Find("MainDataSet"); //lMain contains master data
            dynamic lDetail = IvBizObj.DataSets.Find("cdsKnockOff"); //lDetail contains Knock off data  

            //Step 4 : Find CN Number
            dynamic lDocNo = "OR-00071";
            dynamic lDocKey = IvBizObj.FindKeyByRef("DOCNO", lDocNo);
            IvBizObj.Params.Find("DOCKEY").Value = lDocKey;

            Console.WriteLine("MAIN");
            for(var i=0;i<lMain.Fields.Count;i++){
                Console.WriteLine(lMain.Fields.Items(i).FieldName);
                Console.WriteLine(lMain.Fields.Items(i).value.ToString());
            }
            Console.WriteLine("SUB");
            for(var i=0;i<lDetail.Fields.Count;i++){
                Console.WriteLine(lDetail.Fields.Items(i).FieldName+" "+lDetail.Fields.Items(i).value);
            }
        }
    }
    
}
