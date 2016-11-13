USE UnstuckME_DB;
GO

/*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
DO A select FOR ALL TABLES
~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/
SELECT *
FROM [dbo].[Chat]
SELECT *
FROM [dbo].[Classes]
SELECT *
FROM [dbo].[Files]
SELECT *
FROM [dbo].[Messages]
SELECT *
FROM [dbo].[OfficialMentor]
SELECT *
FROM [dbo].[OmToUser]
SELECT *
FROM [dbo].[Picture]
SELECT *
FROM [dbo].[Report]
SELECT *
FROM [dbo].[Review]
SELECT *
FROM [dbo].[Server]
SELECT *
FROM [dbo].[Sticker]
SELECT *
FROM [dbo].[UserProfile]
SELECT *
FROM [dbo].[UserToChat]
ORDER BY UserID
SELECT *
FROM [dbo].[UserToClass]
--~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
--Make Server
INSERT INTO [Server]
VALUES ('OITServer', '192.168.1.1', 'oit.edu', 'Oregon Institute of Technology', 'admin', 'password', '.oit.edu')

--Make users
INSERT INTO UserProfile
	(DisplayFName, DisplayLName, EmailAddress, UserPassword, Privileges)
	VALUES
	('Ryan', 'Bell', 'Ryan.Bell@oit.edu', 'ThisISMyPassword', 1111);
INSERT INTO UserProfile
	(DisplayFName, DisplayLName, EmailAddress, UserPassword, Privileges)
	VALUES
	('Aruthor', 'Clark', 'AJ.Clark@oit.edu', '192@$#330-//dwd\wd', 1011);
INSERT INTO UserProfile
(DisplayFName, DisplayLName, EmailAddress, UserPassword, Privileges)
	VALUES
	('Matthew', 'Clark', 'Matt.Clark@oit.edu', 'ilikebigbuttsandicannotlie', 1000);
INSERT INTO UserProfile
	(DisplayFName, DisplayLName, EmailAddress, UserPassword, Privileges)
	VALUES
	('Kronn', 'Morgan', 'Kyronn.Morgan@oit.edu', '"~`\''565456!@#$%^&*((((', 0010);
INSERT INTO UserProfile
	(DisplayFName, DisplayLName, EmailAddress, UserPassword, Privileges)
	VALUES
	('Jacob', 'Arnt', 'Jacob.Arnt@oit.edu', 'JacobArnt', 0110);
INSERT INTO UserProfile
	(DisplayFName, DisplayLName, EmailAddress, UserPassword, Privileges)
	VALUES
	('Taylor', 'Jones', 'Taulor.Jones@oit.edu', 'password', 11101);

SELECT *
FROM UserProfile

--Add Profile images to each user
insert into Picture(UserID, Photo) values (1, CONVERT(varbinary(MAX), 'vulputate nonummy maecenas tincidunt lacus at velit vivamus vel nulla eget'));
insert into Picture (UserID, Photo) values (3, CONVERT(varbinary(MAX), 'primis in faucibus orci luctus et ultrices posuere cubilia curae'));
insert into Picture (UserID, Photo) values (5, CONVERT(varbinary(MAX), 'elementum ligula vehicula consequat morbi a ipsum integer a nibh in quis justo'));
insert into Picture (UserID, Photo) values (6, CONVERT(varbinary(MAX), 'diam erat fermentum justo nec condimentum neque sapien placerat ante nulla justo aliquam quis'));

--8 New chats
INSERT INTO CHAT
DEFAULT VALUES
INSERT INTO CHAT
DEFAULT VALUES
INSERT INTO CHAT
DEFAULT VALUES
INSERT INTO CHAT
DEFAULT VALUES
INSERT INTO CHAT
DEFAULT VALUES
INSERT INTO CHAT
DEFAULT VALUES
INSERT INTO CHAT
DEFAULT VALUES
INSERT INTO CHAT
DEFAULT VALUES

--Insert Fake Messeges
INSERT INTO [Messages]
VALUES	(1,'Hi9DH7QeVwXHRoynjrgj0oN2CTHv5aS67KekwhsJUxUSsuf7EoynWo9hFsGZpDOLQC8HNIg2Z6vh9LN7GYpAbXv78VtaXit3sXCCPaEp2h3vY8vRuO3jY56R1iPZlHx9itkBiSA1l5J0hpFhvT94HNmQzg0og0Keu1BgCJYtCAQTJ3oC78QXfHCL8uVRnCpo0AsVZVQ4fCalAhpv9wnlPhrDa3yzY8bjhwmgZKM3XK4D5rqRGRYTkqBojbJkl1WbwKapsqtSaZxOWHet8TPVrIBKUzocDezTQ8yHFhShLLYIUAbs7hr5HuOxuniEgYxQhzTiiF59UzgDv0TLsq8wXmxfLir1wwpEpvPSLiMSQF0yxg3Gx8mo9MPe8vYII45iQkYruB6XIRI1Y4wEZEbNpBRMpkWkyMKrWRlmroGHXQznyD4I9xKosC0gDRFDc9LQXT'),
(1, 'b21ArDaWkua1rVsRJQQjAFUNJYCLaJb4YJP9WmbRL7AsC94xc2yAxiZJV49DNFUMvl18bpfFRotYhc4yepiHWzGtE62FqiuZQnxW0PXB0YW7Bx1VWv03ANKUAiEVxyEqX2MurzN15pweatNvAiq6hF0CHPZ6UuOHe6nocuPTBswIjH2xjf0TttDrQgOoreNs69t9OFcPFrJP3JIuL2mQzsHjsoARtr60M0GLKWWiM2FfgCg3sFCLkvNBRjvFrclOEoqstttVQtnvJToSiXVKULSG3pc26M40wvWO3PrRAi2Nw1cKD5TJCHuarBCSXbHiHX4ElGZhyUSYSBjzfoiaC1QQvArKcqi82NNYYtQnEOHeWjFWqBxKJncDsSyj6lmDEMMrYRBNTaWjpDZllD9nFIlKg3pnxKqER4ONalowugHTTw87Lcz3BTiuocjtk5GVLM'),
(2, 'oojmAHvgsu31Agxf0CD1kRo96tSWPZJllbblbmSVOYLXqbDrSIjUatoSk9YXS1j9rtf3APawQT9xfpqySZjJyQ3xyit50LmnXiiGIfONArBZN2VhW3xgG4mDsQtSnF5jMjPinrLCWQFjOgQAp7ftxxmAiChDPxD4v1pVAuLSe42arnGqfpbaP2nUXyqoQ8ruArB2bIw1T7bGfMgwMG1hH1sazeEmYl5r4H3ooAmk85pl89Quwn10PocaNO913Kt2KUzS0Jifz49xHsTvI1E3jXgTqy0ApDJbwTsZqlY2QeU9CeLOISnMHth5uMlyaA3jQ5C9GWe5MufN8Th8xiLcpkBVkIQM2ppG1K0pNEkNxoVIXGgFCur4CqMcMm3ZCnXG2vvPpyj8ReiKPp0BcMf4kRzmGqTm5avU0NIO7gqQXKmzwnh6u4qafvDK7ZL6w4OsfW'),
(3, '4yLHBeoMLpitWAji1EfQzMVuPVeO4MyZf034ClKuUaKGtpTPUort00D32OATkrBrfSHMDCEmnlPNeziA0EjOiI5aNVBhQil1jK4UQ1nN1m80KGb8RZxkkXC9FBpJrQrSqBjnDCQcT3PgqNe4E6gzwU03YEojKXBeRepwPJ8B35h6UEGZH3vWPLGIaNCZIZhFrlcqtBYMrFFazuWHXzxP9yA7ME1aGQlKY2fDFxugNmY0WZNXuJZxyt8kPC82JSLxfSxfTFZ0klpru1FHjfyglfMkM3Z3wpXweDIVe2Tna02zf46zXeCcsBPJ2bVk5T9SeA68hAbue2sF5TVXarRNRrPngqZ19KJzqOXYUrtxUmnxg4NGDDrXV4LrvTGCftuut96AjNAV0YOJqifJAqAtui2nDxXx247vv4t7QvQMZ4hbznu4SWRQZ0LwRmOMkigFkl'),
(4, 'Grx4qYnBxPyESa4T3B4TPAx0bzrM3HIE5PxKFpF64ugshwemHM3KbFKMeXBi5A41HL74UfPfMDENeytcap3OmAMlvM0abp3x4qXANCSU3quPm2oLsD95Aull5NXoCS6pQou7R4jzqOVrLVGqy3ghf26E0NmnmyFnkN82J9KZzoUHIP9ovZy0QVYnjTUuJkLpSeou8w8PyqOCQsSLgb1ID829fArugUsG19sL8nIAjMlsKNC5eiCNxJ0999Asq7hPnNoXAI0otmONABPlcsIXWQJh14RXefsWuc43iAfk7WXR3cH9UCQZbxOeXZYHQ5oECtcAxFakIzDJxlpnAtNWIrqHhw0Y2VG2W9ERLfQmiWuCZpxUVa5makbEDD6nSnFecH3hvSiSqg8bP6yRAs1UDiHwv3zcIBmmlHWnuCHr9UlCzFub4nzKMB1a4Z5P4BRNrx'),
(5, 'KHQ2CJl37nmomaEnQ7CnLXgQ8VyuYtFQSBt2WCaqEVMCGpKmqkcfOqNi5AjGTlyfsjPj3852X5GNWis9CaDyajDmJqcZ4pT7koZikfEegyG83Go3JgkAGha1gNZMzureMS8xB6f2wRN1O5MI1Aa5bpoyeBg9nsJDVZ38QaVtp6IYBFmrpfZIGUGzxvY3GyIY2cubouVNJe84IV2klF36YEI8ofyXPezosCCILYYEoREDKsc6734AOUImL4w1vQOaOnMFzsmkHx6jb0GDXOBMmX05jVOYPAI2FGggcTLXs7Kwrv2m6R9tizWZNfBNKNqKtAEya1tU17chhMF5pcgzcGrF6hGKQKUgYBIMWebGBRPKcep6Eqb8r2PgrVm7LEomND4jRNhQ9e72rWOkiJCeKqJpevgZluMhHWBiE6e5xQVjJrTkZkOqQnequ2LUw7xWyk'),
(6, 'sX1Jfw2ngV4EU49QN0AQiQMZkBRR33OSrf2NYPjkLgII3sbeX3U5zwiQk1vqDEPb8GmG6cIAYPz5e57Swk6AiyxN5FzkKGGRtFuIt8x8cXt0nmI3tEV55UgoktCjsJ1zVHvRxMtEmqVy9MubNhTmPeYTc3YrBcyVzoi50jzDV7Xy0MAKPf5izS1BJoDKg9xwIR2V01IDuoHqRkh322lBwLxuKxcNyVXRi4p2u2xIn3Ixfee8SjOkW9oEM2tlh79DEVwaYZ1bf7K3yafU1NJ8umXtbSsimsKujbZJR2LKyu5F0YKsKRCIwPiL5OB3VaOo6EkhDRNeLPLfvn3fUyvsaLuA1iGzGWT5lM3A3ub1yKs242DvaUjeSyaunB0YKNAueqxPgKlHz2FYRU9CXyRNI5gszQHHVi7S7HqmalgAylnymGryCCPlAhYTMM9CtWm2KW'),
(7, '0fubERoY244VKxVg6R7Q6bqNTjn6yi5gcDeyGJECcT1h6brwsOPhJHSFkA9rmekGsgcVHsJVo4a3pifrogmhBrAoyYGTKU2wsuOaCx64xxf91q5c7gfGgP9Uupig9K4SRo8uCJ6z09VcE2VFQ57oWvYyqvEsPWYKi2VqD9fP4t0Q3qXl9MEA6koVTkbAjWLAhwxk5jIqB8UejYlgs8yk4wFGa1U1ph3pzK7k5k0CAtOilJoAhMSj4HrvOocM1J6J5kgUV6Gw3VQE0hIDTeeneanEJVnUces4sptOJAunD7AjnqDP2VGDQ7a46FY6mOx9NQI6Jv5R14X0X1q2KcYShZ4nGjgtJR1085rsM1htrRvoUmiqtB3HX4lc1IRXDZ0fUiqHo7UkSuotbbFGIkORprH9rjYF8M03DuEjXBABqDVCZrQsJ0DO7TYSpC8iCNwWiX'),
(8, 'JXjToQDhzgHEZB7sxvwKG00FjirCtsrXThxJowbEa00TEQIjmHfaFCgialBTCq86ViSUn6h6le3572Yx1L4wCLOrNo8KS5awMefFjJ5BcNGzXQPaJbowBR49coagfwjBaeBrRkDsNxyPKXs0rxQ4eHhESHeu5CcmZwwOlnlnDSMbLSIKnDqvkc68A6UxbhJvUbN7wbNRocMItGeTCJBZCGI3b8ZY1kFck4rWtBXGMe0aUcjvMik67gXX0594GJ9iobRaXIO6oC63Jea6KWU1Eht8qQuYspwcupwoucP7lsN3XAkUE9qqyllVN9LuST7MAo9qSPQLKtoMjAcyYhAcIPyXmnfNqm3eJBOVDEVHgkaeAtg2tMcYAFg5wZitpQ3aVvYcEEtcxl5ipD7X0F5SbYshUE7GvyIANuBJU4t6gbmoScCs2xGCQUB7Teh8oqnTMk'),
(1, '5iI2LrYl3DKphozlMuQTpeaC8RUOTPcwjZaZ6hYgVtHPLHTfMInKjAJ3h66jX5KK8GyipUHuzZyo57HalRfjnZ81zxAL71TUWVHyxLSakACoWGRx4xwVybfAI91xUHSWKzClfQR8w3KDN0Uei98Fm7XlRIFWxs2ZZGY69WrxQ0v6qC8QwFBfwVkM5XVABgSCpV77aGYpnvrRC9nkWBy73g6QoXTirwzZfORclu4z7fVVajuK0lvknElpREekSSBYjr8Jg3s4s2r2CwpUWR2kOsW9aAsMQvgT8xwIjU5jkNOWL6P9IhcKRJuCDWHVS4Ijb4WV3qGfBfrwMwCgPZoBlRHYGUpjFXB7gmOMELnB5GI4xquhcPBJvJWSwaaKuQ68W4jnuPYuZj0HTcY2feLBnGYkRbj0JgiDAIQla7Z39NOFum1o0W8IoEBl4AWFz7iWqe'),
(2, 'Hi9DH7QeVwXHRoynjrgj0oN2CTHv5aS67KekwhsJUxUSsuf7EoynWo9hFsGZpDOLQC8HNIg2Z6vh9LN7GYpAbXv78VtaXit3sXCCPaEp2h3vY8vRuO3jY56R1iPZlHx9itkBiSA1l5J0hpFhvT94HNmQzg0og0Keu1BgCJYtCAQTJ3oC78QXfHCL8uVRnCpo0AsVZVQ4fCalAhpv9wnlPhrDa3yzY8bjhwmgZKM3XK4D5rqRGRYTkqBojbJkl1WbwKapsqtSaZxOWHet8TPVrIBKUzocDezTQ8yHFhShLLYIUAbs7hr5HuOxuniEgYxQhzTiiF59UzgDv0TLsq8wXmxfLir1wwpEpvPSLiMSQF0yxg3Gx8mo9MPe8vYII45iQkYruB6XIRI1Y4wEZEbNpBRMpkWkyMKrWRlmroGHXQznyD4I9xKosC0gDRFDc9LQXT'),
(4, 'b21ArDaWkua1rVsRJQQjAFUNJYCLaJb4YJP9WmbRL7AsC94xc2yAxiZJV49DNFUMvl18bpfFRotYhc4yepiHWzGtE62FqiuZQnxW0PXB0YW7Bx1VWv03ANKUAiEVxyEqX2MurzN15pweatNvAiq6hF0CHPZ6UuOHe6nocuPTBswIjH2xjf0TttDrQgOoreNs69t9OFcPFrJP3JIuL2mQzsHjsoARtr60M0GLKWWiM2FfgCg3sFCLkvNBRjvFrclOEoqstttVQtnvJToSiXVKULSG3pc26M40wvWO3PrRAi2Nw1cKD5TJCHuarBCSXbHiHX4ElGZhyUSYSBjzfoiaC1QQvArKcqi82NNYYtQnEOHeWjFWqBxKJncDsSyj6lmDEMMrYRBNTaWjpDZllD9nFIlKg3pnxKqER4ONalowugHTTw87Lcz3BTiuocjtk5GVLM'),
(6, 'oojmAHvgsu31Agxf0CD1kRo96tSWPZJllbblbmSVOYLXqbDrSIjUatoSk9YXS1j9rtf3APawQT9xfpqySZjJyQ3xyit50LmnXiiGIfONArBZN2VhW3xgG4mDsQtSnF5jMjPinrLCWQFjOgQAp7ftxxmAiChDPxD4v1pVAuLSe42arnGqfpbaP2nUXyqoQ8ruArB2bIw1T7bGfMgwMG1hH1sazeEmYl5r4H3ooAmk85pl89Quwn10PocaNO913Kt2KUzS0Jifz49xHsTvI1E3jXgTqy0ApDJbwTsZqlY2QeU9CeLOISnMHth5uMlyaA3jQ5C9GWe5MufN8Th8xiLcpkBVkIQM2ppG1K0pNEkNxoVIXGgFCur4CqMcMm3ZCnXG2vvPpyj8ReiKPp0BcMf4kRzmGqTm5avU0NIO7gqQXKmzwnh6u4qafvDK7ZL6w4OsfW'),
(7, '4yLHBeoMLpitWAji1EfQzMVuPVeO4MyZf034ClKuUaKGtpTPUort00D32OATkrBrfSHMDCEmnlPNeziA0EjOiI5aNVBhQil1jK4UQ1nN1m80KGb8RZxkkXC9FBpJrQrSqBjnDCQcT3PgqNe4E6gzwU03YEojKXBeRepwPJ8B35h6UEGZH3vWPLGIaNCZIZhFrlcqtBYMrFFazuWHXzxP9yA7ME1aGQlKY2fDFxugNmY0WZNXuJZxyt8kPC82JSLxfSxfTFZ0klpru1FHjfyglfMkM3Z3wpXweDIVe2Tna02zf46zXeCcsBPJ2bVk5T9SeA68hAbue2sF5TVXarRNRrPngqZ19KJzqOXYUrtxUmnxg4NGDDrXV4LrvTGCftuut96AjNAV0YOJqifJAqAtui2nDxXx247vv4t7QvQMZ4hbznu4SWRQZ0LwRmOMkigFkl'),
(1, 'Grx4qYnBxPyESa4T3B4TPAx0bzrM3HIE5PxKFpF64ugshwemHM3KbFKMeXBi5A41HL74UfPfMDENeytcap3OmAMlvM0abp3x4qXANCSU3quPm2oLsD95Aull5NXoCS6pQou7R4jzqOVrLVGqy3ghf26E0NmnmyFnkN82J9KZzoUHIP9ovZy0QVYnjTUuJkLpSeou8w8PyqOCQsSLgb1ID829fArugUsG19sL8nIAjMlsKNC5eiCNxJ0999Asq7hPnNoXAI0otmONABPlcsIXWQJh14RXefsWuc43iAfk7WXR3cH9UCQZbxOeXZYHQ5oECtcAxFakIzDJxlpnAtNWIrqHhw0Y2VG2W9ERLfQmiWuCZpxUVa5makbEDD6nSnFecH3hvSiSqg8bP6yRAs1UDiHwv3zcIBmmlHWnuCHr9UlCzFub4nzKMB1a4Z5P4BRNrx'),
(4, 'KHQ2CJl37nmomaEnQ7CnLXgQ8VyuYtFQSBt2WCaqEVMCGpKmqkcfOqNi5AjGTlyfsjPj3852X5GNWis9CaDyajDmJqcZ4pT7koZikfEegyG83Go3JgkAGha1gNZMzureMS8xB6f2wRN1O5MI1Aa5bpoyeBg9nsJDVZ38QaVtp6IYBFmrpfZIGUGzxvY3GyIY2cubouVNJe84IV2klF36YEI8ofyXPezosCCILYYEoREDKsc6734AOUImL4w1vQOaOnMFzsmkHx6jb0GDXOBMmX05jVOYPAI2FGggcTLXs7Kwrv2m6R9tizWZNfBNKNqKtAEya1tU17chhMF5pcgzcGrF6hGKQKUgYBIMWebGBRPKcep6Eqb8r2PgrVm7LEomND4jRNhQ9e72rWOkiJCeKqJpevgZluMhHWBiE6e5xQVjJrTkZkOqQnequ2LUw7xWyk'),
(2, 'sX1Jfw2ngV4EU49QN0AQiQMZkBRR33OSrf2NYPjkLgII3sbeX3U5zwiQk1vqDEPb8GmG6cIAYPz5e57Swk6AiyxN5FzkKGGRtFuIt8x8cXt0nmI3tEV55UgoktCjsJ1zVHvRxMtEmqVy9MubNhTmPeYTc3YrBcyVzoi50jzDV7Xy0MAKPf5izS1BJoDKg9xwIR2V01IDuoHqRkh322lBwLxuKxcNyVXRi4p2u2xIn3Ixfee8SjOkW9oEM2tlh79DEVwaYZ1bf7K3yafU1NJ8umXtbSsimsKujbZJR2LKyu5F0YKsKRCIwPiL5OB3VaOo6EkhDRNeLPLfvn3fUyvsaLuA1iGzGWT5lM3A3ub1yKs242DvaUjeSyaunB0YKNAueqxPgKlHz2FYRU9CXyRNI5gszQHHVi7S7HqmalgAylnymGryCCPlAhYTMM9CtWm2KW'),
(2, '0fubERoY244VKxVg6R7Q6bqNTjn6yi5gcDeyGJECcT1h6brwsOPhJHSFkA9rmekGsgcVHsJVo4a3pifrogmhBrAoyYGTKU2wsuOaCx64xxf91q5c7gfGgP9Uupig9K4SRo8uCJ6z09VcE2VFQ57oWvYyqvEsPWYKi2VqD9fP4t0Q3qXl9MEA6koVTkbAjWLAhwxk5jIqB8UejYlgs8yk4wFGa1U1ph3pzK7k5k0CAtOilJoAhMSj4HrvOocM1J6J5kgUV6Gw3VQE0hIDTeeneanEJVnUces4sptOJAunD7AjnqDP2VGDQ7a46FY6mOx9NQI6Jv5R14X0X1q2KcYShZ4nGjgtJR1085rsM1htrRvoUmiqtB3HX4lc1IRXDZ0fUiqHo7UkSuotbbFGIkORprH9rjYF8M03DuEjXBABqDVCZrQsJ0DO7TYSpC8iCNwWiX'),
(3, 'JXjToQDhzgHEZB7sxvwKG00FjirCtsrXThxJowbEa00TEQIjmHfaFCgialBTCq86ViSUn6h6le3572Yx1L4wCLOrNo8KS5awMefFjJ5BcNGzXQPaJbowBR49coagfwjBaeBrRkDsNxyPKXs0rxQ4eHhESHeu5CcmZwwOlnlnDSMbLSIKnDqvkc68A6UxbhJvUbN7wbNRocMItGeTCJBZCGI3b8ZY1kFck4rWtBXGMe0aUcjvMik67gXX0594GJ9iobRaXIO6oC63Jea6KWU1Eht8qQuYspwcupwoucP7lsN3XAkUE9qqyllVN9LuST7MAo9qSPQLKtoMjAcyYhAcIPyXmnfNqm3eJBOVDEVHgkaeAtg2tMcYAFg5wZitpQ3aVvYcEEtcxl5ipD7X0F5SbYshUE7GvyIANuBJU4t6gbmoScCs2xGCQUB7Teh8oqnTMk'),
(4, '5iI2LrYl3DKphozlMuQTpeaC8RUOTPcwjZaZ6hYgVtHPLHTfMInKjAJ3h66jX5KK8GyipUHuzZyo57HalRfjnZ81zxAL71TUWVHyxLSakACoWGRx4xwVybfAI91xUHSWKzClfQR8w3KDN0Uei98Fm7XlRIFWxs2ZZGY69WrxQ0v6qC8QwFBfwVkM5XVABgSCpV77aGYpnvrRC9nkWBy73g6QoXTirwzZfORclu4z7fVVajuK0lvknElpREekSSBYjr8Jg3s4s2r2CwpUWR2kOsW9aAsMQvgT8xwIjU5jkNOWL6P9IhcKRJuCDWHVS4Ijb4WV3qGfBfrwMwCgPZoBlRHYGUpjFXB7gmOMELnB5GI4xquhcPBJvJWSwaaKuQ68W4jnuPYuZj0HTcY2feLBnGYkRbj0JgiDAIQla7Z39NOFum1o0W8IoEBl4AWFz7iWqe'),
(4, 'Hi9DH7QeVwXHRoynjrgj0oN2CTHv5aS67KekwhsJUxUSsuf7EoynWo9hFsGZpDOLQC8HNIg2Z6vh9LN7GYpAbXv78VtaXit3sXCCPaEp2h3vY8vRuO3jY56R1iPZlHx9itkBiSA1l5J0hpFhvT94HNmQzg0og0Keu1BgCJYtCAQTJ3oC78QXfHCL8uVRnCpo0AsVZVQ4fCalAhpv9wnlPhrDa3yzY8bjhwmgZKM3XK4D5rqRGRYTkqBojbJkl1WbwKapsqtSaZxOWHet8TPVrIBKUzocDezTQ8yHFhShLLYIUAbs7hr5HuOxuniEgYxQhzTiiF59UzgDv0TLsq8wXmxfLir1wwpEpvPSLiMSQF0yxg3Gx8mo9MPe8vYII45iQkYruB6XIRI1Y4wEZEbNpBRMpkWkyMKrWRlmroGHXQznyD4I9xKosC0gDRFDc9LQXT'),
(4, 'b21ArDaWkua1rVsRJQQjAFUNJYCLaJb4YJP9WmbRL7AsC94xc2yAxiZJV49DNFUMvl18bpfFRotYhc4yepiHWzGtE62FqiuZQnxW0PXB0YW7Bx1VWv03ANKUAiEVxyEqX2MurzN15pweatNvAiq6hF0CHPZ6UuOHe6nocuPTBswIjH2xjf0TttDrQgOoreNs69t9OFcPFrJP3JIuL2mQzsHjsoARtr60M0GLKWWiM2FfgCg3sFCLkvNBRjvFrclOEoqstttVQtnvJToSiXVKULSG3pc26M40wvWO3PrRAi2Nw1cKD5TJCHuarBCSXbHiHX4ElGZhyUSYSBjzfoiaC1QQvArKcqi82NNYYtQnEOHeWjFWqBxKJncDsSyj6lmDEMMrYRBNTaWjpDZllD9nFIlKg3pnxKqER4ONalowugHTTw87Lcz3BTiuocjtk5GVLM'),
(2, 'oojmAHvgsu31Agxf0CD1kRo96tSWPZJllbblbmSVOYLXqbDrSIjUatoSk9YXS1j9rtf3APawQT9xfpqySZjJyQ3xyit50LmnXiiGIfONArBZN2VhW3xgG4mDsQtSnF5jMjPinrLCWQFjOgQAp7ftxxmAiChDPxD4v1pVAuLSe42arnGqfpbaP2nUXyqoQ8ruArB2bIw1T7bGfMgwMG1hH1sazeEmYl5r4H3ooAmk85pl89Quwn10PocaNO913Kt2KUzS0Jifz49xHsTvI1E3jXgTqy0ApDJbwTsZqlY2QeU9CeLOISnMHth5uMlyaA3jQ5C9GWe5MufN8Th8xiLcpkBVkIQM2ppG1K0pNEkNxoVIXGgFCur4CqMcMm3ZCnXG2vvPpyj8ReiKPp0BcMf4kRzmGqTm5avU0NIO7gqQXKmzwnh6u4qafvDK7ZL6w4OsfW'),
(6, '4yLHBeoMLpitWAji1EfQzMVuPVeO4MyZf034ClKuUaKGtpTPUort00D32OATkrBrfSHMDCEmnlPNeziA0EjOiI5aNVBhQil1jK4UQ1nN1m80KGb8RZxkkXC9FBpJrQrSqBjnDCQcT3PgqNe4E6gzwU03YEojKXBeRepwPJ8B35h6UEGZH3vWPLGIaNCZIZhFrlcqtBYMrFFazuWHXzxP9yA7ME1aGQlKY2fDFxugNmY0WZNXuJZxyt8kPC82JSLxfSxfTFZ0klpru1FHjfyglfMkM3Z3wpXweDIVe2Tna02zf46zXeCcsBPJ2bVk5T9SeA68hAbue2sF5TVXarRNRrPngqZ19KJzqOXYUrtxUmnxg4NGDDrXV4LrvTGCftuut96AjNAV0YOJqifJAqAtui2nDxXx247vv4t7QvQMZ4hbznu4SWRQZ0LwRmOMkigFkl'),
(7, 'Grx4qYnBxPyESa4T3B4TPAx0bzrM3HIE5PxKFpF64ugshwemHM3KbFKMeXBi5A41HL74UfPfMDENeytcap3OmAMlvM0abp3x4qXANCSU3quPm2oLsD95Aull5NXoCS6pQou7R4jzqOVrLVGqy3ghf26E0NmnmyFnkN82J9KZzoUHIP9ovZy0QVYnjTUuJkLpSeou8w8PyqOCQsSLgb1ID829fArugUsG19sL8nIAjMlsKNC5eiCNxJ0999Asq7hPnNoXAI0otmONABPlcsIXWQJh14RXefsWuc43iAfk7WXR3cH9UCQZbxOeXZYHQ5oECtcAxFakIzDJxlpnAtNWIrqHhw0Y2VG2W9ERLfQmiWuCZpxUVa5makbEDD6nSnFecH3hvSiSqg8bP6yRAs1UDiHwv3zcIBmmlHWnuCHr9UlCzFub4nzKMB1a4Z5P4BRNrx'),
(2, 'KHQ2CJl37nmomaEnQ7CnLXgQ8VyuYtFQSBt2WCaqEVMCGpKmqkcfOqNi5AjGTlyfsjPj3852X5GNWis9CaDyajDmJqcZ4pT7koZikfEegyG83Go3JgkAGha1gNZMzureMS8xB6f2wRN1O5MI1Aa5bpoyeBg9nsJDVZ38QaVtp6IYBFmrpfZIGUGzxvY3GyIY2cubouVNJe84IV2klF36YEI8ofyXPezosCCILYYEoREDKsc6734AOUImL4w1vQOaOnMFzsmkHx6jb0GDXOBMmX05jVOYPAI2FGggcTLXs7Kwrv2m6R9tizWZNfBNKNqKtAEya1tU17chhMF5pcgzcGrF6hGKQKUgYBIMWebGBRPKcep6Eqb8r2PgrVm7LEomND4jRNhQ9e72rWOkiJCeKqJpevgZluMhHWBiE6e5xQVjJrTkZkOqQnequ2LUw7xWyk'),
(5, 'sX1Jfw2ngV4EU49QN0AQiQMZkBRR33OSrf2NYPjkLgII3sbeX3U5zwiQk1vqDEPb8GmG6cIAYPz5e57Swk6AiyxN5FzkKGGRtFuIt8x8cXt0nmI3tEV55UgoktCjsJ1zVHvRxMtEmqVy9MubNhTmPeYTc3YrBcyVzoi50jzDV7Xy0MAKPf5izS1BJoDKg9xwIR2V01IDuoHqRkh322lBwLxuKxcNyVXRi4p2u2xIn3Ixfee8SjOkW9oEM2tlh79DEVwaYZ1bf7K3yafU1NJ8umXtbSsimsKujbZJR2LKyu5F0YKsKRCIwPiL5OB3VaOo6EkhDRNeLPLfvn3fUyvsaLuA1iGzGWT5lM3A3ub1yKs242DvaUjeSyaunB0YKNAueqxPgKlHz2FYRU9CXyRNI5gszQHHVi7S7HqmalgAylnymGryCCPlAhYTMM9CtWm2KW'),
(2, '0fubERoY244VKxVg6R7Q6bqNTjn6yi5gcDeyGJECcT1h6brwsOPhJHSFkA9rmekGsgcVHsJVo4a3pifrogmhBrAoyYGTKU2wsuOaCx64xxf91q5c7gfGgP9Uupig9K4SRo8uCJ6z09VcE2VFQ57oWvYyqvEsPWYKi2VqD9fP4t0Q3qXl9MEA6koVTkbAjWLAhwxk5jIqB8UejYlgs8yk4wFGa1U1ph3pzK7k5k0CAtOilJoAhMSj4HrvOocM1J6J5kgUV6Gw3VQE0hIDTeeneanEJVnUces4sptOJAunD7AjnqDP2VGDQ7a46FY6mOx9NQI6Jv5R14X0X1q2KcYShZ4nGjgtJR1085rsM1htrRvoUmiqtB3HX4lc1IRXDZ0fUiqHo7UkSuotbbFGIkORprH9rjYF8M03DuEjXBABqDVCZrQsJ0DO7TYSpC8iCNwWiX'),
(2, 'JXjToQDhzgHEZB7sxvwKG00FjirCtsrXThxJowbEa00TEQIjmHfaFCgialBTCq86ViSUn6h6le3572Yx1L4wCLOrNo8KS5awMefFjJ5BcNGzXQPaJbowBR49coagfwjBaeBrRkDsNxyPKXs0rxQ4eHhESHeu5CcmZwwOlnlnDSMbLSIKnDqvkc68A6UxbhJvUbN7wbNRocMItGeTCJBZCGI3b8ZY1kFck4rWtBXGMe0aUcjvMik67gXX0594GJ9iobRaXIO6oC63Jea6KWU1Eht8qQuYspwcupwoucP7lsN3XAkUE9qqyllVN9LuST7MAo9qSPQLKtoMjAcyYhAcIPyXmnfNqm3eJBOVDEVHgkaeAtg2tMcYAFg5wZitpQ3aVvYcEEtcxl5ipD7X0F5SbYshUE7GvyIANuBJU4t6gbmoScCs2xGCQUB7Teh8oqnTMk'),
(3, '5iI2LrYl3DKphozlMuQTpeaC8RUOTPcwjZaZ6hYgVtHPLHTfMInKjAJ3h66jX5KK8GyipUHuzZyo57HalRfjnZ81zxAL71TUWVHyxLSakACoWGRx4xwVybfAI91xUHSWKzClfQR8w3KDN0Uei98Fm7XlRIFWxs2ZZGY69WrxQ0v6qC8QwFBfwVkM5XVABgSCpV77aGYpnvrRC9nkWBy73g6QoXTirwzZfORclu4z7fVVajuK0lvknElpREekSSBYjr8Jg3s4s2r2CwpUWR2kOsW9aAsMQvgT8xwIjU5jkNOWL6P9IhcKRJuCDWHVS4Ijb4WV3qGfBfrwMwCgPZoBlRHYGUpjFXB7gmOMELnB5GI4xquhcPBJvJWSwaaKuQ68W4jnuPYuZj0HTcY2feLBnGYkRbj0JgiDAIQla7Z39NOFum1o0W8IoEBl4AWFz7iWqe'),
(2, 'Hi9DH7QeVwXHRoynjrgj0oN2CTHv5aS67KekwhsJUxUSsuf7EoynWo9hFsGZpDOLQC8HNIg2Z6vh9LN7GYpAbXv78VtaXit3sXCCPaEp2h3vY8vRuO3jY56R1iPZlHx9itkBiSA1l5J0hpFhvT94HNmQzg0og0Keu1BgCJYtCAQTJ3oC78QXfHCL8uVRnCpo0AsVZVQ4fCalAhpv9wnlPhrDa3yzY8bjhwmgZKM3XK4D5rqRGRYTkqBojbJkl1WbwKapsqtSaZxOWHet8TPVrIBKUzocDezTQ8yHFhShLLYIUAbs7hr5HuOxuniEgYxQhzTiiF59UzgDv0TLsq8wXmxfLir1wwpEpvPSLiMSQF0yxg3Gx8mo9MPe8vYII45iQkYruB6XIRI1Y4wEZEbNpBRMpkWkyMKrWRlmroGHXQznyD4I9xKosC0gDRFDc9LQXT'),
(1, 'b21ArDaWkua1rVsRJQQjAFUNJYCLaJb4YJP9WmbRL7AsC94xc2yAxiZJV49DNFUMvl18bpfFRotYhc4yepiHWzGtE62FqiuZQnxW0PXB0YW7Bx1VWv03ANKUAiEVxyEqX2MurzN15pweatNvAiq6hF0CHPZ6UuOHe6nocuPTBswIjH2xjf0TttDrQgOoreNs69t9OFcPFrJP3JIuL2mQzsHjsoARtr60M0GLKWWiM2FfgCg3sFCLkvNBRjvFrclOEoqstttVQtnvJToSiXVKULSG3pc26M40wvWO3PrRAi2Nw1cKD5TJCHuarBCSXbHiHX4ElGZhyUSYSBjzfoiaC1QQvArKcqi82NNYYtQnEOHeWjFWqBxKJncDsSyj6lmDEMMrYRBNTaWjpDZllD9nFIlKg3pnxKqER4ONalowugHTTw87Lcz3BTiuocjtk5GVLM'),
(6, 'oojmAHvgsu31Agxf0CD1kRo96tSWPZJllbblbmSVOYLXqbDrSIjUatoSk9YXS1j9rtf3APawQT9xfpqySZjJyQ3xyit50LmnXiiGIfONArBZN2VhW3xgG4mDsQtSnF5jMjPinrLCWQFjOgQAp7ftxxmAiChDPxD4v1pVAuLSe42arnGqfpbaP2nUXyqoQ8ruArB2bIw1T7bGfMgwMG1hH1sazeEmYl5r4H3ooAmk85pl89Quwn10PocaNO913Kt2KUzS0Jifz49xHsTvI1E3jXgTqy0ApDJbwTsZqlY2QeU9CeLOISnMHth5uMlyaA3jQ5C9GWe5MufN8Th8xiLcpkBVkIQM2ppG1K0pNEkNxoVIXGgFCur4CqMcMm3ZCnXG2vvPpyj8ReiKPp0BcMf4kRzmGqTm5avU0NIO7gqQXKmzwnh6u4qafvDK7ZL6w4OsfW'),
(5, '4yLHBeoMLpitWAji1EfQzMVuPVeO4MyZf034ClKuUaKGtpTPUort00D32OATkrBrfSHMDCEmnlPNeziA0EjOiI5aNVBhQil1jK4UQ1nN1m80KGb8RZxkkXC9FBpJrQrSqBjnDCQcT3PgqNe4E6gzwU03YEojKXBeRepwPJ8B35h6UEGZH3vWPLGIaNCZIZhFrlcqtBYMrFFazuWHXzxP9yA7ME1aGQlKY2fDFxugNmY0WZNXuJZxyt8kPC82JSLxfSxfTFZ0klpru1FHjfyglfMkM3Z3wpXweDIVe2Tna02zf46zXeCcsBPJ2bVk5T9SeA68hAbue2sF5TVXarRNRrPngqZ19KJzqOXYUrtxUmnxg4NGDDrXV4LrvTGCftuut96AjNAV0YOJqifJAqAtui2nDxXx247vv4t7QvQMZ4hbznu4SWRQZ0LwRmOMkigFkl'),
(4, 'Grx4qYnBxPyESa4T3B4TPAx0bzrM3HIE5PxKFpF64ugshwemHM3KbFKMeXBi5A41HL74UfPfMDENeytcap3OmAMlvM0abp3x4qXANCSU3quPm2oLsD95Aull5NXoCS6pQou7R4jzqOVrLVGqy3ghf26E0NmnmyFnkN82J9KZzoUHIP9ovZy0QVYnjTUuJkLpSeou8w8PyqOCQsSLgb1ID829fArugUsG19sL8nIAjMlsKNC5eiCNxJ0999Asq7hPnNoXAI0otmONABPlcsIXWQJh14RXefsWuc43iAfk7WXR3cH9UCQZbxOeXZYHQ5oECtcAxFakIzDJxlpnAtNWIrqHhw0Y2VG2W9ERLfQmiWuCZpxUVa5makbEDD6nSnFecH3hvSiSqg8bP6yRAs1UDiHwv3zcIBmmlHWnuCHr9UlCzFub4nzKMB1a4Z5P4BRNrx'),
(4, 'KHQ2CJl37nmomaEnQ7CnLXgQ8VyuYtFQSBt2WCaqEVMCGpKmqkcfOqNi5AjGTlyfsjPj3852X5GNWis9CaDyajDmJqcZ4pT7koZikfEegyG83Go3JgkAGha1gNZMzureMS8xB6f2wRN1O5MI1Aa5bpoyeBg9nsJDVZ38QaVtp6IYBFmrpfZIGUGzxvY3GyIY2cubouVNJe84IV2klF36YEI8ofyXPezosCCILYYEoREDKsc6734AOUImL4w1vQOaOnMFzsmkHx6jb0GDXOBMmX05jVOYPAI2FGggcTLXs7Kwrv2m6R9tizWZNfBNKNqKtAEya1tU17chhMF5pcgzcGrF6hGKQKUgYBIMWebGBRPKcep6Eqb8r2PgrVm7LEomND4jRNhQ9e72rWOkiJCeKqJpevgZluMhHWBiE6e5xQVjJrTkZkOqQnequ2LUw7xWyk'),
(4, 'sX1Jfw2ngV4EU49QN0AQiQMZkBRR33OSrf2NYPjkLgII3sbeX3U5zwiQk1vqDEPb8GmG6cIAYPz5e57Swk6AiyxN5FzkKGGRtFuIt8x8cXt0nmI3tEV55UgoktCjsJ1zVHvRxMtEmqVy9MubNhTmPeYTc3YrBcyVzoi50jzDV7Xy0MAKPf5izS1BJoDKg9xwIR2V01IDuoHqRkh322lBwLxuKxcNyVXRi4p2u2xIn3Ixfee8SjOkW9oEM2tlh79DEVwaYZ1bf7K3yafU1NJ8umXtbSsimsKujbZJR2LKyu5F0YKsKRCIwPiL5OB3VaOo6EkhDRNeLPLfvn3fUyvsaLuA1iGzGWT5lM3A3ub1yKs242DvaUjeSyaunB0YKNAueqxPgKlHz2FYRU9CXyRNI5gszQHHVi7S7HqmalgAylnymGryCCPlAhYTMM9CtWm2KW'),
(4, '0fubERoY244VKxVg6R7Q6bqNTjn6yi5gcDeyGJECcT1h6brwsOPhJHSFkA9rmekGsgcVHsJVo4a3pifrogmhBrAoyYGTKU2wsuOaCx64xxf91q5c7gfGgP9Uupig9K4SRo8uCJ6z09VcE2VFQ57oWvYyqvEsPWYKi2VqD9fP4t0Q3qXl9MEA6koVTkbAjWLAhwxk5jIqB8UejYlgs8yk4wFGa1U1ph3pzK7k5k0CAtOilJoAhMSj4HrvOocM1J6J5kgUV6Gw3VQE0hIDTeeneanEJVnUces4sptOJAunD7AjnqDP2VGDQ7a46FY6mOx9NQI6Jv5R14X0X1q2KcYShZ4nGjgtJR1085rsM1htrRvoUmiqtB3HX4lc1IRXDZ0fUiqHo7UkSuotbbFGIkORprH9rjYF8M03DuEjXBABqDVCZrQsJ0DO7TYSpC8iCNwWiX'),
(4, 'JXjToQDhzgHEZB7sxvwKG00FjirCtsrXThxJowbEa00TEQIjmHfaFCgialBTCq86ViSUn6h6le3572Yx1L4wCLOrNo8KS5awMefFjJ5BcNGzXQPaJbowBR49coagfwjBaeBrRkDsNxyPKXs0rxQ4eHhESHeu5CcmZwwOlnlnDSMbLSIKnDqvkc68A6UxbhJvUbN7wbNRocMItGeTCJBZCGI3b8ZY1kFck4rWtBXGMe0aUcjvMik67gXX0594GJ9iobRaXIO6oC63Jea6KWU1Eht8qQuYspwcupwoucP7lsN3XAkUE9qqyllVN9LuST7MAo9qSPQLKtoMjAcyYhAcIPyXmnfNqm3eJBOVDEVHgkaeAtg2tMcYAFg5wZitpQ3aVvYcEEtcxl5ipD7X0F5SbYshUE7GvyIANuBJU4t6gbmoScCs2xGCQUB7Teh8oqnTMk'),
(4, '5iI2LrYl3DKphozlMuQTpeaC8RUOTPcwjZaZ6hYgVtHPLHTfMInKjAJ3h66jX5KK8GyipUHuzZyo57HalRfjnZ81zxAL71TUWVHyxLSakACoWGRx4xwVybfAI91xUHSWKzClfQR8w3KDN0Uei98Fm7XlRIFWxs2ZZGY69WrxQ0v6qC8QwFBfwVkM5XVABgSCpV77aGYpnvrRC9nkWBy73g6QoXTirwzZfORclu4z7fVVajuK0lvknElpREekSSBYjr8Jg3s4s2r2CwpUWR2kOsW9aAsMQvgT8xwIjU5jkNOWL6P9IhcKRJuCDWHVS4Ijb4WV3qGfBfrwMwCgPZoBlRHYGUpjFXB7gmOMELnB5GI4xquhcPBJvJWSwaaKuQ68W4jnuPYuZj0HTcY2feLBnGYkRbj0JgiDAIQla7Z39NOFum1o0W8IoEBl4AWFz7iWqe'),
(4, 'Hi9DH7QeVwXHRoynjrgj0oN2CTHv5aS67KekwhsJUxUSsuf7EoynWo9hFsGZpDOLQC8HNIg2Z6vh9LN7GYpAbXv78VtaXit3sXCCPaEp2h3vY8vRuO3jY56R1iPZlHx9itkBiSA1l5J0hpFhvT94HNmQzg0og0Keu1BgCJYtCAQTJ3oC78QXfHCL8uVRnCpo0AsVZVQ4fCalAhpv9wnlPhrDa3yzY8bjhwmgZKM3XK4D5rqRGRYTkqBojbJkl1WbwKapsqtSaZxOWHet8TPVrIBKUzocDezTQ8yHFhShLLYIUAbs7hr5HuOxuniEgYxQhzTiiF59UzgDv0TLsq8wXmxfLir1wwpEpvPSLiMSQF0yxg3Gx8mo9MPe8vYII45iQkYruB6XIRI1Y4wEZEbNpBRMpkWkyMKrWRlmroGHXQznyD4I9xKosC0gDRFDc9LQXT'),
(4, 'b21ArDaWkua1rVsRJQQjAFUNJYCLaJb4YJP9WmbRL7AsC94xc2yAxiZJV49DNFUMvl18bpfFRotYhc4yepiHWzGtE62FqiuZQnxW0PXB0YW7Bx1VWv03ANKUAiEVxyEqX2MurzN15pweatNvAiq6hF0CHPZ6UuOHe6nocuPTBswIjH2xjf0TttDrQgOoreNs69t9OFcPFrJP3JIuL2mQzsHjsoARtr60M0GLKWWiM2FfgCg3sFCLkvNBRjvFrclOEoqstttVQtnvJToSiXVKULSG3pc26M40wvWO3PrRAi2Nw1cKD5TJCHuarBCSXbHiHX4ElGZhyUSYSBjzfoiaC1QQvArKcqi82NNYYtQnEOHeWjFWqBxKJncDsSyj6lmDEMMrYRBNTaWjpDZllD9nFIlKg3pnxKqER4ONalowugHTTw87Lcz3BTiuocjtk5GVLM'),
(4, 'oojmAHvgsu31Agxf0CD1kRo96tSWPZJllbblbmSVOYLXqbDrSIjUatoSk9YXS1j9rtf3APawQT9xfpqySZjJyQ3xyit50LmnXiiGIfONArBZN2VhW3xgG4mDsQtSnF5jMjPinrLCWQFjOgQAp7ftxxmAiChDPxD4v1pVAuLSe42arnGqfpbaP2nUXyqoQ8ruArB2bIw1T7bGfMgwMG1hH1sazeEmYl5r4H3ooAmk85pl89Quwn10PocaNO913Kt2KUzS0Jifz49xHsTvI1E3jXgTqy0ApDJbwTsZqlY2QeU9CeLOISnMHth5uMlyaA3jQ5C9GWe5MufN8Th8xiLcpkBVkIQM2ppG1K0pNEkNxoVIXGgFCur4CqMcMm3ZCnXG2vvPpyj8ReiKPp0BcMf4kRzmGqTm5avU0NIO7gqQXKmzwnh6u4qafvDK7ZL6w4OsfW'),
(3, '4yLHBeoMLpitWAji1EfQzMVuPVeO4MyZf034ClKuUaKGtpTPUort00D32OATkrBrfSHMDCEmnlPNeziA0EjOiI5aNVBhQil1jK4UQ1nN1m80KGb8RZxkkXC9FBpJrQrSqBjnDCQcT3PgqNe4E6gzwU03YEojKXBeRepwPJ8B35h6UEGZH3vWPLGIaNCZIZhFrlcqtBYMrFFazuWHXzxP9yA7ME1aGQlKY2fDFxugNmY0WZNXuJZxyt8kPC82JSLxfSxfTFZ0klpru1FHjfyglfMkM3Z3wpXweDIVe2Tna02zf46zXeCcsBPJ2bVk5T9SeA68hAbue2sF5TVXarRNRrPngqZ19KJzqOXYUrtxUmnxg4NGDDrXV4LrvTGCftuut96AjNAV0YOJqifJAqAtui2nDxXx247vv4t7QvQMZ4hbznu4SWRQZ0LwRmOMkigFkl'),
(4, 'Grx4qYnBxPyESa4T3B4TPAx0bzrM3HIE5PxKFpF64ugshwemHM3KbFKMeXBi5A41HL74UfPfMDENeytcap3OmAMlvM0abp3x4qXANCSU3quPm2oLsD95Aull5NXoCS6pQou7R4jzqOVrLVGqy3ghf26E0NmnmyFnkN82J9KZzoUHIP9ovZy0QVYnjTUuJkLpSeou8w8PyqOCQsSLgb1ID829fArugUsG19sL8nIAjMlsKNC5eiCNxJ0999Asq7hPnNoXAI0otmONABPlcsIXWQJh14RXefsWuc43iAfk7WXR3cH9UCQZbxOeXZYHQ5oECtcAxFakIzDJxlpnAtNWIrqHhw0Y2VG2W9ERLfQmiWuCZpxUVa5makbEDD6nSnFecH3hvSiSqg8bP6yRAs1UDiHwv3zcIBmmlHWnuCHr9UlCzFub4nzKMB1a4Z5P4BRNrx'),
(4, 'KHQ2CJl37nmomaEnQ7CnLXgQ8VyuYtFQSBt2WCaqEVMCGpKmqkcfOqNi5AjGTlyfsjPj3852X5GNWis9CaDyajDmJqcZ4pT7koZikfEegyG83Go3JgkAGha1gNZMzureMS8xB6f2wRN1O5MI1Aa5bpoyeBg9nsJDVZ38QaVtp6IYBFmrpfZIGUGzxvY3GyIY2cubouVNJe84IV2klF36YEI8ofyXPezosCCILYYEoREDKsc6734AOUImL4w1vQOaOnMFzsmkHx6jb0GDXOBMmX05jVOYPAI2FGggcTLXs7Kwrv2m6R9tizWZNfBNKNqKtAEya1tU17chhMF5pcgzcGrF6hGKQKUgYBIMWebGBRPKcep6Eqb8r2PgrVm7LEomND4jRNhQ9e72rWOkiJCeKqJpevgZluMhHWBiE6e5xQVjJrTkZkOqQnequ2LUw7xWyk'),
(4, 'sX1Jfw2ngV4EU49QN0AQiQMZkBRR33OSrf2NYPjkLgII3sbeX3U5zwiQk1vqDEPb8GmG6cIAYPz5e57Swk6AiyxN5FzkKGGRtFuIt8x8cXt0nmI3tEV55UgoktCjsJ1zVHvRxMtEmqVy9MubNhTmPeYTc3YrBcyVzoi50jzDV7Xy0MAKPf5izS1BJoDKg9xwIR2V01IDuoHqRkh322lBwLxuKxcNyVXRi4p2u2xIn3Ixfee8SjOkW9oEM2tlh79DEVwaYZ1bf7K3yafU1NJ8umXtbSsimsKujbZJR2LKyu5F0YKsKRCIwPiL5OB3VaOo6EkhDRNeLPLfvn3fUyvsaLuA1iGzGWT5lM3A3ub1yKs242DvaUjeSyaunB0YKNAueqxPgKlHz2FYRU9CXyRNI5gszQHHVi7S7HqmalgAylnymGryCCPlAhYTMM9CtWm2KW'),
(5, '0fubERoY244VKxVg6R7Q6bqNTjn6yi5gcDeyGJECcT1h6brwsOPhJHSFkA9rmekGsgcVHsJVo4a3pifrogmhBrAoyYGTKU2wsuOaCx64xxf91q5c7gfGgP9Uupig9K4SRo8uCJ6z09VcE2VFQ57oWvYyqvEsPWYKi2VqD9fP4t0Q3qXl9MEA6koVTkbAjWLAhwxk5jIqB8UejYlgs8yk4wFGa1U1ph3pzK7k5k0CAtOilJoAhMSj4HrvOocM1J6J5kgUV6Gw3VQE0hIDTeeneanEJVnUces4sptOJAunD7AjnqDP2VGDQ7a46FY6mOx9NQI6Jv5R14X0X1q2KcYShZ4nGjgtJR1085rsM1htrRvoUmiqtB3HX4lc1IRXDZ0fUiqHo7UkSuotbbFGIkORprH9rjYF8M03DuEjXBABqDVCZrQsJ0DO7TYSpC8iCNwWiX'),
(5, 'JXjToQDhzgHEZB7sxvwKG00FjirCtsrXThxJowbEa00TEQIjmHfaFCgialBTCq86ViSUn6h6le3572Yx1L4wCLOrNo8KS5awMefFjJ5BcNGzXQPaJbowBR49coagfwjBaeBrRkDsNxyPKXs0rxQ4eHhESHeu5CcmZwwOlnlnDSMbLSIKnDqvkc68A6UxbhJvUbN7wbNRocMItGeTCJBZCGI3b8ZY1kFck4rWtBXGMe0aUcjvMik67gXX0594GJ9iobRaXIO6oC63Jea6KWU1Eht8qQuYspwcupwoucP7lsN3XAkUE9qqyllVN9LuST7MAo9qSPQLKtoMjAcyYhAcIPyXmnfNqm3eJBOVDEVHgkaeAtg2tMcYAFg5wZitpQ3aVvYcEEtcxl5ipD7X0F5SbYshUE7GvyIANuBJU4t6gbmoScCs2xGCQUB7Teh8oqnTMk'),
(6, '5iI2LrYl3DKphozlMuQTpeaC8RUOTPcwjZaZ6hYgVtHPLHTfMInKjAJ3h66jX5KK8GyipUHuzZyo57HalRfjnZ81zxAL71TUWVHyxLSakACoWGRx4xwVybfAI91xUHSWKzClfQR8w3KDN0Uei98Fm7XlRIFWxs2ZZGY69WrxQ0v6qC8QwFBfwVkM5XVABgSCpV77aGYpnvrRC9nkWBy73g6QoXTirwzZfORclu4z7fVVajuK0lvknElpREekSSBYjr8Jg3s4s2r2CwpUWR2kOsW9aAsMQvgT8xwIjU5jkNOWL6P9IhcKRJuCDWHVS4Ijb4WV3qGfBfrwMwCgPZoBlRHYGUpjFXB7gmOMELnB5GI4xquhcPBJvJWSwaaKuQ68W4jnuPYuZj0HTcY2feLBnGYkRbj0JgiDAIQla7Z39NOFum1o0W8IoEBl4AWFz7iWqe')


--Insert Fake Files
insert into Files (ChatID, FileData) 
	values (7, CONVERT(varbinary(MAX),'dui maecenas tristique est et tempus semper est quam pharetra magna ac consequat metus sapien ut nunc')),
		   (7, CONVERT(varbinary(MAX),'sit amet turpis elementum ligula vehicula consequat morbi a ipsum integer a nibh in quis justo maecenas rhoncus aliquam')),
		   (6, CONVERT(varbinary(MAX),'non mauris morbi non lectus aliquam sit amet diam in magna bibendum imperdiet')),
		   (3, CONVERT(varbinary(MAX),'interdum in ante vestibulum ante ipsum primis in faucibus orci luctus et')),
		   (1, CONVERT(varbinary(MAX),'sodales scelerisque mauris sit amet eros suspendisse accumsan tortor quis turpis sed ante vivamus tortor duis mattis egestas')),
		   (8, CONVERT(varbinary(MAX),'ut dolor morbi vel lectus in quam fringilla rhoncus mauris enim leo rhoncus')),
		   (3, CONVERT(varbinary(MAX),'duis aliquam convallis nunc proin at turpis a pede posuere nonummy integer non velit')),
		   (3, CONVERT(varbinary(MAX),'blandit ultrices enim lorem ipsum dolor sit amet consectetuer adipiscing elit proin interdum')),
		   (3, CONVERT(varbinary(MAX),'cubilia curae donec pharetra magna vestibulum aliquet ultrices erat tortor sollicitudin mi sit amet lobortis sapien sapien non mi integer')),
		   (3, CONVERT(varbinary(MAX),'nisi at nibh in hac habitasse platea dictumst aliquam augue')),
		   (6, CONVERT(varbinary(MAX),'lectus in quam fringilla rhoncus mauris enim leo rhoncus sed vestibulum sit amet cursus id turpis integer aliquet massa id')),
		   (6, CONVERT(varbinary(MAX),'ligula nec sem duis aliquam convallis nunc proin at turpis')),
		   (8, CONVERT(varbinary(MAX),'tincidunt lacus at velit vivamus vel nulla eget eros elementum pellentesque')),
		   (1, CONVERT(varbinary(MAX),'orci vehicula condimentum curabitur in libero ut massa volutpat convallis morbi odio odio')),
		   (8, CONVERT(varbinary(MAX),'eros vestibulum ac est lacinia nisi venenatis tristique fusce congue diam id ornare imperdiet sapien urna pretium nisl ut')),
		   (3, CONVERT(varbinary(MAX),'volutpat eleifend donec ut dolor morbi vel lectus in quam fringilla rhoncus mauris enim leo')),
		   (1, CONVERT(varbinary(MAX),'nibh fusce lacus purus aliquet at feugiat non pretium quis lectus suspendisse potenti in eleifend quam')),
		   (4, CONVERT(varbinary(MAX),'libero rutrum ac lobortis vel dapibus at diam nam tristique tortor eu')),
		   (8, CONVERT(varbinary(MAX),'felis sed interdum venenatis turpis enim blandit mi in porttitor pede justo eu massa donec')),
		   (8, CONVERT(varbinary(MAX),'molestie hendrerit at vulputate vitae nisl aenean lectus pellentesque eget nunc donec quis'));


INSERT INTO Classes
VALUES	('Writing 121', 'WRI', 121, 1), 
		('Writing 122', 'WRI', 122, 1),
		('Writing 115', 'WRI', 115, 1),
		('C++ Programming 1', 'CST', 116, 1),
		('C++ Programming 2', 'CST', 136, 1),
		('OOP With C++', 'CST', 136, 1),
		('Data Structures', 'CST', 211, 2),
		('Unix', 'CST', 240, 2),
		('Computer Assembly Language', 'CST', 250, 1)



--Insert mentoring programs
INSERT INTO OfficialMentor
VALUES ('The ROCK'),('Peer Consulting'), ('LCC Mentor')


--Om to userprofile
INSERT INTO OmToUser (UserID, MentorID)
	VALUES (1,1),
		   (1,2),
		   (1,3),
		   (2,2),
		   (3,1),
		   (4,3),
		   (4,2),
		   (4,1),
		   (6,1),
		   (6,2);
GO

SELECT * From UserProfile
SELECT * FROM Classes
SELECT * FROM UserToClass

--Cordinate users with classes
INSERT INTO UserToClass (UserID, ClassID) VALUES (1, 6);
INSERT INTO UserToClass (UserID, ClassID) VALUES (4, 3);
INSERT INTO UserToClass (UserID, ClassID) VALUES (5, 5);
INSERT INTO UserToClass (UserID, ClassID) VALUES (3, 8);
INSERT INTO UserToClass (UserID, ClassID) VALUES (4, 7);
INSERT INTO UserToClass (UserID, ClassID) VALUES (2, 1);
INSERT INTO UserToClass (UserID, ClassID) VALUES (2, 9);
INSERT INTO UserToClass (UserID, ClassID) VALUES (3, 7);
INSERT INTO UserToClass (UserID, ClassID) VALUES (5, 1);
INSERT INTO UserToClass (UserID, ClassID) VALUES (3, 8);
INSERT INTO UserToClass (UserID, ClassID) VALUES (4, 1);
INSERT INTO UserToClass (UserID, ClassID) VALUES (5, 1);
INSERT INTO UserToClass (UserID, ClassID) VALUES (2, 7);
INSERT INTO UserToClass (UserID, ClassID) VALUES (1, 1);
INSERT INTO UserToClass (UserID, ClassID) VALUES (1, 2);
INSERT INTO UserToClass (UserID, ClassID) VALUES (5, 6);
INSERT INTO UserToClass (UserID, ClassID) VALUES (2, 3);
INSERT INTO UserToClass (UserID, ClassID) VALUES (4, 5);
INSERT INTO UserToClass (UserID, ClassID) VALUES (6, 7);
INSERT INTO UserToClass (UserID, ClassID) VALUES (2, 9);
INSERT INTO UserToClass (UserID, ClassID) VALUES (3, 2);
INSERT INTO UserToClass (UserID, ClassID) VALUES (3, 8);
INSERT INTO UserToClass (UserID, ClassID) VALUES (5, 4);
INSERT INTO UserToClass (UserID, ClassID) VALUES (3, 7);
INSERT INTO UserToClass (UserID, ClassID) VALUES (3, 7);
INSERT INTO UserToClass (UserID, ClassID) VALUES (6, 4);
INSERT INTO UserToClass (UserID, ClassID) VALUES (5, 2);
INSERT INTO UserToClass (UserID, ClassID) VALUES (5, 7);
INSERT INTO UserToClass (UserID, ClassID) VALUES (6, 7);
INSERT INTO UserToClass (UserID, ClassID) VALUES (2, 8);
INSERT INTO UserToClass (UserID, ClassID) VALUES (2, 7);
INSERT INTO UserToClass (UserID, ClassID) VALUES (2, 8);
INSERT INTO UserToClass (UserID, ClassID) VALUES (4, 1);
INSERT INTO UserToClass (UserID, ClassID) VALUES (4, 3);
INSERT INTO UserToClass (UserID, ClassID) VALUES (1, 6);
INSERT INTO UserToClass (UserID, ClassID) VALUES (2, 8);
INSERT INTO UserToClass (UserID, ClassID) VALUES (3, 4);
INSERT INTO UserToClass (UserID, ClassID) VALUES (1, 6);
INSERT INTO UserToClass (UserID, ClassID) VALUES (1, 1);
INSERT INTO UserToClass (UserID, ClassID) VALUES (4, 6);
INSERT INTO UserToClass (UserID, ClassID) VALUES (1, 2);
INSERT INTO UserToClass (UserID, ClassID) VALUES (6, 5);
INSERT INTO UserToClass (UserID, ClassID) VALUES (2, 3);
INSERT INTO UserToClass (UserID, ClassID) VALUES (6, 8);
INSERT INTO UserToClass (UserID, ClassID) VALUES (4, 1);
INSERT INTO UserToClass (UserID, ClassID) VALUES (5, 2);
INSERT INTO UserToClass (UserID, ClassID) VALUES (2, 5);
INSERT INTO UserToClass (UserID, ClassID) VALUES (3, 5);
INSERT INTO UserToClass (UserID, ClassID) VALUES (5, 7);
INSERT INTO UserToClass (UserID, ClassID) VALUES (6, 9);

--Insert Report
INSERT INTO Report (ReportDescription, FlaggerID)
VALUES('I dont think this vulger langugae belongs on UnstuckME', 1)

--Insert some reviews
INSERT INTO Review
VALUES (DEFAULT, 4, 'The tutor was amazing at explaining Data Structs!')

INSERT INTO Review
VALUES (DEFAULT, 5, 'The tutor was the coolest most helpful person!')

INSERT INTO Review
VALUES (DEFAULT, 3, 'The tutor was very nice, but did not know the subject very well.')

INSERT INTO Review
VALUES (DEFAULT, 1, 'Worst Tutor Ever!')

INSERT INTO Review
VALUES (DEFAULT, 1, 'Not a very goood Tutor!')

INSERT INTO Review
VALUES (DEFAULT, 5, 'I Loved working with this student. Such a quick learner!')

INSERT INTO Review
Values(1, 1, 'This tutor can go fuck himself, Literally JAKE is the worst human beiugn ever!!!!')

--Insert some stickers  --Description, ClassID, StudentID, TutorID, StudentReviewID, TutorReviewID, Min Star Rating, Submit Time, Timout
INSERT INTO Sticker
VALUES ('I need help with creating my Array class for Data Structures', 7, 1, 3, 1, 5, 3.5, GETDATE(), DATEADD(hour, 3.5, GETDATE()))

INSERT INTO Sticker
VALUES ('I need help with EVERYTHING!', 2, 4, DEFAULT, 4, NULL, 3.5, GETDATE(), DATEADD(hour, 3.5, GETDATE()))

INSERT INTO Sticker
VALUES ('QUICK HELP RN DUE PPRETTY SOON!!!!', 1, 3, 5, NULL, NULL, NULL, GETDATE(), DATEADD(hour, 0.5, GETDATE()))

INSERT INTO Sticker
VALUES ('Any time this week would be great, just having a little trouble.', 5, 5, DEFAULT, 7, NULL, NULL, GETDATE(), DATEADD(hour, 168, GETDATE()))



--INsert into UserTOChat (18)
insert into UserToChat (UserID, ChatID) values (4, 8);
insert into UserToChat (UserID, ChatID) values (2, 6);
insert into UserToChat (UserID, ChatID) values (3, 4);
insert into UserToChat (UserID, ChatID) values (2, 1);
insert into UserToChat (UserID, ChatID) values (1, 4);
insert into UserToChat (UserID, ChatID) values (5, 3);
insert into UserToChat (UserID, ChatID) values (4, 1);
insert into UserToChat (UserID, ChatID) values (4, 7);
insert into UserToChat (UserID, ChatID) values (1, 8);
insert into UserToChat (UserID, ChatID) values (2, 7);
insert into UserToChat (UserID, ChatID) values (3, 2);
insert into UserToChat (UserID, ChatID) values (6, 5);
insert into UserToChat (UserID, ChatID) values (6, 1);
insert into UserToChat (UserID, ChatID) values (1, 7);
insert into UserToChat (UserID, ChatID) values (1, 5);
insert into UserToChat (UserID, ChatID) values (4, 2);
insert into UserToChat (UserID, ChatID) values (3, 8);
insert into UserToChat (UserID, ChatID) values (2, 8);

