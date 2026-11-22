# SQL notes

> Учебная БД: PracticeDB. Скрипт создания — `init.sql`.
> Запросы написаны на T-SQL (Microsoft SQL Server 2019+). Задания 28–30 — C#, см. `csharp/`.
>
> ⚠️ Таблицы результатов получены расчётом на копии тех же данных (SQL Server при подготовке не запускался). Перед сдачей прогоните запросы в SSMS, сверьте с таблицами и вставьте скриншоты. Задание 20 содержит поля, которые заполняются только после запуска. Эту заметку потом удалите.
> Задания 16–17 меняют данные: после них выполните `init.sql` заново.

## Task 01 — Создание базы и таблиц

### Запрос

```sql
CREATE DATABASE PracticeDB;
GO
USE PracticeDB;
GO
CREATE TABLE Departments (
    DepartmentId   INT PRIMARY KEY IDENTITY(1,1),
    DepartmentName NVARCHAR(100) NOT NULL
);
CREATE TABLE Employees (
    EmployeeId   INT PRIMARY KEY IDENTITY(1,1),
    EmployeeName NVARCHAR(100) NOT NULL,
    Salary       DECIMAL(10,2) NOT NULL,
    HireDate     DATE NOT NULL,
    DepartmentId INT NOT NULL,
    ManagerId    INT NULL,
    CONSTRAINT FK_Employees_Departments FOREIGN KEY (DepartmentId)
        REFERENCES Departments(DepartmentId),
    CONSTRAINT FK_Employees_Manager FOREIGN KEY (ManagerId)
        REFERENCES Employees(EmployeeId)
);
CREATE TABLE Projects (
    ProjectId   INT PRIMARY KEY IDENTITY(1,1),
    ProjectName NVARCHAR(100) NOT NULL,
    StartDate   DATE NOT NULL
);
CREATE TABLE EmployeeProjects (
    EmployeeId INT NOT NULL,
    ProjectId  INT NOT NULL,
    Hours      INT NOT NULL DEFAULT 0,
    CONSTRAINT PK_EmployeeProjects PRIMARY KEY (EmployeeId, ProjectId),
    CONSTRAINT FK_EP_Employees FOREIGN KEY (EmployeeId) REFERENCES Employees(EmployeeId),
    CONSTRAINT FK_EP_Projects  FOREIGN KEY (ProjectId)  REFERENCES Projects(ProjectId)
);
GO
-- Проверка: таблицы
SELECT TABLE_NAME
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_TYPE = 'BASE TABLE'
ORDER BY TABLE_NAME;

-- Проверка: внешние ключи
SELECT fk.name AS ForeignKey,
       OBJECT_NAME(fk.parent_object_id)     AS ChildTable,
       OBJECT_NAME(fk.referenced_object_id) AS ParentTable
FROM sys.foreign_keys AS fk
ORDER BY ChildTable, ForeignKey;
```

### Результат

**Таблицы**

| TABLE_NAME |
| --- |
| Departments |
| EmployeeProjects |
| Employees |
| Projects |

*Строк: 4*

**Внешние ключи**

| ForeignKey | ChildTable | ParentTable |
| --- | --- | --- |
| FK_EP_Employees | EmployeeProjects | Employees |
| FK_EP_Projects | EmployeeProjects | Projects |
| FK_Employees_Departments | Employees | Departments |
| FK_Employees_Manager | Employees | Employees |

*Строк: 4*

### Вывод

Созданы все 4 таблицы и 4 внешних ключа; `Employees.ManagerId` ссылается на `Employees.EmployeeId` (самоссылка). `CREATE DATABASE` и `CREATE TABLE` лежат в `init.sql`, там же `IF DB_ID(...) ... DROP DATABASE` для повторного запуска. Нюанс: после `CREATE DATABASE` нужен `USE PracticeDB`, иначе таблицы создадутся в `master`.

---

## Task 02 — INSERT: заполнение данными

### Запрос

```sql
INSERT INTO Departments (DepartmentName) VALUES
    (N'Разработка'), (N'Аналитика'), (N'Тестирование'), (N'Менеджмент');

INSERT INTO Employees (EmployeeName, Salary, HireDate, DepartmentId, ManagerId) VALUES
    (N'Анна Иванова',    65000, '2020-03-15', 1, NULL),
    (N'Пётр Сидоров',    72000, '2019-07-01', 1, NULL),
    (N'Мария Кузнецова', 55000, '2021-11-20', 1, NULL),
    (N'Иван Петров',     48000, '2023-02-10', 1, NULL),
    (N'Ольга Смирнова',  61000, '2020-09-05', 2, NULL),
    (N'Дмитрий Орлов',   58000, '2022-04-18', 2, NULL),
    (N'Елена Волкова',   52000, '2023-06-01', 2, NULL),
    (N'Сергей Морозов',  45000, '2021-12-12', 3, NULL),
    (N'Наталья Соколова',43000, '2024-01-15', 3, NULL),
    (N'Алексей Лебедев', 90000, '2018-05-25', 4, NULL);

-- ManagerId можно задать только после вставки: руководитель должен уже существовать
UPDATE Employees SET ManagerId = 1  WHERE EmployeeId = 4;
UPDATE Employees SET ManagerId = 2  WHERE EmployeeId = 3;
UPDATE Employees SET ManagerId = 5  WHERE EmployeeId = 6;
UPDATE Employees SET ManagerId = 10 WHERE EmployeeId = 9;

INSERT INTO Projects (ProjectName, StartDate) VALUES
    (N'CRM System', '2023-01-10'), (N'Mobile App', '2023-05-01'),
    (N'Data Warehouse', '2022-11-20'), (N'AI Assistant', '2024-02-01'),
    (N'Legacy Refactor', '2021-06-15');

INSERT INTO EmployeeProjects (EmployeeId, ProjectId, Hours) VALUES
    (1, 1, 120), (1, 3, 80), (2, 1, 150), (2, 4, 60), (3, 2, 100), (4, 2, 90),
    (4, 5, 40),  (5, 3, 110), (6, 3, 70), (6, 4, 50), (7, 2, 95),  (8, 5, 130),
    (10, 4, 200),(10, 1, 30);
GO
-- Проверка
SELECT 'Departments' AS TableName, COUNT(*) AS RowsCount FROM Departments
UNION ALL SELECT 'Employees',        COUNT(*) FROM Employees
UNION ALL SELECT 'Projects',         COUNT(*) FROM Projects
UNION ALL SELECT 'EmployeeProjects', COUNT(*) FROM EmployeeProjects;
```

### Результат

| TableName | RowsCount |
| --- | --- |
| Departments | 4 |
| Employees | 10 |
| Projects | 5 |
| EmployeeProjects | 14 |

*Строк: 4*

### Вывод

Во всех таблицах ожидаемое число строк: 4 отдела, 10 сотрудников, 5 проектов, 14 назначений. Несколько строк вставляются одним `INSERT` с несколькими `VALUES`. Строки с кириллицей нужно писать с префиксом `N'...'`, иначе возможны искажения символов. Руководителей проставляем отдельными `UPDATE`, потому что при вставке они ещё не существуют.

---

## Task 03 — SELECT + WHERE: базовые выборки

### Запрос

```sql
-- 3.1
SELECT EmployeeId, EmployeeName, Salary FROM Employees WHERE Salary > 50000;
-- 3.2
SELECT EmployeeId, EmployeeName, HireDate FROM Employees WHERE YEAR(HireDate) = 2023;
-- 3.3
SELECT EmployeeId, EmployeeName FROM Employees WHERE EmployeeName LIKE N'А%';
-- 3.4
SELECT EmployeeId, EmployeeName, DepartmentId FROM Employees WHERE DepartmentId = 2;
-- 3.5
SELECT EmployeeId, EmployeeName, Salary FROM Employees WHERE Salary BETWEEN 40000 AND 60000;
```

### Результат

**3.1. Зарплата > 50000**

| EmployeeId | EmployeeName | Salary |
| --- | --- | --- |
| 1 | Анна Иванова | 65000.00 |
| 2 | Пётр Сидоров | 72000.00 |
| 3 | Мария Кузнецова | 55000.00 |
| 5 | Ольга Смирнова | 61000.00 |
| 6 | Дмитрий Орлов | 58000.00 |
| 7 | Елена Волкова | 52000.00 |
| 10 | Алексей Лебедев | 90000.00 |

