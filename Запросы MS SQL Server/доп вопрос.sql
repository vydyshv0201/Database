SELECT concat(Students.family, ' ', Students.Name) AS Stud, Subjects.lecturer,
(SELECT COUNT(*) FROM Subjects s1    
   WHERE s1.lecturer = Subjects.lecturer 
   GROUP BY s1.Name) AS Count 
FROM Subjects INNER JOIN (Students INNER JOIN Marks ON Students.ID_stud = Marks.ID_stud) ON Subjects.ID_subj = Marks.ID_subj
 

