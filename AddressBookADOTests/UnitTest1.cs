using Microsoft.VisualStudio.TestTools.UnitTesting;
using AddressBook_ADO.NET;
using System;

namespace AddressBookADOTests
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// Updates the contact details. UC17
        /// </summary>
        /// <returns></returns>
        public AddressBookContactDetails UpdateContactDetails()
        {
            //passing data for updating
            AddressBookContactDetails contactDetails = new AddressBookContactDetails();
            contactDetails.firstName = "Vishal";
            contactDetails.lastName = "Garg";
            contactDetails.address = "Barwala";
            contactDetails.city = "Hisar";
            contactDetails.state = "Haryana";
            contactDetails.zip = 125121;
            contactDetails.phoneNo = 8570934858;
            contactDetails.eMail = "vishal.garg";
            contactDetails.addressBookName = "A";
            //passing data to update method in address book operations
            AddressBookOperations addressBookOperations = new AddressBookOperations();
            bool result = addressBookOperations.UpdateContactDetailsInDataBase(contactDetails);
            Console.WriteLine(result);
            return contactDetails;
        }
        [TestMethod]
        public void CheckingIfContactDetailsAreGettingUpdated()
        {
            //calling update contact details
            //getting actual data
            AddressBookContactDetails actual=  UpdateContactDetails();
            AddressBookContactDetails contactDetails = new AddressBookContactDetails();
            //passing data to get updated contact details
            contactDetails.firstName = "Vishal";
            contactDetails.lastName = "Garg";
            contactDetails.addressBookName = "A";
            AddressBookOperations addressBookOperations = new AddressBookOperations();
            //getting expected data from address book operations -getting updated details
            AddressBookContactDetails expected=  addressBookOperations.GettingUpdatedDetails(contactDetails);
            //assert
            Assert.AreEqual(expected, actual);


        }
    }
 
}
