SELECT Name, Count(lecturer) AS [Count]
FROM Subjects
GROUP BY Name;
