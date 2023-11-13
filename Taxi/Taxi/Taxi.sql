CREATE DATABASE Taxi_park

CREATE TABLE Client
(
Client_id NVARCHAR(3) PRIMARY KEY,
Phone NVARCHAR(40) NOT NULL,
[User_Id] INT UNIQUE FOREIGN KEY ([User_Id]) REFERENCES [User] ([User_Id]) ON DELETE CASCADE
)

CREATE TABLE Taxi
(
Taxi_id INT PRIMARY KEY IDENTITY(1,1),
Automobile_id INT FOREIGN KEY (Automobile_id) REFERENCES Automobile (Automobile_id) ON DELETE CASCADE NOT NULL,
[User_Id] INT UNIQUE FOREIGN KEY ([User_Id]) REFERENCES [User] ([User_Id]) ON DELETE CASCADE
)

CREATE TABLE Automobile
(
Automobile_id INT PRIMARY KEY IDENTITY(1,1),
Model NVARCHAR(30) NOT NULL,
Number_automobile NVARCHAR(9) NOT NULL,
Color NVARCHAR(30) NOT NULL
)

CREATE TABLE [Order]
(
Order_id INT PRIMARY KEY IDENTITY(1,1),
Client_id NVARCHAR(3) FOREIGN KEY (Client_id) REFERENCES Client (Client_id) ON DELETE CASCADE NOT NULL,
Taxi_id INT FOREIGN KEY (Taxi_id) REFERENCES Taxi (Taxi_id) ON DELETE NO ACTION,
Price MONEY NOT NULL,
StartAddress NVARCHAR(40) NOT NULL,
EndAddress NVARCHAR(40) NOT NULL,
Status_id INT FOREIGN KEY (Status_id) REFERENCES [Status] (Status_id) ON DELETE NO ACTION,
Start_order DATETIME NOT NULL DEFAULT GETDATE(),
End_order DATETIME NOT NULL
)

CREATE TABLE [Status]
(
Status_id INT PRIMARY KEY IDENTITY(1,1),
Status_name NVARCHAR(40)
)

CREATE TABLE [User]
(
[User_Id] INT PRIMARY KEY IDENTITY(1,1),
[Login] NVARCHAR(15) NOT NULL,
[Password] NVARCHAR(15) NOT NULL,
[Name] NVARCHAR(40) NOT NULL,
Last_name NVARCHAR(40) NOT NULL,
Second_name NVARCHAR(40) NOT NULL,
Role_id INT FOREIGN KEY (Role_id) REFERENCES [Role] (Role_id)
)

CREATE TABLE [Role]
(
Role_id INT PRIMARY KEY IDENTITY(1,1),
Role_name NVARCHAR(7)
)

SELECT [Login],[Password] FROM [User]

select * FROM Taxi

SELECT * FROM [User]

SELECT [Login],[Password] FROM [User] 
WHERE [Login] = @login