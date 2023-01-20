	insert menus values (
	null, 
	'Carga CINEPLANET', 
	'Permite cargar de un excel códigos de promoción Cineplanet', 
	'Upload',
	'Index',
	'waves-effect',
	'fas ifas fa-tag',
	1,null,null,null,null)
	go
	insert role_menus (id_menu, id_role, status) values ((select top 1 id from menus where name = 'Carga CINEPLANET'),2,1)
	insert role_menus (id_menu, id_role, status) values ((select top 1 id from menus where name = 'Carga CINEPLANET'),5,1)
	go
	create table UPLOADS (
		ID int not null primary key identity (1,1),
		CREATED_AT datetime,
		CREATED_USER int,
		[STATUS] int
	)
	go
	create table CODES (
		ID_UPLOAD int,
		CODE varchar(50),
		[STATUS] int
	)
	go
	alter table CODES
	alter column ID_UPLOAD int not null
	go
	alter table CODES
	alter column CODE varchar(50) not null
	go
	alter table CODES
	add constraint PK_CODES primary key (ID_UPLOAD, CODE)
	go
	alter table CODES
	add constraint FK_CODES foreign key (ID_UPLOAD) references Uploads(ID)
	go
	alter proc SP_UPLOADS_INSERT
		@CREATED_USER int
	as
	begin
		insert UPLOADS (
			CREATED_AT,
			CREATED_USER,
			[STATUS]
		) values (
			getdate(),
			@CREATED_USER,
			1
		)

		select scope_identity() as id
	end
	go
	create proc SP_CODES_INSERT
		@ID_UPLOAD int,
		@CODE varchar(50)
	as
	begin
		insert CODES (
			ID_UPLOAD,
			CODE,
			[STATUS]
		) values (
			@ID_UPLOAD,
			@CODE,
			1
		)
	end