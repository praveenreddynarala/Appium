using Appium.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutonitroFramework.Report
{
    public class HTMLvariables
    {
        public HTMLvariables()
        {
            //Initializing the variable values
            string[] formatDate = null;
            string exeDate;
            formatDate = DateTime.Now.ToLongDateString().Split(',');
            exeDate = "Date: " + formatDate[1] + formatDate[2];
        }

        public string buildHTML()
        {
            //Initializing the variable values
            //Declaring all the required variables
            StringBuilder htmlFile = new StringBuilder();
            htmlFile.Clear();
            string funcShowDiv, divLHN, cssBodyStyle, divSuiteSummary;
            string funcUpdateSumTable, funcFindSuite, funcFindSuite2;
            string funcCalTime;
            string ReportHeader;
            string[] formatDate = null;
            string exeDate;
            string EnvDetailsTable;
            string funcDrawLine;
            string funcDispLog;
            string funcDispLog1;
            string funcDoMenu;
            string funcCountTC, suiteDivVar, summayrdivVar;
            DateTime nStartTime = DateTime.Now;
            string HtmlHeader = null;
            formatDate = DateTime.Now.ToLongDateString().Split(',');
            exeDate = "Date: " + formatDate[1] + formatDate[2];

            //HTML Report Header
            HtmlHeader = "<HTML>" + "\r\n" +
                "<HEAD>" + "\r\n" +
                "<script langauge=\"JavaScript\" type=\"text/javascript\">" + "\r\n";

            //All Java Functions
            funcDoMenu = "var timeDuration = 0;" + "\r\n" +
                "function  doMenu(item) {" + "\r\n" +
                "obj=document.getElementById(item);" + "\r\n" +
                "col=document.getElementById(\"x\" + item);" + "\r\n" +
                "if (obj.style.display==\"none\") {" + "\r\n" +
                "obj.style.display=\"block\";" + "\r\n" +
                "col.innerHTML=\"[-]\";" + "\r\n" +
                "}else { " + "\r\n" +
                "obj.style.display=\"none\";" + "\r\n" +
                "col.innerHTML=\"[+]\";" + "\r\n" +
                "}" + "\r\n" +
                "drawLine()" + "\r\n" +
                "}" + "\r\n" + "\r\n";

            funcDrawLine = "function drawLine()" + "\r\n" +
                "{" + "\r\n" + "objTabl=document.getElementById(\"main\");" + "\r\n" +
                "mainDivLen=objTabl.offsetHeight" + "\r\n" +
                "objTabl=document.getElementById(\"list\");" + "\r\n" +
                "listDivLen=objTabl.offsetHeight" + "\r\n" +
                "if (mainDivLen <= 580)" + "\r\n" +
                "{" + "\r\n" +
                "objTabl.style.height=700" + "\r\n" +
                "}" + "\r\n" +
                "else" + "\r\n" +
                "{" + "\r\n" +
                "objTabl.style.height=mainDivLen + 100" + "\r\n" +
                "}" + "\r\n" +
                "timeDuration = 0;" + "\r\n" +
                "}" + "\r\n" + "\r\n";

            funcDispLog = "function dispLog(tcName, fileName, logs)" + "\r\n" +
                "{" + "\r\n" +
                "buttonDiv = document.getElementById(\"button\")" + "\r\n" +
                "tcDiv = document.getElementById(tcName)" + "\r\n" +
                "newwindow = window.open(\"\",\"\");" + "\r\n" +
                "newwindow.document.write(tcDiv.innerHTML)" + "\r\n" +
                "newwindow.document.write(\"<B>Logs:</B>\")" + "\r\n" +
                "newwindow.document.write(logs)" + "\r\n" + "\r\n" +
                "newwindow.document.write(\"<B>Active Screen:</B>\")" + "\r\n" +
                "filePath = String(newwindow.location)" + "\r\n" +
                "startPos = filePath.indexOf(\":\")" + "\r\n" +
                "endPos = filePath.lastIndexOf(\"/\") + 1" + "\r\n" +
                "strLen = filePath.length" + "\r\n" +
                "sFPath = String(filePath).substring(startPos + 4,endPos)" + "\r\n" +
                "newwindow.document.write('<img src=\"' + fileName + '\"/>')" + "\r\n" +
                "newwindow.document.write(buttonDiv.innerHTML)" + "\r\n" +
                "}" + "\r\n" + "\r\n";

            funcDispLog1 = "function dispLog1(tcName, logs)" + "\r\n" +
                "{" + "\r\n" +
                "buttonDiv = document.getElementById(\"button\")" + "\r\n" +
                "tcDiv = document.getElementById(tcName)" + "\r\n" +
                "newwindow = window.open(\"\",\"\");" + "\r\n" +
                "newwindow.document.write(tcDiv.innerHTML)" + "\r\n" +
                "newwindow.document.write(\"<B>Logs:</B>\")" + "\r\n" +
                "newwindow.document.write(logs)" + "\r\n" +
                "newwindow.document.write(buttonDiv.innerHTML)" + "\r\n" +
                "}" + "\r\n" + "\r\n";





            funcCalTime = "function calTime(idName)" + "\r\n" +
                "{" + "\r\n" +
                "totalDuration = 0;" + "\r\n" +
                "allTDs = document.getElementsByTagName(\"td\");" + "\r\n" +
                "for(i=0;i<allTDs.length;i++)" +
                "\r\n" +
                "{" +
                "\r\n" +
                "if(allTDs(i).className == idName)" + "\r\n" +
                "{" + "\r\n" +
                "tdVal = (allTDs(i).innerHTML).split(\" \");" + "\r\n" +
                "totalDuration = totalDuration + parseInt(tdVal[0])" + "\r\n" +
                "}}return(totalDuration)}" + "\r\n" + "\r\n";


            funcCountTC = "function countTC(divName)" + "\r\n" +
                "{" + "\r\n" +
                "suiteDuration = 0" + "\r\n" +
                "parentDiv = document.getElementById(divName);" + "\r\n" +
                "childsDiv = parentDiv.childNodes;" + "\r\n" +
                "var retAry = new Array(2)" + "\r\n" +
                "total_tcCount=0" + "\r\n" +
                "failed_tcCount=0" + "\r\n" +
                "passed_tcCount=0" + "\r\n" +
                "/*switch (divName)" + "\r\n" +
                "{" + "\r\n" +
                "case \"Sportsbook_T\": suiteDuration = calTime(\"MarketTimeTD\"); break" + "\r\n" +
                "case \"Event_T\": suiteDuration = calTime(\"EventTimeTD\"); break" + "\r\n" +
                "case \"Oxi_T\": suiteDuration = calTime(\"oxiTimeTD\"); break" + "\r\n" +
                "case \"Affiliate_T\": suiteDuration = calTime(\"affiliateTimeTD\"); break" + "\r\n" +
                "}" + "\r\n" +
                "timeDuration = timeDuration + suiteDuration */" + "\r\n" +
                "for(i=0;i<childsDiv.length;i++)" + "\r\n" +
                "{" + "\r\n" +
                "if (childsDiv[i].id == \"outer_table\")" + "\r\n" +
                "{" + "\r\n" +
                "total_tcCount = total_tcCount + 1" + "\r\n" +
                "row = childsDiv[i].rows[0];" + "\r\n" +
                "cell = row.cells[1];" + "\r\n" +
                "if (cell.innerText == \"Pass\")" + "\r\n" +
                "{" + "\r\n" +
                "passed_tcCount = passed_tcCount + 1" + "\r\n" +
                "}" + "\r\n" +
                "else" + "\r\n" +
                "{" + "\r\n" +
                "failed_tcCount = failed_tcCount + 1" + "\r\n" +
                "}" + "\r\n" +
                "}" + "\r\n" +
                "}" + "\r\n" +
                "SuiteSumDiv = document.getElementById(\"Suite_Summary_Div\");" +
                "\r\n" +
                "SuiteSumTabl = SuiteSumDiv.childNodes;" + "\r\n" +
                "for(i=0;i<SuiteSumTabl.length;i++)" + "\r\n" +
                "{" + "\r\n" +
                "if (SuiteSumTabl[i].id == \"Suite_Summary_Table\")" + "\r\n" +
                "{" + "\r\n" +
                "pos = divName.indexOf(\"_\")" + "\r\n" +
                "SuiteSumTabl[i].rows[0].cells[0].innerText = divName.substring(0,pos);" + "\r\n" +
                "row = SuiteSumTabl[i].rows[0]" + "\r\n" +
                "cell = row.cells[0];" + "\r\n" +
                "cell.align = \"center\"" + "\r\n" +
                "cell.style.color=\"#393B0B\"" + "\r\n" +
                "SuiteSumTabl[i].rows[1].cells[1].innerText = total_tcCount;" + "\r\n" +
                "SuiteSumTabl[i].rows[2].cells[1].innerText = passed_tcCount;" + "\r\n" +
                "SuiteSumTabl[i].rows[3].cells[1].innerText = failed_tcCount;" + "\r\n" +
                "SuiteSumTabl[i].rows[4].cells[1].innerText = parseInt((passed_tcCount/total_tcCount)*100) + \" %\";" + "\r\n" +
                "//SuiteSumTabl[i].rows[4].cells[1].innerText = suiteDuration + \" mins\";" + "\r\n" +
                "retAry[0] = total_tcCount" + "\r\n" +
                "retAry[1] = passed_tcCount" + "\r\n" +
                "retAry[2] = failed_tcCount" + "\r\n" +
                "return retAry;" + "\r\n" + "}" + "\r\n" +
                "}" + "\r\n" +
                "}" + "\r\n";

            funcUpdateSumTable = "function updateSumTabl(tblVal,tblName)" + "\r\n" +
                "{" + "\r\n" +
                "mainSumDiv = document.getElementById(\"summary_T\");" + "\r\n" +
                "if(tblName==\"sumTable_Summary\")" + "\r\n" +
                "{" + "\r\n" +
                "mainSumTabl = mainSumDiv.getElementsByTagName(\"TABLE\");" + "\r\n" +
                "mainSumTabl[0].rows[1].cells[1].innerText = tblVal[0];" + "\r\n" +
                "mainSumTabl[0].rows[2].cells[1].innerText = tblVal[1];" + "\r\n" +
                "mainSumTabl[0].rows[3].cells[1].innerText = tblVal[2];" + "\r\n" +
                "mainSumTabl[0].rows[4].cells[1].innerText = ((tblVal[1] / tblVal[0])*100) +\" %\";" + "\r\n" +
                "return" + "\r\n" +
                "}" + "\r\n" +
                "childSubTabls = mainSumDiv.getElementsByTagName(\"TABLE\")" + "\r\n" +
                "for(i=0;i<childSubTabls.length;i++)" + "\r\n" +
                "{" + "\r\n" +
                "if (childSubTabls[i].id == tblName)" + "\r\n" +
                "{" + "\r\n" +
                "childSubTabls[i].rows[0].cells[1].innerText = tblVal[0];" + "\r\n" +
                "childSubTabls[i].rows[1].cells[1].innerText = tblVal[1];" + "\r\n" +
                "childSubTabls[i].rows[2].cells[1].innerText = tblVal[2];" + "\r\n" +
                "childSubTabls[i].rows[3].cells[1].innerText = int.Parse((tblVal[1]/tblVal[0])*100) + \" %\";" + "\r\n" +
                "}" + "\r\n" +
                "}" + "\r\n" +
                "}" + "\r\n";

            funcFindSuite = "function findSuite()" + "\r\n" +
                "{" + "\r\n" +
                "var sumtotal = new Array(2)" + "\r\n" +
                "sumtotal[0] = 0" + "\r\n" +
                "sumtotal[1] = 0" + "\r\n" +
                "sumtotal[2] = 0" + "\r\n" +
                "var divNameAry = []" + "\r\n" +
                "var tableNameAry = new Array(3)" + "\r\n" +
                //"divNameAry[0] =\"Market_T\"" + "\r\n" +
                //"divNameAry[1] = \"Event_T\"" + "\r\n" +
                "fixtures = document.getElementsByName(\"fixture\")" + "\r\n" +

                "for(i=0;i<fixtures.length;i++)" + "\r\n" +
                     "{" + "\r\n" +
                      "divNameAry.push(fixtures(i).getAttribute(\"id\"))" + "\r\n" +

                     "}" + "\r\n" +
                "tableNameAry[0] = \"sumTable_Market\"" + "\r\n" +
                "tableNameAry[1] = \"sumTable_Event\"" + "\r\n" +
                //"suiteDiv = document.getElementById(divNameAry[0]);" + "\r\n" +
                "for(suitLen=0;suitLen<tableNameAry.length;suitLen++)" + "\r\n" +
                "{" + "\r\n" +
                "suiteDiv = document.getElementById(divNameAry[suitLen]);" + "\r\n" +
                "if(suiteDiv)" + "\r\n" +
                "{" + "\r\n" +
                "retVal = countTC(divNameAry[suitLen])" + "\r\n" +
                "//Updating All th table values in Summary table" + "\r\n" +
                "sumtotal[0] = sumtotal[0] + retVal[0]" + "\r\n" +
                "sumtotal[1] = sumtotal[1] + retVal[1]" + "\r\n" +
                "sumtotal[2] = sumtotal[2] + retVal[2]" + "\r\n" +
                "updateSumTabl(retVal,divNameAry[suitLen])" + "\r\n" +
                "}" + "\r\n" +
                "}" + "\r\n" +
                "//Updating Main Summary table value in Summary table" + "\r\n" +
                "updateSumTabl(sumtotal,'sumTable_Summary')" + "\r\n" +
                "}" + "\r\n";

            funcFindSuite2 = "function findSuite()" + "\r\n" +
                "{" + "\r\n" +
                "var sumtotal = new Array(2)" + "\r\n" +
                "sumtotal[0] = 0" + "\r\n" +
                "sumtotal[1] = 0" + "\r\n" +
                "sumtotal[2] = 0" + "\r\n" +
                "var divNameAry = new Array(3)" + "\r\n" +
                "var tableNameAry = new Array(3)" + "\r\n" +
                "divNameAry[0] =\"Sportsbook_T\"" + "\r\n" +
                "divNameAry[1] = \"Event_T\"" + "\r\n" +
                "tableNameAry[0] = \"sumTable_Market\"" + "\r\n" +
                "tableNameAry[1] = \"sumTable_Event\"" + "\r\n" +
                "suiteDiv = document.getElementById(divNameAry[0]);" + "\r\n" +
                //"for(suitLen=0;suitLen<tableNameAry.length;suitLen++)" + "\r\n" +
                //"{" + "\r\n" +
                //"suiteDiv = document.getElementById(divNameAry[suitLen]);" + "\r\n" +
                //"if((suiteDiv)" + "\r\n" +
                //"{" + "\r\n" +
                "retVal = countTC(divNameAry[0])" + "\r\n" +
                "//Updating All th table values in Summary table" + "\r\n" +
                "sumtotal[0] = sumtotal[0] + retVal[0]" + "\r\n" +
                "sumtotal[1] = sumtotal[1] + retVal[1]" + "\r\n" +
                "sumtotal[2] = sumtotal[2] + retVal[2]" + "\r\n" +
                //"updateSumTabl(retVal,tableNameAry[suitLen])" + "\r\n" +
                //"}" + "\r\n" +
                //"}" + "\r\n" +
                //"//Updating Main Summary table value in Summary table" + "\r\n" +
                "updateSumTabl(sumtotal,'sumTable_Summary')" + "\r\n" +
                "}" + "\r\n";


            funcShowDiv = "function showDiv(id)" + "\r\n" +
                "{" + "\r\n" +
                "if (!(id==\"summary_T\"))" + "\r\n" +
                "{" + "\r\n" +
                "countTC(id)" + "\r\n" +
                "}" + "\r\n" +
                "else {" + "\r\n" +
                "findSuite()" + "\r\n" +
                "}" + "\r\n" +
                "var allDiv=new Array(3)" + "\r\n" +
                "allDiv[0]=\"summary_T\"" + "\r\n" +
                "allDiv[1]=\"Sportsbook_T\"" + "\r\n" +
                "allDiv[2]=\"Event_T\"" + "\r\n" +
                "allDiv[3]=\"Oxi_T\"" + "\r\n" +
                "for (i=0;i<allDiv.length;i++ )" + "\r\n" +
                "{" + "\r\n" +
                "if (!(allDiv[i]==id))" + "\r\n" +
                "{" + "\r\n" +
                "objDiv=document.getElementsByTagName(\"div\");" + "\r\n" +
                "for (i=0;i<objDiv.length;i++ )" + "\r\n" +
                "{" + "\r\n" +
                "if ((objDiv[i].id!=id) && ((objDiv[i].id!=\"list\") && (objDiv[i].id!=\"main\")))" + "\r\n" +
                "{" + "\r\n" + "objDiv[i].style.display=\"none\";" + "\r\n" +
                "}" + "\r\n" +
                "}" + "\r\n" +
                "}" + "\r\n" +
                "}" + "\r\n" +
                "objDiv=document.getElementsByTagName(\"div\");" + "\r\n" +
                "for (i=0;i<objDiv.length;i++ )" + "\r\n" +
                "{" + "\r\n" +
                "if (objDiv[i].id==id)" + "\r\n" +
                "{" + "\r\n" +
                "if (!(id==\"summary_T\"))" + "\r\n" +
                "{" + "\r\n" +
                "SuiteSumDiv = document.getElementById(\"Suite_Summary_Div\");" + "\r\n" +
                "SuiteSumDiv.style.display = \"block\";" + "\r\n" +
                "}" + "\r\n" +
                "objDiv[i].style.display=\"block\";" + "\r\n" +
                "}" + "\r\n" +
                "}" + "\r\n" +
                "drawLine()" + "\r\n" +
                "}" + "\r\n" +
                "</script>" + "\r\n";


            //CSS file string
            cssBodyStyle = "<style type=\"text/css\" >" + "\r\n" +
                "html,body{" + "\r\n" +
                "top:0px;" + "\r\n" +
                "padding:0px;" + "\r\n" +
                "margin-top:0px;" + "\r\n" +
                "}" + "\r\n" +
                "div#list" + "\r\n" +
                "{" + "\r\n" +
                "float:left;" + "\r\n" +
                "padding-top: 150px;" + "\r\n" +
                "position: absolute;" + "\r\n" +
                "border-right:5px solid;" + "\r\n" +
                "border-right-style: outset;" + "\r\n" +
                "height:100%;" + "\r\n" +
                "top: 20px;" + "\r\n" +
                "}" + "\r\n" +
                ".body-font{" + "\r\n" +
                "font-family:Calibri, san-serif;" + "\r\n" +
                "font-size:16px;" + "\r\n" +
                "font-weight:bold;" + "\r\n" +
                "color:#000;" + "\r\n" +
                "text-decoration:none;" + "\r\n" +
                "}" + "\r\n" +
                ".menu-font{" + "\r\n" +
                "font-family: arial, san-serif, verdana;" + "\r\n" +
                "font-size:20px;" + "\r\n" +
                "font-weight:bold;" + "\r\n" +
                "color:\"#3300FF\";" + "\r\n" +
                "text-decoration:none;" + "\r\n" +
                "}" + "\r\n" +
                ".header-font{" + "\r\n" +
                "font-family: arial, san-serif, verdana;" + "\r\n" +
                "font-size:12px;" + "\r\n" +
                "font-weight:bold;" + "\r\n" +
                "color:\"#0B2161\";" + "\r\n" +
                "text-decoration:none;" + "\r\n" +
                "}" + "\r\n" +
                "</style>" + "\r\n" +
                "<BODY class=\"body-font\" onload=\"JavaScript:showDiv('summary_T')\">" + "\r\n";

            //Report Header
            ReportHeader = "<TABLE align = \"center\" style=\"color:#FFFFFF; font:Arial, Helvetica, sans-serif; font-style:italic; font-size:36px;\" border=\"0\" >" + "\r\n" +
            "<tr><td width='40%'></td>" + "\r\n" +
            "<td align= \"center\" bgcolor=\"#B00000\"><strong>&nbsp;Test Report&nbsp;</strong></td>" + "\r\n" +
            "<td  width = '40%' align = 'Center'></td>" + "\r\n" +
            "</tr>" + "\r\n" +
            "</TABLE>" + "\r\n";

            //Details Table
            EnvDetailsTable = "<TABLE style=\"color:blue\" class=\"header-font\"  width=\"100%\" border = \"2\">" + "\r\n" +
                "<tr>" + "\r\n" +
                "<TD width = \"33%\" align = \"Center\">" + "EnvironmentConfigFileReader.BuildNumber" + "</TD>" + "\r\n" +
                "<TD width = \"33%\" align = \"Center\">" + "EnvironmentConfigFileReader.FeedProvider" + "</TD>" + "\r\n" +
                "<TD width = \"33%\" align = \"Center\">" + exeDate + "</TD>" + "\r\n" +
                "</tr>" + "\r\n" +
                "</table>" + "\r\n";

            //All the DIV variable of HTML
            divLHN = "<div id=\"list\" >" + "\r\n" +
                "<span id=\"Summary_span\"><a href=\"JavaScript:showDiv('summary_T');\" id=\"Summary_link\"><h2 class=\"menu-font\">Summary</h2></a></span>" +
                   "\r\n" +
                "</div>" + "\r\n" +
                " <br></br>" + "\r\n";

            //Summary Page Table
            divSuiteSummary = "<div id = \"Suite_Summary_Div\" style=\"margin-left:1em; display:none\">" + "\r\n" +
                "<TABLE id=\"Suite_Summary_Table\" align = \"Center\" style= \"border-color: black; border-style: solid;\"   border=\"1\" width=\"350\" face=\"Trebuchet MS\">" + "\r\n" +
                "<tr><th colspan=\"2\"><h3 align = \"Center\" >Summary</h3></th></tr>" + "\r\n" +
                "<tr><td> Total Test Run </td><td> </td></tr>" + "\r\n" +
                "<tr><td> Total Test Pass </td><td> </td></tr>" + "\r\n" +
                "<tr><td> Total Test Fail </td><td>0</td></tr>" + "\r\n" +
                "<tr><td> Pass Percentage </td><td> </td></tr>" + "\r\n" +
                "</TABLE>" + "\r\n" +
                "</div>" + "\r\n";

            //Summary Page Table
            divSuiteSummary = "<div id = \"Suite_Summary_Div\" style=\"margin-left:1em; display:none\">" + "\r\n" +
                "<TABLE id=\"Suite_Summary_Table\" align = \"Center\" style= \"border-color: black; border-style: solid;\"   border=\"1\" width=\"350\" face=\"Trebuchet MS\">" + "\r\n" +
                "<tr><th colspan=\"2\"><h3 align = \"Center\" >Summary</h3></th></tr>" + "\r\n" +
                "<tr><td> Total Test Run </td><td> </td></tr>" + "\r\n" +
                "<tr><td> Total Test Pass </td><td> </td></tr>" + "\r\n" +
                "<tr><td> Total Test Fail </td><td>0</td></tr>" + "\r\n" +
                "<tr><td> Pass Percentage </td><td> </td></tr>" + "\r\n" +
                "</TABLE>" + "\r\n" +
                "</div>" + "\r\n";

            suiteDivVar = "<div id= \"Sportsbook_T\" style=\"margin-left:5%; display:none\">" + "\r\n" +
                "<h1 align=\"center\"></h1><!-- Outer table --><TABLE align = \"center\" style= \"border-color: black; border-style: solid;\"   border=\"1\" width=\"700\" face=\"Trebuchet MS\"><tr><td><h3 align = \"Center\">Web application scenarios</h3></td></tr>" + "\r\n" +
                "<div id=\"button\">" + "\r\n" +
                "<form>" + "\r\n" +
                "<center>" + "\r\n" +
                "<input type=\"button\" style=\"width:60px;height:30px;background:#2F4F4F;border:1px solid black\" value=\"Close\" onClick=\"javascript:window.close();\">" + "\r\n" +
                "</center>" + "\r\n" +
                "</form>" + "\r\n" +
                "</div>" + "\r\n";

            summayrdivVar = "<div id= \"summary_T\" style=\"margin-left:1em; display:none\">" + "\r\n" +
                "<TABLE id = \"sumTable_Summary\" align = \"Center\" style= \"border-color: black; border-style: solid;\"   border=\"1\" width=\"500\" face=\"Trebuchet MS\">" + "\r\n" +
                "<tr><td><h3 align = \"Center\">Summary</h3></td></tr>" + "\r\n" +
                "<tr><td width = \"80%\">Total Test Cases Run </td><td width = \"20%\">" + "Not Run" + "</td></tr>" + "\r\n" +
                "<tr><td width = \"80%\">Total Test Cases Passed</td><td width = \"20%\">" + "Not Run" + "</td></tr>" + "\r\n" +
                "<tr><td width = \"80%\">Total Test Cases Failed</td><td width = \"20%\">" + "Not Run" + "</td></tr>" + "\r\n" +
                "<tr><td width = \"80%\"> Pass Percentage</td><td width = \"20%\">" + "Not Run" + "%" + "</td></tr>" + "\r\n" +
                "</TABLE>" + "\r\n" +
                "</Div>" + "\r\n" +
                "<tr><TD >" + "\r\n" +
                "</tr></TD>" + "\r\n" +
                "<br></br>" + "\r\n";

            htmlFile.Append(HtmlHeader);
            htmlFile.Append(funcDoMenu);
            htmlFile.Append(funcDrawLine);
            htmlFile.Append(funcDispLog);
            htmlFile.Append(funcDispLog1);
            htmlFile.Append(funcCalTime);
            htmlFile.Append(funcCountTC);
            htmlFile.Append(funcUpdateSumTable);
            htmlFile.Append(funcFindSuite);
            htmlFile.Append(funcShowDiv);
            htmlFile.Append(cssBodyStyle);
            //htmlFile.Append(EnvDetailsTable);
            htmlFile.Append(divLHN);
            htmlFile.Append(ReportHeader);
            htmlFile.Append("<div id= \"main\">" + "\r\n");
            htmlFile.Append("<h1 align=\"center\">Automation Report</h1>");
            htmlFile.Append(divSuiteSummary);
            htmlFile.Append(summayrdivVar);
            //htmlFile.Append(suiteDivVar);
            //htmlFile.Append(HtmlHeader);            
            return htmlFile.ToString();
        }
    }
}
