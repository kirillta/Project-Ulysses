create table [dbo].[Pubs] (
	[Id] int identity(1, 1) not null,
	[Title] nvarchar(100) not null,
	[EnglishTitle] nvarchar(100),
	[Type] int not null,
  [Address] nvarchar(200) not null,
  [District] nvarchar(50),
  [PostalCode] nvarchar(10),
  [Phone]  nvarchar(20),
  [WebSite] nvarchar(50),
	[IsActive] bit not null,
	[Latitude] varchar(15),
	[Longitude] varchar(15),
	[Coordinates] geography
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
		Coordinates
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
		geography::STGeomFromText('POINT(' + i.Latitude + ' ' + i.Longitude + ')', 4326)
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
go


select * from [dbo].[Pubs]