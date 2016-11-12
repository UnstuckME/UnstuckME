USE UnstuckME_DB;
GO
--Make users
INSERT INTO UserProfile
	(DisplayFName, DisplayLName, EmailAddress, UserPassword, Privileges)
	VALUES
	('Ryan', 'Bell', 'Ryan.Bell@oit.edu', 'ThisISMyPassword', 1111);
INSERT INTO UserProfile
	(DisplayFName, DisplayLName, EmailAddress, UserPassword, Privileges)
	VALUES
	('Aruthor', 'Clark', 'AJ.Clark@oit.edu', '192@$#330-//dwd\wd', 1011);
INSERT INTO UserProfile
(DisplayFName, DisplayLName, EmailAddress, UserPassword, Privileges)
	VALUES
	('Matthew', 'Clark', 'Matt.Clark@oit.edu', 'ilikebigbuttsandicannotlie', 1000);
INSERT INTO UserProfile
	(DisplayFName, DisplayLName, EmailAddress, UserPassword, Privileges)
	VALUES
	('Kronn', 'Morgan', 'Kyronn.Morgan@oit.edu', '"~`\''565456!@#$%^&*((((', 0010);
INSERT INTO UserProfile
	(DisplayFName, DisplayLName, EmailAddress, UserPassword, Privileges)
	VALUES
	('Jacob', 'Arnt', 'Jacob.Arnt@oit.edu', 'JacobArnt', 0110);
INSERT INTO UserProfile
	(DisplayFName, DisplayLName, EmailAddress, UserPassword, Privileges)
	VALUES
	('Taylor', 'Jones', 'Taulor.Jones@oit.edu', 'password', 11101);

SELECT *
FROM UserProfile

--Add Profile images to each user
insert into Picture(UserID, Photo) values (1, CONVERT(varbinary(MAX), 'vulputate nonummy maecenas tincidunt lacus at velit vivamus vel nulla eget'));
insert into Picture (UserID, Photo) values (3, CONVERT(varbinary(MAX), 'primis in faucibus orci luctus et ultrices posuere cubilia curae'));
insert into Picture (UserID, Photo) values (5, CONVERT(varbinary(MAX), 'elementum ligula vehicula consequat morbi a ipsum integer a nibh in quis justo'));
insert into Picture (UserID, Photo) values (6, CONVERT(varbinary(MAX), 'diam erat fermentum justo nec condimentum neque sapien placerat ante nulla justo aliquam quis'));

SELECT *
FROM Picture

--Insert Fake Files
insert into Files (ChatID, FileData) values (7, CONVERT(varbinary(MAX),'dui maecenas tristique est et tempus semper est quam pharetra magna ac consequat metus sapien ut nunc'));
insert into Files (ChatID, FileData) values (7, CONVERT(varbinary(MAX),'sit amet turpis elementum ligula vehicula consequat morbi a ipsum integer a nibh in quis justo maecenas rhoncus aliquam'));
insert into Files (ChatID, FileData) values (6, CONVERT(varbinary(MAX),'non mauris morbi non lectus aliquam sit amet diam in magna bibendum imperdiet'));
insert into Files (ChatID, FileData) values (3, CONVERT(varbinary(MAX),'interdum in ante vestibulum ante ipsum primis in faucibus orci luctus et'));
insert into Files (ChatID, FileData) values (1, CONVERT(varbinary(MAX),'sodales scelerisque mauris sit amet eros suspendisse accumsan tortor quis turpis sed ante vivamus tortor duis mattis egestas'));
insert into Files (ChatID, FileData) values (8, CONVERT(varbinary(MAX),'ut dolor morbi vel lectus in quam fringilla rhoncus mauris enim leo rhoncus'));
insert into Files (ChatID, FileData) values (3, CONVERT(varbinary(MAX),'duis aliquam convallis nunc proin at turpis a pede posuere nonummy integer non velit'));
insert into Files (ChatID, FileData) values (3, CONVERT(varbinary(MAX),'blandit ultrices enim lorem ipsum dolor sit amet consectetuer adipiscing elit proin interdum'));
insert into Files (ChatID, FileData) values (3, CONVERT(varbinary(MAX),'cubilia curae donec pharetra magna vestibulum aliquet ultrices erat tortor sollicitudin mi sit amet lobortis sapien sapien non mi integer'));
insert into Files (ChatID, FileData) values (3, CONVERT(varbinary(MAX),'nisi at nibh in hac habitasse platea dictumst aliquam augue'));
insert into Files (ChatID, FileData) values (6, CONVERT(varbinary(MAX),'lectus in quam fringilla rhoncus mauris enim leo rhoncus sed vestibulum sit amet cursus id turpis integer aliquet massa id'));
insert into Files (ChatID, FileData) values (6, CONVERT(varbinary(MAX),'ligula nec sem duis aliquam convallis nunc proin at turpis'));
insert into Files (ChatID, FileData) values (8, CONVERT(varbinary(MAX),'tincidunt lacus at velit vivamus vel nulla eget eros elementum pellentesque'));
insert into Files (ChatID, FileData) values (1, CONVERT(varbinary(MAX),'orci vehicula condimentum curabitur in libero ut massa volutpat convallis morbi odio odio'));
insert into Files (ChatID, FileData) values (8, CONVERT(varbinary(MAX),'eros vestibulum ac est lacinia nisi venenatis tristique fusce congue diam id ornare imperdiet sapien urna pretium nisl ut'));
insert into Files (ChatID, FileData) values (3, CONVERT(varbinary(MAX),'volutpat eleifend donec ut dolor morbi vel lectus in quam fringilla rhoncus mauris enim leo'));
insert into Files (ChatID, FileData) values (1, CONVERT(varbinary(MAX),'nibh fusce lacus purus aliquet at feugiat non pretium quis lectus suspendisse potenti in eleifend quam'));
insert into Files (ChatID, FileData) values (4, CONVERT(varbinary(MAX),'libero rutrum ac lobortis vel dapibus at diam nam tristique tortor eu'));
insert into Files (ChatID, FileData) values (8, CONVERT(varbinary(MAX),'felis sed interdum venenatis turpis enim blandit mi in porttitor pede justo eu massa donec'));
insert into Files (ChatID, FileData) values (8, CONVERT(varbinary(MAX),'molestie hendrerit at vulputate vitae nisl aenean lectus pellentesque eget nunc donec quis'));

--8 New chats
INSERT INTO CHAT
DEFAULT VALUES
INSERT INTO CHAT
DEFAULT VALUES
INSERT INTO CHAT
DEFAULT VALUES
INSERT INTO CHAT
DEFAULT VALUES
INSERT INTO CHAT
DEFAULT VALUES
INSERT INTO CHAT
DEFAULT VALUES
INSERT INTO CHAT
DEFAULT VALUES
INSERT INTO CHAT
DEFAULT VALUES

SELECT *
FROM Chat

--Om to userprofile
SELECT * FROM UserProfile
SELECT * FROM OfficialMentor

SELECT * FROM OmToUser
ORDER BY UserId


INSERT INTO OmToUser 
	(UserID, MentorID)
	VALUES 
	(1,1)

INSERT INTO OmToUser 
	(UserID, MentorID)
	VALUES 
	(1,2)

INSERT INTO OmToUser 
	(UserID, MentorID)
	VALUES 
	(1,3)

INSERT INTO OmToUser 
	(UserID, MentorID)
	VALUES 
	(2,2)

INSERT INTO OmToUser 
(UserID, MentorID)
VALUES 
(3,1)

INSERT INTO OmToUser 
(UserID, MentorID)
VALUES 
(4,3)

INSERT INTO OmToUser 
(UserID, MentorID)
VALUES 
(4,2)

INSERT INTO OmToUser 
(UserID, MentorID)
VALUES 
(4,1)

INSERT INTO OmToUser 
(UserID, MentorID)
VALUES 
(6,1)

INSERT INTO OmToUser 
(UserID, MentorID)
VALUES 
(6,2)

SELECT * From UserProfile
SELECT * FROM Classes
SELECT * FROM UserToClass

--Cordinate users with classes
INSERT INTO UserToClass (UserID, ClassID) VALUES (1, 6);
INSERT INTO UserToClass (UserID, ClassID) VALUES (4, 3);
INSERT INTO UserToClass (UserID, ClassID) VALUES (5, 5);
INSERT INTO UserToClass (UserID, ClassID) VALUES (3, 8);
INSERT INTO UserToClass (UserID, ClassID) VALUES (4, 7);
INSERT INTO UserToClass (UserID, ClassID) VALUES (2, 1);
INSERT INTO UserToClass (UserID, ClassID) VALUES (2, 9);
INSERT INTO UserToClass (UserID, ClassID) VALUES (3, 7);
INSERT INTO UserToClass (UserID, ClassID) VALUES (5, 1);
INSERT INTO UserToClass (UserID, ClassID) VALUES (3, 8);
INSERT INTO UserToClass (UserID, ClassID) VALUES (4, 1);
INSERT INTO UserToClass (UserID, ClassID) VALUES (5, 1);
INSERT INTO UserToClass (UserID, ClassID) VALUES (2, 7);
INSERT INTO UserToClass (UserID, ClassID) VALUES (1, 1);
INSERT INTO UserToClass (UserID, ClassID) VALUES (1, 2);
INSERT INTO UserToClass (UserID, ClassID) VALUES (5, 6);
INSERT INTO UserToClass (UserID, ClassID) VALUES (2, 3);
INSERT INTO UserToClass (UserID, ClassID) VALUES (4, 5);
INSERT INTO UserToClass (UserID, ClassID) VALUES (6, 7);
INSERT INTO UserToClass (UserID, ClassID) VALUES (2, 9);
INSERT INTO UserToClass (UserID, ClassID) VALUES (3, 2);
INSERT INTO UserToClass (UserID, ClassID) VALUES (3, 8);
INSERT INTO UserToClass (UserID, ClassID) VALUES (5, 4);
INSERT INTO UserToClass (UserID, ClassID) VALUES (3, 7);
INSERT INTO UserToClass (UserID, ClassID) VALUES (3, 7);
INSERT INTO UserToClass (UserID, ClassID) VALUES (6, 4);
INSERT INTO UserToClass (UserID, ClassID) VALUES (5, 2);
INSERT INTO UserToClass (UserID, ClassID) VALUES (5, 7);
INSERT INTO UserToClass (UserID, ClassID) VALUES (6, 7);
INSERT INTO UserToClass (UserID, ClassID) VALUES (2, 8);
INSERT INTO UserToClass (UserID, ClassID) VALUES (2, 7);
INSERT INTO UserToClass (UserID, ClassID) VALUES (2, 8);
INSERT INTO UserToClass (UserID, ClassID) VALUES (4, 1);
INSERT INTO UserToClass (UserID, ClassID) VALUES (4, 3);
INSERT INTO UserToClass (UserID, ClassID) VALUES (1, 6);
INSERT INTO UserToClass (UserID, ClassID) VALUES (2, 8);
INSERT INTO UserToClass (UserID, ClassID) VALUES (3, 4);
INSERT INTO UserToClass (UserID, ClassID) VALUES (1, 6);
INSERT INTO UserToClass (UserID, ClassID) VALUES (1, 1);
INSERT INTO UserToClass (UserID, ClassID) VALUES (4, 6);
INSERT INTO UserToClass (UserID, ClassID) VALUES (1, 2);
INSERT INTO UserToClass (UserID, ClassID) VALUES (6, 5);
INSERT INTO UserToClass (UserID, ClassID) VALUES (2, 3);
INSERT INTO UserToClass (UserID, ClassID) VALUES (6, 8);
INSERT INTO UserToClass (UserID, ClassID) VALUES (4, 1);
INSERT INTO UserToClass (UserID, ClassID) VALUES (5, 2);
INSERT INTO UserToClass (UserID, ClassID) VALUES (2, 5);
INSERT INTO UserToClass (UserID, ClassID) VALUES (3, 5);
INSERT INTO UserToClass (UserID, ClassID) VALUES (5, 7);
INSERT INTO UserToClass (UserID, ClassID) VALUES (6, 9);


