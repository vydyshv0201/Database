SELECT DISTINCT Subjects.Name
FROM Subjects INNER JOIN (Students INNER JOIN Marks ON Students.ID_stud = Marks.ID_stud) ON Subjects.ID_subj = Marks.ID_subj
WHERE Marks.Date > Students.Date;
