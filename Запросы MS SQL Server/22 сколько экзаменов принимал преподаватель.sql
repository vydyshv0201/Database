SELECT lecturer, Count(*) AS [Count]
FROM Marks INNER JOIN Subjects ON Marks.ID_subj = Subjects.ID_subj
GROUP BY lecturer;


