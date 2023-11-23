--c) запит зі складним критерієм;
SELECT *
FROM students
WHERE (GroupUni = 'Group2' AND AverageScore < 90 AND City = 'City3');