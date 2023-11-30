--//f) запит з групуванням по заданому полю, використовуючи умову групування;
SELECT
    GroupUni,
    AVG(AverageScore) AS AverageScore
FROM students
GROUP BY GroupUni
HAVING AVG(AverageScore) > 80;