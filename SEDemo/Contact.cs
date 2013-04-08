using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SEDemo.BdcModel1
{
    /// <summary>
    /// This class contains the properties for Entity1. The properties keep the data for Entity1.
    /// If you want to rename the class, don't forget to rename the entity in the model xml as well.
    /// </summary>
    public partial class Contact
    {
        //TODO: Implement additional properties here. The property accountName is just a sample how a property could look like.
        public string contactID { get; set; }
        public string contactName { get; set; }
        public string contactPhone { get; set; }
        public string contactAccountID { get; set; }
        public string contactCompany { get; set; }
        public string contactPicture { get; set; }
        public string mailingcountry { get; set; }
        public string mailingState { get; set; }
        public string mailingStreet { get; set; }
        public string contactEmail { get; set; }
        public string contactDepartment { get; set; }
        public string contactTitle { get; set; }
        public string contactLeadSource { get; set; }
        public string contactAssistantName { get; set; }
        public string contactAssistantPhone { get; set; }
        public string contactPostCode { get; set; }
    }
}

