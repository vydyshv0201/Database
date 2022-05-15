select * from phone_location;
select * from owner_info;
select * from service_date;
select * from number_info;
select * from service;
select * from main;

create temp view Список_клиентов
as 
select owner as Владелец, street as Улица, house as Дом, apartment as Квартира
from owner_info;

create or replace view Список_клиентов
as 
select owner, street, house, apartment, passport
from owner_info;

select * from Список_клиентов;
drop view Список_клиентов;



create view Список_номеров
as 
select phone_number, phone_location.street, phone_location.house, phone_location.apartment
from phone_location right join number_info
on phone_location.id_phone_location = number_info.id_phone_location;

select * from Список_номеров;
drop view Список_номеров;

create view Количество_номеров_по_услугам
as 
select open_service, count (*) количество
from service inner join main on service.id_service = main.id_service
group by open_service;

select * from Количество_номеров_по_услугам;
drop view Количество_номеров_по_услугам;

create view Пользователи_всех_телефонов 
as 
select owner
from owner_info oi1 
where exists 
	(select owner from owner_info inner join main on owner_info.id_owner_info = main.id_owner_info 
	 	inner join type on main.id_type = type.id_type
			  where type = 'сотовый' and oi1.owner = owner) 
and exists 
	(select owner from owner_info inner join main on owner_info.id_owner_info = main.id_owner_info 
	 	inner join type on main.id_type = type.id_type
			  where type = 'стационарный' and oi1.owner = owner) 
and exists 
	(select owner from owner_info inner join main on owner_info.id_owner_info = main.id_owner_info 
	 	inner join type on main.id_type = type.id_type
			  where type = 'радиотелефон' and oi1.owner = owner);
		
select * from Пользователи_всех_телефонов;	
drop view Пользователи_всех_телефонов;	

create view Пользователи_всех_телефонов_2
as 
select owner 
from owner_info inner join main on owner_info.id_owner_info = main.id_owner_info 
inner join type on main.id_type = type.id_type
where type = 'сотовый'
intersect
select owner 
from owner_info inner join main on owner_info.id_owner_info = main.id_owner_info 
inner join type on main.id_type = type.id_type
where type = 'стационарный'
intersect
select owner 
from owner_info inner join main on owner_info.id_owner_info = main.id_owner_info 
inner join type on main.id_type = type.id_type
where type = 'радиотелефон';

select * from Пользователи_всех_телефонов_2;
drop view Пользователи_всех_телефонов_2;	

create view Услуги_которые_не_закрывались
as 
select open_service
from service
except
select open_service
from service inner join main on service.id_service = main.id_service 
inner join service_date on main.id_services_dates = service_date.id_services_dates 
where shutdown_date is not null;

select * from Услуги_которые_не_закрывались;
drop view Услуги_которые_не_закрывались;	





create view Номера_с_инт_или_межг
as 
select phone_number, open_service
from number_info, service
where number_info.id_number_info = service.id_number_info and open_service = 'Межгород'
union
select phone_number, open_service
from number_info, service
where number_info.id_number_info = service.id_number_info and open_service = 'Интернет'
order by phone_number;

select * from Номера_с_инт_или_межг;
drop view Номера_с_инт_или_межг;	

