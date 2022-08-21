use NTT
GO

ALTER TABLE cuenta
	DROP CONSTRAINT fk_cliente_cuenta;
ALTER TABLE cuenta_movimiento
	DROP CONSTRAINT fk_cuen_cuenta_movimiento;
ALTER TABLE cuenta_movimiento
	DROP CONSTRAINT fk_mov_cuenta_movimiento;
ALTER TABLE cliente
	DROP CONSTRAINT fk_persona_cliente;
IF OBJECT_ID('cliente') IS NOT NULL
DROP TABLE cliente;
IF OBJECT_ID('cuenta') IS NOT NULL
DROP TABLE cuenta;
IF OBJECT_ID('cuenta_movimiento') IS NOT NULL
DROP TABLE cuenta_movimiento;
IF OBJECT_ID('movimiento') IS NOT NULL
DROP TABLE movimiento;
IF OBJECT_ID('.persona') IS NOT NULL
DROP TABLE persona;
CREATE TABLE cliente  ( 
	cliente_id	int IDENTITY NOT NULL,
	contrasena	varchar(25) NOT NULL,
	estado    	bit NOT NULL,
	persona_id	int NOT NULL,
	CONSTRAINT PK_cliente_1 PRIMARY KEY CLUSTERED(cliente_id)
);
CREATE TABLE cuenta  ( 
	cuenta_id    	int IDENTITY NOT NULL,
	numero_cuenta	varchar(10) NOT NULL,
	tipo_cuenta  	varchar(25) NOT NULL,
	saldo_inicial	decimal(10,2) NOT NULL,
	estado       	bit NOT NULL,
	cliente_id   	int NOT NULL,
	CONSTRAINT PK_cuenta_1 PRIMARY KEY CLUSTERED(cuenta_id)
);
CREATE TABLE cuenta_movimiento  ( 
	cuenta_movimiento_id	int IDENTITY NOT NULL,
	movimiento_id       	int NOT NULL,
	cuenta_id           	int NOT NULL,
	saldo               	decimal(10,2) NULL,
	movimiento          	decimal(10,2) NULL,
	CONSTRAINT PK_cuenta_movimiento_1 PRIMARY KEY CLUSTERED(cuenta_movimiento_id)
);
CREATE TABLE movimiento  ( 
	movimiento_id  	int IDENTITY NOT NULL,
	cuenta_id      	int NOT NULL,
	fecha          	date NOT NULL,
	tipo_movimiento	varchar(25) NOT NULL,
	estado         	bit NOT NULL,
	saldo          	decimal(10,2) NOT NULL,
	CONSTRAINT PK_movimiento_1 PRIMARY KEY CLUSTERED(movimiento_id)
);
CREATE TABLE persona  ( 
	persona_id    	int IDENTITY NOT NULL,
	nombre        	varchar(150) NOT NULL,
	genero        	varchar(10) NOT NULL,
	edad          	tinyint NOT NULL,
	identificacion	varchar(13) NOT NULL,
	direccion     	varchar(250) NOT NULL,
	telefono      	varchar(10) NOT NULL,
	CONSTRAINT PK_persona_1 PRIMARY KEY CLUSTERED(persona_id)
);
ALTER TABLE cuenta
	ADD CONSTRAINT fk_cliente_cuenta
	FOREIGN KEY(cliente_id)
	REFERENCES cliente(cliente_id);
ALTER TABLE cuenta_movimiento
	ADD CONSTRAINT fk_cuen_cuenta_movimiento
	FOREIGN KEY(cuenta_id)
	REFERENCES cuenta(cuenta_id)
	ON DELETE CASCADE 
	ON UPDATE CASCADE ;
ALTER TABLE cuenta_movimiento
	ADD CONSTRAINT fk_mov_cuenta_movimiento
	FOREIGN KEY(movimiento_id)
	REFERENCES movimiento(movimiento_id)
	ON DELETE CASCADE 
	ON UPDATE CASCADE ;
ALTER TABLE cliente
	ADD CONSTRAINT fk_persona_cliente
	FOREIGN KEY(persona_id)
	REFERENCES persona(persona_id);
