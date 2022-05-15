---Триггер автоматически выставляет дату подключения 
create trigger auto_date
	after insert on main 
	for each row
	when (NEW.id_services_dates is null)
	execute procedure auto_date_pr();

alter table main disable trigger auto_date ;
alter table main enable trigger auto_date ;
drop trigger auto_date on main;

create or replace function auto_date_pr() returns trigger as $$
declare
	kol int=0;
	flag1 bool; 
begin
	select into flag1 exists (select * from service_date 
	where shutdown_date is null and connection_date = current_date::date);
	select into kol count(*) from service_date;
	if flag1 = false then
		kol=kol+1;
		INSERT INTO service_date (id_services_dates, connection_date) values 
        (kol, current_date::date);
	end if;	
	update main set id_services_dates=kol where id_owner_info = NEW.id_owner_info and id_number_info = NEW.id_number_info and
	id_type = NEW.id_type and id_service = NEW.id_service;
	return NEW;
end;
$$ language plpgsql;

drop function auto_date_pr();

select * from main;
select * from service_date;

delete from service_date where id_services_dates = 7;

delete from main where id_owner_info = 6 and id_services_dates = 7;

INSERT INTO main (id_owner_info, id_number_info, id_type, id_service) values
(
	6,
	7, 
	3,
	2
);

--Если дата окончания пустая, то добавляется +5 лет от даты подключения
create trigger auto_shutdown
	before insert on service_date
	for each row
	when (NEW.shutdown_date is null)
	execute procedure auto_shutdown_pr();
	
drop trigger auto_shutdown on service_date;	
	
create or replace function auto_shutdown_pr() returns trigger as $$
begin
	NEW.shutdown_date = current_date::date + interval '5 year'; 
	return NEW;
end;
$$ language plpgsql;	
	

-----------------------------------------------------------------------------

--Триггер запрещает удалять записи без даты окончания

create trigger stop_delete
	before delete on main 
	for each row
	execute procedure stop_delete_pr();
	
drop trigger stop_delete on main;

create or replace function stop_delete_pr() returns trigger as $$
begin
	if (select shutdown_date from service_date s where s.id_services_dates = OLD.id_services_dates) is null then
		RAISE EXCEPTION 'Nonono'; 
  	END IF; 
    return old; 
END;
$$
LANGUAGE plpgsql;

drop function stop_delete_pr();

select * from main;
select * from service_date;

delete from main where id_owner_info = 6 and id_services_dates = 8;

-----------------------------------------------------------

--триггер, проверяющий, что в таблице phone_location обязательно содержится название улицы; 

create trigger check_street
	before insert or update on phone_location 
	for each row
	execute procedure check_street_pr();
	
drop trigger check_street on phone_location;

create or replace function check_street_pr() returns trigger as $$
begin
	if NEW.street is null or trim(NEW.street) = '' then
		RAISE EXCEPTION 'Введите улицу'; 
  	END IF; 
    return new; 
END;
$$
LANGUAGE plpgsql;

drop function check_street_pr();

delete from phone_location where id_phone_location = 4;

select * from phone_location;
	
INSERT INTO phone_location values 
        ( 4,
		 'Ленина',
		 12,
		 6
        );
		
		
		
---------------------------------------------------------------

CREATE TABLE logs
(
  	operation text,
 	date timestamp,
	sys_user text
);	

select * from logs;

-- триггер, фиксирующий операции создания, изменения структуры и удаления таблиц в базе данных; 

create event trigger add_log
	on ddl_command_end 
	WHEN TAG in ('DROP TABLE', 'CREATE TABLE', 'ALTER TABLE')  
	execute procedure add_log_pr();
	
drop event trigger add_log ;

create or replace function add_log_pr() returns event_trigger as $$
begin
	INSERT INTO logs values (tg_tag, current_timestamp, session_user);
END;
$$
LANGUAGE plpgsql;

drop function add_log_pr();

CREATE TABLE example
(
  	operation text,
 	date timestamp,
	sys_user text
);	

drop table example;
	