*Строк: 7*

**3.2. Нанят в 2023 году**

| EmployeeId | EmployeeName | HireDate |
| --- | --- | --- |
| 4 | Иван Петров | 2023-02-10 |
| 7 | Елена Волкова | 2023-06-01 |

*Строк: 2*

**3.3. Имя начинается на «А»**

| EmployeeId | EmployeeName |
| --- | --- |
| 1 | Анна Иванова |
| 10 | Алексей Лебедев |

*Строк: 2*

**3.4. Отдел 2**

| EmployeeId | EmployeeName | DepartmentId |
| --- | --- | --- |
| 5 | Ольга Смирнова | 2 |
| 6 | Дмитрий Орлов | 2 |
| 7 | Елена Волкова | 2 |

*Строк: 3*

**3.5. Зарплата 40000–60000**

| EmployeeId | EmployeeName | Salary |
| --- | --- | --- |
| 3 | Мария Кузнецова | 55000.00 |
| 4 | Иван Петров | 48000.00 |
| 6 | Дмитрий Орлов | 58000.00 |
| 7 | Елена Волкова | 52000.00 |
| 8 | Сергей Морозов | 45000.00 |
| 9 | Наталья Соколова | 43000.00 |

*Строк: 6*

### Вывод

Все 5 запросов вернули непустой результат (7, 2, 2, 3 и 6 строк соответственно). `BETWEEN` включает обе границы. В `LIKE` для кириллицы нужен `N'А%'`; буква в запросе должна быть именно кириллической «А», а не латинской A. `YEAR(HireDate) = 2023` работает, но не позволяет использовать индекс по `HireDate`; на больших таблицах лучше диапазон `HireDate >= '2023-01-01' AND HireDate < '2024-01-01'`.

---

## Task 04 — ORDER BY + TOP: сортировка и лимит

### Запрос

```sql
-- 4.1 Топ-3 по зарплате
SELECT TOP 3 EmployeeName, Salary FROM Employees ORDER BY Salary DESC;
-- 4.2 Топ-5 самых давних сотрудников
SELECT TOP 5 EmployeeName, HireDate FROM Employees ORDER BY HireDate ASC;
-- 4.3 По отделу (возр.), затем по зарплате (убыв.)
SELECT EmployeeName, DepartmentId, Salary FROM Employees ORDER BY DepartmentId ASC, Salary DESC;
```

### Результат

**4.1**

| EmployeeName | Salary |
| --- | --- |
| Алексей Лебедев | 90000.00 |
| Пётр Сидоров | 72000.00 |
| Анна Иванова | 65000.00 |

*Строк: 3*

**4.2**

| EmployeeName | HireDate |
| --- | --- |
| Алексей Лебедев | 2018-05-25 |
| Пётр Сидоров | 2019-07-01 |
| Анна Иванова | 2020-03-15 |
| Ольга Смирнова | 2020-09-05 |
| Мария Кузнецова | 2021-11-20 |

*Строк: 5*

**4.3**

| EmployeeName | DepartmentId | Salary |
| --- | --- | --- |
| Пётр Сидоров | 1 | 72000.00 |
| Анна Иванова | 1 | 65000.00 |
| Мария Кузнецова | 1 | 55000.00 |
| Иван Петров | 1 | 48000.00 |
| Ольга Смирнова | 2 | 61000.00 |
| Дмитрий Орлов | 2 | 58000.00 |
| Елена Волкова | 2 | 52000.00 |
| Сергей Морозов | 3 | 45000.00 |
| Наталья Соколова | 3 | 43000.00 |
| Алексей Лебедев | 4 | 90000.00 |

*Строк: 10*

### Вывод

`TOP` без `ORDER BY` возвращает произвольные строки, поэтому сортировка обязательна. Порядок в 4.3 задаётся двумя ключами: сначала отдел, внутри отдела зарплата по убыванию.

---

## Task 05 — Агрегация: COUNT, SUM, AVG, MIN, MAX

### Запрос

```sql
SELECT
    COUNT(*)                                  AS TotalEmployees,
    SUM(Salary)                               AS TotalSalary,
    ROUND(AVG(Salary), 2)                     AS AvgSalary,
    MIN(Salary)                               AS MinSalary,
    MAX(Salary)                               AS MaxSalary,
    COUNT(CASE WHEN Salary > 50000 THEN 1 END) AS Above50k
FROM Employees;
```

### Результат

| TotalEmployees | TotalSalary | AvgSalary | MinSalary | MaxSalary | Above50k |
| --- | --- | --- | --- | --- | --- |
| 10 | 589000.00 | 58900.000000 | 43000.00 | 90000.00 | 7 |

*Строк: 1*

### Вывод

Ручная проверка: сумма зарплат 589000, делим на 10 сотрудников, получаем 58900; минимум 43000, максимум 90000; выше 50000 получают 7 человек. `AVG` по `DECIMAL(10,2)` в SQL Server возвращает `DECIMAL` с 6 знаками после запятой, поэтому `ROUND(..., 2)` не убирает хвост нулей из вывода. Количество с условием удобно считать через `COUNT(CASE WHEN ... THEN 1 END)`: `COUNT` игнорирует `NULL`.

---

## Task 06 — GROUP BY: статистика по отделам

### Запрос

```sql
SELECT DepartmentId,
       COUNT(*)              AS EmployeesCount,
       ROUND(AVG(Salary), 2) AS AvgSalary,
       SUM(Salary)           AS TotalSalary
FROM Employees
GROUP BY DepartmentId
ORDER BY AVG(Salary) DESC;
```

### Результат

| DepartmentId | EmployeesCount | AvgSalary | TotalSalary |
| --- | --- | --- | --- |
| 4 | 1 | 90000.000000 | 90000.00 |
| 1 | 4 | 60000.000000 | 240000.00 |
| 2 | 3 | 57000.000000 | 171000.00 |
| 3 | 2 | 44000.000000 | 88000.00 |

*Строк: 4*

### Вывод

Для каждого отдела одна строка, порядок по средней зарплате по убыванию: 4, 1, 2, 3. Все столбцы в `SELECT`, которые не агрегаты, должны быть в `GROUP BY`. Сортировка по `AVG(Salary)`, а не по округлённому значению, чтобы не терять точность.

---

## Task 07 — GROUP BY + HAVING: фильтрация групп

### Запрос

```sql
-- 7.1 Отделы, где больше 2 сотрудников
SELECT DepartmentId, COUNT(*) AS EmployeesCount
FROM Employees GROUP BY DepartmentId HAVING COUNT(*) > 2;

-- 7.2 Средняя зарплата > 55000
SELECT DepartmentId, ROUND(AVG(Salary), 2) AS AvgSalary
FROM Employees GROUP BY DepartmentId HAVING AVG(Salary) > 55000;

-- 7.3 Суммарная зарплата > 150000
SELECT DepartmentId, SUM(Salary) AS TotalSalary
FROM Employees GROUP BY DepartmentId HAVING SUM(Salary) > 150000;
```

### Результат

**7.1**

| DepartmentId | EmployeesCount |
| --- | --- |
| 1 | 4 |
| 2 | 3 |

*Строк: 2*

**7.2**

| DepartmentId | AvgSalary |
| --- | --- |
| 1 | 60000.000000 |
| 2 | 57000.000000 |
| 4 | 90000.000000 |

*Строк: 3*

**7.3**

| DepartmentId | TotalSalary |
| --- | --- |
| 1 | 240000.00 |
| 2 | 171000.00 |

*Строк: 2*

### Вывод

`HAVING` фильтрует группы после агрегации, а `WHERE` фильтрует строки до неё, поэтому условие на `COUNT/AVG/SUM` можно писать только в `HAVING`. Результат: 7.1 даёт отделы 1 и 2, 7.2 даёт отделы 1, 2, 4, 7.3 даёт отделы 1 и 2.

---

## Task 08 — INNER JOIN: сотрудники и отделы

### Запрос

