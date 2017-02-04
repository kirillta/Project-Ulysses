create table [dbo].[Pubs] (
	[Id] int identity(1, 1) not null,
	[Title] nvarchar(100) not null,
	[EnglishTitle] nvarchar(100),
	[Type]  nvarchar(50) not null,
	[Address] nvarchar(200) not null,
	[District] nvarchar(50),
	[PostalCode] nvarchar(10),
	[Phone]  nvarchar(20),
	[WebSite] nvarchar(255),
	[IsActive] bit not null,
	[Latitude] varchar(15),
	[Longitude] varchar(15),
	[Coordinates] geography,
	[Added] date,
	[Updated] date,
	[Deleted] date null
)
go


create trigger [dbo].[geo_insert] on [dbo].[Pubs]
instead of insert
as
set nocount on
	insert into [dbo].[Pubs] (
		Title,
		EnglishTitle,
		[Type],
		[Address],
		District,
		PostalCode,
		Phone,
		WebSite,
		IsActive,
		Latitude,
		Longitude,
		Coordinates,
		Added,
		Updated,
		Deleted
	) select
		i.Title,
		i.EnglishTitle,
		i.[Type],
		i.[Address],
		i.District,
		i.PostalCode,
		i.Phone,
		i.WebSite,
		i.IsActive,
		i.Latitude,
		i.Longitude,
		geography::STGeomFromText('POINT(' + i.Latitude + ' ' + i.Longitude + ')', 4326),
		getdate(),
		getdate(),
		null
	from inserted i
go


create trigger [dbo].[geo_update] on [dbo].[Pubs]
for update
as
set nocount on
if (update([Latitude]) or update([Longitude]))
begin
	update pub
	set [Coordinates] = geography::STGeomFromText('POINT(' + i.Latitude + ' ' + i.Longitude + ')', 4326)
	from [dbo].[Pubs] pub
	join inserted i 
		on pub.Id = i.Id
	where i.Latitude is not null 
		and i.Longitude is not null
end
if update([IsActive])
begin
	update pub
	set [Deleted] = getdate()
	from [dbo].[Pubs] pub
	join inserted i 
		on pub.Id = i.Id
	where i.IsActive = 0
end
else
begin
	update pub
	set [Updated] = getdate()
	from [dbo].[Pubs] pub
	join inserted i 
		on pub.Id = i.Id
end
go

--drop table [dbo].[Pubs]
--drop trigger [dbo].[geo_insert], [dbo].[geo_update]
/*
truncate table [dbo].[Pubs]
dbcc checkident('[Pubs]', reseed, 0)
*/

select * from [dbo].[Pubs]