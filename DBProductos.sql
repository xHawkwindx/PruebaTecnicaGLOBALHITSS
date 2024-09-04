create table Producto(
	IdProducto int primary key identity,
	Nombre varchar (75), 
	Descripcion varchar (150),
    Precio decimal(12,2), 
	CantidadStock int
) 

go 

create procedure SP_getProductos
as
begin
	select * from Producto
end

go

create procedure SP_getByIdProducto(@IdProducto int)
as
begin
	select * from Producto
	where IdProducto = @IdProducto
end
go

create procedure SP_createProducto(
	@NombreProducto varchar(75),
	@Descripcion varchar (150),
	@Precio decimal (12,2),
	@CantidadStock int
)

as 
begin
	insert into Producto (Nombre, Descripcion, Precio, CantidadStock) 
	values (@NombreProducto, @Descripcion, @Precio, @CantidadStock)
end
go

create procedure SP_editProducto(
	@IdProducto int,
	@NombreProducto varchar(75),
	@Descripcion varchar (150),
	@Precio decimal (12,2),
	@CantidadStock int
)

as 
begin
	update Producto 
	 set 
	 Nombre = @NombreProducto, 
	 Descripcion = @Descripcion,
	 Precio = @Precio, 
	 CantidadStock = @CantidadStock
	 where IdProducto = @IdProducto
end
go

create procedure SP_deleteProducto(
	@IdProducto int
)

as 
begin
	delete from producto 
	where IdProducto = @IdProducto
end

go 
insert into Producto (Nombre, Descripcion, Precio, CantidadStock) values ('Silla', 'Pequeña', 80000, 80)
insert into Producto (Nombre, Descripcion, Precio, CantidadStock) values ('Mesa', '4 Puestos', 700000, 20)

