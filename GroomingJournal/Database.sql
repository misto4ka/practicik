/* ============================================================
   База данных "Груминг-салон" (Microsoft SQL Server)
   Запускать целиком: Ctrl+A, затем F5 (SSMS) или Execute (Visual Studio).
   Скрипт можно запускать повторно — существующие таблицы не пересоздаются.
   ============================================================ */

-- 1. Создаём базу данных, если её ещё нет
IF DB_ID('GroomingSalon') IS NULL
    CREATE DATABASE GroomingSalon;
GO

-- Переключаемся на неё
USE GroomingSalon;
GO

/* ---------- Таблица "Виды животных" (справочник) ----------
   Собака, Кошка, Кролик... Название хранится один раз,
   в таблице животных на него ссылаются по номеру. */
IF OBJECT_ID('dbo.AnimalTypes', 'U') IS NULL
CREATE TABLE dbo.AnimalTypes
(
    Id   INT IDENTITY(1,1) PRIMARY KEY,      -- IDENTITY: номер растёт сам (1, 2, 3...)
    Name NVARCHAR(50) NOT NULL UNIQUE        -- UNIQUE: два одинаковых вида быть не может
);
GO

/* ---------- Таблица "Клиенты" (хозяева животных) ---------- */
IF OBJECT_ID('dbo.Clients', 'U') IS NULL
CREATE TABLE dbo.Clients
(
    Id       INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,         -- ФИО хозяина
    Phone    NVARCHAR(20)  NOT NULL UNIQUE   -- телефон: по нему узнаём постоянного клиента
);
GO

/* ---------- Таблица "Животные" ----------
   У одного клиента может быть несколько животных (связь "один ко многим").
   ClientId и AnimalTypeId — внешние ключи (FOREIGN KEY):
   нельзя указать клиента или вид, которых нет в своих таблицах. */
IF OBJECT_ID('dbo.Pets', 'U') IS NULL
CREATE TABLE dbo.Pets
(
    Id           INT IDENTITY(1,1) PRIMARY KEY,
    ClientId     INT NOT NULL,
    AnimalTypeId INT NOT NULL,
    Name         NVARCHAR(50) NOT NULL,      -- кличка
    Breed        NVARCHAR(80) NOT NULL,      -- порода
    CONSTRAINT FK_Pets_Clients     FOREIGN KEY (ClientId)     REFERENCES dbo.Clients(Id),
    CONSTRAINT FK_Pets_AnimalTypes FOREIGN KEY (AnimalTypeId) REFERENCES dbo.AnimalTypes(Id)
);
GO

/* ---------- Таблица "Услуги" (прайс-лист) ---------- */
IF OBJECT_ID('dbo.Services', 'U') IS NULL
CREATE TABLE dbo.Services
(
    Id    INT IDENTITY(1,1) PRIMARY KEY,
    Name  NVARCHAR(100) NOT NULL UNIQUE,
    Price DECIMAL(10,2) NOT NULL CHECK (Price >= 0)   -- CHECK: цена не может быть отрицательной
);
GO

/* ---------- Таблица "Записи" (сам журнал) ----------
   Запись связывает животное и услугу на конкретные дату и время. */
IF OBJECT_ID('dbo.Appointments', 'U') IS NULL
CREATE TABLE dbo.Appointments
(
    Id              INT IDENTITY(1,1) PRIMARY KEY,
    PetId           INT NOT NULL,
    ServiceId       INT NOT NULL,
    AppointmentDate DATE    NOT NULL,        -- только дата
    AppointmentTime TIME(0) NOT NULL,        -- только время (без долей секунды)
    CONSTRAINT FK_Appointments_Pets     FOREIGN KEY (PetId)     REFERENCES dbo.Pets(Id),
    CONSTRAINT FK_Appointments_Services FOREIGN KEY (ServiceId) REFERENCES dbo.Services(Id),
    -- В салоне один мастер: на одну дату и время может быть только одна запись
    CONSTRAINT UQ_Appointments_DateTime UNIQUE (AppointmentDate, AppointmentTime)
);
GO

/* ============ Начальные данные ============ */

-- Виды животных
IF NOT EXISTS (SELECT 1 FROM dbo.AnimalTypes)
INSERT INTO dbo.AnimalTypes (Name) VALUES
    (N'Собака'), (N'Кошка'), (N'Кролик'), (N'Хомяк'), (N'Морская свинка'), (N'Хорёк');
GO

-- Услуги и цены
IF NOT EXISTS (SELECT 1 FROM dbo.Services)
INSERT INTO dbo.Services (Name, Price) VALUES
    (N'Мытьё и сушка',               800),
    (N'Стрижка',                    1500),
    (N'Комплекс (мытьё + стрижка)', 2200),
    (N'Тримминг',                   2000),
    (N'Стрижка когтей',              300),
    (N'Чистка ушей',                 250);
GO

-- Демонстрационные клиенты, животные и записи (чтобы журнал не был пустым).
-- Их можно удалить: DELETE FROM dbo.Appointments; DELETE FROM dbo.Pets; DELETE FROM dbo.Clients;
IF NOT EXISTS (SELECT 1 FROM dbo.Clients)
BEGIN
    INSERT INTO dbo.Clients (FullName, Phone) VALUES
        (N'Иванова Мария Петровна', N'+7 900 111-22-33'),
        (N'Петров Сергей Алексеевич', N'+7 900 444-55-66');

    INSERT INTO dbo.Pets (ClientId, AnimalTypeId, Name, Breed) VALUES
        (1, (SELECT Id FROM dbo.AnimalTypes WHERE Name = N'Собака'), N'Бим',   N'Пудель'),
        (1, (SELECT Id FROM dbo.AnimalTypes WHERE Name = N'Кошка'),  N'Муся',  N'Персидская'),
        (2, (SELECT Id FROM dbo.AnimalTypes WHERE Name = N'Собака'), N'Рекс',  N'Немецкая овчарка');

    -- GETDATE() — текущая дата, DATEADD прибавляет дни: записи на сегодня и ближайшие дни
    INSERT INTO dbo.Appointments (PetId, ServiceId, AppointmentDate, AppointmentTime) VALUES
        (1, (SELECT Id FROM dbo.Services WHERE Name = N'Стрижка'),        CAST(GETDATE() AS DATE),              '10:00'),
        (2, (SELECT Id FROM dbo.Services WHERE Name = N'Мытьё и сушка'),  CAST(GETDATE() AS DATE),              '12:30'),
        (3, (SELECT Id FROM dbo.Services WHERE Name = N'Тримминг'),       CAST(DATEADD(DAY, 1, GETDATE()) AS DATE), '15:00');
END
GO

/* ============ Проверочный запрос: как выглядит журнал ============ */
SELECT a.AppointmentDate AS [Дата],
       DATENAME(WEEKDAY, a.AppointmentDate) AS [День недели],
       a.AppointmentTime AS [Время],
       p.Name   AS [Кличка],
       t.Name   AS [Животное],
       p.Breed  AS [Порода],
       c.FullName AS [Хозяин],
       s.Name   AS [Услуга],
       s.Price  AS [Цена]
FROM dbo.Appointments a
JOIN dbo.Pets        p ON p.Id = a.PetId
JOIN dbo.Clients     c ON c.Id = p.ClientId
JOIN dbo.AnimalTypes t ON t.Id = p.AnimalTypeId
JOIN dbo.Services    s ON s.Id = a.ServiceId
ORDER BY a.AppointmentDate, a.AppointmentTime;
GO
