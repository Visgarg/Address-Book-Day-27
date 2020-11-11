alter table addressbook
alter column dateAdded date;

select * from addressbook;

update AddressBook
set dateAdded= Cast('2020-09-18' as date)
where firstname='Vishal';
update AddressBook
set dateAdded= Cast('2018-09-18' as date)
where firstname='Mahak';
update AddressBook
set dateAdded= Cast('2019-09-18' as date)
where firstname='abc';

select * from addressbook where dateadded between cast('2019-01-01' as date) and cast('2020-01-01' as date)