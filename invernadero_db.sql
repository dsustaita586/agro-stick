CREATE DATABASE invernadero_db;

USE invernadero_db;

CREATE TABLE tbl_usuario(
	cveUsuario SMALLINT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    nombre VARCHAR(150) NOT NULL,
    apellidos VARCHAR(450) NOT NULL,
    fechaRegistro DATETIME DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE tbl_dispositivo(
	cveDispositivo SMALLINT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    nombre VARCHAR(350) NOT NULL,
    estatus TINYINT(1),
    cveUsuario SMALLINT,
    FOREIGN KEY (cveUsuario) REFERENCES tbl_usuario(cveUsuario)
);

CREATE TABLE tbl_temperatura(
	cveTemperatura SMALLINT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    temperatura FLOAT NOT NULL,
    fechaRegistro DATETIME DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE tbl_humedad(
	cveHumedad SMALLINT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    humedad FLOAT NOT NULL,
    fechaRegistro DATETIME DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE tbl_ppm(
	cvePpm SMALLINT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    ppm FLOAT NOT NULL,
    fechaRegistro DATETIME DEFAULT CURRENT_TIMESTAMP
);


SELECT * FROM tbl_dispositivo;
SELECT * FROM tbl_temperatura;
SELECT * FROM tbl_humedad;
SELECT * FROM tbl_ppm;


SELECT DATE(fechaRegistro), SUM(temperatura) / COUNT(cveTemperatura)
FROM tbl_temperatura
WHERE DATE(fechaRegistro) BETWEEN '2022-07-13' AND '2022-07-22'
GROUP BY DATE(fechaRegistro);