```sql
SELECT e.EmployeeName, d.DepartmentName, e.Salary
FROM Employees AS e
INNER JOIN Departments AS d ON d.DepartmentId = e.DepartmentId
ORDER BY d.DepartmentName, e.Salary DESC;
```

### Результат

| EmployeeName | DepartmentName | Salary |
| --- | --- | --- |
| Ольга Смирнова | Аналитика | 61000.00 |
| Дмитрий Орлов | Аналитика | 58000.00 |
| Елена Волкова | Аналитика | 52000.00 |
| Алексей Лебедев | Менеджмент | 90000.00 |
| Пётр Сидоров | Разработка | 72000.00 |
| Анна Иванова | Разработка | 65000.00 |
| Мария Кузнецова | Разработка | 55000.00 |
| Иван Петров | Разработка | 48000.00 |
| Сергей Морозов | Тестирование | 45000.00 |
| Наталья Соколова | Тестирование | 43000.00 |

*Строк: 10*

### Вывод

Вернулись все 10 сотрудников, у каждого есть название отдела. Строк без отдела нет: `DepartmentId` в `Employees` объявлен `NOT NULL` и защищён внешним ключом.

---

## Task 09 — LEFT JOIN: сотрудники без проектов

### Запрос

```sql
SELECT e.EmployeeId, e.EmployeeName
FROM Employees AS e
LEFT JOIN EmployeeProjects AS ep ON ep.EmployeeId = e.EmployeeId
WHERE ep.EmployeeId IS NULL;
```

### Результат

| EmployeeId | EmployeeName |
| --- | --- |
| 9 | Наталья Соколова |

*Строк: 1*

### Вывод

Без проектов остался один сотрудник: Наталья Соколова. `LEFT JOIN` сохраняет всех сотрудников, а отсутствие пары даёт `NULL` в столбцах `EmployeeProjects`, по нему и фильтруем. Проверять на `NULL` нужно столбец из правой таблицы, который в совпавших строках точно не `NULL`.

---

## Task 10 — Множественный JOIN: сотрудники, отделы, проекты

### Запрос

```sql
SELECT e.EmployeeName, d.DepartmentName, p.ProjectName, ep.Hours
FROM Employees AS e
INNER JOIN Departments      AS d  ON d.DepartmentId = e.DepartmentId
INNER JOIN EmployeeProjects AS ep ON ep.EmployeeId  = e.EmployeeId
INNER JOIN Projects         AS p  ON p.ProjectId    = ep.ProjectId
ORDER BY d.DepartmentName, e.EmployeeName, p.ProjectName;
```

### Результат

| EmployeeName | DepartmentName | ProjectName | Hours |
| --- | --- | --- | --- |
| Дмитрий Орлов | Аналитика | AI Assistant | 50 |
| Дмитрий Орлов | Аналитика | Data Warehouse | 70 |
| Елена Волкова | Аналитика | Mobile App | 95 |
| Ольга Смирнова | Аналитика | Data Warehouse | 110 |
| Алексей Лебедев | Менеджмент | AI Assistant | 200 |
| Алексей Лебедев | Менеджмент | CRM System | 30 |
| Анна Иванова | Разработка | CRM System | 120 |
| Анна Иванова | Разработка | Data Warehouse | 80 |
| Иван Петров | Разработка | Legacy Refactor | 40 |
| Иван Петров | Разработка | Mobile App | 90 |
| Мария Кузнецова | Разработка | Mobile App | 100 |
| Пётр Сидоров | Разработка | AI Assistant | 60 |
| Пётр Сидоров | Разработка | CRM System | 150 |
| Сергей Морозов | Тестирование | Legacy Refactor | 130 |

*Строк: 14*

### Вывод

14 строк, по одной на каждое назначение, во всех виден сотрудник, отдел, проект и часы. Сотрудник без проектов (Соколова) сюда не попадает, потому что соединение внутреннее. Таблица `EmployeeProjects` нужна как связующая: напрямую `Employees` и `Projects` не связаны.

---

## Task 11 — Self-join: иерархия руководителей

### Запрос

```sql
SELECT e.EmployeeName, m.EmployeeName AS ManagerName
FROM Employees AS e
LEFT JOIN Employees AS m ON m.EmployeeId = e.ManagerId
ORDER BY e.EmployeeId;
```

### Результат

| EmployeeName | ManagerName |
| --- | --- |
| Анна Иванова | NULL |
| Пётр Сидоров | NULL |
| Мария Кузнецова | Пётр Сидоров |
| Иван Петров | Анна Иванова |
| Ольга Смирнова | NULL |
| Дмитрий Орлов | Ольга Смирнова |
| Елена Волкова | NULL |
| Сергей Морозов | NULL |
| Наталья Соколова | Алексей Лебедев |
| Алексей Лебедев | NULL |

*Строк: 10*

### Вывод

У четырёх сотрудников руководитель найден, у остальных шести `ManagerName = NULL`. Нужен именно `LEFT JOIN`: с `INNER JOIN` сотрудники без руководителя пропали бы. Одна и та же таблица подключается дважды под разными псевдонимами (`e` и `m`).

---

## Task 12 — Подзапросы в WHERE

### Запрос

```sql
-- 12.1 Зарплата выше средней
SELECT EmployeeId, EmployeeName, Salary
FROM Employees
WHERE Salary > (SELECT AVG(Salary) FROM Employees)
ORDER BY Salary DESC;

-- 12.2 Максимальная зарплата
SELECT EmployeeId, EmployeeName, Salary
FROM Employees
WHERE Salary = (SELECT MAX(Salary) FROM Employees);
```

### Результат

**12.1 (средняя = 58900)**

| EmployeeId | EmployeeName | Salary |
| --- | --- | --- |
| 10 | Алексей Лебедев | 90000.00 |
| 2 | Пётр Сидоров | 72000.00 |
| 1 | Анна Иванова | 65000.00 |
| 5 | Ольга Смирнова | 61000.00 |

*Строк: 4*

**12.2**

| EmployeeId | EmployeeName | Salary |
| --- | --- | --- |
| 10 | Алексей Лебедев | 90000.00 |

*Строк: 1*

### Вывод

Скалярный подзапрос возвращает одно значение, поэтому его можно сравнивать через `>` и `=`. В 12.2 строк может быть несколько, если максимум повторяется, поэтому используется `=`, а не `TOP 1`.

---

## Task 13 — Скалярные подзапросы в SELECT

### Запрос

```sql
SELECT e.EmployeeName,
       e.Salary,
       e.DepartmentId,
       (SELECT AVG(e2.Salary) FROM Employees AS e2
        WHERE e2.DepartmentId = e.DepartmentId)                          AS DeptAvgSalary,
       ROUND(e.Salary - (SELECT AVG(e2.Salary) FROM Employees AS e2
                         WHERE e2.DepartmentId = e.DepartmentId), 2)      AS Diff
FROM Employees AS e
ORDER BY e.DepartmentId, e.Salary DESC;
```

### Результат

| EmployeeName | Salary | DepartmentId | DeptAvgSalary | Diff |
| --- | --- | --- | --- | --- |
| Пётр Сидоров | 72000.00 | 1 | 60000.000000 | 12000.000000 |
| Анна Иванова | 65000.00 | 1 | 60000.000000 | 5000.000000 |
| Мария Кузнецова | 55000.00 | 1 | 60000.000000 | -5000.000000 |
| Иван Петров | 48000.00 | 1 | 60000.000000 | -12000.000000 |
| Ольга Смирнова | 61000.00 | 2 | 57000.000000 | 4000.000000 |
| Дмитрий Орлов | 58000.00 | 2 | 57000.000000 | 1000.000000 |
| Елена Волкова | 52000.00 | 2 | 57000.000000 | -5000.000000 |
| Сергей Морозов | 45000.00 | 3 | 44000.000000 | 1000.000000 |
| Наталья Соколова | 43000.00 | 3 | 44000.000000 | -1000.000000 |
| Алексей Лебедев | 90000.00 | 4 | 90000.000000 | 0.000000 |

*Строк: 10*

### Вывод

