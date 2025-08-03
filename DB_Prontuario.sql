DROP DATABASE IF EXISTS db_prontuario;

CREATE DATABASE db_prontuario;
USE db_prontuario;

CREATE TABLE psicologos(
id_psicologo INT PRIMARY KEY,
nome_psicologo VARCHAR(50)
);

INSERT INTO psicologos(id_psicologo, nome_psicologo)
VALUES (1, 'Alice Martins');

INSERT INTO psicologos(id_psicologo, nome_psicologo)
VALUES (2, 'Igor Ferreira');

CREATE TABLE pacientes(
id_pacientes INT UNIQUE auto_increment,
cpf_pacientes VARCHAR(20) PRIMARY KEY NOT NULL,
nome VARCHAR(120) NOT NULL,
data_nascimento varchar(12) NOT NULL,
telefone varchar(15) NOT NULL,
valor varchar(8) not null,
valor_nota boolean not null,
nome_responsavel varchar(120),
telefone_responsavel varchar(15),
id_psicologo INT,
observacoes varchar(300),
pagamento INT
);

ALTER TABLE pacientes
ADD CONSTRAINT fk_id_psicologo_psicologos_pacientes
FOREIGN KEY (id_psicologo)
REFERENCES psicologos (id_psicologo);

CREATE TABLE registros(
id_registro INT PRIMARY KEY AUTO_INCREMENT,
id_paciente INT,
registro TEXT,
data_registro VARCHAR(12)
);

CREATE TABLE pagamentos(
id_pagamento INT PRIMARY KEY AUTO_INCREMENT,
id_pacientes INT,
data_pagamento DATE NOT NULL,
valor DECIMAL (10,2) NOT NULL
);

ALTER TABLE pagamentos
ADD CONSTRAINT fk_id_pacientes_pacientes_pagamentos
FOREIGN KEY (id_pacientes)
REFERENCES pacientes(id_pacientes);

INSERT INTO pacientes (cpf_pacientes, nome, data_nascimento, telefone, valor, valor_nota, nome_responsavel, telefone_responsavel, id_psicologo, pagamento)
VALUES('123.456.789.12', 'Theodoro Martins de Freitas', '22-08-2003', '(11)94230-7430', '120.00', 0, 'Dorvalina Martins Barroso', '(11)91234-5678', 1, 2);

SELECT * FROM pagamentos;

