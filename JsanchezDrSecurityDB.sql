CREATE DATABASE JSanchezDrSecurity
USE JSanchezDrSecurity
GO

CREATE TABLE Direccion(
	IdDireccion INT IDENTITY(1,1) PRIMARY KEY,
	Calle VARCHAR(50) NULL,
	NumeroInterior VARCHAR(20) NULL,
	NumeroExterior VARCHAR(20) NULL,
	Colonia VARCHAR(50),
	Municipio VARCHAR(50),
	Estado VARCHAR(50)
)
GO
SELECT * FROM Direccion
INSERT INTO Direccion(Calle,NumeroInterior,NumeroExterior,Colonia,Municipio,Estado)VALUES('18 de octubre','45','2','dsfs','Puebla','Puebla')
GO

CREATE TABLE Persona(
	IdPersona INT IDENTITY(1,1) PRIMARY KEY,
	Nombre VARCHAR(50) NOT NULL,
	ApellidoPaterno VARCHAR(50) NOT NULL,
	ApellidoMaterno VARCHAR(50) NULL,
	FechaNacimiento DATE,
	Sexo VARCHAR(10),
	EstadoNacimiento VARCHAR(50),
	Telefono VARCHAR(10),
	CURP VARCHAR(18),
	IdDireccion INT
	CONSTRAINT fk_PersonaDireccion FOREIGN KEY (IdDireccion) REFERENCES Direccion(IdDireccion)
)
GO

ALTER PROCEDURE PersonaAdd --'Jesus','Sanchez','Xihuitl','15-10-1969','Masculino','Puebla','5523951845','sdfdfdasdgs',2
@Nombre VARCHAR(50),
@ApellidoPaterno VARCHAR(50),
@ApellidoMaterno VARCHAR(50),
@FechaNacimiento DATE,
@Sexo VARCHAR(10),
@EstadoNacimiento VARCHAR(50),
@Telefono VARCHAR(10),
@CURP VARCHAR(18),
@IdDireccion INT
AS
INSERT INTO Persona(Nombre,ApellidoPaterno,ApellidoMaterno,FechaNacimiento,Sexo,EstadoNacimiento,Telefono,CURP,IdDireccion)
			VALUES(@Nombre,@ApellidoPaterno,@ApellidoMaterno,@FechaNacimiento,@Sexo,@EstadoNacimiento,@Telefono,@CURP,@IdDireccion)
GO

ALTER PROCEDURE PersonaUpdate --2,'Luis','Lopez','Arias','15-10-1969','Masculino','Puebla','5523951845','sdfdfdasdgs',2
@IdPersona INT,
@Nombre VARCHAR(50),
@ApellidoPaterno VARCHAR(50),
@ApellidoMaterno VARCHAR(50),
@FechaNacimiento DATE,
@Sexo VARCHAR(10),
@EstadoNacimiento VARCHAR(50),
@Telefono VARCHAR(10),
@CURP VARCHAR(18),
@IdDireccion INT
AS
UPDATE Persona
SET
Nombre=@Nombre,
ApellidoPaterno=@ApellidoPaterno,
ApellidoMaterno=@ApellidoMaterno,
FechaNacimiento=@FechaNacimiento,
Sexo=@Sexo,
EstadoNacimiento=@EstadoNacimiento,
Telefono=@Telefono,
CURP=@CURP,
IdDireccion=@IdDireccion
WHERE IdPersona=@IdPersona
GO

CREATE PROCEDURE PersonaDelete
@IdPersona INT
AS
DELETE FROM Persona WHERE IdPersona = @IdPersona
GO

CREATE PROCEDURE PersonaGetAll
AS
SELECT IdPersona,Nombre,ApellidoPaterno,ApellidoMaterno,FechaNacimiento,Sexo,EstadoNacimiento,Telefono,CURP,
Direccion.IdDireccion,Calle,NumeroInterior,NumeroExterior,Colonia,Municipio,Estado
FROM Persona
INNER JOIN Direccion ON Persona.IdDireccion = Direccion.IdDireccion
GO

CREATE PROCEDURE PersonaGetById --1
@IdPersona INT
AS
SELECT IdPersona,Nombre,ApellidoPaterno,ApellidoMaterno,FechaNacimiento,Sexo,EstadoNacimiento,Telefono,CURP,
Direccion.IdDireccion,Calle,NumeroInterior,NumeroExterior,Colonia,Municipio,Estado
FROM Persona
INNER JOIN Direccion ON Persona.IdDireccion = Direccion.IdDireccion
WHERE IdPersona=@IdPersona
GO