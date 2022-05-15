SELECT Min(Mark) AS [Min], Round(Avg(CONVERT(DECIMAL,Mark)), 1) AS [AVG], Max(Mark) AS [Max]
FROM Marks;
