SELECT Students.Name, Students.family, Subjects.Name, Marks.Mark
FROM Subjects INNER JOIN (Students INNER JOIN Marks ON Students.ID_stud = Marks.ID_stud) ON Subjects.ID_subj = Marks.ID_subj
WHERE Marks.Mark=2;