Подзапрос коррелированный: для каждой строки внешнего запроса среднее считается по её отделу. Проверка: в отделе 1 среднее 60000, у Петра Сидорова отклонение +12000, у Ивана Петрова −12000. Сумма отклонений внутри каждого отдела равна нулю. Для больших таблиц оконная функция (Task 24) быстрее.

---

## Task 14 — EXISTS / NOT EXISTS

### Запрос

```sql
-- 14.1 Есть хотя бы один проект
SELECT e.EmployeeId, e.EmployeeName
FROM Employees AS e
WHERE EXISTS (SELECT 1 FROM EmployeeProjects AS ep WHERE ep.EmployeeId = e.EmployeeId);

-- 14.2 Нет проектов
SELECT e.EmployeeId, e.EmployeeName
FROM Employees AS e
WHERE NOT EXISTS (SELECT 1 FROM EmployeeProjects AS ep WHERE ep.EmployeeId = e.EmployeeId);
```

### Результат

**14.1**

| EmployeeId | EmployeeName |
| --- | --- |
| 1 | Анна Иванова |
| 2 | Пётр Сидоров |
| 3 | Мария Кузнецова |
| 4 | Иван Петров |
| 5 | Ольга Смирнова |
| 6 | Дмитрий Орлов |
| 7 | Елена Волкова |
| 8 | Сергей Морозов |
| 10 | Алексей Лебедев |

*Строк: 9*

**14.2**

| EmployeeId | EmployeeName |
| --- | --- |
| 9 | Наталья Соколова |

*Строк: 1*

### Вывод

14.1 вернул 9 сотрудников, 14.2 вернул одного (Соколова). Второй результат совпадает с заданием 9, а первый совпадает с набором уникальных сотрудников из задания 10. В условии «готово, если» указаны задания 8 и 9; по смыслу сравнение с заданием 8 (отделы) не подходит, судя по всему, имелось в виду задание 10. `SELECT 1` внутри `EXISTS` — это просто конвенция, значения столбцов не читаются.

---

## Task 15 — UNION / UNION ALL

### Запрос

```sql
-- 15.1 UNION: уникальные строки
SELECT EmployeeId, EmployeeName, Salary, HireDate FROM Employees WHERE Salary > 60000
UNION
SELECT EmployeeId, EmployeeName, Salary, HireDate FROM Employees WHERE HireDate < '2022-01-01'
ORDER BY EmployeeId;

-- 15.2 UNION ALL: с дубликатами
SELECT EmployeeId, EmployeeName, Salary, HireDate FROM Employees WHERE Salary > 60000
UNION ALL
SELECT EmployeeId, EmployeeName, Salary, HireDate FROM Employees WHERE HireDate < '2022-01-01'
ORDER BY EmployeeId;

-- 15.3 Сравнение количества строк
SELECT 'UNION' AS Variant, COUNT(*) AS RowsCount FROM (
    SELECT EmployeeId FROM Employees WHERE Salary > 60000
    UNION SELECT EmployeeId FROM Employees WHERE HireDate < '2022-01-01') AS t
UNION ALL
SELECT 'UNION ALL', COUNT(*) FROM (
    SELECT EmployeeId FROM Employees WHERE Salary > 60000
    UNION ALL SELECT EmployeeId FROM Employees WHERE HireDate < '2022-01-01') AS t;
```

### Результат

**15.1 UNION**

| EmployeeId | EmployeeName | Salary | HireDate |
| --- | --- | --- | --- |
| 1 | Анна Иванова | 65000.00 | 2020-03-15 |
| 2 | Пётр Сидоров | 72000.00 | 2019-07-01 |
| 3 | Мария Кузнецова | 55000.00 | 2021-11-20 |
| 5 | Ольга Смирнова | 61000.00 | 2020-09-05 |
| 8 | Сергей Морозов | 45000.00 | 2021-12-12 |
| 10 | Алексей Лебедев | 90000.00 | 2018-05-25 |

*Строк: 6*

**15.2 UNION ALL**

| EmployeeId | EmployeeName | Salary | HireDate |
| --- | --- | --- | --- |
| 1 | Анна Иванова | 65000.00 | 2020-03-15 |
| 1 | Анна Иванова | 65000.00 | 2020-03-15 |
| 2 | Пётр Сидоров | 72000.00 | 2019-07-01 |
| 2 | Пётр Сидоров | 72000.00 | 2019-07-01 |
| 3 | Мария Кузнецова | 55000.00 | 2021-11-20 |
| 5 | Ольга Смирнова | 61000.00 | 2020-09-05 |
| 5 | Ольга Смирнова | 61000.00 | 2020-09-05 |
| 8 | Сергей Морозов | 45000.00 | 2021-12-12 |
| 10 | Алексей Лебедев | 90000.00 | 2018-05-25 |
| 10 | Алексей Лебедев | 90000.00 | 2018-05-25 |

*Строк: 10*

**15.3 Сравнение**

| Variant | RowsCount |
| --- | --- |
| UNION | 6 |
| UNION ALL | 10 |

*Строк: 2*

### Вывод

Первый запрос даёт 4 строки, второй 6; пересечение — 4 сотрудника (Иванова, Сидоров, Смирнова, Лебедев), поэтому `UNION` вернул 6 строк, а `UNION ALL` 10. `UNION` дополнительно убирает дубликаты, то есть делает лишнюю работу, и когда дубликатов быть не может, лучше `UNION ALL`. `ORDER BY` пишется один раз в конце всего объединения.

---

## Task 16 — UPDATE: изменение данных

### Запрос

```sql
-- Копия состояния «до» для проверки
SELECT EmployeeId, Salary, HireDate INTO #Before FROM Employees;

-- Проверка до
SELECT EmployeeId, EmployeeName, Salary, HireDate, DepartmentId FROM Employees ORDER BY EmployeeId;

-- 16.1 +10% всем сотрудникам отдела 1
UPDATE Employees SET Salary = Salary * 1.10 WHERE DepartmentId = 1;
-- 16.2 +5000 нанятым до 2022 года
UPDATE Employees SET Salary = Salary + 5000 WHERE HireDate < '2022-01-01';
-- 16.3 Изменить HireDate одного сотрудника
UPDATE Employees SET HireDate = '2023-06-15' WHERE EmployeeId = 7;

-- Проверка после
SELECT EmployeeId, EmployeeName, Salary, HireDate, DepartmentId FROM Employees ORDER BY EmployeeId;

-- Какие строки изменились
SELECT e.EmployeeId, e.EmployeeName,
       b.Salary AS OldSalary, e.Salary AS NewSalary,
       b.HireDate AS OldHireDate, e.HireDate AS NewHireDate
FROM Employees AS e
JOIN #Before AS b ON b.EmployeeId = e.EmployeeId
WHERE e.Salary <> b.Salary OR e.HireDate <> b.HireDate;

DROP TABLE #Before;
-- После заданий 16–17 выполните init.sql заново, чтобы вернуть исходные данные
```

### Результат

**До**

| EmployeeId | EmployeeName | Salary | HireDate | DepartmentId |
| --- | --- | --- | --- | --- |
| 1 | Анна Иванова | 65000.00 | 2020-03-15 | 1 |
| 2 | Пётр Сидоров | 72000.00 | 2019-07-01 | 1 |
| 3 | Мария Кузнецова | 55000.00 | 2021-11-20 | 1 |
| 4 | Иван Петров | 48000.00 | 2023-02-10 | 1 |
| 5 | Ольга Смирнова | 61000.00 | 2020-09-05 | 2 |
| 6 | Дмитрий Орлов | 58000.00 | 2022-04-18 | 2 |
| 7 | Елена Волкова | 52000.00 | 2023-06-01 | 2 |
| 8 | Сергей Морозов | 45000.00 | 2021-12-12 | 3 |
| 9 | Наталья Соколова | 43000.00 | 2024-01-15 | 3 |
| 10 | Алексей Лебедев | 90000.00 | 2018-05-25 | 4 |

*Строк: 10*

**Затронуто строк:** 16.1 — 4, 16.2 — 6, 16.3 — 1

**После**

