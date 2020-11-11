using System;
using System.Collections.Generic;

namespace AddressBook_ADO.NET
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Address Book Problem, ADO.NET use cases");
            //instatiating address book operations
            AddressBookOperations addressBookOperations = new AddressBookOperations();
            //UC16 getting contact details
            try
            {
                List<AddressBookContactDetails> contactDetailsList = addressBookOperations.GetAllContactDetails();
                contactDetailsList.ForEach(contactDetails =>
                {
                    Console.WriteLine("ContactID:- " + contactDetails.contactID + " First Name:- " + contactDetails.firstName + " Last Name:- " + contactDetails.lastName + " Address:- " + contactDetails.address + " City:- " + contactDetails.city + " State:- " + contactDetails.state + " Zip:- " + contactDetails.zip + " phone number:- " + contactDetails.phoneNo + " Email:- " + contactDetails.eMail);
                    Console.WriteLine("address Book Name id: -" + contactDetails.addressBookNameId + " address book name: -" + contactDetails.addressBookName);
                    Console.WriteLine("Type id: -" + contactDetails.typeId+" type name: -" + contactDetails.typeName);
                });
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            //UC18 getting contact details in date range
            //UC19 getting contact details for particular city or state
            Console.WriteLine("*************");
            try
            {
                List<AddressBookContactDetails> contactDetailsListInDateRange = addressBookOperations.GetAllContactDetailsWithConditions();
                contactDetailsListInDateRange.ForEach(contactDetails =>
                {
                    Console.WriteLine("ContactID:- " + contactDetails.contactID + " First Name:- " + contactDetails.firstName + " Last Name:- " + contactDetails.lastName + " Address:- " + contactDetails.address + " City:- " + contactDetails.city + " State:- " + contactDetails.state + " Zip:- " + contactDetails.zip + " phone number:- " + contactDetails.phoneNo + " Email:- " + contactDetails.eMail);
                    //Console.WriteLine("address Book Name id: -" + contactDetails.addressBookNameId + " address book name: -" + contactDetails.addressBookName);
                    //Console.WriteLine("Type id: -" + contactDetails.typeId + " type name: -" + contactDetails.typeName);
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
