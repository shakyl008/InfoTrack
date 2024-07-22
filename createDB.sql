CREATE DATABASE app;
USE app;

CREATE TABLE Search (
    id INT PRIMARY KEY IDENTITY,       -- auto increment
    searchQuery NVARCHAR(255),		   -- search query
    url NVARCHAR(255),                 -- url provided
    positions NVARCHAR(355),           -- positions
    searchDate DATETIME                -- date
);
