create table Chair
(
	ID smallint IDENTITY PRIMARY KEY,
	Name char(25) NOT NULL UNIQUE
);

create table Student
(
	ID smallint IDENTITY PRIMARY KEY,
	Surname nchar(20) NOT NULL,
	Name varchar(20) NOT NULL,
	Patronymic char(20) NULL,
	Residence_address char(50) NOT NULL,
	Registration_address char(50) NULL,
	Gradebook_number char(6) NOT NULL,
	ID_group smallint NOT NULL,
	Another_city char(3) DEFAULT 'Нет' CHECK (Another_city IN ('Нет', 'Да')),
	Date_of_birth date, 
	Age AS YEAR(GETDATE())-YEAR(Date_of_birth)
);

create table Session
(
	ID smallint IDENTITY PRIMARY KEY,
	Mark tinyint CHECK (Mark IN (5, 4, 3, 2)), 
	Exam_date date NOT NULL, 
	ID_student smallint NOT NULL, 
	ID_subject smallint NOT NULL
);

create table Subjects
(
	ID smallint IDENTITY PRIMARY KEY,
	Name char(25) NOT NULL UNIQUE, 
	ID_lecturer smallint NOT NULL
);

create table Groups
(
	ID smallint IDENTITY PRIMARY KEY,
	Name char(25) NOT NULL UNIQUE,
	Year int NOT NULL CHECK ( Year > 0 AND Year < YEAR(GETDATE())  ),
	ID_chair smallint NOT NULL,
);

create table Lecturers
(
	ID smallint IDENTITY PRIMARY KEY,
	Full_name char(25) NOT NULL UNIQUE,
	Post char(25) NOT NULL
);

ALTER TABLE Student ADD CONSTRAINT FK_Student FOREIGN KEY (ID_group)  REFERENCES Groups (ID);
ALTER TABLE Session ADD CONSTRAINT FK_Session1 FOREIGN KEY (ID_student) REFERENCES Student (ID);
ALTER TABLE Session ADD CONSTRAINT FK_Session2 FOREIGN KEY (ID_subject) REFERENCES Subjects (ID);
ALTER TABLE Subjects ADD CONSTRAINT FK_Subjects FOREIGN KEY (ID_lecturer) REFERENCES Lecturers (ID);
ALTER TABLE Groups ADD CONSTRAINT FK_Groups FOREIGN KEY (ID_chair)  REFERENCES Chair (ID);

select * from Chair;
select * from Student;
select * from Session;
select * from Subjects;
select * from Groups;
select * from Lecturers;
select * from Student left join Groups on Student.ID_group = Groups.ID;

ALTER TABLE Student DROP CONSTRAINT FK_Student;
ALTER TABLE Session DROP CONSTRAINT FK_Session1;
ALTER TABLE Session DROP CONSTRAINT FK_Session2;
ALTER TABLE Subjects DROP CONSTRAINT FK_Subjects;
ALTER TABLE Groups DROP CONSTRAINT FK_Groups;

DROP TABLE Chair;
DROP TABLE Student;
DROP TABLE Session;
DROP TABLE Subjects;
DROP TABLE Groups;
DROP TABLE Lecturers;

INSERT INTO Lecturers VALUES ('Иванов В. В.', 'Доцент'), ('Петров И. В.', 'Доцент'), ('Сидоров В. Д.', 'Доцент'),('Павлова И. И.', 'Доцент'),
('Николаев Л. В.', 'Доцент'), ('Иванова И. Т.', 'Доцент'), ('Петрова М. М.', 'Доцент'),('Сидорова Л. С.', 'Доцент'),
('Павлов Д. С.', 'Доцент'), ('Николаева В. В.', 'Доцент'), ('Никитин Ф. И.', 'Профессор'),('Никитина З. Т.', 'Профессор'),
('Шевченко И. К.', 'Профессор'), ('Литвинчук М. М.', 'Профессор'), ('Воронков П. А.', 'Профессор'),('Марданов Ю. М.', 'Профессор'),
('Писковой И. А.', 'Профессор'), ('Гурский Д. М.', 'Профессор'), ('Подкин Т. И.', 'Профессор'),('Богачева Д. В.', 'Профессор');

INSERT INTO Chair (Name) VALUES ('ИБ'), ('КБ'), ('ИБАС'), ('ЭБ'), ('Экономика'), ('Физика'), ('Тех. физика'), ('МиР'), ('Лингвистика'), ('Математика'), ('Химия'), ('География'),
('Биология'), ('Пр. инф.'), ('Психология'), ('Менеджмент'), ('Социология'), ('Юриспруденция'), ('Пед. обр.'), ('Межд. отн.');
 
INSERT INTO Groups VALUES ('19.01-1', 2019, 1), ('19.01-2', 2019, 2), ('19.02-1', 2019, 3), ('19.02-2', 2019, 4),
('18.01-1', 2018, 5), ('18.01-2', 2018, 6), ('18.02-1', 2018, 7), ('18.02-2', 2018, 8),
('17.01-1', 2017, 9), ('17.01-2', 2017, 10), ('17.02-1', 2017, 11), ('17.02-2', 2017, 12),
('16.01-1', 2016, 13), ('16.01-2', 2016, 14), ('16.02-1', 2016, 15), ('16.02-2', 2016, 16),
('15.01-1', 2015, 17), ('15.01-2', 2015, 18), ('15.02-1', 2015, 19), ('15.02-2', 2015, 20);

