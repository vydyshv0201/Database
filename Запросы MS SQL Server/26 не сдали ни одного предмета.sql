SELECT family, Name
FROM Students
WHERE NOT EXISTS (SELECT Mark FROM Marks WHERE Students.ID_stud =Marks.ID_stud AND (Mark <> 2 OR Marks.Date > '01/09/2012' OR Marks.Date IS NULL));
