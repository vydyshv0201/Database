SELECT Subjects.Name, Count(Mark) AS [Count]
FROM Subjects INNER JOIN Marks ON Subjects.ID_subj = Marks.ID_subj
WHERE Mark = 5
GROUP BY Subjects.Name;