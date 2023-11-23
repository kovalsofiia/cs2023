--b) запит на вибірку з використанням спеціальних функцій: LIKE, IS NULL, IN, BETWEEN;
--вибрати студентів у яких прізвище починається із Sm
SELECT *
FROM students
WHERE Surname LIKE ('Sm%');