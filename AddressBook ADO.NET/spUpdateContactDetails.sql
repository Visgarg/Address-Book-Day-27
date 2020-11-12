alter procedure spUpdateContactDetails
(
@firstname varchar(30),
@lastname varchar(30),
@address varchar(50),
@city varchar(30),
@state varchar(30),
@zip int,
@phonenumber bigint,
@email varchar(50),
@addressbookname varchar(30)
)
as
begin
update a
set a.address= @address,a.city=@city,a.state= @state,a.zip=@zip,a.phoneNumber=@phonenumber,a.eMail= @email
from AddressBook a
 join addressbookMapper b 
on a.contactid= b.contactid
 join AddressBookNames c
on c.addressBookId = b.addressbookid
where a.firstName=@firstname and a.lastName=@lastname and c.addressBookName= @addressbookname
end

Select a.firstname,a.lastname,a.address,a.city,a.state,a.zip,a.phonenumber,a.email,c.addressbookname from addressbook a join addressbookmapper b on a.contactid=b.contactid join addressbooknames c on c.addressbookid=b.addressbookid where a.firstname='Vishal' and a.lastname='Garg' and c.addressbookname='A'