| EmployeeId | EmployeeName | Salary | HireDate | DepartmentId |
| --- | --- | --- | --- | --- |
| 1 | Анна Иванова | 76500.00 | 2020-03-15 | 1 |
| 2 | Пётр Сидоров | 84200.00 | 2019-07-01 | 1 |
| 3 | Мария Кузнецова | 65500.00 | 2021-11-20 | 1 |
| 4 | Иван Петров | 52800.00 | 2023-02-10 | 1 |
| 5 | Ольга Смирнова | 66000.00 | 2020-09-05 | 2 |
| 6 | Дмитрий Орлов | 58000.00 | 2022-04-18 | 2 |
| 7 | Елена Волкова | 52000.00 | 2023-06-15 | 2 |
| 8 | Сергей Морозов | 50000.00 | 2021-12-12 | 3 |
| 9 | Наталья Соколова | 43000.00 | 2024-01-15 | 3 |
| 10 | Алексей Лебедев | 95000.00 | 2018-05-25 | 4 |

*Строк: 10*

**Изменившиеся строки**

| EmployeeId | EmployeeName | OldSalary | NewSalary | OldHireDate | NewHireDate |
| --- | --- | --- | --- | --- | --- |
| 1 | Анна Иванова | 65000.00 | 76500.00 | 2020-03-15 | 2020-03-15 |
| 2 | Пётр Сидоров | 72000.00 | 84200.00 | 2019-07-01 | 2019-07-01 |
| 3 | Мария Кузнецова | 55000.00 | 65500.00 | 2021-11-20 | 2021-11-20 |
| 4 | Иван Петров | 48000.00 | 52800.00 | 2023-02-10 | 2023-02-10 |
| 5 | Ольга Смирнова | 61000.00 | 66000.00 | 2020-09-05 | 2020-09-05 |
| 7 | Елена Волкова | 52000.00 | 52000.00 | 2023-06-01 | 2023-06-15 |
| 8 | Сергей Морозов | 45000.00 | 50000.00 | 2021-12-12 | 2021-12-12 |
| 10 | Алексей Лебедев | 90000.00 | 95000.00 | 2018-05-25 | 2018-05-25 |

*Строк: 8*

Не изменились: Дмитрий Орлов, Наталья Соколова.

### Вывод

Изменились 8 строк из 10: зарплата у сотрудников отдела 1 и у нанятых до 2022 года, плюс дата найма у сотрудника 7; остальные строки остались как были. Порядок важен: сотрудники 1, 2 и 3 попали под оба условия (отдел 1 и найм до 2022), поэтому у них сначала +10%, потом +5000. Если поменять порядок, итог будет другим. Условие `WHERE` обязательно: без него обновятся все строки. После выполнения перезапустите `init.sql`, чтобы остальные задания считались на исходных данных.

---

## Task 17 — DELETE: удаление данных

### Запрос

```sql
-- Состояние до
SELECT COUNT(*) AS EmployeeProjectsCount FROM EmployeeProjects;
SELECT COUNT(*) AS EmployeesCount FROM Employees;

-- 17.1 Назначения с Hours = 0 (если есть)
DELETE FROM EmployeeProjects WHERE Hours = 0;
SELECT COUNT(*) AS EmployeeProjectsCount FROM EmployeeProjects;

-- 17.2 Удалить сотрудника 8: сначала его назначения (FK), потом его самого
DELETE FROM EmployeeProjects WHERE EmployeeId = 8;
SELECT COUNT(*) AS EmployeeProjectsCount FROM EmployeeProjects;
DELETE FROM Employees WHERE EmployeeId = 8;
SELECT COUNT(*) AS EmployeesCount FROM Employees;

-- 17.3 DELETE с WHERE и без — на тестовой копии
SELECT * INTO EmployeeProjects_Test FROM EmployeeProjects;
SELECT COUNT(*) AS TestCount FROM EmployeeProjects_Test;

DELETE FROM EmployeeProjects_Test WHERE Hours < 100;     -- с WHERE
SELECT COUNT(*) AS TestCount FROM EmployeeProjects_Test;

DELETE FROM EmployeeProjects_Test;                       -- без WHERE
SELECT COUNT(*) AS TestCount FROM EmployeeProjects_Test;

DROP TABLE EmployeeProjects_Test;
-- После заданий 16–17 выполните init.sql заново
```

### Результат

| Шаг | Удалено строк | EmployeeProjects | Employees |
| --- | --- | --- | --- |
| Исходное состояние | — | 14 | 10 |
| 17.1 DELETE Hours = 0 | 0 | 14 | 10 |
| 17.2a DELETE назначений сотрудника 8 | 1 | 13 | 10 |
| 17.2b DELETE сотрудника 8 | 1 | 13 | 9 |

*Строк: 4*

| Шаг (тестовая копия) | Удалено строк | Строк в копии |
| --- | --- | --- |
| Копия создана | — | 13 |
| DELETE ... WHERE Hours < 100 | 8 | 5 |
| DELETE без WHERE | 5 | 0 |

*Строк: 3*

### Вывод

Строк с `Hours = 0` нет, поэтому 17.1 удалил 0 строк, и это нормальный результат. Сотрудник 8 не руководит никем, поэтому его можно удалить; если сначала удалить сотрудника, а не его назначения, сработает внешний ключ `FK_EP_Employees`, и удаление будет отклонено. Руководителей (1, 2, 5, 10) так удалить нельзя без обнуления `ManagerId` у их подчинённых. `DELETE` без `WHERE` очищает всю таблицу, поэтому показан только на копии. Целостность связей после удаления проверена: нарушений внешних ключей нет.

---

## Task 18 — Транзакции: BEGIN TRAN / COMMIT / ROLLBACK

### Запрос

```sql
-- Исходное значение
SELECT EmployeeId, EmployeeName, Salary FROM Employees WHERE EmployeeId = 4;

-- 18.1 ROLLBACK
BEGIN TRANSACTION;
    UPDATE Employees SET Salary = Salary + 10000 WHERE EmployeeId = 4;
    SELECT EmployeeId, Salary AS SalaryInsideTran FROM Employees WHERE EmployeeId = 4;
ROLLBACK TRANSACTION;
SELECT EmployeeId, Salary AS SalaryAfterRollback FROM Employees WHERE EmployeeId = 4;

-- 18.2 COMMIT
BEGIN TRANSACTION;
    UPDATE Employees SET Salary = Salary + 10000 WHERE EmployeeId = 4;
    SELECT EmployeeId, Salary AS SalaryInsideTran FROM Employees WHERE EmployeeId = 4;
COMMIT TRANSACTION;
SELECT EmployeeId, Salary AS SalaryAfterCommit FROM Employees WHERE EmployeeId = 4;

-- Вернуть исходное значение, чтобы дальнейшие задания считались на прежних данных
UPDATE Employees SET Salary = Salary - 10000 WHERE EmployeeId = 4;
SELECT EmployeeId, Salary AS SalaryRestored FROM Employees WHERE EmployeeId = 4;
```

### Результат

| Момент | Salary сотрудника 4 (Иван Петров) |
| --- | --- |
| До транзакции | 48000.00 |
| 18.1 внутри транзакции | 58000.00 |
| 18.1 после ROLLBACK | 48000.00 |
| 18.2 внутри транзакции | 58000.00 |
| 18.2 после COMMIT | 58000.00 |
| После ручного возврата | 48000.00 |

*Строк: 6*

### Вывод

После `ROLLBACK` зарплата вернулась к 48000.00, после `COMMIT` осталась 58000.00. Внутри транзакции изменение видно только текущему соединению. Чтобы остальные задания считались на исходных данных, значение возвращено обратным `UPDATE`. В реальном коде транзакцию оборачивают в `TRY...CATCH` и делают `ROLLBACK` в `CATCH`.

---

## Task 19 — Представления (VIEW)

### Запрос

```sql
CREATE VIEW vw_EmployeeDetails
AS
SELECT e.EmployeeName, d.DepartmentName, p.ProjectName, ep.Hours
FROM Employees AS e
INNER JOIN Departments      AS d  ON d.DepartmentId = e.DepartmentId
INNER JOIN EmployeeProjects AS ep ON ep.EmployeeId  = e.EmployeeId
INNER JOIN Projects         AS p  ON p.ProjectId    = ep.ProjectId;
GO

SELECT * FROM vw_EmployeeDetails
ORDER BY DepartmentName, EmployeeName, ProjectName;
GO

DROP VIEW vw_EmployeeDetails;
GO

-- Проверка, что представления больше нет (вернёт NULL)
SELECT OBJECT_ID('vw_EmployeeDetails', 'V') AS ViewId;
```

