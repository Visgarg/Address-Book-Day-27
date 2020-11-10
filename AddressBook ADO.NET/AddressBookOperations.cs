using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace AddressBook_ADO.NET
{
    class AddressBookOperations
    {
        //making object of DBConnection
        DBConnection dBConnection = new DBConnection();
        /// <summary>
        /// Gets all contact details. UC16
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception">No data found in the database</exception>
        public List<AddressBookContactDetails> GetAllContactDetails()
        {
            //defining list for adding data
            List<AddressBookContactDetails> contactDetailsList = new List<AddressBookContactDetails>();
            //getting sql connection
            SqlConnection connection = dBConnection.GetConnection();
            //using connection, if available
            try
            {
                using (connection)
                {
                    //sql command using stored procedure
                    SqlCommand command = new SqlCommand("spGetAllContacts", connection);
                    connection.Open();
                    //sql data reader class for reading data 
                    SqlDataReader dr = command.ExecuteReader();
                    //executes if rows are there in database tables
                    if (dr.HasRows)
                    {
                        //iterates until data is read across rows
                        while (dr.Read())
                        {
                            //saving data in contact details object
                            AddressBookContactDetails contactDetails = new AddressBookContactDetails();
                            contactDetails.contactID = dr.GetInt32(0);
                            contactDetails.firstName = dr.GetString(1);
                            contactDetails.lastName = dr.GetString(2);
                            contactDetails.address = dr.GetString(3);
                            contactDetails.city = dr.GetString(4);
                            contactDetails.state = dr.GetString(5);
                            contactDetails.zip = dr.GetInt32(6);
                            contactDetails.phoneNo = dr.GetInt64(7);
                            contactDetails.eMail = dr.GetString(8);
                            contactDetails.addressBookNameId = dr.GetInt32(9);
                            contactDetails.addressBookName = dr.GetString(10);
                            contactDetails.typeId = dr.GetInt32(11);
                            contactDetails.typeName = dr.GetString(12);
                            //adding details in contact details list
                            contactDetailsList.Add(contactDetails);
                        }
                        //closing execute reader connection
                        dr.Close();
                        //closing sql connection
                        connection.Close();
                        //returns list
                        return contactDetailsList;
                    }
                    else
                    {
                        throw new Exception("No data found in the database");
                    }
                }
            }
            //catching up exception
            catch(Exception ex)
            {
                
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
