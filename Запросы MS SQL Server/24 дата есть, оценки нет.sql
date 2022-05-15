SELECT DISTINCT Students.family, Students.Name
FROM Students INNER JOIN Marks ON Students.ID_stud = Marks.ID_stud
WHERE Marks.Date IS NOT NULL AND Mark IS NULL;