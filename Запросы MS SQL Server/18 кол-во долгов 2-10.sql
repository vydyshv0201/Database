SELECT concat(Students.family, ' ', Students.Name), Count(Mark) AS [Count]
FROM Students INNER JOIN Marks ON Students.ID_stud = Marks.ID_stud
WHERE Mark=2
GROUP BY concat(Students.family, ' ', Students.Name)
HAVING Count(Mark) BETWEEN 3 AND 9;