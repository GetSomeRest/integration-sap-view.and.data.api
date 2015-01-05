
#SAP Integration sample


##Description

*This sample is part of the [Developer-Autodesk/Autodesk-View-and-Data-API-Samples](https://github.com/Developer-Autodesk/autodesk-view-and-data-api-samples) repository.*

An ASP.Net MVC website sample that integrates the Autodesk View and Data API with an SAP database using the [SAP Gateway for Microsoft (GWM)](http://scn.sap.com/docs/DOC-47563) technology. The sample demonstrates:
* Displaying data from an SAP database in a custom panel when a model element is selected.
* Selecting a model element when an item in a separate tree view is clicked.
* Updating an SAP data item and redisplaying the data in the model.

##Dependencies

This sample has a dependency on [Developer-Autodesk/library-dotnet](https://github.com/Developer-Autodesk/library-dotnet). You will also need access to an SAP database and SAP GWM.

##Setup/Usage Instructions


* You need to have an SAP developer account to be able to run that sample. You can request an SAP account there: http://scn.sap.com/community/developer-center
* Fill up the Credentials.cs class with valid credentials obtained by creating your Cloud Application at https://developer.autodesk.com/
* Fill up the Credentials.cs class with your SAP username and password
* Build the sample
* Deploy in IIS with method of your choice


## License

This sample is licensed under the terms of the [MIT License](http://opensource.org/licenses/MIT). Please see the [LICENSE](LICENSE) file for full details.

##Written by 

[Philippe Leefsma](http://adndevblog.typepad.com/cloud_and_mobile/philippe-leefsma.html)
