CREATE TABLE Accounts (
	[Number] INT  NOT NULL PRIMARY KEY,
	[Name] VARCHAR(64),
	[Balance] DECIMAL
)

GO

CREATE TABLE Vouchers (
	ID INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	Number INT NOT NULL,
	Date DATE,
	Description TEXT
)

GO

CREATE TABLE Lines (
	ID INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	Account INT,
	Debet DECIMAL,
	Kredit DECIMAL,
	Amount DECIMAL,
	Date DATE,
	Description TEXT
)

GO

CREATE TABLE VoucherLines (
	Voucher_ID INT NOT NULL FOREIGN KEY REFERENCES Vouchers(ID),
	Line_ID INT NOT NULL FOREIGN KEY REFERENCES Lines(ID)
)

GO

CREATE TABLE Invoices (
	ID INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	Name VARCHAR(30),
	Description TEXT,
	Date DATE,
	Address TEXT,
	YourReference VARCHAR(40),
	OurReference VARCHAR(40)
)

GO

CREATE TABLE InvoiceLines (
	Invoice_ID INT NOT NULL FOREIGN KEY REFERENCES Invoices(ID),
	Line_ID INT NOT NULL FOREIGN KEY REFERENCES Lines(ID)
)

GO

CREATE TABLE Global (
	VoucherNumber INT,
	InvoiceNumber INT,
	PaymentTerm INT,
    PenaltyInterest DECIMAL,
    Address TEXT,
    Name VARCHAR(40),
    PhoneNumber VARCHAR(20),
    Email VARCHAR(50),
    OrgNumber VARCHAR(20),
    FSkatt BIT,
    Bankgiro VARCHAR(20)
)