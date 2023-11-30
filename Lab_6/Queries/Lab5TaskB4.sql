--b) запит на вибірку з використанням спеціальних функцій: LIKE, IS NULL, IN, BETWEEN;
--вибрати студентів рік народження яких між 2000 і 2001 із використанням функції BETWEEN
SELECT * 
FROM students 
WHERE BirthYear
BETWEEN 2000 AND 2001;
