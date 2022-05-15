
select * FROM EMPLOYEES;

select * FROM EMPLOYEES WHERE FIRST_NAME = 'David';

select * FROM EMPLOYEES WHERE JOB_ID = 'IT_PROG';

select * FROM EMPLOYEES WHERE department_id = 50 and salary > 4000;

select * FROM EMPLOYEES WHERE department_id in (20, 30) ;

select * FROM EMPLOYEES WHERE TRIM(FIRST_NAME) LIKE '%a';

select * FROM EMPLOYEES WHERE department_id in (50, 80) and commission_pct is not null ;

select * FROM EMPLOYEES WHERE FIRST_NAME ILIKE '%n%n%';

select * FROM EMPLOYEES WHERE LEN(FIRST_NAME) > 4;

select * FROM EMPLOYEES WHERE SALARY between 8000 and 9000;

SELECT * FROM employees WHERE first_name LIKE '%#%%' ESCAPE '#';

SELECT DISTINCT manager_id FROM employees WHERE manager_id IS NOT NULL;

SELECT first_name || '(' || LOWER (job_id) || ')' List FROM employees;

-----------------------------------------------------------------------

SELECT * FROM employees WHERE LENGTH(first_name) > 10;

SELECT * FROM employees WHERE STRPOS(LOWER (first_name), 'b') > 0; 

SELECT * FROM employees 
WHERE STRPOS(SUBSTR(LOWER (first_name), POSITION('a' in LOWER (first_name))+1, length(first_name)), 'a') > 0;  

SELECT * FROM employees WHERE MOD (salary, 1000) = 0;

SELECT phone_number, SUBSTR (phone_number, 1, 3) new_phone_number FROM employees 
WHERE trim(phone_number) SIMILAR TO '[0-9][0-9][0-9].[0-9][0-9][0-9].[0-9][0-9][0-9][0-9]'; 

SELECT department_name, SUBSTR (department_name, 1, STRPOS (department_name, ' ')-1) first_word FROM departments 
WHERE STRPOS (department_name, ' ') > 0;

SELECT first_name, SUBSTR (first_name, 2, LENGTH (first_name) - 2) new_name FROM employees;

SELECT * FROM employees WHERE SUBSTR (first_name, LENGTH (first_name)) = 'm' AND LENGTH(first_name)>5;

SELECT 'TOMORROW'::date + ( 5 + 7 - extract ( dow FROM 'TOMORROW'::date))::int%7;

SELECT * FROM employees WHERE date_part('year', AGE(hire_date)) > 17;

SELECT * FROM employees 
WHERE MOD (SUBSTR (phone_number, LENGTH (phone_number))::int, 2) != 0 
AND trim(phone_number) not like '%.%.%.%';

SELECT * FROM employees
WHERE LENGTH (SUBSTR (job_id, STRPOS (job_id, '_') + 1)) > 3 AND SUBSTR (job_id, STRPOS (job_id, '_') + 1) != 'CLERK';

SELECT phone_number, REPLACE (phone_number, '.', '-') new_phone_number FROM employees;

---------------------------------------------------------------------------------------------------------

SELECT * FROM employees WHERE TO_CHAR (hire_date, 'DD') = '01';

SELECT * FROM employees WHERE TO_CHAR (hire_date, 'YYYY') = '2008';

SELECT TO_CHAR ('TOMORROW'::date, '"Tomorrow is" fmddth "day of" Month'); 
  
SELECT first_name, TO_CHAR (hire_date, 'fmddth "of" Month, YYYY') hire_date FROM employees;  

SELECT first_name, TO_CHAR (salary + salary * 0.20, 'fm$999,999.00') new_salary FROM employees;

SELECT * FROM employees WHERE to_char(hire_date,'MM.YYYY') = '02.2007';

SELECT current_timestamp now,
date_part('second', current_timestamp)+1 , 
date_part('minute', current_timestamp)+1 ,
date_part('hour', current_timestamp)+1 ,
date_part('day', current_timestamp)+1 ,
date_part('month', current_timestamp)+1 ,
date_part('year', current_timestamp)+1 ;
  
SELECT first_name, salary, TO_CHAR (salary + salary * COALESCE(commission_pct, 0), 'fm$99,999.00') full_salary 
FROM employees; 

SELECT first_name, commission_pct, 
CASE 
WHEN commission_pct is null THEN 'No'
WHEN commission_pct is not null THEN 'Yes'
ELSE 'other'
END
has_bonus
FROM employees;
  
