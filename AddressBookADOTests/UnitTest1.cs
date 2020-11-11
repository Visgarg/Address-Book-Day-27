using Microsoft.VisualStudio.TestTools.UnitTesting;
using AddressBook_ADO.NET;
using System;
using System.Collections.Generic;
using System.Diagnostics;

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
        /// <summary>
        /// Checkings if contact details are getting updated. UC17
        /// </summary>
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
        /// <summary>
        /// Checkings for getting contact details in particular time range. UC18
        /// </summary>
        [TestMethod]
        public void CheckingForGettingContactDetailsInParticularTimeRange()
        {
            //creating list for expected output
            List<AddressBookContactDetails> contactDetailsExpected = new List<AddressBookContactDetails>();
            //adding data
            contactDetailsExpected.Add(new AddressBookContactDetails { firstName = "abc", lastName = "xyz", address = "pqr", city = "Bangalore", state = "Karnataka", zip = 123456, phoneNo = 9419494949, eMail = "abc.xyz" });
            //instatiating object for address book operations
            AddressBookOperations addressBookOperations = new AddressBookOperations();
            //getting actual contact list from address book operations-getting contact details from particular date range
            List<AddressBookContactDetails> contactDetailsActual = addressBookOperations.GetAllContactDetailsWithConditions(1);
            //assert for comparing list
            CollectionAssert.AreEqual(contactDetailsActual, contactDetailsExpected);
        }
        /// <summary>
        /// Checkings for getting contact details for particular state. UC19
        /// </summary>
        [TestMethod]
        public void CheckingForGettingContactDetailsForParticularState()
        {
            //creating list for expected output
            List<AddressBookContactDetails> contactDetailsExpected = new List<AddressBookContactDetails>();
            //adding data
            contactDetailsExpected.Add(new AddressBookContactDetails { firstName = "abc", lastName = "xyz", address = "pqr", city = "Bangalore", state = "Karnataka", zip = 123456, phoneNo = 9419494949, eMail = "abc.xyz" });
            //instatiating object for address book operations
            AddressBookOperations addressBookOperations = new AddressBookOperations();
            //getting actual contact list from address book operations-getting contact details from particular date range
            List<AddressBookContactDetails> contactDetailsActual = addressBookOperations.GetAllContactDetailsWithConditions(2);
            //assert for comparing list
            CollectionAssert.AreEqual(contactDetailsActual, contactDetailsExpected);
        }
        /// <summary>
        /// Checkings for getting contact details for particular city. UC19
        /// </summary>
        [TestMethod]
        public void CheckingForGettingContactDetailsForParticularCity()
        {
            //creating list for expected output
            List<AddressBookContactDetails> contactDetailsExpected = new List<AddressBookContactDetails>();
            //adding data
            contactDetailsExpected.Add(new AddressBookContactDetails { firstName = "Vishal", lastName = "Garg", address = "Barwala", city = "Hisar", state = "Haryana", zip = 125121, phoneNo = 8570934858, eMail = "vishal.garg" });
            contactDetailsExpected.Add(new AddressBookContactDetails { firstName = "Mahak", lastName = "Singla", address = "address", city = "Hisar", state = "Haryana", zip = 125001, phoneNo = 9494949494, eMail = "mahak.singla" });

            //instatiating object for address book operations
            AddressBookOperations addressBookOperations = new AddressBookOperations();
            //getting actual contact list from address book operations-getting contact details from particular date range
            List<AddressBookContactDetails> contactDetailsActual = addressBookOperations.GetAllContactDetailsWithConditions(3);
            //assert for comparing list
            CollectionAssert.AreEqual(contactDetailsActual, contactDetailsExpected);
        }
        /// <summary>
        /// Multiple contacts to be added in the list by threading
        /// </summary>
        /// <returns></returns>
        public List<AddressBookContactDetails> MultipleContactsToBeAddedInList()
        {
            List<AddressBookContactDetails> contactDetails = new List<AddressBookContactDetails>();
            contactDetails.Add(new AddressBookContactDetails { firstName = "Joey", lastName = "Sidhar", address = "urban estate", city = "Noida", state = "UP", zip = 125124, phoneNo = 8528528525, eMail = "teenasidhar@gmail.com", dateAdded = Convert.ToDateTime("2016-01-01"),addressBookName= "E" });
            contactDetails.Add(new AddressBookContactDetails { firstName = "peter", lastName = "Sehrawat", address = "Model Town", city = "Hyderabad", state = "Telangana", zip = 124424, phoneNo = 7568459855, eMail = "chelsysehrawat@gmail.com", dateAdded = Convert.ToDateTime("2016-01-01"), addressBookName = "E" });
            contactDetails.Add(new AddressBookContactDetails { firstName = "Michael", lastName = "Jain", address = "Main Road", city = "Kolkota", state = "West Bengal", zip = 125144, phoneNo = 7539514566, eMail = "muditjain@gmail.com", dateAdded = Convert.ToDateTime("2016-01-01"), addressBookName = "F" });
            contactDetails.Add(new AddressBookContactDetails { firstName = "Sindhu", lastName = "Goyal", address = "Ring Road", city = "Chennai", state = "Tamilnadu", zip = 125184, phoneNo = 9638257895, eMail = "vineetgoyal@gmail.com", dateAdded = Convert.ToDateTime("2016-01-01"), addressBookName = "F" });
            return contactDetails;
        }
        /// <summary>
        /// Addings the multiple contacts into data base using threading. UC21
        /// </summary>
        [TestMethod]
        public void AddingMultipleContactsIntoDataBaseUsingThreading()
        {
            //instatiating address book operations object
            AddressBookOperations addressBookOperations = new AddressBookOperations();
            //getting data from multiple contacts to be added in list method
            List<AddressBookContactDetails> contactDetails = MultipleContactsToBeAddedInList();
            //starting stopwatch
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            //executing method of adding multiple contacts using threading 
            addressBookOperations.AddingMultipleContactDetailsUsingThreading(contactDetails);
            stopwatch.Stop();
            Console.WriteLine("Elapsed Time: " + stopwatch.Elapsed);
            
        }
    }
 
}
