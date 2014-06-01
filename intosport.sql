-- --------------------------------------------------------
-- Host                          :127.0.0.1
-- Server versie                 :5.5.20 - MySQL Community Server (GPL)
-- Server OS                     :Win32
-- HeidiSQL Versie               :7.0.0.4288
-- Aangemaakt                    :2013-04-15 13:32:58
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

-- Dumping database structure for db_intosport
CREATE DATABASE IF NOT EXISTS `db_intosport` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `db_intosport`;


-- Dumping structure for table db_intosport.tbl_categorie
CREATE TABLE IF NOT EXISTS `tbl_categorie` (
  `Categorie_ID` int(10) NOT NULL AUTO_INCREMENT,
  `Create_date` datetime NOT NULL,
  `Timestamp_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `Categorie` varchar(30) NOT NULL,
  `Actief` enum('Y','N') NOT NULL DEFAULT 'Y',
  PRIMARY KEY (`Categorie_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;

-- Dumping data for table db_intosport.tbl_categorie: ~7 rows (approximately)
/*!40000 ALTER TABLE `tbl_categorie` DISABLE KEYS */;
INSERT INTO `tbl_categorie` (`Categorie_ID`, `Create_date`, `Timestamp_date`, `Categorie`, `Actief`) VALUES
	(1, '2013-03-22 17:49:12', '2013-04-11 18:12:14', 'Schoenen', 'Y'),
	(2, '2013-03-22 17:49:15', '2013-03-22 17:49:17', 'Voetballen', 'Y'),
	(3, '2013-03-22 17:49:18', '2013-03-22 17:49:19', 'Voetbalschoenen', 'Y'),
	(4, '2013-03-22 17:49:20', '2013-03-22 17:49:21', 'Heren Sportkleding', 'Y'),
	(5, '2013-03-22 17:49:22', '2013-03-22 17:49:23', 'Vrouwen Sportkleding', 'Y'),
	(6, '2013-04-04 11:17:29', '2013-04-04 11:17:29', 'Rackets', 'Y'),
	(7, '2013-04-04 11:47:21', '2013-04-12 13:08:19', 'Helmen', 'Y');
/*!40000 ALTER TABLE `tbl_categorie` ENABLE KEYS */;


-- Dumping structure for table db_intosport.tbl_gebruiker
CREATE TABLE IF NOT EXISTS `tbl_gebruiker` (
  `Gebruiker_ID` int(10) NOT NULL AUTO_INCREMENT,
  `Create_date` datetime NOT NULL,
  `Timestamp_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `Type_ID` int(10) NOT NULL,
  `Gebruikersnaam` varchar(50) NOT NULL,
  `Wachtwoord` char(32) NOT NULL,
  `Adres` varchar(50) NOT NULL,
  `Postcode` char(6) NOT NULL,
  `Woonplaats` varchar(50) NOT NULL,
  `Telefoonnr` char(10) DEFAULT NULL,
  `Email` varchar(80) DEFAULT NULL,
  `Actief` enum('Y','N') DEFAULT 'Y',
  `Goldmembership` tinyint(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`Gebruiker_ID`),
  UNIQUE KEY `Email` (`Email`),
  KEY `FK_tbl_gebruiker_tbl_gebruiker_type` (`Type_ID`),
  CONSTRAINT `FK_tbl_gebruiker_tbl_gebruiker_type` FOREIGN KEY (`Type_ID`) REFERENCES `tbl_gebruiker_type` (`Type_ID`) ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8;

-- Dumping data for table db_intosport.tbl_gebruiker: ~10 rows (approximately)
/*!40000 ALTER TABLE `tbl_gebruiker` DISABLE KEYS */;
INSERT INTO `tbl_gebruiker` (`Gebruiker_ID`, `Create_date`, `Timestamp_date`, `Type_ID`, `Gebruikersnaam`, `Wachtwoord`, `Adres`, `Postcode`, `Woonplaats`, `Telefoonnr`, `Email`, `Actief`, `Goldmembership`) VALUES
	(1, '2013-03-22 17:40:37', '2013-04-15 13:29:11', 1, 'admin', 'abc', 'Dorpsstraat 22', '4345CD', 'Nieuw Genua', '108295622', 'kippenbout@hotmail.com', 'Y', 0),
	(2, '2013-03-22 17:48:10', '2013-04-15 13:31:38', 3, 'Arie', 'qwert', 'Hoofdstraat 101C', '8462HG', 'Amsterdam', '1234567890', 'arie2@outlook.com', 'Y', 0),
	(3, '2013-04-05 10:17:53', '2013-04-12 13:09:36', 3, 'koos', 'cba', 'olmendreef 178', '3249WN', 'ROTTERDAM', '103747833', 'koos@gmail.com', 'Y', 0),
	(4, '2013-04-09 17:25:26', '2013-04-15 13:00:00', 2, 'Niels', '', 'maassluis', '3133ka', 'Vlaardingen', '193203904', 'trol@lol.com', 'Y', 1),
	(5, '2013-04-10 21:09:27', '2013-04-15 10:58:45', 1, 'haha', 'abcd', 'fout', '2222aa', 'dfsd', '0612345678', 'testen@hotmail.com', 'Y', 0),
	(6, '2013-04-10 21:39:37', '2013-04-10 21:39:37', 1, 'beheerder', 'beheer', 'beheer', '1111bb', 'den haag', '612345679', 'beheer@hotmail.com', 'Y', 0),
	(7, '2013-04-10 21:43:01', '2013-04-15 11:36:00', 1, 'aaa  h', 'arie', 'asdf', '1011ss', 'den haag', '612345678', 'arie@outlook.com', 'Y', 0),
	(8, '2013-04-12 13:31:30', '2013-04-15 12:37:10', 1, 'Basman', 'abc', 'Straatlaan 33', '2222GG', 'Den Haag', '1234567890', 'bas.rade@outlook.com', 'Y', 0),
	(9, '2013-04-12 13:58:14', '2013-04-12 13:58:14', 1, 'Gerard', 'asdf', 'Straatlaan 33', '1234EE', 'Amsterdam', '1234567890', 'gerard@hhs.nl', 'Y', 0),
	(10, '2013-04-15 10:56:08', '2013-04-15 10:56:09', 3, 'lol', 'lol', 'lol 33', '2222aa', 'lol', '123', 'lol@lol.com', 'Y', 0);
/*!40000 ALTER TABLE `tbl_gebruiker` ENABLE KEYS */;


-- Dumping structure for table db_intosport.tbl_gebruiker_type
CREATE TABLE IF NOT EXISTS `tbl_gebruiker_type` (
  `Type_ID` int(10) NOT NULL AUTO_INCREMENT,
  `Create_date` datetime NOT NULL,
  `Timestamp_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `Type` varchar(30) NOT NULL,
  PRIMARY KEY (`Type_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

-- Dumping data for table db_intosport.tbl_gebruiker_type: ~3 rows (approximately)
/*!40000 ALTER TABLE `tbl_gebruiker_type` DISABLE KEYS */;
INSERT INTO `tbl_gebruiker_type` (`Type_ID`, `Create_date`, `Timestamp_date`, `Type`) VALUES
	(1, '2013-03-22 17:44:38', '2013-03-22 17:44:40', 'Beheerder'),
	(2, '2013-03-22 17:44:41', '2013-03-22 17:44:43', 'Manager'),
	(3, '2013-03-22 17:44:43', '2013-03-22 17:44:45', 'Klant');
/*!40000 ALTER TABLE `tbl_gebruiker_type` ENABLE KEYS */;


-- Dumping structure for table db_intosport.tbl_merk
CREATE TABLE IF NOT EXISTS `tbl_merk` (
  `Merk_ID` int(10) NOT NULL AUTO_INCREMENT,
  `Create_date` datetime NOT NULL,
  `Timestamp_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `Merk` varchar(30) NOT NULL,
  `Actief` enum('Y','N') NOT NULL DEFAULT 'Y',
  PRIMARY KEY (`Merk_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8;

-- Dumping data for table db_intosport.tbl_merk: ~9 rows (approximately)
/*!40000 ALTER TABLE `tbl_merk` DISABLE KEYS */;
INSERT INTO `tbl_merk` (`Merk_ID`, `Create_date`, `Timestamp_date`, `Merk`, `Actief`) VALUES
	(1, '2013-03-23 21:39:31', '2013-04-11 17:46:36', 'Overig', 'Y'),
	(2, '2013-03-23 21:41:54', '2013-04-12 13:53:55', 'Adidas', 'Y'),
	(3, '2013-03-23 21:42:05', '2013-04-11 17:48:33', 'Puma', 'Y'),
	(4, '2013-03-23 21:43:24', '2013-03-23 21:43:32', 'Nike', 'Y'),
	(5, '2013-03-23 21:43:38', '2013-03-23 21:43:49', 'Derbystar', 'Y'),
	(7, '2013-04-04 11:31:32', '2013-04-11 17:48:03', 'Impact', 'Y'),
	(8, '2013-04-04 11:32:20', '2013-04-04 11:32:20', 'Asics', 'Y'),
	(9, '2013-04-04 11:48:04', '2013-04-12 13:06:19', 'Sparco', 'Y'),
	(10, '2013-04-12 13:54:04', '2013-04-12 13:54:21', 'Rebook', 'N');
/*!40000 ALTER TABLE `tbl_merk` ENABLE KEYS */;


-- Dumping structure for table db_intosport.tbl_order
CREATE TABLE IF NOT EXISTS `tbl_order` (
  `Order_ID` int(10) NOT NULL AUTO_INCREMENT,
  `Create_date` datetime NOT NULL,
  `Timestamp_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `Gebruiker_ID` int(10) NOT NULL,
  `Order_status_ID` int(10) NOT NULL,
  `Totaal` double NOT NULL DEFAULT '0',
  PRIMARY KEY (`Order_ID`),
  KEY `FK_tbl_order_tbl_order_status` (`Order_status_ID`),
  KEY `FK_tbl_order_tbl_gebruiker` (`Gebruiker_ID`),
  KEY `Totaal` (`Totaal`),
  CONSTRAINT `FK_tbl_order_tbl_gebruiker` FOREIGN KEY (`Gebruiker_ID`) REFERENCES `tbl_gebruiker` (`Gebruiker_ID`) ON UPDATE CASCADE,
  CONSTRAINT `FK_tbl_order_tbl_order_status` FOREIGN KEY (`Order_status_ID`) REFERENCES `tbl_order_status` (`Order_status_ID`) ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=4313 DEFAULT CHARSET=utf8;

-- Dumping data for table db_intosport.tbl_order: ~8 rows (approximately)
/*!40000 ALTER TABLE `tbl_order` DISABLE KEYS */;
INSERT INTO `tbl_order` (`Order_ID`, `Create_date`, `Timestamp_date`, `Gebruiker_ID`, `Order_status_ID`, `Totaal`) VALUES
	(4301, '2013-04-05 20:53:25', '2013-04-05 21:47:21', 3, 6, 289.75),
	(4303, '2013-04-05 23:18:09', '2013-04-05 23:18:09', 2, 3, 199.98),
	(4304, '2013-04-09 17:27:35', '2013-04-12 13:53:15', 4, 7, 699.74),
	(4305, '2013-04-09 23:13:00', '2013-04-12 13:29:26', 4, 3, 95.904),
	(4306, '2013-04-10 01:05:43', '2013-04-10 01:05:43', 4, 3, 28.752),
	(4307, '2013-04-10 11:39:36', '2013-04-10 11:39:36', 4, 3, 134.35199999999998),
	(4311, '2013-04-12 09:03:12', '2013-04-12 09:03:12', 3, 3, 76.65599999999999),
	(4312, '2013-04-15 12:42:57', '2013-04-15 12:42:57', 10, 3, 59.95);
/*!40000 ALTER TABLE `tbl_order` ENABLE KEYS */;


-- Dumping structure for table db_intosport.tbl_orderregel
CREATE TABLE IF NOT EXISTS `tbl_orderregel` (
  `Product_ID` int(10) NOT NULL,
  `Order_ID` int(10) NOT NULL,
  `Create_date` datetime NOT NULL,
  `Timestamp_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `Aantal` smallint(6) NOT NULL,
  `Subtotaal` double unsigned NOT NULL,
  PRIMARY KEY (`Product_ID`,`Order_ID`),
  KEY `FK_tbl_orderregel_tbl_order` (`Order_ID`),
  KEY `Aantal` (`Aantal`),
  KEY `Subtotaal` (`Subtotaal`),
  CONSTRAINT `FK_tbl_orderregel_tbl_order` FOREIGN KEY (`Order_ID`) REFERENCES `tbl_order` (`Order_ID`) ON UPDATE CASCADE,
  CONSTRAINT `FK_tbl_orderregel_tbl_product` FOREIGN KEY (`Product_ID`) REFERENCES `tbl_product` (`Product_ID`) ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Dumping data for table db_intosport.tbl_orderregel: ~16 rows (approximately)
/*!40000 ALTER TABLE `tbl_orderregel` DISABLE KEYS */;
INSERT INTO `tbl_orderregel` (`Product_ID`, `Order_ID`, `Create_date`, `Timestamp_date`, `Aantal`, `Subtotaal`) VALUES
	(1, 4304, '2013-04-09 17:27:35', '2013-04-09 17:27:35', 1, 69.95),
	(1, 4305, '2013-04-09 23:13:00', '2013-04-09 23:13:00', 1, 67.152),
	(2, 4304, '2013-04-09 17:27:35', '2013-04-09 17:27:35', 1, 59.95),
	(2, 4312, '2013-04-15 12:42:57', '2013-04-15 12:42:57', 1, 59.95),
	(3, 4305, '2013-04-09 23:13:00', '2013-04-09 23:13:00', 1, 28.752),
	(3, 4306, '2013-04-10 01:05:43', '2013-04-10 01:05:43', 1, 28.752),
	(6, 4301, '2013-04-05 20:53:25', '2013-04-05 20:53:25', 1, 29.95),
	(6, 4311, '2013-04-12 09:03:12', '2013-04-12 09:03:12', 1, 28.752),
	(8, 4311, '2013-04-12 09:03:12', '2013-04-12 09:03:12', 1, 28.752),
	(10, 4304, '2013-04-09 17:27:35', '2013-04-09 17:27:35', 1, 139.95),
	(10, 4307, '2013-04-10 11:39:36', '2013-04-10 11:39:36', 1, 134.35199999999998),
	(11, 4301, '2013-04-05 20:53:25', '2013-04-05 20:53:25', 2, 259.8),
	(11, 4304, '2013-04-09 17:27:35', '2013-04-09 17:27:35', 1, 129.9),
	(20, 4311, '2013-04-12 09:03:12', '2013-04-12 09:03:12', 1, 19.151999999999997),
	(26, 4304, '2013-04-09 17:27:35', '2013-04-09 17:27:35', 1, 299.99),
	(29, 4303, '2013-04-05 23:18:09', '2013-04-05 23:18:09', 2, 199.98);
/*!40000 ALTER TABLE `tbl_orderregel` ENABLE KEYS */;


-- Dumping structure for table db_intosport.tbl_order_status
CREATE TABLE IF NOT EXISTS `tbl_order_status` (
  `Order_status_ID` int(10) NOT NULL AUTO_INCREMENT,
  `Create_date` datetime NOT NULL,
  `Timestamp_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `Status` varchar(30) NOT NULL,
  PRIMARY KEY (`Order_status_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8;

-- Dumping data for table db_intosport.tbl_order_status: ~6 rows (approximately)
/*!40000 ALTER TABLE `tbl_order_status` DISABLE KEYS */;
INSERT INTO `tbl_order_status` (`Order_status_ID`, `Create_date`, `Timestamp_date`, `Status`) VALUES
	(3, '2013-03-22 18:03:13', '2013-03-22 18:07:15', 'Bevestigd'),
	(4, '2013-03-22 18:03:01', '2013-04-12 13:28:29', 'Geannuleerd'),
	(6, '2013-03-22 17:54:39', '2013-04-12 13:28:34', 'Verzonden'),
	(7, '2013-04-12 13:28:38', '2013-04-12 13:28:39', 'In behandeling'),
	(8, '2013-04-12 13:28:49', '2013-04-12 13:28:50', 'Uitgesteld'),
	(9, '2013-04-12 13:28:59', '2013-04-12 13:29:10', 'Betaling mislukt');
/*!40000 ALTER TABLE `tbl_order_status` ENABLE KEYS */;


-- Dumping structure for table db_intosport.tbl_product
CREATE TABLE IF NOT EXISTS `tbl_product` (
  `Product_ID` int(10) NOT NULL AUTO_INCREMENT,
  `Create_date` datetime NOT NULL,
  `Timestamp_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `Categorie_ID` int(10) NOT NULL,
  `Merk_ID` int(10) NOT NULL,
  `Productnaam` varchar(50) NOT NULL,
  `Prijs` double unsigned NOT NULL DEFAULT '0',
  `Voorraad` smallint(5) NOT NULL DEFAULT '0',
  `Beschrijving` varchar(255) DEFAULT NULL,
  `Actief` enum('Y','N') DEFAULT 'Y',
  `Image` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`Product_ID`),
  UNIQUE KEY `Productnaam` (`Productnaam`),
  KEY `Prijs` (`Prijs`),
  KEY `Voorraad` (`Voorraad`),
  KEY `FK_tbl_product_tbl_merk` (`Merk_ID`),
  KEY `FK_tbl_product_tbl_categorie` (`Categorie_ID`),
  CONSTRAINT `FK_tbl_product_tbl_categorie` FOREIGN KEY (`Categorie_ID`) REFERENCES `tbl_categorie` (`Categorie_ID`) ON UPDATE CASCADE,
  CONSTRAINT `FK_tbl_product_tbl_merk` FOREIGN KEY (`Merk_ID`) REFERENCES `tbl_merk` (`Merk_ID`) ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=33 DEFAULT CHARSET=utf8;

-- Dumping data for table db_intosport.tbl_product: ~23 rows (approximately)
/*!40000 ALTER TABLE `tbl_product` DISABLE KEYS */;
INSERT INTO `tbl_product` (`Product_ID`, `Create_date`, `Timestamp_date`, `Categorie_ID`, `Merk_ID`, `Productnaam`, `Prijs`, `Voorraad`, `Beschrijving`, `Actief`, `Image`) VALUES
	(1, '2013-03-22 17:49:48', '2013-04-12 14:06:04', 1, 2, 'Adidas Pro Play', 69.99, 8, 'Lopen lekker', 'N', 'adidas_proplay.png'),
	(2, '2013-03-22 17:49:48', '2013-04-11 16:14:01', 1, 3, 'Puma fieldsprint', 59.95, 10, 'Geschikt voor sprinten', 'Y', 'puma_fieldsprint.png'),
	(3, '2013-03-22 17:49:48', '2013-04-11 16:14:00', 2, 4, 'Nike NL prestige', 29.95, 20, 'Licht en stevig', 'Y', 'nike_prestige.png'),
	(4, '2013-03-22 17:49:48', '2013-04-11 17:17:35', 3, 2, 'Adidas F-50', 39.95, 15, 'Kwaliteit', 'Y', 'adidas_f50.png'),
	(5, '2013-03-22 17:49:48', '2013-04-11 16:13:53', 4, 2, 'Adidas t-shirt', 49.45, 12, 'Neemt goed zweet op', 'Y', 'adidas_tshirt.png'),
	(6, '2013-03-22 17:49:48', '2013-04-11 17:17:25', 2, 5, 'Derbystar Voetbal', 29.95, -3, 'Zowel schopbaar als gooibaar', 'Y', 'derbystar_voetbal.png'),
	(8, '2013-03-22 17:49:48', '2013-04-11 17:18:01', 2, 5, 'Derbystar Voetbal2', 29.95, 2, 'Zeer rond', 'Y', 'derbystar_voetbal2.png'),
	(9, '2013-03-22 17:49:48', '2013-04-11 17:08:31', 2, 5, 'Derbystar Voetbal3', 29.95, 0, 'Schop bestending', 'Y', 'derbystar_voetbal3.png'),
	(10, '2013-01-12 00:00:00', '2013-04-11 16:13:56', 1, 4, 'Nike Air Classic', 139.95, 5, 'Vooral lucht', 'Y', 'nike_air_classic.png'),
	(11, '2013-04-04 12:30:14', '2013-04-11 16:13:56', 3, 2, 'Adidas F-30', 129.9, 12, 'je kan er mee voetballen', 'Y', 'adidas_f30.png'),
	(12, '2013-04-04 12:30:18', '2013-04-11 16:13:57', 1, 4, 'Nike Lunarfly', 110, 23, 'Voelt alsof je op de maan loopt, zonder dood te gaan aan zuurstofgebrek', 'Y', 'nike_lunarfly.png'),
	(14, '2013-04-04 12:30:24', '2013-04-11 16:13:46', 5, 4, 'Nike Sport BH', 15.99, 40, 'Geschikt voor sportende vrouwen en dikke mannen', 'Y', 'nike_sport_bh.png'),
	(17, '2013-04-04 12:30:26', '2013-04-11 16:13:45', 5, 2, 'Adidas Tennisjurk', 49.95, 2, 'Krijs net zo hard als de toptennisters met deze jurk!', 'Y', 'adidas_tennisjurk.png'),
	(18, '2013-04-04 12:30:30', '2013-04-04 12:30:31', 5, 2, 'Adidas badpak', 19.95, 1, 'Werkt door zijn drijfvermogen tegen verdrinkingsverschijnselen', 'Y', 'adidas_badpak.png'),
	(20, '2013-03-28 13:50:33', '2013-03-28 13:51:27', 5, 3, 'Puma trainingsbroek', 19.95, 82, 'Verberg die dikke dijen met een brede broek!', 'Y', 'puma_trainingsbroek.png'),
	(22, '2013-03-28 13:57:03', '2013-04-11 16:10:08', 4, 1, 'Heren badpak', 99.95, 0, 'Special ontworpen voor maximale hydrodynamica!', 'Y', 'heren_zwempak.png'),
	(23, '2013-03-28 14:05:02', '2013-03-28 14:05:01', 4, 1, 'Hockey masker', 9.95, 33, 'Machete niet meegeleverd', 'Y', 'hockeymasker.png'),
	(25, '2013-03-28 14:16:45', '2013-03-28 14:16:46', 4, 3, 'Puma sokken', 8.95, 200, 'Alleen geschikt voor voeten', 'Y', 'puma_sokken.png'),
	(26, '2013-04-04 11:30:38', '2013-04-05 11:51:21', 6, 7, 'Impact tennisracket', 299.99, 12, 'Dit racket slaat beter dan je blote handen.', 'Y', 'impact_tennisracket.png'),
	(28, '2013-04-04 11:37:57', '2013-04-04 11:39:03', 6, 8, 'Asics badmintonracket', 19.99, 2, 'Alleen met deze kan je wereldkampioen worden!', 'Y', 'asics_badmintonracket.png'),
	(29, '2013-04-04 11:48:19', '2013-04-11 17:07:12', 7, 9, 'Sparco race helm', 99.99, 1, 'Houd je hoofd veilig als je verongelukt of als je iemand een kopstoot geeft.', 'Y', 'sparco_helm.png'),
	(30, '2013-04-04 12:15:37', '2013-04-07 22:15:10', 7, 2, 'Adidas fiets helm', 59.95, 22, 'Met deze helm op kan je veilig mountainbiken.', 'Y', 'adidas_fietshelm.png'),
	(32, '2013-04-12 14:03:43', '2013-04-12 14:04:09', 3, 4, 'Schoen', 59.99, 12, 'Schoenen voor om je voeten.', 'Y', 'schoen.png');
/*!40000 ALTER TABLE `tbl_product` ENABLE KEYS */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