SELECT first_name, salary,
CASE
WHEN salary < 5000 THEN 'Low'
WHEN salary >= 5000 AND salary < 10000 THEN 'Normal'
WHEN salary >= 10000 THEN 'High'
ELSE 'Unknown'
END
salary_level
FROM employees;

SELECT country_name,
CASE region_id
WHEN 1 THEN 'Europe'
WHEN 2 THEN 'America'
WHEN 3 THEN 'Asia'
WHEN 4 THEN 'Africa'
ELSE 'Unknown'
END
region
FROM countries;
  
----------------------------------------------------------------------------

SELECT department_id,
MIN (salary) min_salary,
MAX (salary) max_salary,
MIN (hire_date) min_hire_date,
MAX (hire_date) max_hire_Date,
COUNT (*) count
FROM employees
GROUP BY department_id
order by count desc;

SELECT SUBSTR (first_name, 1, 1) first_char, COUNT (*)
FROM employees
GROUP BY SUBSTR (first_name, 1, 1)
HAVING COUNT (*) > 1
ORDER BY 2 DESC;

SELECT department_id, salary, COUNT (*)
FROM employees
GROUP BY department_id, salary
HAVING COUNT (*) > 1;
  
SELECT TO_CHAR (hire_Date, 'Day'), COUNT (*)
FROM employees
GROUP BY TO_CHAR (hire_Date, 'Day')
ORDER BY 2 DESC;

SELECT TO_CHAR (hire_date, 'YYYY'), COUNT (*)
FROM employees
GROUP BY TO_CHAR (hire_date, 'YYYY');

SELECT COUNT(cnt)     
FROM (select COUNT(*) cnt FROM employees
WHERE department_id IS NOT NULL
GROUP BY department_id) X;
SELECT DISTINCT COUNT (COUNT (*))   OVER ()  department_count
    FROM employees
   WHERE department_id IS NOT NULL
GROUP BY department_id;

SELECT department_id
FROM employees
GROUP BY department_id
HAVING COUNT (*) > 30;

SELECT department_id, ROUND (AVG (salary)) avg_salary
FROM employees
GROUP BY department_id;

SELECT region_id
FROM countries
GROUP BY region_id
HAVING SUM (LENGTH (country_name)) > 60;

SELECT department_id
FROM employees
GROUP BY department_id
HAVING COUNT (DISTINCT job_id) > 1;

SELECT manager_id
FROM employees
GROUP BY manager_id
HAVING COUNT (*) > 5 AND SUM (salary) > 50000;

SELECT manager_id, AVG (salary) avg_salary
FROM employees
WHERE commission_pct IS NULL
GROUP BY manager_id
HAVING AVG (salary) BETWEEN 6000 AND 9000;

SELECT MAX (salary) max_salary
FROM employees
WHERE trim(job_id) LIKE '%CLERK';

SELECT DISTINCT MAX (AVG (salary)) OVER ()
FROM employees
GROUP BY department_id; 

SELECT LENGTH (first_name), COUNT (*)
FROM employees
GROUP BY LENGTH (first_name)
HAVING LENGTH (first_name) > 5 AND COUNT (*) > 20
ORDER BY LENGTH (first_name);

SELECT region_name, COUNT (*)
FROM employees e
JOIN departments d ON (e.department_id = d.department_id)
JOIN locations l ON (d.location_id = l.location_id)
JOIN countries c ON (l.country_id = c.country_id)
JOIN regions r ON (c.region_id = r.region_id)
GROUP BY region_name;

SELECT First_name, Last_name, Department_name, Job_id, street_address, Country_name, Region_name
FROM employees  e
JOIN departments d ON (e.department_id = d.department_id)
JOIN locations l ON (d.location_id = l.location_id)
JOIN countries c ON (l.country_id = c.country_id)
JOIN regions r ON (c.region_id = r.region_id);

SELECT man.first_name, COUNT (*)
FROM employees emp JOIN employees man ON (emp.manager_id = man.employee_id)
GROUP BY man.first_name
HAVING COUNT (*) > 6;

SELECT first_name
FROM employees
WHERE manager_id IS NULL;

SELECT first_name,
CASE 
WHEN end_date is not null THEN TO_CHAR (end_date, 'fm"Left the company at" DD "of" fmMonth, YYYY')
WHEN end_date is null THEN 'Currently Working'
ELSE 'other'
END
status
FROM employees e LEFT JOIN job_history j ON (e.employee_id = j.employee_id); 
  
