create or replace view Список_клиентов
as 
select owner, street, house, apartment, passport
from owner_info;

select * from Список_клиентов;
drop view Список_клиентов;

create or replace view Список_номеров
as 
select phone_number, phone_location.street, phone_location.house, phone_location.apartment
from number_info full join phone_location
on phone_location.id_phone_location = number_info.id_phone_location;

select * from Список_номеров;
drop view Список_номеров;

create or replace view Список_услуг
as 
select open_service, connection_date, shutdown_date, phone_number, type, owner
from main full join type on main.id_type = type.id_type full join service on main.id_service = service.id_service full join service_date on main.id_services_dates=service_date.id_services_dates 
full join owner_info on main.id_owner_info=owner_info.id_owner_info full join number_info on main.id_number_info=number_info.id_number_info full join phone_location on number_info.id_phone_location=phone_location.id_phone_location
where open_service is not null;

select * from Список_услуг;
drop view Список_услуг;

select * from main;

create view Полная_информация
as 
select open_service, connection_date, shutdown_date, phone_number, type, phone_location.street as phone_street, phone_location.house as phone_house, phone_location.apartment as phone_apartment, owner, passport, owner_info.street, owner_info.house, owner_info.apartment
from main full join type on main.id_type = type.id_type full join service on main.id_service = service.id_service full join service_date on main.id_services_dates=service_date.id_services_dates 
full join owner_info on main.id_owner_info=owner_info.id_owner_info full join number_info on main.id_number_info=number_info.id_number_info full join phone_location on number_info.id_phone_location=phone_location.id_phone_location
where open_service is not null;

select * from Полная_информация;
drop view Полная_информация;

select * from logs;

create or replace view Типы
as 
select type
from type;

select * from Типы;

create or replace view Сервисы
as 
select open_service
from service;

select * from Сервисы;

create or replace view Отчет
as 
select (select count(*) from owner_info) as "Количество клиентов", 
(select count(*) from number_info) as "Количество номеров",
(select avg(count_1) as "Среднее кол-во сервисов" from (select count(id_owner_info) count_1 from main group by id_owner_info) X);

select * from Отчет;

drop view Отчет;
-------------------------------------------------------

create or replace procedure Добавить_клиента (varchar(60), varchar(30), int, int, varchar(100)) as $$
declare kol int=0;  
begin
select into kol id_owner_info from owner_info order by id_owner_info desc limit 1;
kol=kol+1;
INSERT INTO owner_info values 
(kol, $5, $1, $2, $3, $4);
end;
$$ language 'plpgsql';

call Добавить_клиента ('Сергеев Никита Сергеевич',
	'Ленина',
	46,
	10,
	'Серия 7479 № 923701 выдан ГОМ ГУВД Тюменской области 24.09.2012'
);

INSERT INTO owner_info values 
(5,
 'Серия 7479 № 923701 выдан ГОМ ГУВД Тюменской области 24.09.2012',
 'Юрьев Никита Сергеевич',
	'Ленина',
	46,
	10
);

delete from owner_info where id_owner_info = 12

select * from Список_клиентов;

select * from Owner_info;

create or replace procedure Добавить_номер (varchar(20), varchar(30), int, int) as $$
declare  kol int=0; kol2 int=0;  
begin
select into kol id_phone_location from phone_location order by id_phone_location desc limit 1;
kol=kol+1;
select into kol2 id_number_info from number_info order by id_number_info desc limit 1;
kol2=kol2+1;
INSERT INTO phone_location values 
(kol, $2, $3, $4);
INSERT INTO number_info values 
(kol2, $1, kol);
end;
$$ language 'plpgsql';

call Добавить_номер ('33-33-33', 'Мира', 12, 23)
select * from Список_номеров;
 select * from phone_location;
 select * from number_info;

create or replace procedure Добавить_услугу (varchar(20), date, date, varchar(20), varchar(20), varchar(60)) as $$
declare  kol int=0; kol2 int=0; kol3 int=0; kol4 int=0; kol5 int=0; 
begin
select into kol id_owner_info from owner_info 
where owner = $6;
select into kol2 id_number_info from number_info 
where phone_number = $4;
select into kol3 id_type from type 
where type = $5;
select into kol4 id_service from service
where open_service = $1;
select into kol5 id_services_dates from service_date order by id_services_dates desc limit 1;
kol5=kol5+1;
INSERT INTO service_date values 
(kol5, $2, $3);
INSERT INTO main values 
(kol, kol2, kol3, kol4, kol5);
end;
$$ language 'plpgsql';

