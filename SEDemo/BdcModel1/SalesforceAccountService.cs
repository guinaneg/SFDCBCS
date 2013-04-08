using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SEDemo.sforce;

namespace SEDemo.BdcModel1
{
    /// <summary>
    /// All the methods for retrieving, updating and deleting data are implemented in this class file.
    /// The samples below show the finder and specific finder method for Entity1.
    /// </summary>
    public class SalesforceAccountService
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

        public Account GetAccountById(string accountId)
        {

            Entity1 account = new Entity1();
            SFDCUtils utils = new SFDCUtils();

            QueryResult qr = ExecuteSalesForceQuery(string.Format(utils.ReadConfigurationList("accountssoql") + " WHERE Id = '{0}'", accountId));

            sforce.Account sAccount = (sforce.Account)qr.records[0];

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
            binding = null;

            return sAccount;

        }
        /// <summary>
        /// This is a sample specific finder method for Entity1.
        /// If you want to delete or rename the method think about changing the xml in the BDC model file as well.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Entity1</returns>
        public static Entity1 ReadItem(string id)
        {
            SalesforceAccountService cla = new SalesforceAccountService();
            Entity1 entity1 = new Entity1();

            Account account = cla.GetAccountById(id);
            entity1.accountNumber = account.Id;
            entity1.accountName = account.Name;
            entity1.phoneNumber = account.Phone;
            entity1.billingCountry = account.BillingCountry;
            entity1.description = account.Description;
            //entity1.annualIncome = account.Annual_Income__c.ToString();
            entity1.annualRevenue = account.AnnualRevenue.ToString();
            entity1.website = account.Website;
            entity1.industry = account.Industry;
           // entity1.rating = account.Rating;
            return entity1;
        }
        /// <summary>
        /// This is a sample finder method for Entity1.
        /// If you want to delete or rename the method think about changing the xml in the BDC model file as well.
        /// </summary>
        /// <returns>IEnumerable of Entities</returns>
        public static List<Entity1> ReadList()
        {

            SalesforceAccountService cla = new SalesforceAccountService();
            //List<Account> GetAccounts()
            List<Entity1> accountList = cla.GetAccounts();

            Entity1[] entityList = new Entity1[accountList.Count];
            for (int i = 0; i < accountList.Count; i++)
            {
                Entity1 entity1 = new Entity1();
                entity1.accountNumber = accountList[i].accountNumber;
                entity1.accountName = accountList[i].accountName;
                entity1.phoneNumber = accountList[i].phoneNumber;
                entity1.billingCountry = accountList[i].billingCountry;
                entity1.description = accountList[i].description;
                entity1.annualIncome = accountList[i].annualIncome;
                entity1.annualRevenue = accountList[i].annualRevenue;
                entity1.website = accountList[i].website;
                entity1.industry = accountList[i].industry;
                entity1.rating = accountList[i].rating;
                entityList[i] = entity1;
                entity1 = null;
            }
            return entityList.ToList();
        }

        public List<Entity1> GetAccounts()
        {

            List<Entity1> accounts = new List<Entity1>();
            SFDCUtils utils = new SFDCUtils();

            QueryResult qr = ExecuteSalesForceQuery(utils.ReadConfigurationList("accountssoql"));

            bool cont = true;

            while (cont)
            {
                foreach (sObject record in qr.records)
                {
                    sforce.Account account = (sforce.Account)record;

                    Entity1 acc = new Entity1();
                    acc.accountNumber = account.Id;
                    acc.accountName = account.Name;
                    acc.phoneNumber = account.Phone;
                    acc.billingCountry = account.BillingCountry;
                    acc.description = account.Description;
                    acc.industry = account.Industry;
                   // acc.rating = account.Rating;
                    acc.website = account.Website;


                    if (account.AnnualRevenue != null)
                    {
                        acc.annualRevenue = account.AnnualRevenue.ToString();
                    }
                    else
                    {
                        acc.annualRevenue = "0";
                    }
                    accounts.Add(acc);

                }

                //handle the loop + 1 problem by checking the most recent 

                if (qr.done)
                    cont = false;
                else
                    qr = binding.queryMore(qr.queryLocator);
            }

            binding = null;
            return accounts;

        }

        public static void Update(String accountId)
        {
            throw new System.NotImplementedException();
        }

        public static IEnumerable<Contact> SalesforceAccountToSalesforceContact(string accountNumber)
        {
            SalesforceAccountService cla = new SalesforceAccountService();
            Entity1 entity1 = new Entity1();
            SFDCUtils utils = new SFDCUtils();


            Account account = cla.GetAccountById(accountNumber);
            List<Contact> contacts = new List<Contact>();

            QueryResult qr = cla.ExecuteSalesForceQuery(string.Format(utils.ReadConfigurationList("contactssoql") + " where Account.Id = '{0}'", accountNumber));

            bool cont = true;
            while (cont)
            {
                foreach (sObject record in qr.records)
                {
                    sforce.Contact contact = (sforce.Contact)record;
                    contacts.Add(new Contact
                    {
                        contactID = contact.Id,
                        contactAccountID = contact.AccountId,
                        contactName = contact.Name,
                        contactPhone = contact.Phone,
                        contactCompany=""
                    });
                }

                //handle the loop + 1 problem by checking the most recent queryResult

                if (qr.done)

                    cont = false;
                else

                    cont = true;               
                }
                return contacts;
            }

        }
    }

