--Количество услуг клиента, подключенных за определенный период 
create function one(varchar(60), date, date) returns int as $$
	select count(*) from main inner join owner_info on main.id_owner_info = owner_info.id_owner_info 
	inner join service_date on main.id_services_dates = service_date.id_services_dates 
	where owner = $1 and connection_date between $2 and $3;
$$ language sql;

drop function one;

select owner, one(owner, '01-01-2001', '01-01-2004') from owner_info;


--Сервивы, отключенные до заданной даты
create function two (date) returns table (open_service varchar(20), owner varchar(60)) as $$
select open_service, owner from main inner join owner_info on main.id_owner_info = owner_info.id_owner_info 
	inner join service_date on main.id_services_dates = service_date.id_services_dates inner join 
	service on main.id_service = service.id_service
	where shutdown_date < $1;
	$$ language sql;
	
	drop function two;
	
	select * from two('01-01-2010');
	
	
	
	
	
create function three() returns varchar(60) as $$
declare i varchar(60);
begin
select owner from owner_info into i;
return i;
end;
$$ language 'plpgsql';

drop function three;
	
	select * from three();
	
	
	
	
--Новый пользователь для подключения мобильного интернета
create or replace procedure insert_owner (varchar(100), varchar(60), varchar(30), int, int, varchar(20)) as $$
declare kol int=0;  kol2 int=0; kol3 int=0;
begin
select into kol count(*) from owner_info;
kol=kol+1;
select into kol2 count(*) from service_date;
kol2=kol2+1;
select into kol3 count(*) from number_info;
kol3=kol3+1;
INSERT INTO owner_info values 
(kol, $1, $2, $3, $4, $5);
INSERT INTO service_date (id_services_dates, connection_date) values 
        ( kol2,
		 'TODAY'::date
        );
INSERT INTO number_info values 
        ( kol3,
		 $6,
		 null
        );	
INSERT INTO main values 
(
	kol,
	kol3, 
	3,
	1, 
	kol2
);
end;
$$ language 'plpgsql';

drop procedure insert_owner;

call insert_owner ('Серия 7479 № 923701 выдан ГОМ ГУВД Тюменской области 24.09.2012',
	'Николаев Никита Сергеевич',
	'Ленина',
	46,
	10, 
	'34-12-88'
);	

select * from owner_info inner join main on owner_info.id_owner_info = main.id_owner_info;	



--Процедура, выводящая таблицу
create or replace procedure pr_one(INOUT ref_ refcursor = '1') 
language 'plpgsql'
as $$
begin
open ref_ for select owner from owner_info union all
select 'итог: ' || count(*)::varchar(60) from owner_info;
end;
$$;

drop procedure pr_one(refcursor);

CALL  pr_one();
fetch all in "1";