select * from Список_услуг;

select * from service_date;

call Добавить_услугу ('Заказ билетов', '2022-05-21', null, '34-12-88', 'сотовый', 'Николаев Никита Сергеевич')

select id_service from service
where open_service = 'Заказ билетов';



create or replace procedure Удалить_услугу (varchar(20), varchar(20)) as $$
declare  kol int=0; kol2 int=0; kol3 int=0;
begin
select into kol id_number_info from number_info where phone_number=$2;
select into kol2 id_service from service where open_service=$1;
select into kol3 id_services_dates from main where id_number_info=kol and id_service=kol2;

delete from main where id_number_info=kol and id_service=kol2;
--delete from service_date where id_services_dates=kol3;
end;
$$ language 'plpgsql';

select * from Список_услуг;

call Удалить_услугу('Конференц-связь', '34-12-88')


create or replace procedure Удалить_клиента (varchar(100)) as $$
begin
delete from owner_info where passport=$1;
end;
$$ language 'plpgsql';

select * from Список_клиентов;


create or replace procedure Удалить_номер (varchar(20)) as $$
declare  kol int=0; 
begin
select into kol id_phone_location from number_info where phone_number=$1;

delete from number_info where phone_number=$1;
delete from phone_location where id_phone_location=kol;
end;
$$ language 'plpgsql';

select * from Список_номеров;

------------------------------------------------

create or replace procedure Изменить_клиента (varchar(60), varchar(30), int, int, varchar(100), varchar(100)) as $$
declare kol int=0;  
begin
if $1 <> (select owner from Список_клиентов where passport = $6) then 
update owner_info set owner=$1 where passport = $6;
end if;
if $2 <> (select street from Список_клиентов where passport = $6) then 
update owner_info set street=$2 where passport = $6;
end if;
if $3 <> (select house from Список_клиентов where passport = $6) then 
update owner_info set house=$3 where passport = $6;
end if;
if $4 <> (select apartment from Список_клиентов where passport = $6) then 
update owner_info set apartment=$4 where passport = $6;
end if;
if $5 <> $6 then 
update owner_info set passport=$5 where passport = $6;
end if;
end;
$$ language 'plpgsql';

select * from Список_клиентов;

create or replace procedure Изменить_номер (varchar(20), varchar(30), int, int, varchar(20)) as $$
declare  kol int=0; 
begin
select into kol id_phone_location from number_info where phone_number = $5; 
if $2 <> (select street from Список_номеров where phone_number = $5) then 
update phone_location set street=$2 where id_phone_location = kol;
end if;
if $3 <> (select house from Список_номеров where phone_number = $5) then 
update phone_location set house=$3 where id_phone_location = kol;
end if;
if $4 <> (select apartment from Список_номеров where phone_number = $5) then 
update phone_location set apartment=$4 where id_phone_location = kol;
end if;
if $1 <> (select phone_number from Список_номеров where phone_number = $5) then 
update number_info set phone_number=$1 where phone_number=$5;
end if;
end;
$$ language 'plpgsql';

select * from Список_номеров;

create or replace procedure Изменить_услугу (varchar(20), date, date, varchar(20), varchar(20), varchar(60), varchar(20), varchar(20)) as $$
declare  kol int=0; kol2 int=0; kol3 int=0; kol4 int=0; kol5 int=0; 
begin
select into kol id_service from service
where open_service = $7;
select into kol2 id_number_info from number_info 
where phone_number = $8;
select into kol3 id_services_dates from main where id_service = kol and id_number_info = kol2;
select into kol4 id_service from service
where open_service = $1;
select into kol5 id_number_info from number_info 
where phone_number = $4;

if $2 <> (select connection_date from Список_услуг where open_service = $7 and phone_number = $8) then 
update service_date set connection_date=$2 where id_services_dates = kol3;
end if;
if $3 <> (select shutdown_date from Список_услуг where open_service = $7 and phone_number = $8) then 
update service_date set shutdown_date=$3 where id_services_dates = kol3;
end if;
if $5 <> (select type from Список_услуг where open_service = $7 and phone_number = $8) then 
update main set id_type=$5 where id_service = kol and id_number_info = kol2;
end if;
if $6 <> (select owner from Список_услуг where open_service = $7 and phone_number = $8) then 
update main set id_owner=$6 where id_service = kol and id_number_info = kol2;
end if;

update main set id_service=kol4, id_number_info=kol5 where id_service = kol and id_number_info = kol2;
end;
$$ language 'plpgsql';

