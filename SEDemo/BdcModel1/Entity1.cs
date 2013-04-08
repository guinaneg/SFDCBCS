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
    public partial class Entity1
    {
        //TODO: Implement additional properties here. The property accountName is just a sample how a property could look like.
        public string accountNumber { get; set; }
        public string accountName { get; set; }
        public string phoneNumber { get; set; }
        public string billingCountry { get; set; }
        public string industry { get; set; }
        public string annualIncome { get; set; }
        public string annualRevenue { get; set; }
        public string description { get; set; }
        public string website { get; set; }
        public string rating { get; set; }
    }
}
