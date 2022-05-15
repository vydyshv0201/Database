SELECT City, Count(Students.ID_stud) AS [Count], Round(Avg(Mark*1.0),1) AS [Avg]
FROM Students INNER JOIN Marks ON Students.ID_stud = Marks.ID_stud
GROUP BY City;