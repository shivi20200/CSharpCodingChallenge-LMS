create database LoanManagementSystem;

use LoanManagementSystem;
go

CREATE TABLE Customer (
    CustomerID INT PRIMARY KEY,  
    Name NVARCHAR(100),               
    EmailAddress NVARCHAR(100) UNIQUE ,
    PhoneNumber NVARCHAR(15),         
    Address NVARCHAR(255),                     
    CreditScore INT CHECK (CreditScore BETWEEN 300 AND 850) 
);

CREATE TABLE Loan (
    LoanID INT PRIMARY KEY IDENTITY(1,1),      
    CustomerID INT,                            
    PrincipalAmount DECIMAL(18, 2) ,   
    InterestRate DECIMAL(5, 2) ,       
    LoanTerm INT NOT NULL CHECK (LoanTerm > 0),
    LoanType NVARCHAR(50) CHECK (LoanType IN ('CarLoan', 'HomeLoan')) , 
    LoanStatus NVARCHAR(50) CHECK (LoanStatus IN ('Pending', 'Approved')), 
    CONSTRAINT FK_CustomerLoan FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID) 
);

CREATE TABLE HomeLoan (
    LoanId INT PRIMARY KEY FOREIGN KEY REFERENCES Loan(LoanId),
    PropertyAddress NVARCHAR(255),
    PropertyValue INT
);


CREATE TABLE CarLoan (
    LoanId INT PRIMARY KEY FOREIGN KEY REFERENCES Loan(LoanId),
    CarModel NVARCHAR(100),
    CarValue INT
);