### Результат

| EmployeeName | DepartmentName | ProjectName | Hours |
| --- | --- | --- | --- |
| Дмитрий Орлов | Аналитика | AI Assistant | 50 |
| Дмитрий Орлов | Аналитика | Data Warehouse | 70 |
| Елена Волкова | Аналитика | Mobile App | 95 |
| Ольга Смирнова | Аналитика | Data Warehouse | 110 |
| Алексей Лебедев | Менеджмент | AI Assistant | 200 |
| Алексей Лебедев | Менеджмент | CRM System | 30 |
| Анна Иванова | Разработка | CRM System | 120 |
| Анна Иванова | Разработка | Data Warehouse | 80 |
| Иван Петров | Разработка | Legacy Refactor | 40 |
| Иван Петров | Разработка | Mobile App | 90 |
| Мария Кузнецова | Разработка | Mobile App | 100 |
| Пётр Сидоров | Разработка | AI Assistant | 60 |
| Пётр Сидоров | Разработка | CRM System | 150 |
| Сергей Морозов | Тестирование | Legacy Refactor | 130 |

*Строк: 14*

**После `DROP VIEW`**

| ViewId |
| --- |
| NULL |

*Строк: 1*

### Вывод

Представление вернуло те же 14 строк, что и запрос из задания 10, `DROP VIEW` выполнился без ошибок. В условии перечислены `Employees`, `Departments`, `Projects`, но напрямую `Employees` и `Projects` не связаны, поэтому в запрос добавлена связующая `EmployeeProjects`. `CREATE VIEW` должен быть первой инструкцией пакета, поэтому перед ним и после него стоит `GO`.

---

## Task 20 — Индексы: создание и анализ

### Запрос

```sql
SET STATISTICS IO ON;
SET STATISTICS TIME ON;

-- До индексов (включите «Include Actual Execution Plan», Ctrl+M)
SELECT EmployeeId, EmployeeName, Salary
FROM Employees
WHERE DepartmentId = 1 AND Salary > 50000;

CREATE INDEX IX_Employees_DepartmentId ON Employees(DepartmentId);
CREATE INDEX IX_Employees_Salary       ON Employees(Salary);
GO

-- После индексов
SELECT EmployeeId, EmployeeName, Salary
FROM Employees
WHERE DepartmentId = 1 AND Salary > 50000;

SET STATISTICS IO OFF;
SET STATISTICS TIME OFF;
```

### Результат

**Данные заданию нужно снять самому в SSMS**: логические чтения и план запроса зависят от SQL Server и без запуска получить их нельзя. Вставьте сюда:

| Вариант | logical reads | Операторы плана |
| --- | --- | --- |
| До индексов | *(из вкладки Messages)* | *(скриншот плана)* |
| После индексов | *(из вкладки Messages)* | *(скриншот плана)* |

Что ожидать: на 10 строках вся таблица занимает одну страницу, оптимизатор, скорее всего, оставит `Clustered Index Scan` в обоих случаях, а `logical reads` будет 1–2. Это нормально: индекс не выгоднее, когда таблица помещается в одну страницу.

**Если нужна заметная разница**, можно проверить на тестовой таблице побольше (она создаётся отдельно и не затрагивает `Employees`):

```sql
SELECT TOP (100000)
       ABS(CHECKSUM(NEWID())) % 4 + 1                AS DepartmentId,
       CAST(30000 + ABS(CHECKSUM(NEWID())) % 70000 AS DECIMAL(10,2)) AS Salary
INTO EmployeesBig
FROM sys.all_objects a CROSS JOIN sys.all_objects b;

SET STATISTICS IO ON;
SELECT COUNT(*) FROM EmployeesBig WHERE DepartmentId = 1 AND Salary > 50000;   -- до: Table Scan
CREATE INDEX IX_Big_Dept_Salary ON EmployeesBig(DepartmentId, Salary);
SELECT COUNT(*) FROM EmployeesBig WHERE DepartmentId = 1 AND Salary > 50000;   -- после: Index Seek
DROP TABLE EmployeesBig;
```

### Вывод

*(Допишите после запуска: сколько было logical reads до и после, какой оператор в плане, использовался ли индекс.)* Общее правило: для условия с двумя столбцами обычно полезнее один составной индекс `(DepartmentId, Salary)`, чем два отдельных, потому что оптимизатор чаще использует один индекс на таблицу. Лишние индексы замедляют `INSERT/UPDATE/DELETE`.

---

## Task 21 — Хранимые процедуры

### Запрос

```sql
CREATE OR ALTER PROCEDURE sp_GetEmployeesByDepartment
    @DeptId INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT EmployeeId, EmployeeName, Salary, HireDate
    FROM Employees
    WHERE DepartmentId = @DeptId
    ORDER BY EmployeeName;
END
GO

CREATE OR ALTER PROCEDURE sp_GetAvgSalaryByDepartment
    @DeptId    INT,
    @AvgSalary DECIMAL(12,2) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT @AvgSalary = AVG(Salary) FROM Employees WHERE DepartmentId = @DeptId;
END
GO

-- Вызовы
EXEC sp_GetEmployeesByDepartment @DeptId = 1;
EXEC sp_GetEmployeesByDepartment @DeptId = 99;      -- крайний случай: отдела нет

DECLARE @Avg DECIMAL(12,2);
EXEC sp_GetAvgSalaryByDepartment @DeptId = 1, @AvgSalary = @Avg OUTPUT;
SELECT @Avg AS AvgSalaryDept1;

EXEC sp_GetAvgSalaryByDepartment @DeptId = 99, @AvgSalary = @Avg OUTPUT;
SELECT @Avg AS AvgSalaryDept99;                     -- NULL
```

### Результат

**EXEC sp_GetEmployeesByDepartment @DeptId = 1**

| EmployeeId | EmployeeName | Salary | HireDate |
| --- | --- | --- | --- |
| 1 | Анна Иванова | 65000.00 | 2020-03-15 |
| 4 | Иван Петров | 48000.00 | 2023-02-10 |
| 3 | Мария Кузнецова | 55000.00 | 2021-11-20 |
| 2 | Пётр Сидоров | 72000.00 | 2019-07-01 |

*Строк: 4*

**EXEC sp_GetEmployeesByDepartment @DeptId = 99**

| EmployeeId | EmployeeName | Salary | HireDate |
| --- | --- | --- | --- |

*Строк: 0*

**OUTPUT-процедура**

| Отдел | AvgSalary |
| --- | --- |
| 1 | 60000.00 |
| 99 | NULL |

### Вывод

Обе процедуры вызываются: первая вернула 4 сотрудников отдела 1, вторая вернула среднюю 60000.00 через `OUTPUT`. Для несуществующего отдела первая возвращает пустой набор, а вторая `NULL`; вызывающий код должен это учитывать. Имя с префиксом `sp_` требуется заданием, но Microsoft не рекомендует его: SQL Server сначала ищет такие процедуры в `master`, поэтому в реальных проектах используют другой префикс (`usp_`). `CREATE OR ALTER` (SQL Server 2016 SP1+) позволяет перезапускать скрипт без ошибки «уже существует».

---

## Task 22 — Функции: скалярная и табличная

### Запрос

```sql
CREATE OR ALTER FUNCTION dbo.fn_GetAnnualSalary (@MonthlySalary DECIMAL(10,2))
RETURNS DECIMAL(12,2)
AS
BEGIN
    RETURN @MonthlySalary * 12;
END
GO

CREATE OR ALTER FUNCTION dbo.fn_GetEmployeesBySalaryRange (@Min DECIMAL(10,2), @Max DECIMAL(10,2))
RETURNS TABLE
AS
RETURN
(
    SELECT EmployeeId, EmployeeName, Salary, DepartmentId
    FROM Employees
    WHERE Salary BETWEEN @Min AND @Max
);
GO

-- Скалярная функция в SELECT
SELECT EmployeeName, Salary, dbo.fn_GetAnnualSalary(Salary) AS AnnualSalary
FROM Employees
ORDER BY EmployeeId;

-- Табличная функция в FROM
SELECT * FROM dbo.fn_GetEmployeesBySalaryRange(50000, 65000)
ORDER BY Salary DESC;
```

