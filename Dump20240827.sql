-- MySQL dump 10.13  Distrib 8.0.38, for Win64 (x86_64)
--
-- Host: localhost    Database: tmdb_users
-- ------------------------------------------------------
-- Server version	8.0.39

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
-- Table structure for table `favorite_movies`
--

DROP TABLE IF EXISTS `favorite_movies`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `favorite_movies` (
  `idmovies` int NOT NULL,
  `iduser` int NOT NULL,
  PRIMARY KEY (`idmovies`,`iduser`),
  KEY `fk_favorite_movies_idmovies_idx` (`idmovies`),
  KEY `fk_favorite_movies_iduser_idx` (`iduser`),
  CONSTRAINT `fk_favorite_movies_idmovies` FOREIGN KEY (`idmovies`) REFERENCES `movies` (`idmovies`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_favorite_movies_iduser` FOREIGN KEY (`iduser`) REFERENCES `users` (`iduser`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `favorite_movies`
--

LOCK TABLES `favorite_movies` WRITE;
/*!40000 ALTER TABLE `favorite_movies` DISABLE KEYS */;
INSERT INTO `favorite_movies` VALUES (120,18),(121,18),(122,18),(322697,14),(519182,14),(533535,14),(533535,15),(533535,16),(718821,14),(718821,15),(1022789,14);
/*!40000 ALTER TABLE `favorite_movies` ENABLE KEYS */;
UNLOCK TABLES;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `favorite_movies_AFTER_INSERT` AFTER INSERT ON `favorite_movies` FOR EACH ROW BEGIN
	UPDATE movies
    SET favorite_amount = favorite_amount + 1
    WHERE idmovies = NEW.idmovies;

    UPDATE users
    SET favorite_amount = favorite_amount + 1
    WHERE iduser = NEW.iduser;
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `favorite_movies_AFTER_DELETE` AFTER DELETE ON `favorite_movies` FOR EACH ROW BEGIN
    UPDATE movies
    SET favorite_amount = favorite_amount - 1
    WHERE idmovies = OLD.idmovies;
    
    UPDATE users
    SET favorite_amount = favorite_amount - 1
    WHERE iduser = OLD.iduser;
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Table structure for table `favorite_series`
--

DROP TABLE IF EXISTS `favorite_series`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `favorite_series` (
  `idseries` int NOT NULL,
  `iduser` int NOT NULL,
  PRIMARY KEY (`idseries`,`iduser`),
  KEY `idseries_idx` (`idseries`),
  KEY `fk_favorite_series_iduser_idx` (`iduser`),
  CONSTRAINT `fk_favorite_series_idseries` FOREIGN KEY (`idseries`) REFERENCES `series` (`idseries`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_favorite_series_iduser` FOREIGN KEY (`iduser`) REFERENCES `users` (`iduser`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `favorite_series`
--

LOCK TABLES `favorite_series` WRITE;
/*!40000 ALTER TABLE `favorite_series` DISABLE KEYS */;
INSERT INTO `favorite_series` VALUES (1418,14),(1418,15),(1418,18),(69294,14),(69294,15),(69294,16),(96404,14),(96404,18),(228508,14),(228508,18);
/*!40000 ALTER TABLE `favorite_series` ENABLE KEYS */;
UNLOCK TABLES;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `favorite_series_AFTER_INSERT` AFTER INSERT ON `favorite_series` FOR EACH ROW BEGIN
    UPDATE series
    SET favorite_amount = favorite_amount + 1
    WHERE idseries = NEW.idseries;
    
	UPDATE users
    SET favorite_amount = favorite_amount + 1
    WHERE iduser = NEW.iduser;
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `favorite_series_AFTER_DELETE` AFTER DELETE ON `favorite_series` FOR EACH ROW BEGIN
    UPDATE series
    SET favorite_amount = favorite_amount - 1
    WHERE idseries = OLD.idseries;
    
	UPDATE users
    SET favorite_amount = favorite_amount - 1
    WHERE iduser = OLD.iduser;
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Table structure for table `movies`
--

DROP TABLE IF EXISTS `movies`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `movies` (
  `idmovies` int NOT NULL,
  `title` varchar(128) DEFAULT NULL,
  `favorite_amount` int DEFAULT NULL,
  PRIMARY KEY (`idmovies`),
  UNIQUE KEY `idMovies_UNIQUE` (`idmovies`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `movies`
--

LOCK TABLES `movies` WRITE;
/*!40000 ALTER TABLE `movies` DISABLE KEYS */;
INSERT INTO `movies` VALUES (120,'The Lord of the Rings: The Fellowship of the Ring',1),(121,'The Lord of the Rings: The Two Towers',1),(122,'The Lord of the Rings: The Return of the King',1),(322697,'Big Bang',1),(519182,'Despicable Me 4',1),(533535,'Deadpool & Wolverine',3),(573435,'Bad Boys: Ride or Die',0),(718821,'Twisters',2),(1022789,'Inside Out 2',1),(1160018,'Kill',0);
/*!40000 ALTER TABLE `movies` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `series`
--

DROP TABLE IF EXISTS `series`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `series` (
  `idseries` int NOT NULL,
  `title` varchar(128) DEFAULT NULL,
  `favorite_amount` int DEFAULT NULL,
  PRIMARY KEY (`idseries`),
  UNIQUE KEY `idseries_UNIQUE` (`idseries`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `series`
--

LOCK TABLES `series` WRITE;
/*!40000 ALTER TABLE `series` DISABLE KEYS */;
INSERT INTO `series` VALUES (1418,'The Big Bang Theory',3),(69294,'Marginal#4 Kiss Kara Tsukuru Big Bang',3),(96404,'Big Bang',2),(121080,'Pleasant Goat and Big Big Wolf: Happy, Happy, Bang! Bang!',0),(228508,'Big Bang',2);
/*!40000 ALTER TABLE `series` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `users` (
  `iduser` int NOT NULL AUTO_INCREMENT,
  `user_name` varchar(45) NOT NULL,
  `password` varchar(45) NOT NULL,
  `favorite_amount` int NOT NULL DEFAULT '0',
  `salt` binary(16) DEFAULT NULL,
  PRIMARY KEY (`iduser`),
  UNIQUE KEY `user_name_UNIQUE` (`user_name`),
  UNIQUE KEY `iduser_UNIQUE` (`iduser`)
) ENGINE=InnoDB AUTO_INCREMENT=19 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES (13,'DeleteLater','6O1nU2Nye2qntWgS/AW1Tlg93HjGJzghLE9nJa7reyk=',0,_binary 'ûØ\—\Ô_Ta®#Sıùx≥¯'),(14,'PatientZero','cCrJdQkt43LyeDg9826K4cENkuHiQf/53TfMZ8EDOUA=',9,_binary 'ô\‘%s~Co[J∫N¸5'),(15,'PatientOne','RcAYINktecN6QKUAP5PEi2xoh9jnRulNCzASL28QvLI=',4,_binary '3.∏©\Ìß\ÍˆïKöqñY'),(16,'PatientTwo','i5uoJEDYEVn5mRNmxWOGn/OocPzaYzWPrF9xZMEKwaI=',2,_binary '2∏§\ÎN\ËG¢1µ™¥RP'),(18,'Admin','mw/7kfQqfcJWKH4xThAJf9ETI9XkuYHq3j54cE1EsZg=',6,_binary '£N\Âs\Ã®+æÖ\÷\"C˙');
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
UNLOCK TABLES;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`root`@`localhost`*/ /*!50003 TRIGGER `users_AFTER_DELETE` AFTER DELETE ON `users` FOR EACH ROW BEGIN
	DELETE FROM tmdb_users.favorite_movies WHERE iduser = OLD.iduser;
	DELETE FROM tmdb_users.favorite_series WHERE iduser = OLD.iduser;
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-08-27 11:15:28
