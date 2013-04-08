using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SEDemo.BdcModel1
{
    /// <summary>
    /// This class contains the properties for The . The properties keep the data for Entity1.
    /// If you want to rename the class, don't forget to rename the entity in the model xml as well.
    /// </summary>
    public partial class Gift
    {
        public string GiftID { get; set; }
        public string GiftName { get; set; }
        public string GiftValue { get; set; }
        public string GiftAction { get; set; }
        public string GiftDescription { get; set; }
        public string GiftContact { get; set; }
        public string GiftOrganisation { get; set; }
        public string GiftType { get; set; }
        public string GiftStatus { get; set; }
    }
}

