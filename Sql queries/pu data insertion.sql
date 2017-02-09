insert into Congratulations (Content, [Date], IsApproved)
values 
	(N'С Днем рождения!) Пусть у тебя всегда будет выбор между лучшим и еще лучшим, как на этом сайте :D', '2017-02-07 01:59:41.270', 1),
	(N'С др!', '2017-02-08 03:45:50.387', 1),
	(N'Ууу, с днем рождения!', '2017-02-08 07:39:38.473', 1),
	(N'здрямки', '2017-02-08 09:33:17.410', 1),
	(N'Кирилл, с Днём Рождения !!! Желаю тебя добра, успехов и больше ахуенный идей! Жаль не смогу присоединиться, так как в другой стране сейчас Зы, пиздатая идея с сайтом да и с форматом ДО, удачи! Женя В.', '2017-02-08 09:47:49.333', 1)
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

insert into Pubs (Id, [Address], EstimateStartTime, IsChosen, RoundId, Title)
values
	(1, N'Сущёвская улица, 9', '14:00', 0, 1, 'The Tipsy Pub'),
	(2, N'Новослободская улица, 18', '14:00', 0, 1, 'Grace O’Malley'),
	(3, N'Петровка, 34', 'TBD', 0, 2, 'The Scotland Yard Pub'),
	(4, N'Костянский переулок, 7/13', 'TBD', 0, 2, 'Bobby Dazzler Pub'),
	(5, N'Мясницкая улица, 15', 'TBD', 0, 3, 'Lion’s Head Pub'),
	(6, N'Мясницкая улица, 13/3', 'TBD', 0, 3, 'Mollie’s'),
	(7, N'Большая Дмитровка улица, 13', 'TBD', 0, 4, 'Tap & Barrel Pub'),
	(8, N'Столешников переулок, 8', 'TBD', 0, 4, 'Connolly Station'),
	(9, N'Неглинная улица, 10', 'TBD', 0, 5, 'The White Hart Pub'),
	(10, N'Пятницкая улица, 24', 'TBD', 0, 6, 'Molly Gwynn’s'),
	(11, N'Пятницкая улица, 29', 'TBD', 0, 6, 'O’Donoghue’s Pub'),
	(12, N'Пятницкая улица, 56, стр. 4', 'TBD', 0, 7, 'John Donne')

set identity_insert Pubs off
select * from Congratulations