Overview
========
Report Viewer reads in Microsoft Report Definition Language (.rdl) files and renders them with WebForms local report viewer.

It automatically generates a page displaying the report and any parameters in the report.

Setup
=====
To run the porject, you must enter the correct connection string into the web.config in DefaultConnection, and set the root directory for the server to look for reports in in the AppSetting ReportPath.

Viewing a Report
================
Navigate to /ReportFile/Get?name=[path to file] to view your report.

ToDo
====
*Add DataTypes Integer, Boolean, Float
*Add Stored Procedures
*Add Parameter choices
*Add Seperation of Report Parameters and Query Parameters
*Add Connection String lookup
*Add Hidden/Internal Parameters
*Allow Multiple Values on Parameters