select * from Список_услуг;

------------------------------------------------

CREATE TABLE logs
(
  	operation text,
 	date timestamp,
	sys_user text, 
	row text
);	

select * from logs;

drop table logs;

CREATE OR REPLACE FUNCTION process_audit1() RETURNS TRIGGER AS $$
    BEGIN
        IF (TG_OP = 'DELETE') THEN
            INSERT INTO logs SELECT 'Delete', now(), 'Администартор',  OLD.*::text;
            RETURN OLD;
        ELSIF (TG_OP = 'UPDATE') THEN
            INSERT INTO logs SELECT 'Update', now(), 'Администартор',  NEW.*::text;
            RETURN NEW;
        ELSIF (TG_OP = 'INSERT') THEN
            INSERT INTO logs SELECT 'Insert', now(), 'Администартор',  NEW.*::text;
            RETURN NEW;
        END IF;
        RETURN NULL; -- возвращаемое значение для триггера AFTER игнорируется
    END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION process_audit2() RETURNS TRIGGER AS $$
    BEGIN
        IF (TG_OP = 'DELETE') THEN
            INSERT INTO logs SELECT 'Delete', now(), 'Оператор',  OLD.*::text;
            RETURN OLD;
        ELSIF (TG_OP = 'UPDATE') THEN
            INSERT INTO logs SELECT 'Update', now(), 'Оператор',  NEW.*::text;
            RETURN NEW;
        ELSIF (TG_OP = 'INSERT') THEN
            INSERT INTO logs SELECT 'Insert', now(), 'Оператор',  NEW.*::text;
            RETURN NEW;
        END IF;
        RETURN NULL; -- возвращаемое значение для триггера AFTER игнорируется
    END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION process_audit3() RETURNS TRIGGER AS $$
    BEGIN
        IF (TG_OP = 'DELETE') THEN
            INSERT INTO logs SELECT 'Delete', now(), 'Читатель',  OLD.*::text;
            RETURN OLD;
        ELSIF (TG_OP = 'UPDATE') THEN
            INSERT INTO logs SELECT 'Update', now(), 'Читатель',  NEW.*::text;
            RETURN NEW;
        ELSIF (TG_OP = 'INSERT') THEN
            INSERT INTO logs SELECT 'Insert', now(), 'Читатель', NEW.*::text;
            RETURN NEW;
        END IF;
        RETURN NULL; -- возвращаемое значение для триггера AFTER игнорируется
    END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER audit1_m
AFTER INSERT OR UPDATE OR DELETE ON main
    FOR EACH ROW EXECUTE PROCEDURE process_audit1();
	
CREATE TRIGGER audit1_p
AFTER INSERT OR UPDATE OR DELETE ON phone_location
    FOR EACH ROW EXECUTE PROCEDURE process_audit1();
	
CREATE TRIGGER audit1_n
AFTER INSERT OR UPDATE OR DELETE ON number_info
    FOR EACH ROW EXECUTE PROCEDURE process_audit1();
	
CREATE TRIGGER audit1_o
AFTER INSERT OR UPDATE OR DELETE ON owner_info
    FOR EACH ROW EXECUTE PROCEDURE process_audit1();
	
CREATE TRIGGER audit1_s
AFTER INSERT OR UPDATE OR DELETE ON service_date
    FOR EACH ROW EXECUTE PROCEDURE process_audit1();
	
alter table main disable trigger audit1_m ;
alter table main disable trigger audit1_p ;
alter table main disable trigger audit1_n;
alter table main disable trigger audit1_o;
alter table main disable trigger audit1_s;

alter table main enable trigger audit1_m ;
alter table main enable trigger audit1_p ;
alter table main enable trigger audit1_n;
alter table main enable trigger audit1_o;
alter table main enable trigger audit1_s;

CREATE TRIGGER audit2_m
AFTER INSERT OR UPDATE OR DELETE ON main
    FOR EACH ROW EXECUTE PROCEDURE process_audit2();
	
CREATE TRIGGER audit2_p
AFTER INSERT OR UPDATE OR DELETE ON phone_location
    FOR EACH ROW EXECUTE PROCEDURE process_audit2();
	
CREATE TRIGGER audit2_n
AFTER INSERT OR UPDATE OR DELETE ON number_info
    FOR EACH ROW EXECUTE PROCEDURE process_audit2();
	
CREATE TRIGGER audit2_o
AFTER INSERT OR UPDATE OR DELETE ON owner_info
    FOR EACH ROW EXECUTE PROCEDURE process_audit2();
	
