using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SEDemo.sforce;

namespace SEDemo.BdcModel1
{
    public partial class SalesforceGiftService
    {
        public SforceService binding = null;

        public IEnumerable<Gift> ReadList()
        {
            SalesforceGiftService cla = new SalesforceGiftService();

            List<Gift> giftList = cla.GetGifts();

            Gift[] gList = new Gift[giftList.Count];

            for (int i = 0; i < giftList.Count; i++)
            {
                Gift gift = new Gift();
                gift.GiftID = gList[i].GiftID;
                gift.GiftName = gList[i].GiftName;
                gift.GiftDescription = gList[i].GiftDescription;
                gift.GiftContact = gList[i].GiftContact;
                gift.GiftOrganisation = gList[i].GiftOrganisation;
                gift.GiftStatus = gList[i].GiftStatus;
                gift.GiftValue = gList[i].GiftValue;
                gift.GiftAction = gList[i].GiftAction;
                /*engifttity1.accountNumber = gList[i].accountNumber;
                gift.accountName = gList[i].accountName;
                gift.phoneNumber = gList[i].phoneNumber;
                entity1.billingCountry = gList[i].billingCountry;
                entity1.description = gList[i].description;
                entity1.annualIncome = gList[i].annualIncome;
                entity1.annualRevenue = gList[i].annualRevenue;
                entity1.website = gList[i].website;
                entity1.industry = gList[i].industry;
                entity1.rating = gList[i].rating;
                */
                giftList[i] = gift;
                gift = null;
            }
            return giftList.ToList();
        }

        private QueryResult ExecuteSalesForceQuery(string queryText)
        {
            SFDCUtils utils = new SFDCUtils();

            binding = utils.getSalesforceConnection(binding);

            QueryResult qr;
            binding.QueryOptionsValue = new QueryOptions();
            binding.QueryOptionsValue.batchSize = 250;
            binding.QueryOptionsValue.batchSizeSpecified = true;
            qr = binding.query(queryText);

            return qr;

        }
        public List<Gift> GetGifts()
        {

            List<Gift> giftList = new List<Gift>();
            SFDCUtils utils = new SFDCUtils();

            QueryResult qr = ExecuteSalesForceQuery(utils.ReadConfigurationList("giftssoql"));

            bool cont = true;

            while (cont)
            {
                foreach (sObject record in qr.records)
                {
                    sforce.Gift__c gifts = (sforce.Gift__c)record;

                    Gift gift = new Gift();
                    gift.GiftDescription = gifts.Description__c;
                    gift.GiftID = gifts.Id;
                    gift.GiftName = gifts.Name;
                    gift.GiftContact = gifts.Contact__c;
                    giftList.Add(gift);

                }

                //handle the loop + 1 problem by checking the most recent 

                if (qr.done)
                    cont = false;
                else
                    qr = binding.queryMore(qr.queryLocator);
            }

            binding = null;
            return giftList;

        }

        public static void ReadList(string parameter)
        {
            throw new System.NotImplementedException();
        }
    }
}
