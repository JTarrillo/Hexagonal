	insert menus values (
	null, 
	'Enviar Notificaciones', 
	'Permite enviar notificaciones', 
	'Notify',
	'Index',
	'waves-effect',
	'fas ifas fa-tag',
	1,null,null,null,null)
	go
	insert role_menus (id_menu, id_role, status) values ((select top 1 id from menus where name = 'Enviar Notificaciones'),2,1)
	insert role_menus (id_menu, id_role, status) values ((select top 1 id from menus where name = 'Enviar Notificaciones'),5,1)