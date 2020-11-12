use AddressBookServiceDataBase;

create Procedure spGetAllContacts
as
Begin
select a.contactid,a.firstName,a.lastName,a.address,a.city,a.state,a.zip,a.phoneNumber,a.email,c.addressBookId,c.addressBookName,e.typeid,e.typename
from addressbook a
join addressbookMapper b
on a.contactid= b.contactid
join AddressBookNames c
on b.addressbookid= c.addressBookId
join typeMapper d
on d.contactid= a.contactid
join TypesOfContacts e
on e.typeid= d.typeid
end
exec spGetAllContacts

exec spupdatecontactdetails 'Vishal','Garg','Anaj Mandi Barwala','Hisar','Haryana',125121,8607533666,'vishal.garg@capgemini.com','A'
