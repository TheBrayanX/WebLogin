create database DBLogin;
use DBLogin;

create table Usuario(
	idUser int primary key identity(1,1),
	NameUser varchar(50),
	clave varchar(50),
);

create proc spValidarUsuario(
	@NameUser varchar(50),
	@clave varchar(50)
)
as
begin
	if(exists(select * from Usuario where NameUser = @NameUser and clave = @clave))
		select idUser from Usuario where NameUser = @NameUser and clave = @clave
	else
		select '0'
end


SELECT * from Usuario;

declare @registrado bit, @mensaje varchar(50);
exec sPRegistarUsuario 'mijose', 'mijo123', @registrado output, @mensaje output
select @mensaje;
select @registrado;

/*create proc sPRegistarUsuario(
	@NameUser varchar(50),
	@clave varchar(50),
	@registrado bit output,
	@mensaje varchar(50) output
)
as
begin
	if(not exists(select * from Usuario where NameUser = @NameUser))
	begin
		insert into Usuario(NameUser,clave) values (@NameUser,@clave)
		set @registrado = 1
		set @mensaje = 'El usuario fue registrado'
	end
	else
	begin
		set @registrado = 0
		set @mensaje = 'El correo ya existe'
	end
end*/

