using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace AddressBook_ADO.NET
{
    public class AddressBookOperations
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
                    command.CommandType = System.Data.CommandType.StoredProcedure;
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
        /// <summary>
        /// Updates the contact details in data base. UC17
        /// </summary>
        /// <param name="contactDetails">The contact details.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool UpdateContactDetailsInDataBase(AddressBookContactDetails contactDetails)
        {
            //getting sql connection
            SqlConnection connection = dBConnection.GetConnection();
            //using connection, if available
            try
            {
                using (connection)
                {
                    //stored procedure for updating details using multiple tables and join statement
                    SqlCommand command = new SqlCommand("spUpdateContactDetails", connection);
                    //passing data about where condition and setting different variables using parmaeters.addwithvalue
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@firstname", contactDetails.firstName);
                    command.Parameters.AddWithValue("@lastname", contactDetails.lastName);
                    command.Parameters.AddWithValue("@address", contactDetails.address);
                    command.Parameters.AddWithValue("@city", contactDetails.city);
                    command.Parameters.AddWithValue("@state", contactDetails.state);
                    command.Parameters.AddWithValue("@zip", contactDetails.zip);
                    command.Parameters.AddWithValue("@phonenumber", contactDetails.phoneNo);
                    command.Parameters.AddWithValue("@email", contactDetails.eMail);
                    command.Parameters.AddWithValue("@addressbookname", contactDetails.addressBookName);
                    connection.Open();
                    //result contain no of affected rows as Execute Non Query gives no of affected rows after query
                    int result = command.ExecuteNonQuery();
                    connection.Close();
                    if (result != 0)
                    {
                        return true;
                    }
                    return false;


                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        /// <summary>
        /// Gettings the updated details. UC17
        /// </summary>
        /// <param name="contact">Address book contact details for selecting required data.</param>
        /// <returns></returns>
        /// <exception cref="Exception">No data found in the database</exception>
        public AddressBookContactDetails GettingUpdatedDetails(AddressBookContactDetails contact)
        {
            //defining list for adding data
            //List<AddressBookContactDetails> contactDetailsList = new List<AddressBookContactDetails>();
            //getting sql connection
            SqlConnection connection = dBConnection.GetConnection();
            //using connection, if available
            try
            {
                using (connection)
                {
                    string query = "Select a.firstname,a.lastname,a.address,a.city,a.state,a.zip,a.phonenumber,a.email,c.addressbookname from addressbook a join addressbookmapper b on a.contactid=b.contactid join addressbooknames c on c.addressbookid=b.addressbookid where a.firstname=@firstname and a.lastname=@lastname and c.addressbookname=@addressbookname";
                    //sql command using stored procedure
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@firstname", contact.firstName);
                    command.Parameters.AddWithValue("@lastname", contact.lastName);
                    command.Parameters.AddWithValue("@addressbookname", contact.addressBookName);
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
                            contactDetails.firstName = dr.GetString(0);
                            contactDetails.lastName = dr.GetString(1);
                            contactDetails.address = dr.GetString(2);
                            contactDetails.city = dr.GetString(3);
                            contactDetails.state = dr.GetString(4);
                            contactDetails.zip = dr.GetInt32(5);
                            contactDetails.phoneNo = dr.GetInt64(6);
                            contactDetails.eMail = dr.GetString(7);
                            contactDetails.addressBookName = dr.GetString(8);
                            //adding details in contact details list
                            //contactDetailsList.Add(contactDetails);
                            return contactDetails;
                        }
                        //closing execute reader connection
                        dr.Close();
                        //closing sql connection
                        connection.Close();
                        return null;
                        //returns list
                        
                    }
                    else
                    {
                        throw new Exception("No data found in the database");
                    }
                }
            }
            //catching up exception
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return null;
            }

        }
        /// <summary>
        /// Gets all contact details with conditions.
        /// for particular date range UC18
        /// for particular state or city UC19
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception">
        /// No data found in the database
        /// or
        /// </exception>
        public List<AddressBookContactDetails> GetAllContactDetailsWithConditions()
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
                    //for particular date range
                    //SqlCommand command = new SqlCommand("select * from addressbook where dateadded between cast('2019-01-01' as date) and cast('2020-01-01' as date)", connection);
                    //for particular state
                    //SqlCommand command = new SqlCommand("select * from addressbook where state='Karnataka'", connection);
                    //for particular city
                    SqlCommand command = new SqlCommand("select * from addressbook where city='Hisar'", connection);
                    //command.CommandType = System.Data.CommandType.StoredProcedure;
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
                            contactDetails.firstName = dr.GetString(0);
                            contactDetails.lastName = dr.GetString(1);
                            contactDetails.address = dr.GetString(2);
                            contactDetails.city = dr.GetString(3);
                            contactDetails.state = dr.GetString(4);
                            contactDetails.zip = dr.GetInt32(5);
                            contactDetails.phoneNo = dr.GetInt64(6);
                            contactDetails.eMail = dr.GetString(7);
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
