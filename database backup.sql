-- phpMyAdmin SQL Dump
-- version 4.9.4
-- https://www.phpmyadmin.net/
--
-- Počítač: localhost
-- Vytvořeno: Ned 05. čen 2022, 18:24
-- Verze serveru: 10.3.25-MariaDB-0+deb10u1
-- Verze PHP: 5.6.36-0+deb8u1

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Databáze: `3c1_pychadavid_db1`
--

-- --------------------------------------------------------

--
-- Struktura tabulky `Admins`
--

CREATE TABLE `Admins` (
  `id` int(11) NOT NULL,
  `username` varchar(255) COLLATE utf8_czech_ci NOT NULL,
  `password` varchar(255) COLLATE utf8_czech_ci NOT NULL,
  `pfp` blob DEFAULT NULL,
  `email` varchar(255) COLLATE utf8_czech_ci DEFAULT NULL,
  `mailsSent` int(5) NOT NULL DEFAULT 0,
  `errors` bit(1) NOT NULL DEFAULT b'1',
  `successes` bit(1) NOT NULL DEFAULT b'1',
  `schedule` varchar(255) COLLATE utf8_czech_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_czech_ci;

--
-- Vypisuji data pro tabulku `Admins`
--

-- --------------------------------------------------------

--
-- Struktura tabulky `Configs`
--

CREATE TABLE `Configs` (
  `id` int(11) NOT NULL,
  `configName` varchar(30) COLLATE utf8_czech_ci DEFAULT NULL,
  `last used` datetime DEFAULT NULL,
  `schedule` varchar(255) COLLATE utf8_czech_ci DEFAULT NULL,
  `type` enum('full','differencial','incremental') COLLATE utf8_czech_ci DEFAULT 'full',
  `backupCount` int(11) DEFAULT NULL,
  `packageCount` int(11) DEFAULT NULL,
  `isZIP` bit(1) DEFAULT b'0'
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_czech_ci;

--
-- Vypisuji data pro tabulku `Configs`
--

-- --------------------------------------------------------

--
-- Struktura tabulky `ConfigsUsers`
--

CREATE TABLE `ConfigsUsers` (
  `userID` int(11) NOT NULL,
  `configID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_czech_ci;

--
-- Vypisuji data pro tabulku `ConfigsUsers`
--

-- --------------------------------------------------------

--
-- Struktura tabulky `DestF`
--

CREATE TABLE `DestF` (
  `id` int(11) NOT NULL,
  `configID` int(11) NOT NULL,
  `path` varchar(255) COLLATE utf8_czech_ci NOT NULL,
  `type` enum('local','ftp') COLLATE utf8_czech_ci NOT NULL,
  `login` varchar(20) COLLATE utf8_czech_ci DEFAULT '',
  `password` varchar(20) COLLATE utf8_czech_ci DEFAULT '',
  `hostname` varchar(20) COLLATE utf8_czech_ci NOT NULL DEFAULT '',
  `port` int(10) DEFAULT 21
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_czech_ci;

--
-- Vypisuji data pro tabulku `DestF`
--

-- --------------------------------------------------------

--
-- Struktura tabulky `Logs`
--

CREATE TABLE `Logs` (
  `id` int(11) NOT NULL,
  `userID` int(11) DEFAULT NULL,
  `configID` int(11) DEFAULT NULL,
  `status` bit(1) DEFAULT NULL,
  `details` int(11) DEFAULT NULL,
  `date` datetime DEFAULT NULL,
  `already sent` bit(1) DEFAULT b'0'
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_czech_ci;

--
-- Vypisuji data pro tabulku `Logs`
--

-- --------------------------------------------------------

--
-- Struktura tabulky `SourceF`
--

CREATE TABLE `SourceF` (
  `id` int(11) NOT NULL,
  `configID` int(11) DEFAULT NULL,
  `path` varchar(255) COLLATE utf8_czech_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_czech_ci;

--
-- Vypisuji data pro tabulku `SourceF`
--


-- --------------------------------------------------------

--
-- Struktura tabulky `Users`
--

CREATE TABLE `Users` (
  `id` int(11) NOT NULL,
  `username` varchar(255) COLLATE utf8_czech_ci DEFAULT NULL,
  `ip` varchar(255) COLLATE utf8_czech_ci DEFAULT NULL,
  `online` bit(1) DEFAULT NULL,
  `last backup` datetime DEFAULT NULL,
  `verified` bit(1) NOT NULL DEFAULT b'0',
  `minutesOnline` int(100) DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_czech_ci;

--
-- Vypisuji data pro tabulku `Users`
--



--
-- Klíče pro exportované tabulky
--

--
-- Klíče pro tabulku `Admins`
--
ALTER TABLE `Admins`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `username` (`username`);

--
-- Klíče pro tabulku `Configs`
--
ALTER TABLE `Configs`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `configName` (`configName`);

--
-- Klíče pro tabulku `ConfigsUsers`
--
ALTER TABLE `ConfigsUsers`
  ADD PRIMARY KEY (`userID`,`configID`),
  ADD KEY `userID` (`userID`) USING BTREE,
  ADD KEY `configID` (`configID`) USING BTREE;

--
-- Klíče pro tabulku `DestF`
--
ALTER TABLE `DestF`
  ADD PRIMARY KEY (`id`),
  ADD KEY `configID` (`configID`) USING BTREE;

--
-- Klíče pro tabulku `Logs`
--
ALTER TABLE `Logs`
  ADD PRIMARY KEY (`id`),
  ADD KEY `configID` (`configID`) USING BTREE,
  ADD KEY `userID` (`userID`) USING BTREE;

--
-- Klíče pro tabulku `SourceF`
--
ALTER TABLE `SourceF`
  ADD PRIMARY KEY (`id`),
  ADD KEY `configID` (`configID`) USING BTREE;

--
-- Klíče pro tabulku `Users`
--
ALTER TABLE `Users`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT pro tabulky
--

--
-- AUTO_INCREMENT pro tabulku `Admins`
--
ALTER TABLE `Admins`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT pro tabulku `Configs`
--
ALTER TABLE `Configs`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=31;

--
-- AUTO_INCREMENT pro tabulku `DestF`
--
ALTER TABLE `DestF`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=35;

--
-- AUTO_INCREMENT pro tabulku `Logs`
--
ALTER TABLE `Logs`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=31;

--
-- AUTO_INCREMENT pro tabulku `SourceF`
--
ALTER TABLE `SourceF`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=40;

--
-- AUTO_INCREMENT pro tabulku `Users`
--
ALTER TABLE `Users`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=18;

--
-- Omezení pro exportované tabulky
--

--
-- Omezení pro tabulku `ConfigsUsers`
--
ALTER TABLE `ConfigsUsers`
  ADD CONSTRAINT `ConfigsUsers_ibfk_1` FOREIGN KEY (`userID`) REFERENCES `Users` (`id`),
  ADD CONSTRAINT `ConfigsUsers_ibfk_2` FOREIGN KEY (`configID`) REFERENCES `Configs` (`id`);

--
-- Omezení pro tabulku `DestF`
--
ALTER TABLE `DestF`
  ADD CONSTRAINT `DestF_ibfk_1` FOREIGN KEY (`configID`) REFERENCES `Configs` (`id`);

--
-- Omezení pro tabulku `Logs`
--
ALTER TABLE `Logs`
  ADD CONSTRAINT `Logs_ibfk_1` FOREIGN KEY (`userID`) REFERENCES `Users` (`id`),
  ADD CONSTRAINT `Logs_ibfk_2` FOREIGN KEY (`configID`) REFERENCES `Configs` (`id`);

--
-- Omezení pro tabulku `SourceF`
--
ALTER TABLE `SourceF`
  ADD CONSTRAINT `SourceF_ibfk_1` FOREIGN KEY (`configID`) REFERENCES `Configs` (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
