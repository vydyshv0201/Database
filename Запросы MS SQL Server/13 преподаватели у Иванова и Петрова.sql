SELECT lecturer
FROM Subjects
WHERE EXISTS (SELECT family FROM Students INNER JOIN Marks ON Students.ID_stud = Marks.ID_stud WHERE Subjects.ID_subj=Marks.ID_subj AND family IN ('Петров', 'Иванов'));
