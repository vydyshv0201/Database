SELECT DISTINCT Subjects.Name
FROM Subjects INNER JOIN (Students INNER JOIN Marks ON Students.ID_stud = Marks.ID_stud) ON Subjects.ID_subj = Marks.ID_subj
WHERE SEX = 'Ì' AND (YEAR(Marks.Date)-YEAR(Students.Date_of_birth))>20;
