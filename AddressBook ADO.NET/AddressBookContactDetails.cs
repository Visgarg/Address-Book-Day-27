using System;
using System.Collections.Generic;
using System.Text;

namespace AddressBook_ADO.NET
{
    public class AddressBookContactDetails
    {
        public int contactID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public int zip { get; set; }
        public long phoneNo { get; set; }
        public string eMail { get; set; }
        public int addressBookNameId { get; set; }
        public string addressBookName { get; set; }
        public int typeId { get; set; }
        public string typeName { get; set; }

        public override bool Equals(Object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (!(obj is AddressBookContactDetails))
            {
                return false;
            }
            return ((this.firstName == (((AddressBookContactDetails)obj).firstName)) && (this.lastName == (((AddressBookContactDetails)obj).lastName)) && (this.address == (((AddressBookContactDetails)obj).address)) && (this.city == (((AddressBookContactDetails)obj).city)) && (this.state == (((AddressBookContactDetails)obj).state)) && (this.zip == (((AddressBookContactDetails)obj).zip)) && (this.phoneNo == (((AddressBookContactDetails)obj).phoneNo)) && (this.eMail == (((AddressBookContactDetails)obj).eMail)) && (this.addressBookName == (((AddressBookContactDetails)obj).addressBookName)));
        }
        public override int GetHashCode()
        {
            return this.firstName.GetHashCode() ^ this.lastName.GetHashCode() ^ this.address.GetHashCode() ^ this.city.GetHashCode() ^ this.state.GetHashCode() ^ this.zip.GetHashCode() ^ this.phoneNo.GetHashCode() ^ this.eMail.GetHashCode() ^ this.addressBookName.GetHashCode();
        }
    }
}
