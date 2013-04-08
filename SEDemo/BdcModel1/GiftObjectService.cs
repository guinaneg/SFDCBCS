using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text;
using SEDemo.sforce;

namespace SEDemo.BdcModel1
{
    public partial class GiftObjectService
    {
        public SforceService binding = null;

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
        public static List<Gift> ReadList()
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
        public Gift__c GetGiftById(String GiftID)
        {
            Gift account = new Gift();
            SFDCUtils utils = new SFDCUtils();

            QueryResult qr = ExecuteSalesForceQuery(string.Format(utils.ReadConfigurationList("giftssoql") + " WHERE Id = '{0}'", GiftID));

            sforce.Gift__c sAccount = (sforce.Gift__c)qr.records[0];
            /*
            account.accountNumber = sAccount.Id;
            account.accountName = sAccount.Name;
            account.phoneNumber = sAccount.Phone;
            account.billingCountry = sAccount.BillingCountry;
            account.description = sAccount.Description;
            account.industry = sAccount.Industry;
            //account.rating = sAccount.Rating;
            account.website = sAccount.Website;
            //account.annualIncome = sAccount.Annual_Income__c.ToString();
            account.annualRevenue = sAccount.AnnualRevenue.ToString();
            */
            binding = null;

            return sAccount;

        }   
        public static Gift__c ReadItem(String GiftID)
        {
            GiftObjectService cla = new GiftObjectService();
            Gift entity1 = new Gift();

            Gift__c gift1 = cla.GetGiftById(GiftID);

            /*entity1.accountNumber = account.Id;
            entity1.accountName = account.Name;
            entity1.phoneNumber = account.Phone;
            entity1.billingCountry = account.BillingCountry;
            entity1.description = account.Description;
            //entity1.annualIncome = account.Annual_Income__c.ToString();
            entity1.annualRevenue = account.AnnualRevenue.ToString();
            entity1.website = account.Website;
            entity1.industry = account.Industry;
            // entity1.rating = account.Rating;
             * */
            return gift1;
        }
    }
}
