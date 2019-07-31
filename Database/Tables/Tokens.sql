use thenutmegstore;


CREATE TABLE `Tokens` (
 `Id` varchar(100) NOT NULL,
 `EmailAddress` varchar(100) NOT NULL,
 `Token` varchar(100) NOT NULL,
 `IsStop` int NOT NULL,
 `ClientId` varchar(100) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

