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
    public class SalesforceContactService
    {
        private static SforceService binding = null;

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

           
        public List<Contact> GetContacts()

        {
            List<Contact> contacts = new List<Contact>();
            SFDCUtils utils = new SFDCUtils();
            QueryResult qr = ExecuteSalesForceQuery(string.Format(utils.ReadConfigurationList("contactssoql")));
            
            bool cont = true;

            while (cont)
    
            {
                foreach (sObject record in qr.records)
                {
                    sforce.Contact contact = (sforce.Contact)record;
                   
                    if (contact!= null )
                    {
                        try
                        {
                            contacts.Add(new Contact
                            {
                                contactID = contact.Id,
                                contactAccountID = contact.AccountId,
                                contactName = contact.Name,
                                contactPhone = contact.Phone,
                                contactCompany = contact.Account.Name,
                               // contactPicture = contact.Notes__c,
                                contactEmail = contact.Email,
                                contactTitle = contact.Title,
                                contactDepartment = contact.Department,
                                contactAssistantPhone = contact.AssistantPhone,
                                contactAssistantName = contact.AssistantName,
                                contactLeadSource  =contact.LeadSource,
                                contactPostCode = contact.LeadSource

                            });
                        }
                        catch (Exception e)
                        {
                            System.Console.Out.Write("ff");
                        }
                    }
                }

                //handle the loop + 1 problem by checking the most recent queryResult

                if (qr.done)
                    cont = false;
                else
                    qr = binding.queryMore(qr.queryLocator);
            }
            binding = null;

            return contacts;
        }


        public Contact GetContactById(string contactId)

        {
            Contact contact = new Contact();
            SFDCUtils utils = new SFDCUtils ();

            QueryResult qr = ExecuteSalesForceQuery(string.Format(utils.ReadConfigurationList("contactssoql" )+ " where Id = '{0}'", contactId));

            sforce.Contact sContact = (sforce.Contact)qr.records[0];

            contact.contactID = sContact.Id;
            contact.contactAccountID = sContact.AccountId;
            contact.contactName = sContact.Name;
            contact.contactPhone = sContact.Phone;
            //contact.contactPicture = sContact.Notes__c;
            contact.contactAssistantName = sContact.AssistantName;
            contact.contactAssistantPhone = sContact.AssistantPhone;
            contact.contactDepartment = sContact.Department;
            contact.contactEmail = sContact.Email;
            contact.contactLeadSource = sContact.LeadSource;
            contact.contactPostCode = sContact.MailingPostalCode;
            contact.contactTitle = sContact.Title;
            contact.mailingcountry = sContact.MailingCountry;
            contact.mailingState = sContact.MailingState;
            return contact;
        }


        public List<string> GetContactIds()

        {
            List<string> contactIds = new List<string>();
            QueryResult qr = ExecuteSalesForceQuery("Select Id From Contact");

            bool cont = true;

            while (cont)

            {

                foreach (sObject record in qr.records)

                {
                    sforce.Contact contact = (sforce.Contact)record;
                    contactIds.Add(contact.Id);
                }

                //handle the loop + 1 problem by checking the most recent queryResult
                if (qr.done)
                    cont = false;
                else
                    qr = binding.queryMore(qr.queryLocator);
            }

            binding = null;
            return contactIds;
        }

        public List<Contact> GetContactsByAccountId(string accountId)
        {
            List<Contact> contacts = new List<Contact>();
            SFDCUtils utils = new SFDCUtils();

            QueryResult qr = ExecuteSalesForceQuery(string.Format(utils.ReadConfigurationList("contactssoql") + " where AccountId = '{0}'", accountId));

            bool cont = true;

            while (cont)

            {

                foreach (sObject record in qr.records)

                {

                    sforce.Contact contact = (sforce.Contact)record;

                    contacts.Add(new Contact {
                        contactID = contact.Id,
                        contactAccountID = contact.AccountId,
                        contactName = contact.Name,
                        contactPhone = contact.Phone,
                        contactCompany = contact.Account.Name,
                        //contactPicture = contact.Notes__c,
                        contactDepartment = contact.Department,
                        contactAssistantName = contact.AssistantName,
                        contactTitle =contact.Title,
                        contactLeadSource = contact.LeadSource,
                        contactEmail = contact.Email,
                        contactPostCode = contact.MailingPostalCode,
                        contactAssistantPhone = contact.AssistantPhone
                    });

                }

                //handle the loop + 1 problem by checking the most recent queryResult
               if (qr.done)
                    cont = false;
               else
                    qr = binding.queryMore(qr.queryLocator);
               }

           binding = null;
           return contacts;

        }

        public static Contact ReadItem(String id)
        {
            SalesforceContactService cla = new SalesforceContactService();
            Contact entity1 = new Contact();

            Contact contact = cla.GetContactById(id);
            entity1.contactAccountID = contact.contactAccountID;
            entity1.contactName = contact.contactName;
            entity1.contactPhone = contact.contactPhone;
            entity1.contactID = contact.contactID;
            entity1.contactCompany = contact.contactCompany;
            entity1.contactPicture = contact.contactPicture;
            entity1.contactAssistantName = contact.contactAssistantName;
            entity1.contactAssistantPhone = contact.contactAssistantPhone;
            entity1.contactDepartment = contact.contactDepartment;
            entity1.contactEmail = contact.contactEmail;
            entity1.contactLeadSource = contact.contactLeadSource;
            entity1.contactPostCode = contact.contactPostCode;
            entity1.contactTitle = contact.contactTitle;
            entity1.mailingcountry = contact.mailingcountry;
            entity1.mailingState = contact.mailingState;
            return entity1;
        }

        public static IEnumerable<Contact> ReadList()
        {
            SalesforceContactService cla = new SalesforceContactService();
            //List<Account> GetAccounts()
            List<Contact> contactList = cla.GetContacts();

            Contact[] entityList = new Contact[contactList.Count];
            for (int i = 0; i < contactList.Count; i++)
            {
                Contact entity1 = new Contact();
                if (contactList[i].contactAccountID == null) {
                    entity1.contactAccountID = "999999999999";
                } else {
                    entity1.contactAccountID = contactList[i].contactAccountID;
                
                }
                entity1.contactName = contactList[i].contactName;
                entity1.contactPhone = contactList[i].contactPhone;
                if (contactList[i].contactID != null)
                {
                    entity1.contactID = contactList[i].contactID;
                }
                else
                {
                    entity1.contactID = "988888888888";
                }
                entity1.contactCompany = contactList[i].contactCompany;
                entity1.contactPicture = contactList[i].contactPicture;
                entity1.contactTitle = contactList[i].contactTitle;
                entity1.mailingcountry = contactList[i].mailingcountry;
                entity1.mailingStreet = contactList[i].mailingState;
                entity1.mailingState = contactList[i].mailingState;
                entity1.contactPostCode = contactList[i].contactPostCode;
                entity1.contactLeadSource = contactList[i].contactLeadSource;
                entity1.contactDepartment = contactList[i].contactDepartment;
                entity1.contactAssistantName = contactList[i].contactAssistantName;
                entity1.contactAssistantPhone = contactList[i].contactAssistantPhone;
                entity1.contactEmail = contactList[i].contactEmail;
                entityList[i] = entity1;
                entity1 = null;
            }
            return entityList;
        }

        public static IEnumerable<Entity1> SalesforceContactToSalesforceAccount(string contactID)
        {
            throw new System.NotImplementedException();
        }

        public static void Update(Contact salesforceContact)
        {
            
            Contact contact = new Contact();
            SalesforceContactService sService = new SalesforceContactService(); 
            SFDCUtils  utils = new SFDCUtils();

            //contact = sService.GetContactById(salesforceContact.contactID.ToString());
            //contact.contactName = "HollyMoly";

            sforce.Contact sfc = new sforce.Contact();
            //Contact contact = new Contact();

            QueryResult qr = sService.ExecuteSalesForceQuery(string.Format(utils.ReadConfigurationList("updatecontactssoql") + " where Id = '{0}'", salesforceContact.contactID));

            sforce.Contact sContact = (sforce.Contact)qr.records[0];

            sContact.Id = salesforceContact.contactID;
            sContact.AccountId = salesforceContact.contactAccountID;
            //sContact.Name = "Gavin Francis";
            //sContact.LastName = salesforceContact.la;
            //sContact.Contact_Location__c = "woo";
           
            sContact.Phone = salesforceContact.contactPhone;
            sContact.AssistantName = salesforceContact.contactAssistantName;
            sContact.AssistantPhone = salesforceContact.contactAssistantPhone;
            sContact.Department = salesforceContact.contactDepartment;
            sContact.Email = salesforceContact.contactEmail;
            sContact.LeadSource = salesforceContact.contactLeadSource;
            sContact.MailingPostalCode = salesforceContact.contactPostCode;
            sContact.Title = salesforceContact.contactTitle;
            sContact.MailingCountry = salesforceContact.mailingcountry;
            sContact.MailingState = salesforceContact.mailingState;
            //return contact;
            //sfc.retrieve();
            SaveResult[] sr = binding.update(new sObject[] { sContact });
            //SaveResult[] sr = binding.create(new sObject[] { cg });
        }

  

    }
}
