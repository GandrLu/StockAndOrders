-- phpMyAdmin SQL Dump
-- version 4.7.4
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Erstellungszeit: 02. Okt 2020 um 18:24
-- Server-Version: 10.1.28-MariaDB
-- PHP-Version: 7.1.10

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Datenbank: `stockandorders`
--
CREATE DATABASE IF NOT EXISTS `stockandorders` DEFAULT CHARACTER SET latin1 COLLATE latin1_swedish_ci;
USE `stockandorders`;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `addresses`
--

CREATE TABLE `addresses` (
  `ID` int(11) NOT NULL,
  `Street` varchar(100) NOT NULL,
  `Housenumber` varchar(50) NOT NULL,
  `PostalCode` varchar(5) NOT NULL,
  `City` varchar(50) NOT NULL,
  `CreatedAt` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `ChangedAt` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Daten für Tabelle `addresses`
--

INSERT INTO `addresses` (`ID`, `Street`, `Housenumber`, `PostalCode`, `City`, `CreatedAt`, `ChangedAt`) VALUES
(1, 'Hauptstraße', '12', '31451', 'Nienburg', '2020-09-24 20:13:45', NULL),
(2, 'Kirchstraße', '52a', '24531', 'Bremen', '2020-09-24 20:13:45', NULL),
(3, 'Nebenstraße', '24', '42131', 'Bonn', '2020-09-24 20:13:45', NULL),
(4, 'Neue Straße', '98', '92734', 'Erfurt', '2020-09-24 20:13:45', NULL),
(5, 'Pushkin-Ring', '112b', '82721', 'Erfurt', '2020-09-24 20:13:45', NULL);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `categories`
--

CREATE TABLE `categories` (
  `ID` int(11) NOT NULL,
  `Name` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Daten für Tabelle `categories`
--

INSERT INTO `categories` (`ID`, `Name`) VALUES
(1, 'Default');

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `customers`
--

CREATE TABLE `customers` (
  `ID` int(11) NOT NULL,
  `Firstname` varchar(100) NOT NULL,
  `Surname` varchar(100) NOT NULL,
  `Address` int(11) NOT NULL,
  `CreatedAt` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `ChangedAt` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Daten für Tabelle `customers`
--

INSERT INTO `customers` (`ID`, `Firstname`, `Surname`, `Address`, `CreatedAt`, `ChangedAt`) VALUES
(1, 'Hans', 'Meyer', 1, '2020-09-24 20:06:22', NULL),
(2, 'Ralf', 'Möller', 2, '2020-09-24 20:15:52', NULL),
(3, 'Peter', 'Mond', 3, '2020-09-24 20:15:52', NULL),
(4, 'Markus', 'Prenz', 4, '2020-09-24 20:15:52', NULL),
(5, 'Karl', 'Kubicki', 5, '2020-09-24 20:15:52', NULL),
(6, 'Max Pascal', 'Meyer', 1, '2020-09-24 20:15:52', NULL);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `items`
--

CREATE TABLE `items` (
  `ID` int(11) NOT NULL,
  `Name` varchar(100) NOT NULL,
  `Description` varchar(150) DEFAULT NULL,
  `Color` varchar(50) DEFAULT NULL,
  `Category` int(11) NOT NULL,
  `Price` float NOT NULL,
  `Quantity` int(11) NOT NULL DEFAULT '1',
  `CreatedAt` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `ChangedAt` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Daten für Tabelle `items`
--

INSERT INTO `items` (`ID`, `Name`, `Description`, `Color`, `Category`, `Price`, `Quantity`, `CreatedAt`, `ChangedAt`) VALUES
(1, 'Tasse', '300 ml', 'Blau', 0, 12, 10, '2020-09-25 19:41:40', NULL),
(2, 'Krug', '500 ml', 'Rot', 0, 25.5, 4, '2020-09-25 19:42:58', NULL);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `orders`
--

CREATE TABLE `orders` (
  `ID` int(11) NOT NULL,
  `Customer` int(11) NOT NULL,
  `ShippingAddress` int(11) NOT NULL,
  `BillingAddress` int(11) DEFAULT NULL,
  `ShoppingCart` int(11) NOT NULL,
  `CreatedAt` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `ChangedAt` timestamp NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Daten für Tabelle `orders`
--

INSERT INTO `orders` (`ID`, `Customer`, `ShippingAddress`, `BillingAddress`, `ShoppingCart`, `CreatedAt`, `ChangedAt`) VALUES
(1, 2, 2, NULL, 1, '2020-09-25 19:51:33', NULL);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `shippingmethods`
--

CREATE TABLE `shippingmethods` (
  `ID` int(11) NOT NULL,
  `Name` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Daten für Tabelle `shippingmethods`
--

INSERT INTO `shippingmethods` (`ID`, `Name`) VALUES
(1, 'DHL'),
(2, 'DHL Express'),
(3, 'Hermes');

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `shoppingcarts`
--

CREATE TABLE `shoppingcarts` (
  `ID` int(11) NOT NULL,
  `Item` int(11) NOT NULL,
  `Order` int(11) NOT NULL,
  `Quantity` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Daten für Tabelle `shoppingcarts`
--

INSERT INTO `shoppingcarts` (`ID`, `Item`, `Order`, `Quantity`) VALUES
(1, 1, 0, 1);

--
-- Indizes der exportierten Tabellen
--

--
-- Indizes für die Tabelle `addresses`
--
ALTER TABLE `addresses`
  ADD PRIMARY KEY (`ID`);

--
-- Indizes für die Tabelle `categories`
--
ALTER TABLE `categories`
  ADD PRIMARY KEY (`ID`);

--
-- Indizes für die Tabelle `customers`
--
ALTER TABLE `customers`
  ADD PRIMARY KEY (`ID`);

--
-- Indizes für die Tabelle `items`
--
ALTER TABLE `items`
  ADD PRIMARY KEY (`ID`);

--
-- Indizes für die Tabelle `orders`
--
ALTER TABLE `orders`
  ADD PRIMARY KEY (`ID`);

--
-- Indizes für die Tabelle `shippingmethods`
--
ALTER TABLE `shippingmethods`
  ADD PRIMARY KEY (`ID`);

--
-- Indizes für die Tabelle `shoppingcarts`
--
ALTER TABLE `shoppingcarts`
  ADD PRIMARY KEY (`ID`);

--
-- AUTO_INCREMENT für exportierte Tabellen
--

--
-- AUTO_INCREMENT für Tabelle `addresses`
--
ALTER TABLE `addresses`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT für Tabelle `categories`
--
ALTER TABLE `categories`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT für Tabelle `customers`
--
ALTER TABLE `customers`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT für Tabelle `items`
--
ALTER TABLE `items`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT für Tabelle `orders`
--
ALTER TABLE `orders`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT für Tabelle `shippingmethods`
--
ALTER TABLE `shippingmethods`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT für Tabelle `shoppingcarts`
--
ALTER TABLE `shoppingcarts`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
