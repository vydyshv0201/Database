SELECT family, Name
FROM Students
WHERE EXISTS (SELECT Mark FROM Marks WHERE Students.ID_stud=Marks.ID_stud AND Mark <> 2);
