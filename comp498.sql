-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Anamakine: 127.0.0.1
-- Üretim Zamanı: 25 Haz 2024, 18:07:20
-- Sunucu sürümü: 10.4.32-MariaDB
-- PHP Sürümü: 8.0.30

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Veritabanı: `comp498`
--

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `items`
--

CREATE TABLE `items` (
  `id` int(11) NOT NULL,
  `name` varchar(100) NOT NULL,
  `description` text DEFAULT NULL,
  `image_url` varchar(255) DEFAULT NULL,
  `price` int(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Tablo döküm verisi `items`
--

INSERT INTO `items` (`id`, `name`, `description`, `image_url`, `price`) VALUES
(1, 'Click Damage', 'Increase the damage each click does.', NULL, 50),
(2, 'Critical Hit Chance', 'Chance for critical hits that deal double damage.', NULL, 250),
(3, 'Auto Click Damage', 'Automatically clicks every second.', NULL, 100),
(4, 'Gold Multiplier', 'Increase the amount of gold earned per click.', NULL, 500);

-- --------------------------------------------------------

--
-- Tablo için tablo yapısı `users`
--

CREATE TABLE `users` (
  `id` int(11) NOT NULL,
  `username` varchar(32) NOT NULL,
  `password` varchar(100) NOT NULL,
  `is_admin` tinyint(1) DEFAULT 0,
  `gold` int(11) DEFAULT 0,
  `level` int(11) DEFAULT 1,
  `damage` int(11) DEFAULT 1,
  `crit_chance` int(11) DEFAULT 1,
  `auto_click_damage` int(11) DEFAULT 0,
  `gold_multiplier` int(11) DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Tablo döküm verisi `users`
--

INSERT INTO `users` (`id`, `username`, `password`, `is_admin`, `gold`, `level`, `damage`, `crit_chance`, `auto_click_damage`, `gold_multiplier`) VALUES
(1, 'AKN', '$2y$10$lzV1v8iU3or7LaRWxoBzP./ZJ0DQck9z9rcEOgU8RVSAj/PzoBuVW', 1, 93356, 47, 24, 51, 5, 11),
(2, 'akn1', '$2y$10$Q5CjDxTSAjETScOSfil3OOtR7UsO731vgc/Q7Kv0MCwsP73EOMcm6', 0, 311, 12, 4, 2, 10, 1),
(3, 'qqq', '$2y$10$WKDS3C/ISb/vL26.KbjRaOWFZ2vxXVb26QUR9mOEbb7i08ACoL.Fu', 0, 310, 20, 10, 1, 0, 1);

--
-- Dökümü yapılmış tablolar için indeksler
--

--
-- Tablo için indeksler `items`
--
ALTER TABLE `items`
  ADD PRIMARY KEY (`id`);

--
-- Tablo için indeksler `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `username` (`username`);

--
-- Dökümü yapılmış tablolar için AUTO_INCREMENT değeri
--

--
-- Tablo için AUTO_INCREMENT değeri `items`
--
ALTER TABLE `items`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=19;

--
-- Tablo için AUTO_INCREMENT değeri `users`
--
ALTER TABLE `users`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