### Результат

**Скалярная fn_GetAnnualSalary**

| EmployeeName | Salary | AnnualSalary |
| --- | --- | --- |
| Анна Иванова | 65000.00 | 780000.00 |
| Пётр Сидоров | 72000.00 | 864000.00 |
| Мария Кузнецова | 55000.00 | 660000.00 |
| Иван Петров | 48000.00 | 576000.00 |
| Ольга Смирнова | 61000.00 | 732000.00 |
| Дмитрий Орлов | 58000.00 | 696000.00 |
| Елена Волкова | 52000.00 | 624000.00 |
| Сергей Морозов | 45000.00 | 540000.00 |
| Наталья Соколова | 43000.00 | 516000.00 |
| Алексей Лебедев | 90000.00 | 1080000.00 |

*Строк: 10*

**Табличная fn_GetEmployeesBySalaryRange(50000, 65000)**

| EmployeeId | EmployeeName | Salary | DepartmentId |
| --- | --- | --- | --- |
| 1 | Анна Иванова | 65000.00 | 1 |
| 5 | Ольга Смирнова | 61000.00 | 2 |
| 6 | Дмитрий Орлов | 58000.00 | 2 |
| 3 | Мария Кузнецова | 55000.00 | 1 |
| 7 | Елена Волкова | 52000.00 | 2 |

*Строк: 5*

### Вывод

Скалярная функция работает в списке `SELECT` (годовая зарплата Анны Ивановой 780000.00), табличная вызывается в `FROM` и вернула 5 сотрудников в диапазоне. В условии параметр объявлен как `DECIMAL` без размера: это `DECIMAL(18,0)`, и копейки отбросились бы, поэтому в функции указано `DECIMAL(10,2)`. Табличная функция сделана встроенной (inline): такие функции оптимизатор раскрывает как обычный запрос, это быстрее многооператорных.

---

## Task 23 — Оконные функции: ROW_NUMBER, RANK, DENSE_RANK

### Запрос

```sql
-- 23.1 Основной запрос
SELECT EmployeeName, DepartmentId, Salary,
       ROW_NUMBER() OVER (ORDER BY Salary DESC)                             AS RowNum,
       RANK()       OVER (PARTITION BY DepartmentId ORDER BY Salary DESC)   AS RankInDept,
       DENSE_RANK() OVER (PARTITION BY DepartmentId ORDER BY Salary DESC)   AS DenseRankInDept
FROM Employees
ORDER BY RowNum;

-- 23.2 В тестовых данных равных зарплат нет, поэтому разницу RANK и DENSE_RANK покажем на наборе с повторами
SELECT Name, Salary,
       RANK()       OVER (ORDER BY Salary DESC) AS Rnk,
       DENSE_RANK() OVER (ORDER BY Salary DESC) AS DenseRnk
FROM (VALUES (N'A', 90000), (N'B', 70000), (N'C', 70000), (N'D', 50000)) AS t(Name, Salary)
ORDER BY Salary DESC, Name;
```

### Результат

**23.1**

| EmployeeName | DepartmentId | Salary | RowNum | RankInDept | DenseRankInDept |
| --- | --- | --- | --- | --- | --- |
| Алексей Лебедев | 4 | 90000.00 | 1 | 1 | 1 |
| Пётр Сидоров | 1 | 72000.00 | 2 | 1 | 1 |
| Анна Иванова | 1 | 65000.00 | 3 | 2 | 2 |
| Ольга Смирнова | 2 | 61000.00 | 4 | 1 | 1 |
| Дмитрий Орлов | 2 | 58000.00 | 5 | 2 | 2 |
| Мария Кузнецова | 1 | 55000.00 | 6 | 3 | 3 |
| Елена Волкова | 2 | 52000.00 | 7 | 3 | 3 |
| Иван Петров | 1 | 48000.00 | 8 | 4 | 4 |
| Сергей Морозов | 3 | 45000.00 | 9 | 1 | 1 |
| Наталья Соколова | 3 | 43000.00 | 10 | 2 | 2 |

*Строк: 10*

**23.2 Набор с равными зарплатами**

| Name | Salary | Rnk | DenseRnk |
| --- | --- | --- | --- |
| A | 90000.00 | 1 | 1 |
| B | 70000.00 | 2 | 2 |
| C | 70000.00 | 2 | 2 |
| D | 50000.00 | 4 | 3 |

*Строк: 4*

### Вывод

`ROW_NUMBER` нумерует все строки подряд (1–10); `RANK` и `DENSE_RANK` с `PARTITION BY DepartmentId` считают место внутри отдела. В исходных данных все зарплаты разные, поэтому ранги совпадают, и разницу видно только на 23.2: при двух равных значениях `RANK` даёт 1, 2, 2, 4 (пропуск), а `DENSE_RANK` даёт 1, 2, 2, 3 (без пропусков).

---

## Task 24 — Оконные агрегаты: SUM OVER, AVG OVER

### Запрос

```sql
SELECT EmployeeName, DepartmentId, Salary,
       SUM(Salary) OVER (PARTITION BY DepartmentId)                    AS DeptTotal,
       AVG(Salary) OVER (PARTITION BY DepartmentId)                    AS DeptAvg,
       ROUND(Salary - AVG(Salary) OVER (PARTITION BY DepartmentId), 2) AS Deviation
FROM Employees
ORDER BY DepartmentId, Salary DESC;

-- Сверка с GROUP BY
SELECT DepartmentId, SUM(Salary) AS DeptTotal, AVG(Salary) AS DeptAvg
FROM Employees
GROUP BY DepartmentId
ORDER BY DepartmentId;
```

### Результат

**Оконные агрегаты**

| EmployeeName | DepartmentId | Salary | DeptTotal | DeptAvg | Deviation |
| --- | --- | --- | --- | --- | --- |
| Пётр Сидоров | 1 | 72000.00 | 240000.00 | 60000.000000 | 12000.000000 |
| Анна Иванова | 1 | 65000.00 | 240000.00 | 60000.000000 | 5000.000000 |
| Мария Кузнецова | 1 | 55000.00 | 240000.00 | 60000.000000 | -5000.000000 |
| Иван Петров | 1 | 48000.00 | 240000.00 | 60000.000000 | -12000.000000 |
| Ольга Смирнова | 2 | 61000.00 | 171000.00 | 57000.000000 | 4000.000000 |
| Дмитрий Орлов | 2 | 58000.00 | 171000.00 | 57000.000000 | 1000.000000 |
| Елена Волкова | 2 | 52000.00 | 171000.00 | 57000.000000 | -5000.000000 |
| Сергей Морозов | 3 | 45000.00 | 88000.00 | 44000.000000 | 1000.000000 |
| Наталья Соколова | 3 | 43000.00 | 88000.00 | 44000.000000 | -1000.000000 |
| Алексей Лебедев | 4 | 90000.00 | 90000.00 | 90000.000000 | 0.000000 |

*Строк: 10*

**Сверка с GROUP BY**

| DepartmentId | DeptTotal | DeptAvg |
| --- | --- | --- |
| 1 | 240000.00 | 60000.000000 |
| 2 | 171000.00 | 57000.000000 |
| 3 | 88000.00 | 44000.000000 |
| 4 | 90000.00 | 90000.000000 |

*Строк: 4*

### Вывод

Итоги и средние в оконном запросе совпадают с `GROUP BY` (отдел 1: 240000 и 60000, отдел 2: 171000 и 57000, отдел 3: 88000 и 44000, отдел 4: 90000 и 90000). Главное отличие: `GROUP BY` схлопывает строки, а окно оставляет каждого сотрудника и добавляет к нему агрегат. Название задания говорит о накопительных итогах, но в требованиях итоги по отделу без `ORDER BY` внутри `OVER`; накопительный вариант потребовал бы `SUM(...) OVER (PARTITION BY ... ORDER BY ...)`.

