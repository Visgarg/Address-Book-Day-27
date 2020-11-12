alter procedure InsertingData
(
@firstname varchar(30),
@lastname varchar(30),
@address varchar(50),
@city varchar(30),
@state varchar(30),
@zip int,
@phonenumber bigint,
@email varchar(50),
@dateadded varchar(30),
@addressbookname varchar(30)
)
as
Begin
     --set nocount on added to prevent extra result sets from
	 --interfering with select statements
Set nocount on;
begin try
Begin transaction
declare @firstnameexists varchar(30)
declare @lastnameexists varchar(30)
declare @zipexists int
declare @phonenumberexists bigint
declare @cityexists varchar(30)
declare @addressbooknameexists varchar(30)

		     --insert into address book table
			 select @firstnameexists=firstName ,@lastnameexists=lastname,@cityexists=city,@zipexists=zip,@phonenumberexists=phoneNumber
			 from addressbook
			 where firstname=@firstname and lastname=@lastname and address=@address and city=@city and state=@state and zip=@zip and phonenumber=@phonenumber
			 if(@firstnameexists is null and @lastnameexists is null and @zipexists is null)
				insert into addressbook(firstname,lastname,address,city,state,zip,phonenumber,email,dateadded)
				values(@firstname,@lastname,@address,@city,@state,@zip,@phonenumber,@email,convert(date,@dateadded));
			else 
				begin
				Rollback Transaction
				end
			--insert into address book names
			select @addressbooknameexists=addressBookName from AddressBookNames where addressBookName=@addressbookname
			if(@addressbooknameexists is null)
				insert into AddressBookNames(addressBookName) values(@addressbookname);
			
		--if not error, commit transaction 
		commit transaction
		End Try
		Begin catch
		   --if error, roll back changes done by any of the sql queries
		  Rollback transaction
		End catch
End

select * from AddressBook;
select * from addressbooknames;
exec InsertingData 'Apoorva','Garg','Delhi','Hisar','Haryana',125121,8570934858,'akshay.a','2020-01-01','E'
