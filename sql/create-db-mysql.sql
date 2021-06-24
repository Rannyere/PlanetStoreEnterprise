-- MySQL dump 10.13  Distrib 8.0.22, for macos10.15 (x86_64)
--
-- Host: localhost    Database: PlanetStoreEnterpriseDB
-- ------------------------------------------------------
-- Server version	8.0.22

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `__EFMigrationsHistory`
--

DROP TABLE IF EXISTS `__EFMigrationsHistory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `__EFMigrationsHistory` (
  `MigrationId` varchar(95) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `__EFMigrationsHistory`
--

LOCK TABLES `__EFMigrationsHistory` WRITE;
/*!40000 ALTER TABLE `__EFMigrationsHistory` DISABLE KEYS */;
INSERT INTO `__EFMigrationsHistory` VALUES ('20210107102009_Initial_Data','3.1.10'),('20210120074657_initial_data_Catalog','3.1.10'),('20210125082913_initial_data_Clients','3.1.10'),('20210126104243_initial_data_Clients','3.1.10'),('20210126105322_initial_data_Catalog','3.1.10'),('20210216114347_initial_data_Cart','3.1.10'),('20210303075915_Voucher','3.1.10'),('20210303082354_Voucher','3.1.10'),('20210309095837_initial_data_Cart','3.1.10'),('20210310150651_Orders','3.1.10'),('20210310151852_Orders','3.1.10'),('20210312101757_CascadaDelete','3.1.10'),('20210429182410_PaymentsInfo','3.1.10'),('20210430151701_PaymentsInfo','3.1.10'),('20210513070956_SecKeys','3.1.10'),('20210514054826_RefreshToken','3.1.10');
/*!40000 ALTER TABLE `__EFMigrationsHistory` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Addresses`
--