INSERT INTO Subjects VALUES ('ЯП', 1), ('СУБД', 2), ('АОС', 3), ('РиЗВП', 4), ('Физ-ра', 5), ('Эл. гражданин', 6), ('Криптография', 7), ('Логика', 8), ('ОИЭ', 9), ('ТиМП', 10),
('Выш. мат.', 11), ('ЦК', 12), ('РиМ', 13), ('ЭВМиВС', 14), ('УП', 15), ('Англ. яз.', 16), ('ВТВиМС', 17), ('Философия', 18), ('ЧиОМ', 19), ('ПСА', 20);

INSERT INTO Student (Surname, Name, Patronymic, Residence_address, Registration_address, Gradebook_number, ID_group, Another_city, Date_of_birth) VALUES 
('Петров', 'Алексей', 'Вадимович', 'г. Тюмень, ул. Ленина 6', 'г. Тюмень, ул. Ленина 6', '29-35', 1, 'Нет', null),
('Нигматуллин', 'Сергей', 'Иванович', 'г. Тюмень, ул. Обдорская 3', 'г. Тюмень, ул. Обдорская 3', '34-35', 2, 'Нет', null),
('Протасов', 'Иван', 'Сергеевич', 'г. Тюмень, ул. Мельникайте 26', 'г. Тюмень, ул. Мельникайте 26', '28-35', 3, 'Нет', null),
('Костромин', 'Алексей', 'Вадимович', 'г. Тюмень, ул. Мельникайте 62', 'г. Тюмень, ул. Ленина 46', '29-75', 4, 'Нет', null),
('Нефедов', 'Никита', 'Иванович', 'г. Тюмень, ул. Московский тракт 10', 'г. Тюмень, ул. Червишевский тракт 12', '09-35', 5, 'Нет', null),
('Шишкин', 'Владимир', 'Николаевич', 'г. Тюмень, ул. А.Бушуева 1', 'г. Тюмень, ул. А.Бушуева 1', '29-78', 6, 'Нет', null),
('Долженицын', 'Алексей', 'Вадимович', 'г. Тюмень, ул. Ленина 36', 'г. Тюмень, ул. Ленина 36', '78-35', 7, 'Нет', null),
('Юрченко', 'Алексей', 'Иванович', 'г. Тюмень, ул. Перекопская 15', 'г. Тюмень, ул. Перекопская 15', '89-35', 8, 'Нет', null),
('Жаворонков', 'Сергей', 'Сергеевич', 'г. Тюмень, ул. Ленина 6', 'г. Тюмень, ул. Ленина 6', '26-36', 9, 'Нет', null),
('Чернявская', 'Вероника', 'Ивановна', 'г. Тюмень, ул. Высоцкого 15', 'г. Тюмень, ул. Ленина 6', '26-85', 10, 'Нет', null),
('Зейналова', 'Зейнаб', 'Муслюмовна', 'г. Тюмень, ул. Мельникайте 48', null, '34-35', 11, 'Нет', '02-01-2001'),
('Таранухина', 'София', 'Сергеевна', 'г. Тюмень, ул. Московский тракт 40', null, '78-95', 12, 'Нет', '02-01-1999'),
('Муфазалова', 'Элина', 'Ильдаровна', 'г. Тюмень, ул. Высоцкого 30', null, '89-24', 13, 'Нет', '02-01-2002'),
('Гарханова', 'Юлия', 'Николаевна', 'г. Тюмень, ул. Ленина 7', null, '17-69', 14, 'Нет', '02-01-1998'),
('Зарипова', 'Владимир', 'Сергеевич', 'г. Тюмень, ул. Обдорская 7', null, '29-35', 15, 'Нет', '02-01-2000'),
('Буйкевич', 'Сергей', 'Иванович', 'г. Тюмень, ул. А.Бушуева 2', null, '89-30', 16, 'Нет', '02-01-2001'),
('Глебов', 'Никита', 'Иванович', 'г. Тюмень, ул. Обдорская 5', null, '79-09', 17, 'Нет', '02-01-1999'),
('Пономарева', 'Яна', 'Владимировна', 'г. Тюмень, ул. Мельникайте 10', null, '45-35', 18, 'Нет', '02-01-2001'),
('Сидоренко', 'Анастасия', 'Геннадьевна', 'г. Тюмень, ул. Московский тракт 23', null, '29-89', 19, 'Нет', '02-01-2002'),
('Савельева', 'Светлана', 'Игоревна', 'г. Тюмень, ул. Ленина 4', null, '07-35', 20, 'Нет', '02-01-2000');

INSERT INTO Session VALUES (5, '01-06-20', 1, 1), (4, '12-06-20', 2, 2), (3, '13-06-20', 3, 3), (2, '17-06-20', 4, 4), (5, '22-06-20', 5, 5),
(4, '12-06-19', 6, 6), (3, '10-06-19', 7, 7), (2, '09-06-19', 8, 8), (5, '18-06-19', 9, 9), (4, '12-06-19', 10, 10),
(3, '11-07-18', 11, 11), (2, '13-07-18', 12, 12), (5, '12-07-18', 13, 13), (4, '29-07-18', 14, 14), (3, '26-07-18', 15, 15),
(2, '30-07-17', 16, 16), (5, '12-07-17', 17, 17), (4, '05-07-17', 18, 18), (3, '20-07-17', 19, 19), (2, '12-07-17', 20, 20);

DELETE FROM Chair WHERE ID=1;
UPDATE Chair SET Name='*' WHERE ID BETWEEN 1 AND 10;