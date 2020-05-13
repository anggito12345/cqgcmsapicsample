INSTRUCTIONS

    1. To open the source code open SourceCode\CmsApiDemo.sln.
       You will need Visual Studio 2012+. You can use free Visual Studio Community 2013 (http://www.visualstudio.com/en-us/news/vs2013-community-vs.aspx)
       with Microsoft .Net Framework 4.5 installed (http://www.microsoft.com/en-us/download/details.aspx?id=30653).

    2. To run the demo you need to fill App.config settings and marked fields in Program.cs with values of your data from https://demo.cqgtrader.com/CAST.

PACKAGE CONTENT

    Sample source code, contains the following projects:

        CmsApiDemo - console Application project that contains code with predefined user input data for CMS API methods;
        CmsApiSamples - class library that contains code of CMS API calls; protocol folder contains generated from the protocol C# classes.
                        Classes from Protocol namespace were generated with Google Protogen tool, which you can find in Google.ProtocolBuffers 2.4.1.555 nuget package (https://www.nuget.org/packages/Google.ProtocolBuffers).
