using Microsoft.VisualStudio.TestTools.UnitTesting;
using AddressBook_ADO.NET;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AddressBookADOTests
{
    [TestClass]
    public class UnitTest1
    {
        //declaring restclient variable
        RestClient client;
        /// <summary>
        /// Setups this instance for the client by giving url along with port.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            client = new RestClient("http://localhost:3000");
        }
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
        /// <summary>
        /// Ons the calling get API return address book. UC22
        /// </summary>
        [TestMethod]
        public void onCallingGetApi_ReturnAddressBook()
        {
            //arrange
            //makes restrequest for getting all the data from json server by giving table name and method.get
            RestRequest request = new RestRequest("/AddressBook", Method.GET);
            //act
            //executing the request using client and saving the result in IrestResponse.
            IRestResponse response = client.Execute(request);
            //assert
            //assert for checking status code of get
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
            //adding the data into list from irestresponse by using deserializing.
            List<AddressBookContactDetails> dataResponse = JsonConvert.DeserializeObject<List<AddressBookContactDetails>>(response.Content);
            //printing out the content for list of address book contact details
            foreach(AddressBookContactDetails contactDetails in dataResponse)
            {
                Console.WriteLine("AddressBookName:- " + contactDetails.addressBookName + " First Name:- " + contactDetails.firstName + " Last Name:- " + contactDetails.lastName + " Address:- " + contactDetails.address + " City:- " + contactDetails.city + " State:- " + contactDetails.state + " Zip:- " + contactDetails.zip + " phone number:- " + contactDetails.phoneNo + " Email:- " + contactDetails.eMail + " Date:-" + contactDetails.dateAdded);
            }
            //adding data in database using threading
            //adding multiple entries
            AddressBookOperations addressBookOperations = new AddressBookOperations();
            addressBookOperations.AddingMultipleContactDetailsUsingThreading(dataResponse);
        }
        /// <summary>
        /// Givens the contact detail on post should be added in json server. UC23
        /// </summary>
        [TestMethod]
        public void givenContactDetail_OnPost_ShouldBeAddedInJsonServer()
        {
            //instatiating object for address book operations
            AddressBookOperations addressBookOperations = new AddressBookOperations();
            //getting list of multiple contacts to be added in json server
            List<AddressBookContactDetails> contactDetails = addressBookOperations.GetAllContactDetails();
            //adding each entry in jsonserver
            contactDetails.ForEach(contact =>
            {
                //arrange
                //adding request to post(add) data
                RestRequest request = new RestRequest("/AddressBook", Method.POST);
                //instatiating jObject for adding data
                JObject jObject = new JObject();
                jObject.Add("firstName", contact.firstName);
                jObject.Add("lastName", contact.lastName);
                jObject.Add("address", contact.address);
                jObject.Add("city", contact.city);
                jObject.Add("state", contact.state);
                jObject.Add("zip", contact.zip);
                jObject.Add("phoneNo", contact.phoneNo);
                jObject.Add("eMail", contact.eMail);
                jObject.Add("addressBookName", contact.addressBookName);
                //as parameters are passed as body hence "request body" call is made, in parameter type
                request.AddParameter("application/json", jObject, ParameterType.RequestBody);
                //Act
                //request contains method of post and along with added parameter which contains data to be added
                //hence response will contain the data which is added and not all the data from jsonserver.
                //data is added to json server json file in this step.
                IRestResponse response = client.Execute(request);
                //assert
                //code will be 201 for posting data
                Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.Created);
                //derserializing object for assert and checking test case
               AddressBookContactDetails dataResponse = JsonConvert.DeserializeObject<AddressBookContactDetails>(response.Content);
                Assert.AreEqual(contact.firstName, dataResponse.firstName);
                Assert.AreEqual(contact.phoneNo, dataResponse.phoneNo);
                Console.WriteLine(response.Content);


            });
        }
        /// <summary>
        /// Givens the contact detail and updated in json server and database. UC24
        /// </summary>
        [TestMethod]
        public void givenContactDetail_updateInJsonServer_andDatabase()
        {
            AddressBookContactDetails contact = new AddressBookContactDetails();
            contact.firstName = "Mahak";
            contact.lastName = "Singla";
            contact.address = "New Grain Market";
            contact.city = "Barwala";
            contact.state = "Haryana";
            contact.zip = 125125;
            contact.phoneNo = 7014245875;
            contact.eMail = "mahak.singla@gmail.com";
            contact.addressBookName = "A";

            //making a request for a particular employee to be updated
            RestRequest request = new RestRequest("AddressBook/4", Method.PUT);
            //creating a jobject for new data to be added in place of old
            //json represents a new json object
            JObject jObject = new JObject();
            jObject.Add("firstName", contact.firstName);
            jObject.Add("lastName", contact.lastName);
            jObject.Add("address", contact.address);
            jObject.Add("city", contact.city);
            jObject.Add("state", contact.state);
            jObject.Add("zip", contact.zip);
            jObject.Add("phoneNo", contact.phoneNo);
            jObject.Add("eMail", contact.eMail);
            jObject.Add("addressBookName", contact.addressBookName);
            //adding parameters in request
            //request body parameter type signifies values added using add.
            request.AddParameter("application/json", jObject, ParameterType.RequestBody);
            //executing request using client
            //IRest response act as a container for the data sent back from api.
            IRestResponse response = client.Execute(request);
            //checking status code of response
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
            //deserializing content added in json file
            AddressBookContactDetails dataResponse = JsonConvert.DeserializeObject<AddressBookContactDetails>(response.Content);
            //asserting for salary
            Assert.AreEqual(dataResponse.address, "New Grain Market");
            //writing content without deserializing from resopnse. 
            Console.WriteLine(response.Content);

            //updating data in database using threading
            AddressBookOperations addressBookOperations = new AddressBookOperations();
            addressBookOperations.UpdateContactDetailsInDataBase(contact);
            //checking if details are updated
            AddressBookContactDetails expected = addressBookOperations.GettingUpdatedDetails(contact);
            Assert.AreEqual(contact, expected);
        }
        /// <summary>
        /// Givens the employee on delete should return success status. UC25
        /// </summary>
        [TestMethod]
        public void GivenEmployee_OnDelete_ShouldReturnSuccessStatus()
        {
            //request for deleting elements from json 
            RestRequest request = new RestRequest("AddressBook/2", Method.DELETE);
            //executing request using rest client
            IRestResponse response = client.Execute(request);
            //console writeline will print null for response content after delete operation
            Console.WriteLine(response.Content);
            //checking status codes.
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
        }
    }

 
}
