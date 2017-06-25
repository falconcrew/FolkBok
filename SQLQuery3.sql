insert into Invoices (Description, Date, Address, YourReference, OurReference) values ('Testing', '2017-06-20', 'Test\nTestvägen 2\222 22 Lund', 'Olov Ferm', 'Test Testsson')

insert into Lines (Description, Date, Amount) values ('Testing', '2017-06-04', 5000)
insert into Lines (Description, Date, Amount) values ('Testing2', '2017-06-04', 200)

insert into InvoiceLines (Invoice_ID, Line_ID) values ((select ID from Invoices where ID=1),(select ID from Lines where ID=1))
insert into InvoiceLines (Invoice_ID, Line_ID) values ((select ID from Invoices where ID=1),(select ID from Lines where ID=2))