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


-- Dumping structure for table db_intosport.tbl_gebruiker_type
CREATE TABLE IF NOT EXISTS `tbl_gebruiker_type` (
  `Type_ID` int(10) NOT NULL AUTO_INCREMENT,
  `Create_date` datetime NOT NULL,
  `Timestamp_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `Type` varchar(30) NOT NULL,
  PRIMARY KEY (`Type_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;


-- Dumping structure for table db_intosport.tbl_merk
CREATE TABLE IF NOT EXISTS `tbl_merk` (
  `Merk_ID` int(10) NOT NULL AUTO_INCREMENT,
  `Create_date` datetime NOT NULL,
  `Timestamp_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `Merk` varchar(30) NOT NULL,
  `Actief` enum('Y','N') NOT NULL DEFAULT 'Y',
  PRIMARY KEY (`Merk_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8;


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


-- Dumping structure for table db_intosport.tbl_order_status
CREATE TABLE IF NOT EXISTS `tbl_order_status` (
  `Order_status_ID` int(10) NOT NULL AUTO_INCREMENT,
  `Create_date` datetime NOT NULL,
  `Timestamp_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `Status` varchar(30) NOT NULL,
  PRIMARY KEY (`Order_status_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8;


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

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