CREATE TRIGGER audit2_s
AFTER INSERT OR UPDATE OR DELETE ON service_date
    FOR EACH ROW EXECUTE PROCEDURE process_audit2();
	
alter table main disable trigger audit2_m ;
alter table main disable trigger audit2_p ;
alter table main disable trigger audit2_n;
alter table main disable trigger audit2_o;
alter table main disable trigger audit2_s;

alter table main enable trigger audit2_m ;
alter table main enable trigger audit2_p ;
alter table main enable trigger audit2_n;
alter table main enable trigger audit2_o;
alter table main enable trigger audit2_s;

CREATE TRIGGER audit3_m
AFTER INSERT OR UPDATE OR DELETE ON main
    FOR EACH ROW EXECUTE PROCEDURE process_audit3();
	
CREATE TRIGGER audit3_p
AFTER INSERT OR UPDATE OR DELETE ON phone_location
    FOR EACH ROW EXECUTE PROCEDURE process_audit3();
	
CREATE TRIGGER audit3_n
AFTER INSERT OR UPDATE OR DELETE ON number_info
    FOR EACH ROW EXECUTE PROCEDURE process_audit3();
	
CREATE TRIGGER audit3_o
AFTER INSERT OR UPDATE OR DELETE ON owner_info
    FOR EACH ROW EXECUTE PROCEDURE process_audit3();
	
CREATE TRIGGER audit3_s
AFTER INSERT OR UPDATE OR DELETE ON service_date
    FOR EACH ROW EXECUTE PROCEDURE process_audit3();
	
alter table main disable trigger audit3_m ;
alter table main disable trigger audit3_p ;
alter table main disable trigger audit3_n;
alter table main disable trigger audit3_o;
alter table main disable trigger audit3_s;

alter table main enable trigger audit3_m ;
alter table main enable trigger audit3_p ;
alter table main enable trigger audit3_n;
alter table main enable trigger audit3_o;
alter table main enable trigger audit3_s;

create or replace procedure Изменить_пользователя (varchar(30)) as $$  
begin
if $1 = 'Администратор' then
alter table main enable trigger audit1_m ;
alter table phone_location enable trigger audit1_p ;
alter table number_info enable trigger audit1_n;
alter table owner_info enable trigger audit1_o;
alter table service_date enable trigger audit1_s;
alter table main disable trigger audit2_m ;
alter table phone_location disable trigger audit2_p ;
alter table number_info disable trigger audit2_n;
alter table owner_info disable trigger audit2_o;
alter table service_date disable trigger audit2_s;
alter table main disable trigger audit3_m ;
alter table phone_location disable trigger audit3_p ;
alter table number_info disable trigger audit3_n;
alter table owner_info disable trigger audit3_o;
alter table service_date disable trigger audit3_s;
end if;
if $1 = 'Оператор' then
alter table main enable trigger audit2_m ;
alter table phone_location enable trigger audit2_p ;
alter table number_info enable trigger audit2_n;
alter table owner_info enable trigger audit2_o;
alter table service_date enable trigger audit2_s;
alter table main disable trigger audit3_m ;
alter table phone_location disable trigger audit3_p ;
alter table number_info disable trigger audit3_n;
alter table owner_info disable trigger audit3_o;
alter table service_date disable trigger audit3_s;
alter table main disable trigger audit1_m ;
alter table phone_location disable trigger audit1_p ;
alter table number_info disable trigger audit1_n;
alter table owner_info disable trigger audit1_o;
alter table service_date disable trigger audit1_s;
end if;
if $1 = 'Читатель' then
alter table main enable trigger audit3_m ;
alter table phone_location enable trigger audit3_p ;
alter table number_info enable trigger audit3_n;
alter table owner_info enable trigger audit3_o;
alter table service_date enable trigger audit3_s;
alter table main disable trigger audit1_m ;
alter table phone_location disable trigger audit1_p ;
alter table number_info disable trigger audit1_n;
alter table owner_info disable trigger audit1_o;
alter table service_date disable trigger audit1_s;
alter table main disable trigger audit2_m ;
alter table phone_location disable trigger audit2_p ;
alter table number_info disable trigger audit2_n;
alter table owner_info disable trigger audit2_o;
alter table service_date disable trigger audit2_s;
end if;
end;
$$ language 'plpgsql';

call Изменить_пользователя('Администратор')
call Изменить_пользователя('Оператор')
call Изменить_пользователя('Читатель')

