insert into Congratulations (Content, [Date], IsApproved)
values 
	(N'С Днем рождения!) Пусть у тебя всегда будет выбор между лучшим и еще лучшим, как на этом сайте :D', '2017-02-07 01:59:41.270', 1),
	(N'С др!', '2017-02-08 03:45:50.387', 1),
	(N'Ууу, с днем рождения!', '2017-02-08 07:39:38.473', 1),
	(N'здрямки', '2017-02-08 09:33:17.410', 1),
	(N'Кирилл, с Днём Рождения !!! Желаю тебя добра, успехов и больше ахуенный идей! Жаль не смогу присоединиться, так как в другой стране сейчас Зы, пиздатая идея с сайтом да и с форматом ДО, удачи! Женя В.', '2017-02-08 09:47:49.333', 1),
	(N'Ёлки-палки! За работой пропустил это замечательное событие. С Днём Рождения! Пусть идеи воплощаются в жизнь, и каждый новый день приносит новые. Ура!', '2017-02-09 12:23:39.0550652', 1),
	(N'Счастья и здоровья, успехов и процветания, интересных проектов поболее и незакрытых issues поменее! P.S. Полуадаптивная верстка неплоха на большом экране.', '2017-02-09 16:47:59.3255387', 1)
go


insert into Rounds (IsDone, Number)
values
	(0, 1),
	(0, 2),
	(0, 3),
	(0, 4),
	(0, 5),
	(0, 6),
	(0, 7)
go


set identity_insert Pubs on

insert into Pubs (Id, [Address], EstimateStartTime, IsChosen, RoundId, Title, Latitude, Longitude, LogoUrl)
values
	(1, N'Сущёвская улица, 9', '2017-02-12T14:00:00', 0, 1, 'The Tipsy Pub', 55.7805478, 37.6017917, 'tipsy-logo.jpg'),
	(2, N'Новослободская улица, 18', '2017-02-12T14:00:00', 0, 1, 'Grace O’Malley', 55.7822492, 37.5992006, 'grace-o-mally-logo.jpg'),
	(3, N'Петровка, 34', '0001-01-01T00:00:00', 0, 2, 'The Scotland Yard Pub', 55.7690176, 37.6132822, 'scotland-yard-logo.jpg'),
	(4, N'Костянский переулок, 7/13', '0001-01-01T00:00:00', 0, 2, 'Bobby Dazzler Pub', 55.7676785, 37.6352938, 'bobby-dazzler-logo.jpg'),
	(5, N'Мясницкая улица, 15', '0001-01-01T00:00:00', 0, 3, 'Lion’s Head Pub', 55.762901, 37.634921, 'lions-head-logo.jpg'),
	(6, N'Мясницкая улица, 13/3', '0001-01-01T00:00:00', 0, 3, 'Mollie’s', 55.7625728, 37.6344354, 'mollies-logo.jpg'),
	(7, N'Большая Дмитровка улица, 13', '0001-01-01T00:00:00', 0, 4, 'Tap & Barrel Pub', 55.76232, 37.61357, 'tap-and-barrel-logo.jpg'),
	(8, N'Столешников переулок, 8', '0001-01-01T00:00:00', 0, 4, 'Connolly Station', 55.76232, 37.61298, 'connolly-station-logo.jpg'),
	(9, N'Неглинная улица, 10', '0001-01-01T00:00:00', 0, 5, 'The White Hart Pub', 55.7622454, 37.620331, 'the-white-hart-pub-logo.jpg'),
	(10, N'Пятницкая улица, 24', '0001-01-01T00:00:00', 0, 6, 'Molly Gwynn’s', 55.7410641, 37.6282972, 'molly-gwynns-logo.jpg'),
	(11, N'Пятницкая улица, 29', '0001-01-01T00:00:00', 0, 6, 'O’Donoghue’s Pub', 55.74093, 37.62933, 'o-donoghues-pub-logo.jpg'),
	(12, N'Пятницкая улица, 56, стр. 4', '0001-01-01T00:00:00', 0, 7, 'John Donne', 55.7337329, 37.6272888, 'john-donne-logo.jpg')

set identity_insert Pubs off
select * from Pubs 


create table Pubs (
	Id int identity(1, 1),
	[Address] nvarchar(max) null,
	[EstimateStartTime] datetime2 not null,
	IsChosen bit not null,
	RoundId int not null,
	Title nvarchar(max) null,
	IconUrl nvarchar(max) null,
	LogoUrl nvarchar(max) null,
	Latitude decimal(13, 8) not null,
	Longitude decimal(13, 8) not null,
	constraint FK_Pubs_Rounds_RoundId foreign key (RoundId) references Rounds (Id) on delete cascade
)
go

create index IX_Pubs_RoundId on Pubs (RoundId)
go
