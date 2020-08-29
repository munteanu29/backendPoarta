-- phpMyAdmin SQL Dump
-- version 4.9.1
-- https://www.phpmyadmin.net/
--
-- Gazdă: mdb
-- Timp de generare: ian. 07, 2020 la 08:18 PM
-- Versiune server: 10.4.1-MariaDB-1:10.4.1+maria~bionic
-- Versiune PHP: 7.2.23

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Bază de date: `sma`
--

-- --------------------------------------------------------

--
-- Structură tabel pentru tabel `AspNetRoleClaims`
--

CREATE TABLE `AspNetRoleClaims` (
  `Id` int(11) NOT NULL,
  `RoleId` varchar(255) COLLATE utf8mb4_unicode_520_ci NOT NULL,
  `ClaimType` longtext COLLATE utf8mb4_unicode_520_ci DEFAULT NULL,
  `ClaimValue` longtext COLLATE utf8mb4_unicode_520_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_520_ci;

-- --------------------------------------------------------

--
-- Structură tabel pentru tabel `AspNetRoles`
--

CREATE TABLE `AspNetRoles` (
  `Id` varchar(255) COLLATE utf8mb4_unicode_520_ci NOT NULL,
  `Name` varchar(256) COLLATE utf8mb4_unicode_520_ci DEFAULT NULL,
  `NormalizedName` varchar(256) COLLATE utf8mb4_unicode_520_ci DEFAULT NULL,
  `ConcurrencyStamp` longtext COLLATE utf8mb4_unicode_520_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_520_ci;

-- --------------------------------------------------------

--
-- Structură tabel pentru tabel `AspNetUserClaims`
--

CREATE TABLE `AspNetUserClaims` (
  `Id` int(11) NOT NULL,
  `UserId` varchar(255) COLLATE utf8mb4_unicode_520_ci NOT NULL,
  `ClaimType` longtext COLLATE utf8mb4_unicode_520_ci DEFAULT NULL,
  `ClaimValue` longtext COLLATE utf8mb4_unicode_520_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_520_ci;

-- --------------------------------------------------------

--
-- Structură tabel pentru tabel `AspNetUserLogins`
--

CREATE TABLE `AspNetUserLogins` (
  `LoginProvider` varchar(128) COLLATE utf8mb4_unicode_520_ci NOT NULL,
  `ProviderKey` varchar(128) COLLATE utf8mb4_unicode_520_ci NOT NULL,
  `ProviderDisplayName` longtext COLLATE utf8mb4_unicode_520_ci DEFAULT NULL,
  `UserId` varchar(255) COLLATE utf8mb4_unicode_520_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_520_ci;

-- --------------------------------------------------------

--
-- Structură tabel pentru tabel `AspNetUserRoles`
--

CREATE TABLE `AspNetUserRoles` (
  `UserId` varchar(255) COLLATE utf8mb4_unicode_520_ci NOT NULL,
  `RoleId` varchar(255) COLLATE utf8mb4_unicode_520_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_520_ci;

-- --------------------------------------------------------

--
-- Structură tabel pentru tabel `AspNetUsers`
--

CREATE TABLE `AspNetUsers` (
  `Id` varchar(255) COLLATE utf8mb4_unicode_520_ci NOT NULL,
  `UserName` varchar(256) COLLATE utf8mb4_unicode_520_ci DEFAULT NULL,
  `NormalizedUserName` varchar(256) COLLATE utf8mb4_unicode_520_ci DEFAULT NULL,
  `Email` varchar(256) COLLATE utf8mb4_unicode_520_ci DEFAULT NULL,
  `NormalizedEmail` varchar(256) COLLATE utf8mb4_unicode_520_ci DEFAULT NULL,
  `EmailConfirmed` bit(1) NOT NULL,
  `PasswordHash` longtext COLLATE utf8mb4_unicode_520_ci DEFAULT NULL,
  `SecurityStamp` longtext COLLATE utf8mb4_unicode_520_ci DEFAULT NULL,
  `ConcurrencyStamp` longtext COLLATE utf8mb4_unicode_520_ci DEFAULT NULL,
  `FirstName` longtext COLLATE utf8mb4_unicode_520_ci DEFAULT NULL,
  `LastName` longtext COLLATE utf8mb4_unicode_520_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_520_ci;

--
-- Eliminarea datelor din tabel `AspNetUsers`
--

INSERT INTO `AspNetUsers` (`Id`, `UserName`, `NormalizedUserName`, `Email`, `NormalizedEmail`, `EmailConfirmed`, `PasswordHash`, `SecurityStamp`, `ConcurrencyStamp`, `FirstName`, `LastName`) VALUES
('7cfe6032-5bc7-4750-832f-bac07189e1f5', 'ceva@mailinator.com', 'CEVA@MAILINATOR.COM', 'ceva@mailinator.com', 'CEVA@MAILINATOR.COM', b'0', 'AQAAAAEAACcQAAAAEE+lQzy6r4YV5irB6cukgYrgBQIiWSxhvwL0x6RN97w9VhD9dnTVeNuC3ctagBd66A==', 'WREHLEPOL2KVOCRYMXW3TQ5MKISFB4B3', '58aeae51-e1df-4516-a431-41f86287e6c2', 'string', 'string');

-- --------------------------------------------------------

--
-- Structură tabel pentru tabel `AspNetUserTokens`
--

CREATE TABLE `AspNetUserTokens` (
  `UserId` varchar(255) COLLATE utf8mb4_unicode_520_ci NOT NULL,
  `LoginProvider` varchar(128) COLLATE utf8mb4_unicode_520_ci NOT NULL,
  `Name` varchar(128) COLLATE utf8mb4_unicode_520_ci NOT NULL,
  `Value` longtext COLLATE utf8mb4_unicode_520_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_520_ci;

-- --------------------------------------------------------

--
-- Structură tabel pentru tabel `HeaterEntities`
--

CREATE TABLE `HeaterEntities` (
  `Id` varchar(255) COLLATE utf8mb4_unicode_520_ci NOT NULL,
  `Deleted` bit(1) NOT NULL,
  `IsOn` bit(1) NOT NULL,
  `Temperature` int(11) NOT NULL,
  `SetTemperature` int(11) NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_520_ci;

--
-- Eliminarea datelor din tabel `HeaterEntities`
--

INSERT INTO `HeaterEntities` (`Id`, `Deleted`, `IsOn`, `Temperature`, `SetTemperature`) VALUES
('1', b'0', b'1', 72, 22);

-- --------------------------------------------------------

--
-- Structură tabel pentru tabel `__EFMigrationsHistory`
--

CREATE TABLE `__EFMigrationsHistory` (
  `MigrationId` varchar(95) COLLATE utf8mb4_unicode_520_ci NOT NULL,
  `ProductVersion` varchar(32) COLLATE utf8mb4_unicode_520_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_520_ci;

--
-- Eliminarea datelor din tabel `__EFMigrationsHistory`
--

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`) VALUES
('20191105205603_initial', '2.2.6-servicing-10079'),
('20191107104950_change_car_foreignkey', '2.2.6-servicing-10079'),
('20191107112848_change_car_year_type', '2.2.6-servicing-10079'),
('20191107145224_deleted', '2.2.6-servicing-10079'),
('20191107153009_add_first_and_last_name', '2.2.6-servicing-10079'),
('20191107160910_remove_unnecessary_fields_from_user', '2.2.6-servicing-10079'),
('20191107211733_updated_stations', '2.2.6-servicing-10079'),
('20191109081545_finished_stations', '2.2.6-servicing-10079'),
('20191109094542_votes', '2.2.6-servicing-10079'),
('20191109180657_forum', '2.2.6-servicing-10079'),
('20200105204216_deletedItec', '2.2.6-servicing-10079'),
('20200106192241_plm', '2.2.6-servicing-10079'),
('20200106193317_plm1', '2.2.6-servicing-10079'),
('20200106203515_addedSetTemperature', '2.2.6-servicing-10079');

--
-- Indexuri pentru tabele eliminate
--

--
-- Indexuri pentru tabele `AspNetRoleClaims`
--
ALTER TABLE `AspNetRoleClaims`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_AspNetRoleClaims_RoleId` (`RoleId`);

--
-- Indexuri pentru tabele `AspNetRoles`
--
ALTER TABLE `AspNetRoles`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `RoleNameIndex` (`NormalizedName`);

--
-- Indexuri pentru tabele `AspNetUserClaims`
--
ALTER TABLE `AspNetUserClaims`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_AspNetUserClaims_UserId` (`UserId`);

--
-- Indexuri pentru tabele `AspNetUserLogins`
--
ALTER TABLE `AspNetUserLogins`
  ADD PRIMARY KEY (`LoginProvider`,`ProviderKey`),
  ADD KEY `IX_AspNetUserLogins_UserId` (`UserId`);

--
-- Indexuri pentru tabele `AspNetUserRoles`
--
ALTER TABLE `AspNetUserRoles`
  ADD PRIMARY KEY (`UserId`,`RoleId`),
  ADD KEY `IX_AspNetUserRoles_RoleId` (`RoleId`);

--
-- Indexuri pentru tabele `AspNetUsers`
--
ALTER TABLE `AspNetUsers`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `UserNameIndex` (`NormalizedUserName`),
  ADD KEY `EmailIndex` (`NormalizedEmail`);

--
-- Indexuri pentru tabele `AspNetUserTokens`
--
ALTER TABLE `AspNetUserTokens`
  ADD PRIMARY KEY (`UserId`,`LoginProvider`,`Name`);

--
-- Indexuri pentru tabele `HeaterEntities`
--
ALTER TABLE `HeaterEntities`
  ADD PRIMARY KEY (`Id`);

--
-- Indexuri pentru tabele `__EFMigrationsHistory`
--
ALTER TABLE `__EFMigrationsHistory`
  ADD PRIMARY KEY (`MigrationId`);

--
-- AUTO_INCREMENT pentru tabele eliminate
--

--
-- AUTO_INCREMENT pentru tabele `AspNetRoleClaims`
--
ALTER TABLE `AspNetRoleClaims`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT pentru tabele `AspNetUserClaims`
--
ALTER TABLE `AspNetUserClaims`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- Constrângeri pentru tabele eliminate
--

--
-- Constrângeri pentru tabele `AspNetRoleClaims`
--
ALTER TABLE `AspNetRoleClaims`
  ADD CONSTRAINT `FK_AspNetRoleClaims_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE;

--
-- Constrângeri pentru tabele `AspNetUserClaims`
--
ALTER TABLE `AspNetUserClaims`
  ADD CONSTRAINT `FK_AspNetUserClaims_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE;

--
-- Constrângeri pentru tabele `AspNetUserLogins`
--
ALTER TABLE `AspNetUserLogins`
  ADD CONSTRAINT `FK_AspNetUserLogins_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE;

--
-- Constrângeri pentru tabele `AspNetUserRoles`
--
ALTER TABLE `AspNetUserRoles`
  ADD CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_AspNetUserRoles_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE;

--
-- Constrângeri pentru tabele `AspNetUserTokens`
--
ALTER TABLE `AspNetUserTokens`
  ADD CONSTRAINT `FK_AspNetUserTokens_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
