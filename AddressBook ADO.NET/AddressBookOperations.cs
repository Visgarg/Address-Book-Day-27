using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
        public List<AddressBookContactDetails> GetAllContactDetailsWithConditions(int task)
        {
            //defining list for adding data
            List<AddressBookContactDetails> contactDetailsList = new List<AddressBookContactDetails>();
            //getting sql connection
            SqlConnection connection = dBConnection.GetConnection();
            string query="0";
            //using connection, if available
            try
            {
                using (connection)
                {
                    if (task == 1)
                    {
                        //sql command using stored procedure
                        //for particular date range
                         query= "select * from addressbook where dateadded between cast('2019-01-01' as date) and getdate()";
                    }
                    if (task == 2)
                    {
                        //for particular state
                         query= "select * from addressbook where state='Karnataka'";
                    }
                    if (task == 3)
                    {
                        //for particular city
                        query= "select * from addressbook where city='Hisar'";
                    }
                    //command.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlCommand command = new SqlCommand(query, connection);
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
                            contactDetails.contactID = dr.GetInt32(8);
                            contactDetails.dateAdded = dr.GetDateTime(9);
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
        /// <summary>
        /// Addings the contact details in database. UC20
        /// </summary>
        /// <param name="contactDetails">The contact details.</param>
        /// <returns></returns>
        public bool AddingContactDetailsInDatabase(AddressBookContactDetails contactDetails)
        {
            //getting the connection
            SqlConnection connection = dBConnection.GetConnection();
            try
            {
                //if connection is valid, then commands are executed
                using(connection)
                {
                    SqlCommand command = new SqlCommand();
                    //stored procedure is added in command text.
                    command.CommandText = "InsertingData";
                    command.Connection = connection;
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@firstname", contactDetails.firstName);
                    command.Parameters.AddWithValue("@lastname", contactDetails.lastName);
                    command.Parameters.AddWithValue("@address", contactDetails.address);
                    command.Parameters.AddWithValue("@city", contactDetails.city);
                    command.Parameters.AddWithValue("@state", contactDetails.state);
                    command.Parameters.AddWithValue("@zip", contactDetails.zip);
                    command.Parameters.AddWithValue("@phonenumber", contactDetails.phoneNo);
                    command.Parameters.AddWithValue("@email", contactDetails.eMail);
                    command.Parameters.AddWithValue("@dateadded", contactDetails.dateAdded);
                    command.Parameters.AddWithValue("@addressbookname", contactDetails.addressBookName);
                    connection.Open();
                    //executing query for adding data
                    int result = command.ExecuteNonQuery();
                    if(result!=0)
                    {
                        return true;
                    }

                    return false;
                }
                
            }
            //catching exception
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            //finally block to close the connection
            //this will execute, even if there is no error for catching exception.
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }
        /// <summary>
        /// Addings the multiple contact details using threading. UC21
        /// </summary>
        /// <param name="contactDetails">The contact details.</param>
        public void AddingMultipleContactDetailsUsingThreading(List<AddressBookContactDetails> contactDetails)
        {
            //whenever thread is used for multi threading, then multi threads are generated
            //whenever task is used then only one thread works.
            // but task is used for generating multi threads, specifically one thread for each task, but this is not happening in task.
            //but it is happening in thread library.
            contactDetails.ForEach(contactData =>
            {
                //using different thread for each contact data for faster adding of data into database.
                Thread thread = new Thread(()=>
                {
                    Console.WriteLine("Address being added" + contactData.firstName);
                    AddingContactDetailsInDatabase(contactData);
                    //prints id for each thread
                    Console.WriteLine("Thread id: " + Thread.CurrentThread.ManagedThreadId);
                    Console.WriteLine("Contact added:" + contactData.firstName);
                });
                //starting a thread
                thread.Start();
                //thread.join-allows one thread to wait until another thread completes its execution. 
                thread.Join();

            });
        }
    }
}
