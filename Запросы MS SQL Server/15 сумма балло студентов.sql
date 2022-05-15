SELECT concat(Students.family, ' ', Students.Name), Sum(Mark) AS [Sum]
FROM Students INNER JOIN Marks ON Students.ID_stud = Marks.ID_stud
GROUP BY concat(Students.family, ' ', Students.Name)
