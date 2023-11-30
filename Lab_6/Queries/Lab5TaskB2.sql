--b) запит на вибірку з використанням спеціальних функцій: LIKE, IS NULL, IN, BETWEEN;
--вибрати студентів де WorkPlace пустий
SELECT * 
FROM students
WHERE WorkPlace IS NULL;