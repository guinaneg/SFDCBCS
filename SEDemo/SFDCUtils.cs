using System;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;
using SEDemo.sforce;

namespace SEDemo
{
    class SFDCUtils
    {

        //public SforceService binding = null;

        public SforceService getSalesforceConnection(SforceService binding)
        {
            binding = new SforceService();
            binding.Timeout = 60000;

            LoginResult loginResult = binding.login(ReadConfigurationList("sfdcusername"), ReadConfigurationList("sfdcpassword"));
            binding.Url = loginResult.serverUrl;
            binding.SessionHeaderValue = new SessionHeader
            {
                sessionId = loginResult.sessionId
            };

            return binding;

        }

        public String ReadConfigurationList(String configVar)
        {
            String pathToSite = "http://sp2010";
            String nameOfWeb = "/";
            String listName = "Configuration";
            String retVal = null;


            // Use using to make sure resources are released properly  
            using (SPSite oSite = new SPSite(pathToSite))
            {

                using (SPWeb oWeb = oSite.AllWebs[nameOfWeb])
                {
                    // Alternately you can use oSite.RootWeb if you want to access the main site  

                    SPList oList = oWeb.Lists[listName];  // The display name, ie. "Calendar"  
                    //String firstVar = (String)oItem["Variable Name"];
                    SPListItemCollection items = oList.GetItems("Title", "Value");
                    SPListItem item = null;

                    foreach (SPListItem oItem in oList.Items)
                    {
                        // Access each item in the list...  

                        foreach (SPListItem it in items)
                        {
                            if (it.Title == configVar)
                            {
                                retVal = (String)it["Value"];
                            }
                        }
                    }

                }
                return retVal;
            }

        }
    }


}
