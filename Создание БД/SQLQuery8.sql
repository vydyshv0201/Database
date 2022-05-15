
select * from EMPLOYEES;

select * from EMPLOYEES WHERE FIRST_NAME = 'David';

select * from EMPLOYEES WHERE JOB_ID = 'IT_PROG';

select * from EMPLOYEES WHERE department_id = 50 and salary > 4000;

select * from EMPLOYEES WHERE department_id in (20, 30) ;

select * from EMPLOYEES WHERE FIRST_NAME LIKE '%a';

select * from EMPLOYEES WHERE department_id in (50, 80) and commission_pct is not null ;

select * from EMPLOYEES WHERE FIRST_NAME LIKE '%n%n%';

select * from EMPLOYEES WHERE LEN(FIRST_NAME) > 4;

select * from EMPLOYEES WHERE SALARY between 8000 and 9000;