SELECT first_name
FROM employees  e
JOIN departments d ON (e.department_id = d.department_id)
JOIN locations l ON (d.location_id = l.location_id)
JOIN countries c ON (l.country_id = c.country_id)
JOIN regions r ON (c.region_id = r.region_id)
WHERE region_name = 'Europe';

SELECT department_name, COUNT (*)
FROM employees e JOIN departments d ON (e.department_id = d.department_id)
GROUP BY department_name
HAVING COUNT (*) > 30;

SELECT first_name
FROM employees
WHERE department_id IS NULL;

SELECT department_name
FROM employees  e
RIGHT JOIN departments d ON (e.department_id = d.department_id)
WHERE first_name IS NULL;

SELECT man.first_name
FROM employees  emp
RIGHT JOIN employees man ON (emp.manager_id = man.employee_id)
WHERE emp.FIRST_NAME IS NULL;

SELECT first_name, job_title, department_name
FROM employees  e
JOIN jobs j ON (e.job_id = j.job_id)
JOIN departments d ON (d.department_id = e.department_id);

SELECT emp.*
FROM employees emp JOIN employees man ON (emp.manager_id = man.employee_id)
WHERE TO_CHAR (man.hire_date, 'YYYY') = '2005' AND emp.hire_date < TO_DATE ('01012005', 'DDMMYYYY');

SELECT emp.*
FROM employees  emp
JOIN employees man ON (emp.manager_id = man.employee_id)
JOIN jobs j ON (emp.job_id = j.job_id)
WHERE TO_CHAR (man.hire_date, 'MM') = '01' AND LENGTH (j.job_title) > 15;

----------------------------------------------------------------------------------

SELECT * FROM employees
WHERE LENGTH (first_name) = (SELECT MAX (LENGTH (first_name)) FROM employees);

SELECT * FROM employees
WHERE salary > (SELECT AVG (salary) FROM employees);

SELECT city FROM employees e
JOIN departments d ON (e.department_id = d.department_id)
JOIN locations l ON (d.location_id = l.location_id)
GROUP BY city
HAVING SUM (salary) =
(SELECT DISTINCT MIN (SUM (salary)) OVER () FROM employees e
JOIN departments d ON (e.department_id = d.department_id)
JOIN locations l ON (d.location_id = l.location_id)
GROUP BY city);

SELECT * FROM employees 
WHERE manager_id IN (SELECT employee_id FROM employees
WHERE salary > 15000);

SELECT * FROM departments
WHERE department_id NOT IN (SELECT department_id FROM employees
WHERE department_id IS NOT NULL);

SELECT * FROM employees
WHERE employee_id NOT IN (SELECT manager_id FROM employees
WHERE manager_id IS NOT NULL);

SELECT * FROM employees e
WHERE (SELECT COUNT (*) FROM employees
WHERE manager_id = e.employee_id) > 6;

SELECT * FROM employees
WHERE department_id = (SELECT department_id FROM departments
WHERE department_name = 'IT');

SELECT first_name,
(SELECT job_title FROM jobs
WHERE job_id = e.job_id)
job_title,
(SELECT department_name FROM departments
WHERE department_id = e.department_id)
department_name FROM employees e;

SELECT * FROM employees
WHERE manager_id IN (SELECT employee_id FROM employees
WHERE TO_CHAR (hire_date, 'YYYY') = '2005') AND hire_date < TO_DATE ('01012005', 'DDMMYYYY');

SELECT emp.*
FROM employees emp JOIN employees emp2 ON (emp.manager_id = emp2.employee_id)
WHERE TO_CHAR (emp2.hire_date, 'YYYY') = '2005' AND emp.hire_date < TO_DATE ('01012005', 'DDMMYYYY');

SELECT * FROM employees e
WHERE manager_id IN (SELECT employee_id FROM employees
WHERE TO_CHAR (hire_date, 'MM') = '01') AND (SELECT LENGTH (job_title) FROM jobs
WHERE job_id = e.job_id) > 15;





SELECT region_name, TO_CHAR (e.hire_date, 'YYYY') new_year, COUNT (*)
FROM employees e
JOIN departments d ON (e.department_id = d.department_id)
JOIN locations l ON (d.location_id = l.location_id)
JOIN countries c ON (l.country_id = c.country_id)
JOIN regions r ON (c.region_id = r.region_id)
GROUP BY region_name, new_year
ORDER BY new_year DESC;

