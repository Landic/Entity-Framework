Create Database AuthorAndBooks;
Go

Use AuthorAndBooks;
Go

Create Table Authors (
    ID int Primary Key Identity(1,1),
    Name nvarchar(max) NOT NULL
);
Go

Create Table Books (
    ID int Primary Key Identity(1,1),
    Name nvarchar(max) NOT NULL,
    AuthorID int,
    Foreign Key (AuthorID) References Authors(ID)
);
Go


Insert Into Authors (Name) Values
('George Orwell'),
('J.K. Rowling'),
('J.R.R. Tolkien'),
('Agatha Christie'),
('Stephen King'),
('Jane Austen'),
('Mark Twain'),
('Ernest Hemingway'),
('F. Scott Fitzgerald'),
('Isaac Asimov');
Go

Insert Into Books (Name, AuthorID) Values
('1984', 1),
('Animal Farm', 1),
('Harry Potter and the Sorcerer''s Stone', 2),
('The Hobbit', 3),
('Murder on the Orient Express', 4),
('The Shining', 5),
('Pride and Prejudice', 6),
('The Adventures of Tom Sawyer', 7),
('The Old Man and the Sea', 8),
('The Great Gatsby', 9),
('Foundation', 10);
Go