DROP TABLE IF EXISTS `Addresses`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Addresses` (
  `Id` char(36) NOT NULL,
  `Street` varchar(200) NOT NULL,
  `Number` varchar(50) NOT NULL,
  `Complement` varchar(250) DEFAULT NULL,
  `ZipCode` varchar(20) NOT NULL,
  `Neighborhood` varchar(100) NOT NULL,
  `City` varchar(100) NOT NULL,
  `State` varchar(50) NOT NULL,
  `CustomerId` char(36) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Addresses_CustomerId` (`CustomerId`),
  CONSTRAINT `FK_Addresses_Customers_CustomerId` FOREIGN KEY (`CustomerId`) REFERENCES `Customers` (`Id`) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Addresses`
--

LOCK TABLES `Addresses` WRITE;
/*!40000 ALTER TABLE `Addresses` DISABLE KEYS */;
INSERT INTO `Addresses` VALUES ('c02b89fe-7be4-4d0e-b7de-a075027b4d54','Paracelssustrasse, Lopes/Almeida','44',NULL,'49065780','Industrial','Bad Friedrichshall','Baden-Württemberg','a4e36e15-6166-48b8-b53a-85932fc64068'),('f7abc888-8a7a-4166-9bca-aa65a3410923','Avenida Escritor Graciliano Ramos, 20','20','Netto supermarkt','49095-650','Jabotiana','Aracaju','Sergipe','594c57de-7068-45da-aa5c-7c8c91b1447b');
/*!40000 ALTER TABLE `Addresses` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `AspNetRoleClaims`
--

DROP TABLE IF EXISTS `AspNetRoleClaims`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `AspNetRoleClaims` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `RoleId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ClaimType` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `ClaimValue` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`),
  KEY `IX_AspNetRoleClaims_RoleId` (`RoleId`),
  CONSTRAINT `FK_AspNetRoleClaims_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `AspNetRoleClaims`
--

LOCK TABLES `AspNetRoleClaims` WRITE;
/*!40000 ALTER TABLE `AspNetRoleClaims` DISABLE KEYS */;
/*!40000 ALTER TABLE `AspNetRoleClaims` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `AspNetRoles`
--

DROP TABLE IF EXISTS `AspNetRoles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `AspNetRoles` (
  `Id` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Name` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `NormalizedName` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `ConcurrencyStamp` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `RoleNameIndex` (`NormalizedName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `AspNetRoles`
--

LOCK TABLES `AspNetRoles` WRITE;
/*!40000 ALTER TABLE `AspNetRoles` DISABLE KEYS */;
/*!40000 ALTER TABLE `AspNetRoles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `AspNetUserClaims`
--

DROP TABLE IF EXISTS `AspNetUserClaims`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `AspNetUserClaims` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ClaimType` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `ClaimValue` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`),
  KEY `IX_AspNetUserClaims_UserId` (`UserId`),
  CONSTRAINT `FK_AspNetUserClaims_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `AspNetUserClaims`
--

LOCK TABLES `AspNetUserClaims` WRITE;
/*!40000 ALTER TABLE `AspNetUserClaims` DISABLE KEYS */;
/*!40000 ALTER TABLE `AspNetUserClaims` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `AspNetUserLogins`
--

DROP TABLE IF EXISTS `AspNetUserLogins`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `AspNetUserLogins` (
  `LoginProvider` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProviderKey` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProviderDisplayName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`LoginProvider`,`ProviderKey`),
  KEY `IX_AspNetUserLogins_UserId` (`UserId`),
  CONSTRAINT `FK_AspNetUserLogins_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `AspNetUserLogins`
--

LOCK TABLES `AspNetUserLogins` WRITE;
/*!40000 ALTER TABLE `AspNetUserLogins` DISABLE KEYS */;
/*!40000 ALTER TABLE `AspNetUserLogins` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `AspNetUserRoles`
--

DROP TABLE IF EXISTS `AspNetUserRoles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `AspNetUserRoles` (
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `RoleId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`UserId`,`RoleId`),
  KEY `IX_AspNetUserRoles_RoleId` (`RoleId`),
  CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_AspNetUserRoles_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `AspNetUserRoles`
--

LOCK TABLES `AspNetUserRoles` WRITE;
/*!40000 ALTER TABLE `AspNetUserRoles` DISABLE KEYS */;
/*!40000 ALTER TABLE `AspNetUserRoles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `AspNetUsers`
--

DROP TABLE IF EXISTS `AspNetUsers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `AspNetUsers` (
  `Id` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `UserName` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `NormalizedUserName` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Email` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `NormalizedEmail` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `EmailConfirmed` tinyint(1) NOT NULL,
  `PasswordHash` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `SecurityStamp` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `ConcurrencyStamp` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `PhoneNumber` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `PhoneNumberConfirmed` tinyint(1) NOT NULL,
  `TwoFactorEnabled` tinyint(1) NOT NULL,
  `LockoutEnd` datetime(6) DEFAULT NULL,
  `LockoutEnabled` tinyint(1) NOT NULL,
  `AccessFailedCount` int NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `UserNameIndex` (`NormalizedUserName`),
  KEY `EmailIndex` (`NormalizedEmail`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `AspNetUsers`
--

LOCK TABLES `AspNetUsers` WRITE;
/*!40000 ALTER TABLE `AspNetUsers` DISABLE KEYS */;
INSERT INTO `AspNetUsers` VALUES ('594c57de-7068-45da-aa5c-7c8c91b1447b','joao@teste.com','JOAO@TESTE.COM','joao@teste.com','JOAO@TESTE.COM',1,'AQAAAAEAACcQAAAAEMWqefkFWV3WbqNRHuI6YAOnO1PT4Zh0vmTLESQI6dTQo1JpNyBXgOT2FtKasXVTWQ==','SIX6I4TWLTZ4BSV4FKM2RJOYJLJKNRMQ','b4ba9ac3-099e-4a99-9969-c339554137d5',NULL,0,0,NULL,1,0),('740a5bfa-0b2d-4db1-b18d-dcd84071f3a3','jhully@teste.com','JHULLY@TESTE.COM','jhully@teste.com','JHULLY@TESTE.COM',1,'AQAAAAEAACcQAAAAEOonDFk8QZXgspmXYVdeBQE3BdJ5dSHeU5bktg2x+E+xqY1AV2P2Bp5DaMSVvsGnKA==','VJ5UMICJ2WLVAA5MZLVT7KMJUPHZKT43','9d6026d4-9559-47d7-8da5-3f5b4b550a9c',NULL,0,0,NULL,1,0),('a4e36e15-6166-48b8-b53a-85932fc64068','ranny@teste.com','RANNY@TESTE.COM','ranny@teste.com','RANNY@TESTE.COM',1,'AQAAAAEAACcQAAAAEIumR/3+fPhZZdHl3+50QCAO3/Ak58TNVUdtc12rtl0z2DDyp8LZPgYxUJU6gjWATw==','WKS26R235X6JNNN6GUTT7J3F32T4OPBN','fabe7dc2-36db-4dbc-894f-52a827cf2a0c',NULL,0,0,NULL,1,0);
/*!40000 ALTER TABLE `AspNetUsers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `AspNetUserTokens`
--

DROP TABLE IF EXISTS `AspNetUserTokens`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `AspNetUserTokens` (
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `LoginProvider` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Name` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Value` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`UserId`,`LoginProvider`,`Name`),
  CONSTRAINT `FK_AspNetUserTokens_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `AspNetUsers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `AspNetUserTokens`
--

LOCK TABLES `AspNetUserTokens` WRITE;
/*!40000 ALTER TABLE `AspNetUserTokens` DISABLE KEYS */;
/*!40000 ALTER TABLE `AspNetUserTokens` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `CartCustomers`
--

DROP TABLE IF EXISTS `CartCustomers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `CartCustomers` (
  `Id` char(36) NOT NULL,
  `CustomerId` char(36) NOT NULL,
  `TotalValue` decimal(65,30) NOT NULL,
  `VoucherUsage` tinyint(1) NOT NULL,
  `Discount` decimal(65,30) NOT NULL,
  `VoucherCode` varchar(50) DEFAULT NULL,
  `DiscountPercentage` decimal(65,30) DEFAULT NULL,
  `DiscountValue` decimal(65,30) DEFAULT NULL,
  `DiscountType` int DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IDX_Customer` (`CustomerId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `CartCustomers`
--

LOCK TABLES `CartCustomers` WRITE;
/*!40000 ALTER TABLE `CartCustomers` DISABLE KEYS */;
INSERT INTO `CartCustomers` VALUES ('25eb33df-e94b-48f0-be6b-afc668359dc8','589f2641-9709-44fb-8a4f-cabe1b029213',75.000000000000000000000000000000,0,0.000000000000000000000000000000,NULL,NULL,NULL,NULL),('91cf115b-5785-4ad0-b935-98bea3f6b4fb','b6f21798-0410-458e-a982-6f43a2804a7b',380.000000000000000000000000000000,1,100.000000000000000000000000000000,'100-OFF',NULL,100.000000000000000000000000000000,1),('eb200ea0-6dc5-4ced-9ab5-c8675f4e1596','844fca2d-324c-4edb-afe0-e87e8f92b0e8',225.000000000000000000000000000000,1,225.000000000000000000000000000000,'50-OFF',50.000000000000000000000000000000,NULL,0),('fd57e8c8-df07-41d2-8059-bee2b226b459','8192ca46-bb81-41c7-b57d-c5d7b8ba2d27',75.000000000000000000000000000000,0,0.000000000000000000000000000000,NULL,NULL,NULL,NULL);
/*!40000 ALTER TABLE `CartCustomers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `CartItems`
--

DROP TABLE IF EXISTS `CartItems`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `CartItems` (
  `Id` char(36) NOT NULL,
  `ProductId` char(36) NOT NULL,
  `Name` varchar(100) DEFAULT NULL,
  `Quantity` int NOT NULL,
  `Value` decimal(65,30) NOT NULL,
  `Image` varchar(100) DEFAULT NULL,
  `CartId` char(36) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_CartItems_CartId` (`CartId`),
  CONSTRAINT `FK_CartItems_CartCustomers_CartId` FOREIGN KEY (`CartId`) REFERENCES `CartCustomers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `CartItems`
--

LOCK TABLES `CartItems` WRITE;
/*!40000 ALTER TABLE `CartItems` DISABLE KEYS */;
INSERT INTO `CartItems` VALUES ('2a6e5e72-8fb4-40ff-98d7-82bb65721406','6ecaaa6b-ad9f-422c-b3bb-6013ec27c4bb','Camiseta Code Life Cinza',1,80.000000000000000000000000000000,'camiseta3.jpg','91cf115b-5785-4ad0-b935-98bea3f6b4fb'),('47e5ff0d-76bd-4ea9-972f-7b8a1e36f966','6ecaaa6b-ad9f-422c-b3bb-6013ec27b4bb','Camiseta Debugar Preta',1,75.000000000000000000000000000000,'camiseta4.jpg','25eb33df-e94b-48f0-be6b-afc668359dc8'),('5cdef793-1274-43ed-999e-056aebfa5ec6','6ecaaa6b-ad9f-422c-b3bb-6013ec27b4bb','Camiseta Debugar Preta',1,75.000000000000000000000000000000,'camiseta4.jpg','fd57e8c8-df07-41d2-8059-bee2b226b459'),('7364ad91-f72e-4cd6-8594-c2564cf5f033','fc184e11-014c-4978-aa10-9eb5e1af369b','Camiseta Software Dev',4,100.000000000000000000000000000000,'camiseta1.jpg','91cf115b-5785-4ad0-b935-98bea3f6b4fb'),('ce81cea7-870d-4f96-8d2f-609943ce37c9','7d67df76-2d4e-4a47-a19c-08eb80a9060b','Camiseta Code Life Preta',5,90.000000000000000000000000000000,'camiseta2.jpg','eb200ea0-6dc5-4ced-9ab5-c8675f4e1596');
/*!40000 ALTER TABLE `CartItems` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Customers`
--

DROP TABLE IF EXISTS `Customers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Customers` (
  `Id` char(36) NOT NULL,
  `Name` varchar(150) NOT NULL,
  `Email` varchar(254) DEFAULT NULL,
  `Cpf` varchar(11) DEFAULT NULL,
  `Removed` tinyint(1) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Customers`
--

LOCK TABLES `Customers` WRITE;
/*!40000 ALTER TABLE `Customers` DISABLE KEYS */;
INSERT INTO `Customers` VALUES ('594c57de-7068-45da-aa5c-7c8c91b1447b','Joao Santos','joao@teste.com','69279413040',0),('740a5bfa-0b2d-4db1-b18d-dcd84071f3a3','Jhully Stefany Lopes Almeida','jhully@teste.com','83118605057',0),('a4e36e15-6166-48b8-b53a-85932fc64068','Rannyere Almeida Lima','ranny@teste.com','06407655056',0);
/*!40000 ALTER TABLE `Customers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `OrderItems`
--

DROP TABLE IF EXISTS `OrderItems`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `OrderItems` (
  `Id` char(36) NOT NULL,
  `OrderId` char(36) NOT NULL,
  `ProductId` char(36) NOT NULL,
  `Name` varchar(250) NOT NULL,
  `Quantity` int NOT NULL,
  `Value` decimal(65,30) NOT NULL,
  `Image` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_OrderItems_OrderId` (`OrderId`),
  CONSTRAINT `FK_OrderItems_Orders_OrderId` FOREIGN KEY (`OrderId`) REFERENCES `Orders` (`Id`) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `OrderItems`
--

LOCK TABLES `OrderItems` WRITE;
/*!40000 ALTER TABLE `OrderItems` DISABLE KEYS */;
INSERT INTO `OrderItems` VALUES ('045d529a-82cb-4287-a4b2-81ba32cb531e','9b9c6565-4373-4404-9185-08381cd57c0a','7d67df76-2d4e-4a47-a19c-08eb80a9060b','Camiseta Code Life Preta',1,95.000000000000000000000000000000,'camiseta2.jpg'),('0ad3e5f8-1c08-4d9e-9554-4223670dead5','7d2fd967-fad0-4a33-9939-3187a12cd662','6ecaaa6b-ad9f-422c-b3bb-6013ec27b4bb','Camiseta Debugar Preta',1,75.000000000000000000000000000000,'camiseta4.jpg'),('125478d3-b3ce-49c2-bff3-96260c5bbc09','dc0647fd-7044-42f8-89f5-fe3d84104693','4848f0aa-5200-4206-8f1e-6a3c85cdb8f8','Caneca Batman',1,20.000000000000000000000000000000,'caneca-Batman.jpg'),('145114f0-c54b-4823-a188-df3bb2fd1b56','482aa7f3-58bf-4831-9598-ed97c672ea95','6ecaaa6b-ad9f-422c-b3bb-6013ec27c4bb','Camiseta Code Life Cinza',1,99.000000000000000000000000000000,'camiseta3.jpg'),('1a8e4ead-2823-4898-a42b-0ab668e81864','1c253ad9-66c3-44f6-8d9a-ad471dcd6cfc','6ecaaa6b-ad9f-422c-b3bb-6013ec27b4bb','Camiseta Debugar Preta',1,75.000000000000000000000000000000,'camiseta4.jpg'),('21d703cf-dc4f-4d79-b7b5-f594f6c3bc3c','1c253ad9-66c3-44f6-8d9a-ad471dcd6cfc','4848f0aa-5200-4206-8f1e-6a3c85cdb8f8','Caneca Batman',1,20.000000000000000000000000000000,'caneca-Batman.jpg'),('2ff229e7-14b1-4e65-8174-ad1f034b6acc','b553f730-76f2-4f2b-bae6-37346476924e','6ecaaa6b-ad9f-422c-b3bb-6013ec27b4bb','Camiseta Debugar Preta',1,75.000000000000000000000000000000,'camiseta4.jpg'),('3f705b30-253b-4ad1-94fc-d85d075747bb','482aa7f3-58bf-4831-9598-ed97c672ea95','7d67df76-2d4e-4a47-a19c-08eb80a9060b','Camiseta Code Life Preta',1,95.000000000000000000000000000000,'camiseta2.jpg'),('4aae04e3-eb6c-4f70-81a5-cf0338b9645e','dc0647fd-7044-42f8-89f5-fe3d84104693','7d67df76-2d4e-4a47-a19c-08eb80a9060b','Camiseta Code Life Preta',1,95.000000000000000000000000000000,'camiseta2.jpg'),('50693bf3-6046-4b71-804d-9480eca405fa','c0fda0b8-4230-4636-bcf7-3a9eeec330c7','7d67df76-2d4e-4a47-a19c-08eb80a9060b','Camiseta Code Life Preta',1,95.000000000000000000000000000000,'camiseta2.jpg'),('6d5c4117-e200-457e-9543-a664588602fb','010a273a-2626-4eb9-a89c-38169b453e33','79001b47-bea5-42d2-8bd7-2f2ec9231872','Camiseta Heisenberg',1,100.000000000000000000000000000000,'Heisenberg.webp'),('6e7ea64a-41d1-4a4b-a9bd-5fc783f8b1a0','bff82b52-7d7a-4bee-a2a6-4a86c7cf48bd','79001b47-bea5-42d2-8bd7-2f2ec9231872','Camiseta Heisenberg',1,100.000000000000000000000000000000,'Heisenberg.webp'),('6f5440ae-73f4-4961-a409-dee7fd2c64ae','99ca674d-7257-47ed-af8a-22bf5111ca85','7d67df76-2d4e-4a47-a19c-08eb80a9060b','Camiseta Code Life Preta',1,95.000000000000000000000000000000,'camiseta2.jpg'),('7fbca806-1076-4f36-89f3-fe5ce88d3444','44b83a76-3668-482d-9e0b-cf88dc5fa8d2','7d67df76-2d4e-4a47-a19c-08eb80a9060b','Camiseta Code Life Preta',1,95.000000000000000000000000000000,'camiseta2.jpg'),('8079fd44-d840-4fcf-b183-27e0a6b043a1','dc0647fd-7044-42f8-89f5-fe3d84104693','6ecaaa6b-ad9f-422c-b3bb-6013ec27b4bb','Camiseta Debugar Preta',1,75.000000000000000000000000000000,'camiseta4.jpg'),('8e42ee69-4750-4871-a918-222a80390a8d','05c6df90-0bc0-4a73-af19-432bab7a1b16','7d67df76-2d4e-4a47-a19c-08eb80a9060b','Camiseta Code Life Preta',1,95.000000000000000000000000000000,'camiseta2.jpg'),('8e85b9d2-d122-470a-8716-e59a1b5c2d30','1c253ad9-66c3-44f6-8d9a-ad471dcd6cfc','7d67df76-2d4e-4a47-a19c-08eb80a9060b','Camiseta Code Life Preta',1,95.000000000000000000000000000000,'camiseta2.jpg'),('8faf0d36-b85b-40b8-b3c6-e718cab11c98','33d778bf-65ee-4a8c-a1c4-acaf8aa87605','6ecaaa6b-ad9f-422c-b3bb-6013ec27b4bb','Camiseta Debugar Preta',1,75.000000000000000000000000000000,'camiseta4.jpg'),('9e307ba6-8121-4624-b924-30af5973481a','462ed9f6-842c-4c6e-bc3e-ae2f42eb73e5','6ecaaa6b-ad9f-422c-b3bb-6013ec27b4bb','Camiseta Debugar Preta',1,75.000000000000000000000000000000,'camiseta4.jpg'),('a121b1ad-3055-4e77-b244-6314e8230333','f6d3b2b6-83c7-4685-82ab-a7dcd81fb77b','7d67df76-2d4e-4a47-a19c-08eb80a9060b','Camiseta Code Life Preta',1,95.000000000000000000000000000000,'camiseta2.jpg'),('a5336d4a-f144-4309-8c37-39aa7fba6711','99ca674d-7257-47ed-af8a-22bf5111ca85','20e08cd4-2402-4e76-a3c9-a026185b193d','Caneca Turn Coffee in Code',1,20.000000000000000000000000000000,'caneca3.jpg'),('b4cdbfc8-af52-4591-950d-23e13ee2e155','e49617c6-3d8a-4d5d-9228-46a7e7f4dcb3','7d67df76-2d4e-4a47-a19c-08eb80a9060b','Camiseta Code Life Preta',1,95.000000000000000000000000000000,'camiseta2.jpg'),('ba782ffd-0830-4146-a941-385f48fae86b','f987a74e-6a75-4e83-ad95-715fa67328ce','7d67df76-2d4e-4a47-a19c-08eb80a9060b','Camiseta Code Life Preta',1,95.000000000000000000000000000000,'camiseta2.jpg'),('cfbdbb34-9cac-42d4-823b-0891ef45b4d7','c2a4fa50-13cc-475e-aae0-9e0214b28fd0','6ecaaa6b-ad9f-422c-b3bb-6013ec27b4bb','Camiseta Debugar Preta',1,75.000000000000000000000000000000,'camiseta4.jpg'),('d345c5b8-9a81-47eb-a0d2-dce802fd927d','064e82aa-2530-417d-9b7e-3690388a4998','7d67df76-2d4e-4a47-a19c-08eb80a9060b','Camiseta Code Life Preta',1,95.000000000000000000000000000000,'camiseta2.jpg'),('e145b106-c8d1-40c3-b874-aca38ce28899','e8812ff5-0df8-4b25-8eac-958344a52898','6ecaaa6b-ad9f-422c-b3bb-6013ec27b4bb','Camiseta Debugar Preta',1,75.000000000000000000000000000000,'camiseta4.jpg');
/*!40000 ALTER TABLE `OrderItems` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Orders`
--

DROP TABLE IF EXISTS `Orders`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Orders` (
  `Id` char(36) NOT NULL,
  `Code` int unsigned NOT NULL AUTO_INCREMENT,
  `CustomerId` char(36) NOT NULL,
  `VoucherId` char(36) DEFAULT NULL,
  `VoucherUsage` tinyint(1) NOT NULL,
  `Discount` decimal(65,30) NOT NULL,
  `TotalValue` decimal(65,30) NOT NULL,
  `DateRegister` datetime(6) NOT NULL,
  `OrderStatus` int NOT NULL,
  `Street` varchar(100) DEFAULT NULL,
  `Number` varchar(100) DEFAULT NULL,
  `Complement` varchar(100) DEFAULT NULL,
  `ZipCode` varchar(100) DEFAULT NULL,
  `Neighborhood` varchar(100) DEFAULT NULL,
  `City` varchar(100) DEFAULT NULL,
  `State` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Code_UNIQUE` (`Code`),
  KEY `IX_Orders_VoucherId` (`VoucherId`),
  CONSTRAINT `FK_Orders_Vouchers_VoucherId` FOREIGN KEY (`VoucherId`) REFERENCES `Vouchers` (`Id`) ON DELETE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=69 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Orders`
--

LOCK TABLES `Orders` WRITE;
/*!40000 ALTER TABLE `Orders` DISABLE KEYS */;
INSERT INTO `Orders` VALUES ('010a273a-2626-4eb9-a89c-38169b453e33',59,'594c57de-7068-45da-aa5c-7c8c91b1447b',NULL,0,0.000000000000000000000000000000,100.000000000000000000000000000000,'2021-06-24 09:09:03.999388',2,'Avenida Escritor Graciliano Ramos, 20','20','Netto supermarkt','49095-650','Jabotiana','Aracaju','Sergipe'),('05c6df90-0bc0-4a73-af19-432bab7a1b16',63,'594c57de-7068-45da-aa5c-7c8c91b1447b',NULL,0,0.000000000000000000000000000000,95.000000000000000000000000000000,'2021-06-24 11:37:39.213174',2,'Avenida Escritor Graciliano Ramos, 20','20','Netto supermarkt','49095-650','Jabotiana','Aracaju','Sergipe'),('064e82aa-2530-417d-9b7e-3690388a4998',54,'594c57de-7068-45da-aa5c-7c8c91b1447b',NULL,0,0.000000000000000000000000000000,95.000000000000000000000000000000,'2021-06-22 07:31:05.514605',2,'Avenida Escritor Graciliano Ramos, 20','20','Netto supermarkt','49095-650','Jabotiana','Aracaju','Sergipe'),('1c253ad9-66c3-44f6-8d9a-ad471dcd6cfc',56,'594c57de-7068-45da-aa5c-7c8c91b1447b',NULL,0,0.000000000000000000000000000000,190.000000000000000000000000000000,'2021-06-23 09:22:58.400145',2,'Avenida Escritor Graciliano Ramos, 20','20','Netto supermarkt','49095-650','Jabotiana','Aracaju','Sergipe'),('33d778bf-65ee-4a8c-a1c4-acaf8aa87605',61,'a4e36e15-6166-48b8-b53a-85932fc64068',NULL,0,0.000000000000000000000000000000,75.000000000000000000000000000000,'2021-06-24 09:16:18.142781',2,'Paracelssustrasse, Lopes/Almeida','44',NULL,'49065780','Industrial','Bad Friedrichshall','Baden-Württemberg'),('44b83a76-3668-482d-9e0b-cf88dc5fa8d2',64,'594c57de-7068-45da-aa5c-7c8c91b1447b',NULL,0,0.000000000000000000000000000000,95.000000000000000000000000000000,'2021-06-24 11:38:10.013532',2,'Avenida Escritor Graciliano Ramos, 20','20','Netto supermarkt','49095-650','Jabotiana','Aracaju','Sergipe'),('462ed9f6-842c-4c6e-bc3e-ae2f42eb73e5',67,'594c57de-7068-45da-aa5c-7c8c91b1447b',NULL,0,0.000000000000000000000000000000,75.000000000000000000000000000000,'2021-06-24 10:05:38.305188',2,'Avenida Escritor Graciliano Ramos, 20','20','Netto supermarkt','49095-650','Jabotiana','Aracaju','Sergipe'),('482aa7f3-58bf-4831-9598-ed97c672ea95',49,'594c57de-7068-45da-aa5c-7c8c91b1447b',NULL,0,0.000000000000000000000000000000,194.000000000000000000000000000000,'2021-05-12 08:45:50.152422',5,'Avenida Escritor Graciliano Ramos, 20','20','Netto supermarkt','49095-650','Jabotiana','Aracaju','Sergipe'),('7d2fd967-fad0-4a33-9939-3187a12cd662',66,'594c57de-7068-45da-aa5c-7c8c91b1447b',NULL,0,0.000000000000000000000000000000,75.000000000000000000000000000000,'2021-06-24 11:55:12.472488',2,'Avenida Escritor Graciliano Ramos, 20','20','Netto supermarkt','49095-650','Jabotiana','Aracaju','Sergipe'),('99ca674d-7257-47ed-af8a-22bf5111ca85',50,'594c57de-7068-45da-aa5c-7c8c91b1447b',NULL,0,0.000000000000000000000000000000,115.000000000000000000000000000000,'2021-05-12 08:47:34.417488',2,'Avenida Escritor Graciliano Ramos, 20','20','Netto supermarkt','49095-650','Jabotiana','Aracaju','Sergipe'),('9b9c6565-4373-4404-9185-08381cd57c0a',52,'594c57de-7068-45da-aa5c-7c8c91b1447b',NULL,0,0.000000000000000000000000000000,95.000000000000000000000000000000,'2021-06-21 15:00:17.931629',2,'Avenida Escritor Graciliano Ramos, 20','20','Netto supermarkt','49095-650','Jabotiana','Aracaju','Sergipe'),('b553f730-76f2-4f2b-bae6-37346476924e',62,'594c57de-7068-45da-aa5c-7c8c91b1447b',NULL,0,0.000000000000000000000000000000,75.000000000000000000000000000000,'2021-06-24 11:37:14.995006',2,'Avenida Escritor Graciliano Ramos, 20','20','Netto supermarkt','49095-650','Jabotiana','Aracaju','Sergipe'),('bff82b52-7d7a-4bee-a2a6-4a86c7cf48bd',58,'594c57de-7068-45da-aa5c-7c8c91b1447b',NULL,0,0.000000000000000000000000000000,100.000000000000000000000000000000,'2021-06-23 12:23:54.316869',2,'Avenida Escritor Graciliano Ramos, 20','20','Netto supermarkt','49095-650','Jabotiana','Aracaju','Sergipe'),('c0fda0b8-4230-4636-bcf7-3a9eeec330c7',55,'594c57de-7068-45da-aa5c-7c8c91b1447b',NULL,0,0.000000000000000000000000000000,95.000000000000000000000000000000,'2021-06-23 09:13:01.652300',2,'Avenida Escritor Graciliano Ramos, 20','20','Netto supermarkt','49095-650','Jabotiana','Aracaju','Sergipe'),('c2a4fa50-13cc-475e-aae0-9e0214b28fd0',65,'a4e36e15-6166-48b8-b53a-85932fc64068',NULL,0,0.000000000000000000000000000000,75.000000000000000000000000000000,'2021-06-24 09:41:50.472451',2,'Paracelssustrasse, Lopes/Almeida','44',NULL,'49065780','Industrial','Bad Friedrichshall','Baden-Württemberg'),('dc0647fd-7044-42f8-89f5-fe3d84104693',57,'594c57de-7068-45da-aa5c-7c8c91b1447b',NULL,0,0.000000000000000000000000000000,190.000000000000000000000000000000,'2021-06-23 11:55:57.203678',2,'Avenida Escritor Graciliano Ramos, 20','20','Netto supermarkt','49095-650','Jabotiana','Aracaju','Sergipe'),('e49617c6-3d8a-4d5d-9228-46a7e7f4dcb3',53,'594c57de-7068-45da-aa5c-7c8c91b1447b',NULL,0,0.000000000000000000000000000000,95.000000000000000000000000000000,'2021-06-21 17:20:36.280763',2,'Avenida Escritor Graciliano Ramos, 20','20','Netto supermarkt','49095-650','Jabotiana','Aracaju','Sergipe'),('e8812ff5-0df8-4b25-8eac-958344a52898',68,'594c57de-7068-45da-aa5c-7c8c91b1447b',NULL,0,0.000000000000000000000000000000,75.000000000000000000000000000000,'2021-06-24 10:09:07.902244',2,'Avenida Escritor Graciliano Ramos, 20','20','Netto supermarkt','49095-650','Jabotiana','Aracaju','Sergipe'),('f6d3b2b6-83c7-4685-82ab-a7dcd81fb77b',51,'594c57de-7068-45da-aa5c-7c8c91b1447b',NULL,0,0.000000000000000000000000000000,95.000000000000000000000000000000,'2021-06-21 14:56:02.780964',2,'Avenida Escritor Graciliano Ramos, 20','20','Netto supermarkt','49095-650','Jabotiana','Aracaju','Sergipe'),('f987a74e-6a75-4e83-ad95-715fa67328ce',60,'594c57de-7068-45da-aa5c-7c8c91b1447b',NULL,0,0.000000000000000000000000000000,95.000000000000000000000000000000,'2021-06-24 09:14:03.262762',2,'Avenida Escritor Graciliano Ramos, 20','20','Netto supermarkt','49095-650','Jabotiana','Aracaju','Sergipe');
/*!40000 ALTER TABLE `Orders` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Payments`
--

DROP TABLE IF EXISTS `Payments`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Payments` (
  `Id` char(36) NOT NULL,
  `OrderId` char(36) NOT NULL,
  `PaymentMehtod` int NOT NULL,
  `TotalValue` decimal(65,30) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Payments`
--

LOCK TABLES `Payments` WRITE;
/*!40000 ALTER TABLE `Payments` DISABLE KEYS */;
INSERT INTO `Payments` VALUES ('09b101bf-3ed9-4ccf-bd2f-9903385b0679','05c6df90-0bc0-4a73-af19-432bab7a1b16',1,95.000000000000000000000000000000),('0e20755d-8e1a-4b8c-9f92-34d124511625','462ed9f6-842c-4c6e-bc3e-ae2f42eb73e5',1,75.000000000000000000000000000000),('11fa0d3b-aefa-4110-944a-6e9546f5e24d','dc0647fd-7044-42f8-89f5-fe3d84104693',1,190.000000000000000000000000000000),('3dbfffef-6314-44da-adcc-0697280afbfb','482aa7f3-58bf-4831-9598-ed97c672ea95',1,194.000000000000000000000000000000),('4191d677-afac-4aec-bd4a-29c108c973b3','99ca674d-7257-47ed-af8a-22bf5111ca85',1,115.000000000000000000000000000000),('49d47ed1-8d20-439e-a208-05bead92c334','1a81fb99-9768-43bb-9a82-ef4f5d161201',1,290.000000000000000000000000000000),('4be69891-b614-4fa2-bcdc-8ddb8dcfe728','7d2fd967-fad0-4a33-9939-3187a12cd662',1,75.000000000000000000000000000000),('50387115-6403-44fd-9a82-87baa2571989','f6c7a668-c829-42b5-a396-a78efab93fb8',1,224.000000000000000000000000000000),('53c0b43d-882b-469e-b8c7-4baa6c7afd59','c0fda0b8-4230-4636-bcf7-3a9eeec330c7',1,95.000000000000000000000000000000),('56ccf0b9-8d4a-4058-8f56-128248064389','9b9c6565-4373-4404-9185-08381cd57c0a',1,95.000000000000000000000000000000),('5a232c2b-226d-40a8-b38f-0a7229d2bbc8','bff82b52-7d7a-4bee-a2a6-4a86c7cf48bd',1,100.000000000000000000000000000000),('5c97430e-0ea5-4558-916d-cf47dc833051','010a273a-2626-4eb9-a89c-38169b453e33',1,100.000000000000000000000000000000),('6728a646-3a8d-4c62-9883-43f7820e3309','e8812ff5-0df8-4b25-8eac-958344a52898',1,75.000000000000000000000000000000),('745aa948-f3f1-4ba9-8c26-f0bb06addc96','e49617c6-3d8a-4d5d-9228-46a7e7f4dcb3',1,95.000000000000000000000000000000),('752b672f-9705-47cc-a9f1-77de4d2839ca','fd00f2d4-b972-4d8f-874c-87919c9fa19e',1,95.000000000000000000000000000000),('85aa57f3-8ff5-4bc6-826b-3b7479ae006d','e77d9d78-7d53-453b-aad2-f08a7b2a5d22',1,90.000000000000000000000000000000),('90be1f81-56e7-4729-a6f2-51a12ae91bd1','b553f730-76f2-4f2b-bae6-37346476924e',1,75.000000000000000000000000000000),('9acbc137-029b-4753-893a-d6c7e62e0a9a','064e82aa-2530-417d-9b7e-3690388a4998',1,95.000000000000000000000000000000),('9e646116-da9a-4a16-87c2-44e58f8b9ede','33d778bf-65ee-4a8c-a1c4-acaf8aa87605',1,75.000000000000000000000000000000),('9e825baf-7670-4e9c-bd8c-eda2202c5013','7e0b50ab-8f17-4393-a424-330f2b46d897',1,99.000000000000000000000000000000),('b668c5e2-940f-4428-9635-b92bd3c9bd8a','44b83a76-3668-482d-9e0b-cf88dc5fa8d2',1,95.000000000000000000000000000000),('b7bb56b2-5de8-4fe8-8a67-2c7a16ff9878','f987a74e-6a75-4e83-ad95-715fa67328ce',1,95.000000000000000000000000000000),('d18ac588-593c-45a8-8dc1-93f4dbb10b64','1c253ad9-66c3-44f6-8d9a-ad471dcd6cfc',1,190.000000000000000000000000000000),('d6f8420c-fe3a-448e-883a-017623cfae02','c2a4fa50-13cc-475e-aae0-9e0214b28fd0',1,75.000000000000000000000000000000),('e8f3c236-ac57-47d6-9b11-0785de811776','8f53d3dd-3345-4845-b57b-57143419bc69',1,218.000000000000000000000000000000),('f50ef85c-9452-4aef-9852-76dc896daeae','f6d3b2b6-83c7-4685-82ab-a7dcd81fb77b',1,95.000000000000000000000000000000);
/*!40000 ALTER TABLE `Payments` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Products`
--

DROP TABLE IF EXISTS `Products`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Products` (
  `Id` char(36) NOT NULL,
  `Name` varchar(250) NOT NULL,
  `Description` varchar(500) NOT NULL,
  `Activ` tinyint(1) NOT NULL,
  `Value` decimal(65,30) NOT NULL,
  `DateRegister` datetime(6) NOT NULL,
  `Image` varchar(250) NOT NULL,
  `QuantityStock` int NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Products`
--

LOCK TABLES `Products` WRITE;
/*!40000 ALTER TABLE `Products` DISABLE KEYS */;
INSERT INTO `Products` VALUES ('191ddd3e-acd4-4c3b-ae74-8e473993c5da','Caneca Programmer Code','Caneca de porcelana com impressão térmica de alta resistência.',1,15.000000000000000000000000000000,'2019-07-19 00:00:00.000000','caneca2.jpg',50),('20e08cd4-2402-4e76-a3c9-a026185b193d','Caneca Turn Coffee in Code','Caneca de porcelana com impressão térmica de alta resistência.',1,20.000000000000000000000000000000,'2019-07-19 00:00:00.000000','caneca3.jpg',49),('4848f0aa-5200-4206-8f1e-6a3c85cdb8f8','Caneca Batman','Caneca de porcelana com impressão térmica de alta resistência.',1,20.000000000000000000000000000000,'2021-05-10 00:00:00.000000','caneca-Batman.jpg',46),('4cbeafe4-709a-4bcf-92f3-4f2d0216c51d','Camiseta X-Man Prof Xaxier','Camiseta 100% algodão, resistente a lavagens e altas temperaturas.',1,99.000000000000000000000000000000,'2021-05-10 00:00:00.000000','Xaviers-School.webp',20),('52dd696b-0882-4a73-9525-6af55dd416a4','Caneca Star Bugs Coffee','Caneca de porcelana com impressão térmica de alta resistência.',1,20.000000000000000000000000000000,'2019-07-19 00:00:00.000000','caneca1.jpg',25),('6ecaaa6b-ad9f-422c-b3bb-6013ec27b4bb','Camiseta Debugar Preta','Camiseta 100% algodão, resistente a lavagens e altas temperaturas.',1,75.000000000000000000000000000000,'2019-07-19 00:00:00.000000','camiseta4.jpg',40),('6ecaaa6b-ad9f-422c-b3bb-6013ec27c4bb','Camiseta Code Life Cinza','Camiseta 100% algodão, resistente a lavagens e altas temperaturas.',1,99.000000000000000000000000000000,'2019-07-19 00:00:00.000000','camiseta3.jpg',0),('7669370a-a9ea-4df4-9ad1-7a36e66ee6df','Caneca Joker','Caneca de porcelana com impressão térmica de alta resistência.',1,15.000000000000000000000000000000,'2021-05-10 00:00:00.000000','caneca-Joker.jpg',50),('78162be3-61c4-4959-89d7-5ebfb476427e','Caneca No Coffee No Code','Caneca de porcelana com impressão térmica de alta resistência.',1,50.000000000000000000000000000000,'2019-07-19 00:00:00.000000','caneca4.jpg',100),('79001b47-bea5-42d2-8bd7-2f2ec9231872','Camiseta Heisenberg','Camiseta 100% algodão, resistente a lavagens e altas temperaturas.',1,100.000000000000000000000000000000,'2021-05-10 00:00:00.000000','Heisenberg.webp',28),('7d67df76-2d4e-4a47-a19c-08eb80a9060b','Camiseta Code Life Preta','Camiseta 100% algodão, resistente a lavagens e altas temperaturas.',1,95.000000000000000000000000000000,'2019-07-19 00:00:00.000000','camiseta2.jpg',85),('94b55c59-3322-464c-a6af-820c1a5f27a5','Caneca Wonder Woman','Caneca de porcelana com impressão térmica de alta resistência.',1,15.000000000000000000000000000000,'2021-05-10 00:00:00.000000','caneca-Wonder-Woman.jpg',15),('9fa5731d-2884-449f-976a-33b732496396','Caneca Ironman','Caneca de porcelana com impressão térmica de alta resistência.',1,15.000000000000000000000000000000,'2021-05-10 00:00:00.000000','caneca-ironman.jpg',100),('b7925424-d7d1-4502-8f8b-bc48cf6a106d','Caneca Rick and Morty','Caneca de porcelana com impressão térmica de alta resistência.',1,15.000000000000000000000000000000,'2021-05-10 00:00:00.000000','caneca-Rick-and-Morty.jpg',50),('bd6cf0c8-cb35-4322-86cd-02c3b7900e7f','Camiseta Yoda','Camiseta 100% algodão, resistente a lavagens e altas temperaturas.',1,95.000000000000000000000000000000,'2019-07-19 00:00:00.000000','Yoda.webp',25),('fc184e11-014c-4978-aa10-9eb5e1af369b','Camiseta Software Dev','Camiseta 100% algodão, resistente a lavagens e altas temperaturas.',1,100.000000000000000000000000000000,'2019-07-19 00:00:00.000000','camiseta1.jpg',50);
/*!40000 ALTER TABLE `Products` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `RefreshTokens`
--

DROP TABLE IF EXISTS `RefreshTokens`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `RefreshTokens` (
  `Id` char(36) NOT NULL,
  `Username` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Token` char(36) NOT NULL,
  `ExpirationDate` datetime(6) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `RefreshTokens`
--

LOCK TABLES `RefreshTokens` WRITE;
/*!40000 ALTER TABLE `RefreshTokens` DISABLE KEYS */;
INSERT INTO `RefreshTokens` VALUES ('605f314b-2564-40cc-8840-5c1f3c435c80','ranny@teste.com','2bb2364a-824f-41dc-90dc-94601371d0fb','2021-06-24 17:15:02.794815'),('75d1ed90-d0d2-4ea4-ac2b-77009ff44938','joao@teste.com','5fe9a37e-ba46-464f-9bf8-0b93905f8237','2021-06-24 18:08:39.592835'),('f5a6f30c-da7d-472e-acdb-83885aaf6ce4','jhully@teste.com','ef1fb32f-1f8e-4427-8109-5a6a631d8d00','2021-05-18 04:38:07.763534');
/*!40000 ALTER TABLE `RefreshTokens` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `SecurityKeys`
--

DROP TABLE IF EXISTS `SecurityKeys`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `SecurityKeys` (
  `Id` char(36) NOT NULL,
  `Parameters` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `KeyId` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Type` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Algorithm` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `CreationDate` datetime(6) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `SecurityKeys`
--

LOCK TABLES `SecurityKeys` WRITE;
/*!40000 ALTER TABLE `SecurityKeys` DISABLE KEYS */;
INSERT INTO `SecurityKeys` VALUES ('aaf5d52e-de5a-4565-8a7c-33d00640ba38','{\"AdditionalData\":{},\"Alg\":\"ES256\",\"Crv\":\"P-256\",\"D\":\"HZKu4Y2lvok5VmgfA_5XxHUH7slj6fehh5NdezxEizg\",\"KeyId\":\"D0DSCXGFHkQsmRkP8PR2dA\",\"KeyOps\":[],\"Kid\":\"D0DSCXGFHkQsmRkP8PR2dA\",\"Kty\":\"EC\",\"Use\":\"sig\",\"X\":\"krELtSxPBNS_VFs3EkR0RGbjQWqZVqnVvK8uAFUQuK8\",\"X5c\":[],\"Y\":\"oikvByNt7MgOl6-xm93zm1wACF9crxcC4wiaqo5dq1c\",\"KeySize\":256,\"HasPrivateKey\":true,\"CryptoProviderFactory\":{\"CryptoProviderCache\":{},\"CacheSignatureProviders\":true,\"SignatureProviderObjectPoolCacheSize\":32}}','D0DSCXGFHkQsmRkP8PR2dA','EC','ES256','2021-05-13 10:05:30.430239');
/*!40000 ALTER TABLE `SecurityKeys` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Transactions`
--

DROP TABLE IF EXISTS `Transactions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Transactions` (
  `Id` char(36) NOT NULL,
  `AuthorizationCode` varchar(100) DEFAULT NULL,
  `FlagCard` varchar(100) DEFAULT NULL,
  `DateTransaction` datetime(6) DEFAULT NULL,
  `TotalValue` decimal(65,30) NOT NULL,
  `CostTransaction` decimal(65,30) NOT NULL,
  `Status` int NOT NULL,
  `TID` varchar(100) DEFAULT NULL,
  `NSU` varchar(100) DEFAULT NULL,
  `PaymentId` char(36) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Transactions_PaymentId` (`PaymentId`),
  CONSTRAINT `FK_Transactions_Payments_PaymentId` FOREIGN KEY (`PaymentId`) REFERENCES `Payments` (`Id`) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Transactions`
--

LOCK TABLES `Transactions` WRITE;
/*!40000 ALTER TABLE `Transactions` DISABLE KEYS */;
INSERT INTO `Transactions` VALUES ('01422c3a-4b52-43bc-8ab7-0abbd4599e02','HSEHPFFRBX','MasterCard','2021-06-24 10:04:00.282992',75.000000000000000000000000000000,0.000000000000000000000000000000,2,'1UK0MJLFZS','B1G7VM1MF1','4be69891-b614-4fa2-bcdc-8ddb8dcfe728'),('06859dac-787a-4402-a519-470cca833bb4','HVIZIKAEGZ','MasterCard','2021-06-24 11:37:24.039199',75.000000000000000000000000000000,0.000000000000000000000000000000,2,'PC3UKE0Y71','2CKUKI1OTF','90be1f81-56e7-4729-a6f2-51a12ae91bd1'),('0931ffc2-228b-4ab3-b516-6be47f403139','6JOS6TH7RW','MasterCard','2021-06-24 10:09:06.992439',75.000000000000000000000000000000,2.250000000000000000000000000000,1,'8Q1F88TU1X','IRPVULFILJ','6728a646-3a8d-4c62-9883-43f7820e3309'),('10cd3369-1d28-4789-89ab-74522fdd4311','P2YZYCNX09','MasterCard','2021-06-21 17:19:41.252214',95.000000000000000000000000000000,0.000000000000000000000000000000,2,'NXRYWILW2J','EQ83FQQ15J','f50ef85c-9452-4aef-9852-76dc896daeae'),('168a516b-5c40-46b8-b8b7-26038c077dcb','PJBK0GSUQ6','MasterCard','2021-06-24 10:09:13.629990',75.000000000000000000000000000000,0.000000000000000000000000000000,2,'8Q1F88TU1X','IRPVULFILJ','6728a646-3a8d-4c62-9883-43f7820e3309'),('1a224ae4-c842-4963-bb68-f29f518339bc','JFA8I4W4LD','MasterCard','2021-06-21 14:56:00.899663',95.000000000000000000000000000000,2.850000000000000000000000000000,1,'NXRYWILW2J','EQ83FQQ15J','f50ef85c-9452-4aef-9852-76dc896daeae'),('246c40a5-1a2d-4048-8c04-fb2a683fc207','94HA9OF4O2','MasterCard','2021-06-24 09:09:07.879293',100.000000000000000000000000000000,0.000000000000000000000000000000,2,'LNLGIAZ1M1','20U5TP146L','5c97430e-0ea5-4558-916d-cf47dc833051'),('2bff2ba5-3b20-4e85-8abf-af615d8b5dfc','0ODDEWFRPL','MasterCard','2021-06-21 17:20:36.177172',95.000000000000000000000000000000,2.850000000000000000000000000000,1,'V15IORD4IN','NOXBEY9EPY','745aa948-f3f1-4ba9-8c26-f0bb06addc96'),('2c4196b0-bc17-48a6-be9b-b8c4ead49b9f','VKIZQGP4H9','MasterCard','2021-06-23 11:54:07.435141',95.000000000000000000000000000000,0.000000000000000000000000000000,2,'0E6A9D4LBJ','TDMK85LO4N','53c0b43d-882b-469e-b8c7-4baa6c7afd59'),('313d4894-16ca-40ab-b9c1-6da7fe4e1672','DV9RYC4J63','MasterCard','2021-06-24 10:05:38.200818',75.000000000000000000000000000000,2.250000000000000000000000000000,1,'RX2V78VOM2','5HMF56TUUA','0e20755d-8e1a-4b8c-9f92-34d124511625'),('3185312e-aff4-47d2-8776-8875a7c02478','CYPXD9F5YU','MasterCard','2021-06-22 07:31:04.590484',95.000000000000000000000000000000,2.850000000000000000000000000000,1,'D4ASJ7H9A5','JA5ZSEUJO7','9acbc137-029b-4753-893a-d6c7e62e0a9a'),('31ee61b8-ce68-4028-ab7f-ff01ffaf5b80','K1KQC6YL9A','MasterCard','2021-06-23 11:54:11.176684',190.000000000000000000000000000000,0.000000000000000000000000000000,2,'N20Q85U9FV','D0K2MLFHQV','d18ac588-593c-45a8-8dc1-93f4dbb10b64'),('42e3f78d-468e-4ce3-a75f-9ae6728dffac','E2JPGUONNT','MasterCard','2021-06-21 17:20:38.612035',95.000000000000000000000000000000,0.000000000000000000000000000000,2,'V15IORD4IN','NOXBEY9EPY','745aa948-f3f1-4ba9-8c26-f0bb06addc96'),('4a34ad05-982f-4ada-bdb6-5136955dd44f','F6FC20VRQN','MasterCard','2021-06-24 11:55:11.666896',75.000000000000000000000000000000,2.250000000000000000000000000000,1,'1UK0MJLFZS','B1G7VM1MF1','4be69891-b614-4fa2-bcdc-8ddb8dcfe728'),('51aee615-9f0e-4066-b925-adbc9b8520b5','NMDHAAAIX5','MasterCard','2021-06-24 11:38:09.908255',95.000000000000000000000000000000,2.850000000000000000000000000000,1,'TP5VYLOTAQ','LFZ81GBNUI','b668c5e2-940f-4428-9635-b92bd3c9bd8a'),('51c3292a-84b2-464f-9381-94a9b7d0cb11','5379DFI71J','MasterCard','2021-06-24 09:42:04.783085',75.000000000000000000000000000000,0.000000000000000000000000000000,2,'E16Z61K8EO','LPRKDX7A52','d6f8420c-fe3a-448e-883a-017623cfae02'),('58fc34c7-83d5-44c4-b88d-6c91f8c10110','XL5DUMBITM','MasterCard','2021-06-24 10:05:40.172577',75.000000000000000000000000000000,0.000000000000000000000000000000,2,'RX2V78VOM2','5HMF56TUUA','0e20755d-8e1a-4b8c-9f92-34d124511625'),('5f39588b-8120-45d8-9e73-74e679360745','DJGTK62KIS','MasterCard','2021-06-24 09:41:50.324694',75.000000000000000000000000000000,2.250000000000000000000000000000,1,'E16Z61K8EO','LPRKDX7A52','d6f8420c-fe3a-448e-883a-017623cfae02'),('656cf194-b4a1-4515-a15d-a47e8e14899c','PRJEQJQ2G7','MasterCard','2021-06-23 12:22:32.085971',190.000000000000000000000000000000,0.000000000000000000000000000000,2,'2CQQNKGIJX','ABLZM2ZA2R','11fa0d3b-aefa-4110-944a-6e9546f5e24d'),('6ed23241-daa9-4ed1-b649-03a372c7221e','','MasterCard','2021-05-12 08:46:01.152090',194.000000000000000000000000000000,0.000000000000000000000000000000,5,'7QXN4U4QBN','AT18XXVM6S','3dbfffef-6314-44da-adcc-0697280afbfb'),('73703261-1b17-4d8b-a704-3df0e3135caf','6HJE1T6AAT','MasterCard','2021-06-23 12:24:07.981102',100.000000000000000000000000000000,0.000000000000000000000000000000,2,'VH0DZARGP3','D26UZ9C7HC','5a232c2b-226d-40a8-b38f-0a7229d2bbc8'),('754163b6-d9e1-4516-bf1f-fff39acd1bd2','0R6M2W1MHO','MasterCard','2021-05-12 08:45:49.404805',194.000000000000000000000000000000,5.820000000000000000000000000000,1,'7QXN4U4QBN','AT18XXVM6S','3dbfffef-6314-44da-adcc-0697280afbfb'),('7e03b2a5-393f-4507-a3dc-135ed1e7f350','MXLYUN57S8','MasterCard','2021-06-24 09:14:06.948195',95.000000000000000000000000000000,0.000000000000000000000000000000,2,'TRL0SY6PBN','M3584FU6CH','b7bb56b2-5de8-4fe8-8a67-2c7a16ff9878'),('823429a5-f172-424b-bd6a-327e8629dba5','3U3BYCO4XM','MasterCard','2021-06-23 08:48:17.562560',95.000000000000000000000000000000,0.000000000000000000000000000000,2,'D4ASJ7H9A5','JA5ZSEUJO7','9acbc137-029b-4753-893a-d6c7e62e0a9a'),('8301a971-5fe9-414e-be91-1a75c82c8dce','21M6UFZZGU','MasterCard','2021-06-24 11:37:39.174589',95.000000000000000000000000000000,2.850000000000000000000000000000,1,'CUB1I2FB71','5AA4EM7AY0','09b101bf-3ed9-4ccf-bd2f-9903385b0679'),('8b71fc65-b047-4911-8f66-39173c042a7f','XH10ROKH9G','MasterCard','2021-05-12 08:47:34.390891',115.000000000000000000000000000000,3.450000000000000000000000000000,1,'KXFC8YPSZW','RN1R0I9TJM','4191d677-afac-4aec-bd4a-29c108c973b3'),('96ffed4b-9e89-47eb-8889-b86ef5e9ffbb','R0JIMY4R21','MasterCard','2021-06-24 11:37:14.181392',75.000000000000000000000000000000,2.250000000000000000000000000000,1,'PC3UKE0Y71','2CKUKI1OTF','90be1f81-56e7-4729-a6f2-51a12ae91bd1'),('982e445d-8388-44c2-8ad9-110a6e270761','WU40YRYBF3','MasterCard','2021-06-24 11:37:52.959883',95.000000000000000000000000000000,0.000000000000000000000000000000,2,'CUB1I2FB71','5AA4EM7AY0','09b101bf-3ed9-4ccf-bd2f-9903385b0679'),('9fd1e455-3af5-4182-a705-1baae8f96c33','OCZSA8UIYC','MasterCard','2021-06-21 15:00:17.824176',95.000000000000000000000000000000,2.850000000000000000000000000000,1,'CCNHK6ZC8V','CS6QKD9N7Q','56ccf0b9-8d4a-4058-8f56-128248064389'),('be7a4c12-1140-4a02-9e48-81ce5a898760','6OULYIKPIQ','MasterCard','2021-06-23 09:22:58.308957',190.000000000000000000000000000000,5.700000000000000000000000000000,1,'N20Q85U9FV','D0K2MLFHQV','d18ac588-593c-45a8-8dc1-93f4dbb10b64'),('bfc17daf-2a7b-4eb8-b7e1-fbe59ee26efa','9QJJLVJV21','MasterCard','2021-06-23 11:56:32.256491',190.000000000000000000000000000000,0.000000000000000000000000000000,2,'2CQQNKGIJX','ABLZM2ZA2R','11fa0d3b-aefa-4110-944a-6e9546f5e24d'),('d08b1347-224b-4141-85ba-a05f78ca5d9b','3YEH4ZG77F','MasterCard','2021-06-21 17:19:53.794097',95.000000000000000000000000000000,0.000000000000000000000000000000,2,'CCNHK6ZC8V','CS6QKD9N7Q','56ccf0b9-8d4a-4058-8f56-128248064389'),('d9ac858d-7e89-488c-bbb7-eafbdff477e2','CNPNDG21F8','MasterCard','2021-06-23 11:56:32.496479',190.000000000000000000000000000000,0.000000000000000000000000000000,2,'2CQQNKGIJX','ABLZM2ZA2R','11fa0d3b-aefa-4110-944a-6e9546f5e24d'),('dc7396de-7179-4b9a-9fc9-74f981f2cdfb','EEOLVN22K9','MasterCard','2021-06-24 09:16:18.126377',75.000000000000000000000000000000,2.250000000000000000000000000000,1,'MD4IK9ROYH','CQIJH5G602','9e646116-da9a-4a16-87c2-44e58f8b9ede'),('df7cf475-6c1f-4e87-88e4-e481e6891ff3','6GMGZKH5VG','MasterCard','2021-05-12 08:47:45.953541',115.000000000000000000000000000000,0.000000000000000000000000000000,2,'KXFC8YPSZW','RN1R0I9TJM','4191d677-afac-4aec-bd4a-29c108c973b3'),('e08fb615-d62d-4de4-8d1b-b06c96b95f23','49ZZ2J8RHT','MasterCard','2021-06-24 09:09:03.079242',100.000000000000000000000000000000,3.000000000000000000000000000000,1,'LNLGIAZ1M1','20U5TP146L','5c97430e-0ea5-4558-916d-cf47dc833051'),('e2dedbb7-41d0-44f5-9e70-7967bd5d2d36','8V346B8YAD','MasterCard','2021-06-24 09:40:54.823902',95.000000000000000000000000000000,0.000000000000000000000000000000,2,'TP5VYLOTAQ','LFZ81GBNUI','b668c5e2-940f-4428-9635-b92bd3c9bd8a'),('e92ff1ba-5ce7-4956-b3b0-a64f2e68b520','CZSGJZKCW0','MasterCard','2021-06-24 09:16:21.759413',75.000000000000000000000000000000,0.000000000000000000000000000000,2,'MD4IK9ROYH','CQIJH5G602','9e646116-da9a-4a16-87c2-44e58f8b9ede'),('e9b634bc-ff37-4e1d-aab7-d18a968a05ad','DPPDEHTMX1','MasterCard','2021-06-24 09:14:03.127172',95.000000000000000000000000000000,2.850000000000000000000000000000,1,'TRL0SY6PBN','M3584FU6CH','b7bb56b2-5de8-4fe8-8a67-2c7a16ff9878'),('eab7dd18-3edd-47b2-bdd0-9f6660aa9d65','JC9THODBY9','MasterCard','2021-06-21 17:19:41.252226',95.000000000000000000000000000000,0.000000000000000000000000000000,2,'NXRYWILW2J','EQ83FQQ15J','f50ef85c-9452-4aef-9852-76dc896daeae'),('edd9b96b-607d-4dea-a970-b412a4317927','LVXWF62RGP','MasterCard','2021-06-23 09:13:00.812637',95.000000000000000000000000000000,2.850000000000000000000000000000,1,'0E6A9D4LBJ','TDMK85LO4N','53c0b43d-882b-469e-b8c7-4baa6c7afd59'),('f24d0546-d01a-49e2-a816-dfd6ef3a9292','3H3206XLKY','MasterCard','2021-06-23 12:23:54.184755',100.000000000000000000000000000000,3.000000000000000000000000000000,1,'VH0DZARGP3','D26UZ9C7HC','5a232c2b-226d-40a8-b38f-0a7229d2bbc8'),('ffa97f13-1f77-4293-bcb4-37c57a4485ed','2PRMFONVXR','MasterCard','2021-06-23 11:55:57.063316',190.000000000000000000000000000000,5.700000000000000000000000000000,1,'2CQQNKGIJX','ABLZM2ZA2R','11fa0d3b-aefa-4110-944a-6e9546f5e24d');
/*!40000 ALTER TABLE `Transactions` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Vouchers`
--

DROP TABLE IF EXISTS `Vouchers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Vouchers` (
  `Id` char(36) NOT NULL,
  `Code` varchar(100) NOT NULL,
  `DiscountPercentage` decimal(65,30) DEFAULT NULL,
  `DiscountValue` decimal(65,30) DEFAULT NULL,
  `Quantity` int NOT NULL,
  `DiscountType` int NOT NULL,
  `DateCreation` datetime(6) NOT NULL,
  `DateUsage` datetime(6) DEFAULT NULL,
  `DateValidity` datetime(6) NOT NULL,
  `Activ` tinyint(1) NOT NULL,
  `Usage` tinyint(1) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Vouchers`
--

LOCK TABLES `Vouchers` WRITE;
/*!40000 ALTER TABLE `Vouchers` DISABLE KEYS */;
/*!40000 ALTER TABLE `Vouchers` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2021-06-24 12:34:02