---

## Task 25 — CTE (Common Table Expressions)

### Запрос

```sql
-- 25.1 Задание 12 через CTE
WITH AvgSalary AS (
    SELECT AVG(Salary) AS AvgValue FROM Employees
)
SELECT e.EmployeeId, e.EmployeeName, e.Salary
FROM Employees AS e
CROSS JOIN AvgSalary AS a
WHERE e.Salary > a.AvgValue
ORDER BY e.Salary DESC;

-- 25.2 Топ-3 отдела по средней зарплате
WITH DeptAvg AS (
    SELECT DepartmentId, AVG(Salary) AS AvgSalary
    FROM Employees
    GROUP BY DepartmentId
)
SELECT TOP 3 d.DepartmentName, ROUND(da.AvgSalary, 2) AS AvgSalary
FROM DeptAvg AS da
INNER JOIN Departments AS d ON d.DepartmentId = da.DepartmentId
ORDER BY da.AvgSalary DESC;

-- 25.3 Сверка: разница между CTE-версией и подзапросом из задания 12 (должно вернуть 0 строк)
WITH AvgSalary AS (SELECT AVG(Salary) AS AvgValue FROM Employees)
SELECT e.EmployeeId FROM Employees e CROSS JOIN AvgSalary a WHERE e.Salary > a.AvgValue
EXCEPT
SELECT EmployeeId FROM Employees WHERE Salary > (SELECT AVG(Salary) FROM Employees);

/* Читаемость: в CTE среднее получает имя и вычисляется отдельным блоком над основным запросом,
   читать удобнее сверху вниз. Подзапрос короче, когда значение нужно один раз и условие простое.
   CTE выигрывает, когда одно и то же промежуточное множество нужно несколько раз или шагов несколько. */
```

### Результат

**25.1 CTE: выше средней**

| EmployeeId | EmployeeName | Salary |
| --- | --- | --- |
| 10 | Алексей Лебедев | 90000.00 |
| 2 | Пётр Сидоров | 72000.00 |
| 1 | Анна Иванова | 65000.00 |
| 5 | Ольга Смирнова | 61000.00 |

*Строк: 4*

**25.2 Топ-3 отделов**

| DepartmentName | AvgSalary |
| --- | --- |
| Менеджмент | 90000.000000 |
| Разработка | 60000.000000 |
| Аналитика | 57000.000000 |

*Строк: 3*

**25.3 Сверка с заданием 12**

| EmployeeId |
| --- |
|  |

*Строк: 0*

### Вывод

CTE-версия вернула тех же четырёх сотрудников, что и подзапрос в задании 12 (разность множеств пуста). Топ-3 отдела: Менеджмент, Разработка, Аналитика. Для такого простого запроса подзапрос короче, но CTE даёт имя промежуточному результату и читается сверху вниз; он полезнее, когда шагов несколько или результат нужен повторно.

---

## Task 26 — CASE: условная логика в SELECT

### Запрос

```sql
SELECT EmployeeName, Salary,
       CASE WHEN Salary > 60000                  THEN 'High'
            WHEN Salary BETWEEN 40000 AND 60000  THEN 'Medium'
            ELSE 'Low'
       END AS SalaryLevel,
       HireDate,
       CASE WHEN HireDate <  '2020-01-01'                         THEN 'Senior'
            WHEN HireDate >= '2020-01-01' AND HireDate < '2023-01-01' THEN 'Mid'
            ELSE 'Junior'
       END AS Tenure
FROM Employees
ORDER BY EmployeeId;
```

### Результат

| EmployeeName | Salary | SalaryLevel | HireDate | Tenure |
| --- | --- | --- | --- | --- |
| Анна Иванова | 65000.00 | High | 2020-03-15 | Mid |
| Пётр Сидоров | 72000.00 | High | 2019-07-01 | Senior |
| Мария Кузнецова | 55000.00 | Medium | 2021-11-20 | Mid |
| Иван Петров | 48000.00 | Medium | 2023-02-10 | Junior |
| Ольга Смирнова | 61000.00 | High | 2020-09-05 | Mid |
| Дмитрий Орлов | 58000.00 | Medium | 2022-04-18 | Mid |
| Елена Волкова | 52000.00 | Medium | 2023-06-01 | Junior |
| Сергей Морозов | 45000.00 | Medium | 2021-12-12 | Mid |
| Наталья Соколова | 43000.00 | Medium | 2024-01-15 | Junior |
| Алексей Лебедев | 90000.00 | High | 2018-05-25 | Senior |

*Строк: 10*

### Вывод

`NULL` нет: у каждого сотрудника есть и уровень, и стаж. Уровень `Low` в данных не встречается (минимальная зарплата 43000), но ветка нужна на случай новых строк. Граница ровно 60000 попадает в `Medium` по условию `> 60000` для `High`. Для дат взяты полуоткрытые интервалы (`>= 2020-01-01 AND < 2023-01-01`), так не теряются даты в последний день 2022 года. Порядок веток в `CASE` важен: срабатывает первая подошедшая.

---

## Task 27 — Работа с датами: DATEADD, DATEDIFF, FORMAT

### Запрос

```sql
SELECT EmployeeName,
       HireDate,
       DATEDIFF(YEAR,  HireDate, GETDATE()) AS YearsDiff,       -- как в задании: считает границы лет
       DATEDIFF(YEAR,  HireDate, GETDATE())
         - CASE WHEN DATEADD(YEAR, DATEDIFF(YEAR, HireDate, GETDATE()), HireDate) > CAST(GETDATE() AS DATE)
                THEN 1 ELSE 0 END                               AS FullYears,       -- именно полные годы
       DATEDIFF(MONTH, HireDate, GETDATE()) AS MonthsDiff,
       DATEADD(YEAR, 1, HireDate)           AS FirstAnniversary,
       FORMAT(HireDate, 'dd.MM.yyyy')       AS HireDateFmt
FROM Employees
ORDER BY EmployeeId;
```

### Результат

*Результат на дату 2026-11-20 (`GETDATE()` при другой дате запуска даст другие числа).*

| EmployeeName | HireDate | YearsDiff | FullYears | MonthsDiff | FirstAnniversary | HireDateFmt |
| --- | --- | --- | --- | --- | --- | --- |
| Анна Иванова | 2020-03-15 | 6 | 6 | 80 | 2021-03-15 | 15.03.2020 |
| Пётр Сидоров | 2019-07-01 | 7 | 7 | 88 | 2020-07-01 | 01.07.2019 |
| Мария Кузнецова | 2021-11-20 | 5 | 5 | 60 | 2022-11-20 | 20.11.2021 |
| Иван Петров | 2023-02-10 | 3 | 3 | 45 | 2024-02-10 | 10.02.2023 |
| Ольга Смирнова | 2020-09-05 | 6 | 6 | 74 | 2021-09-05 | 05.09.2020 |
| Дмитрий Орлов | 2022-04-18 | 4 | 4 | 55 | 2023-04-18 | 18.04.2022 |
| Елена Волкова | 2023-06-01 | 3 | 3 | 41 | 2024-06-01 | 01.06.2023 |
| Сергей Морозов | 2021-12-12 | 5 | 4 | 59 | 2022-12-12 | 12.12.2021 |
| Наталья Соколова | 2024-01-15 | 2 | 2 | 34 | 2025-01-15 | 15.01.2024 |
| Алексей Лебедев | 2018-05-25 | 8 | 8 | 102 | 2019-05-25 | 25.05.2018 |

*Строк: 10*

### Вывод

`DATEDIFF(YEAR, ...)` считает не полные годы, а количество пересечённых границ 1 января: для человека, нанятого в декабре, через месяц он покажет «1 год». Поэтому, кроме столбца из задания (`YearsDiff`), добавлен `FullYears` с поправкой; сравните их, например, у Сергея Морозова (нанят 12.12.2021): 5 по `DATEDIFF` и 4 полных года. `DATEDIFF(MONTH, ...)` тоже считает границы месяцев, а не полные месяцы. `FORMAT` удобен для вывода, но медленнее `CONVERT` на больших таблицах.
