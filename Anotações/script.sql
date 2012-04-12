CREATE TABLE [Funcionario] (
  [FuncionarioId] INT IDENTITY(1,1) NOT NULL ,
  [Nome] VARCHAR(100) NOT NULL ,
  [RG] VARCHAR(11) NOT NULL ,
  [UserId] uniqueidentifier NOT NULL ,
  [endereco] VARCHAR(150) NOT NULL ,
  [funcao] INT NOT NULL ,
  PRIMARY KEY ([FuncionarioId]) ,
  CONSTRAINT [Funcionario_UserId]
    FOREIGN KEY ([UserId] )
    REFERENCES dbo.aspnet_Users ([UserId])
    ON UPDATE NO ACTION);


CREATE TABLE [Telefone] (
  [TelefoneId] INT IDENTITY(1,1) NOT NULL ,
  [Numero] VARCHAR(15) NOT NULL ,
  [Tipo] INT NOT NULL ,
  PRIMARY KEY ([TelefoneId]) );

CREATE TABLE [Especialidade] (
  [EspecialidadeId] INT IDENTITY (1,1) NOT NULL ,
  [Descricao] VARCHAR(100) NOT NULL ,
  PRIMARY KEY ([EspecialidadeId]) );

CREATE TABLE [FuncionarioEspecialidade] (
  [FuncionarioEspecialidadeId] INT IDENTITY(1,1) NOT NULL ,
  [FuncionarioId] INT NOT NULL ,
  [EspecialidadeId] INT NOT NULL ,
  PRIMARY KEY (FuncionarioEspecialidadeId) ,
  CONSTRAINT [FuncionarioEspecialidade_FuncionarioId]
    FOREIGN KEY ([FuncionarioId] )
    REFERENCES [Funcionario] ([FuncionarioId] )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT [FuncionarioEspecialidade_EspecialidadeId]
    FOREIGN KEY ([EspecialidadeId] )
    REFERENCES Especialidade (EspecialidadeId)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);


CREATE  TABLE [Paciente] (
  [PacienteId] INT IDENTITY(1,1) NOT NULL ,
  [Nome] VARCHAR(45) NOT NULL ,
  [Endereco] VARCHAR(100) NOT NULL ,
  [ObservacaoMedico] VARCHAR(100) NULL ,
  PRIMARY KEY ([PacienteId]) );


CREATE  TABLE [Consulta] (
  [ConsultaId] INT IDENTITY(1,1) NOT NULL ,
  [PacienteId] INT NOT NULL ,
  [MedicoId] INT NOT NULL ,
  [Data] DATE NOT NULL ,
  [Hora] TIME NOT NULL ,
  [Status] INT NOT NULL ,
  PRIMARY KEY (ConsultaId) ,
  CONSTRAINT [Consulta_MedicoId]
    FOREIGN KEY ([MedicoId] )
    REFERENCES [Funcionario] ([FuncionarioId] )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT [Consulta_Paciente]
    FOREIGN KEY ([PacienteId] )
    REFERENCES [Paciente] ([PacienteId] )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);


CREATE  TABLE [FuncionarioTelefone] (
  [FuncionarioTelefoneId] INT IDENTITY(1,1) NOT NULL ,
  [FuncionarioId] INT NOT NULL ,
  [TelefoneId] INT NOT NULL ,
  PRIMARY KEY ([FuncionarioTelefoneId]) ,
  CONSTRAINT [FuncionarioTelefone_Telefone]
    FOREIGN KEY ([TelefoneId] )
    REFERENCES [Telefone] ([TelefoneId] )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT [FuncionarioTelefone_Funcionario]
    FOREIGN KEY ([FuncionarioId] )
    REFERENCES [Funcionario] ([FuncionarioId] )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);


CREATE  TABLE [PacienteTelefone] (
  [PacienteTelefoneId] INT IDENTITY(1,1) NOT NULL ,
  [PacienteId] INT NOT NULL ,
  [TelefoneId] INT NOT NULL ,
  PRIMARY KEY ([PacienteTelefoneId]) ,
  CONSTRAINT [PacienteTelefone_Paciente]
    FOREIGN KEY ([PacienteId] )
    REFERENCES [Paciente] ([PacienteId] )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT [PacienteTelefone_Telefone]
    FOREIGN KEY ([TelefoneId] )
    REFERENCES [Telefone] ([TelefoneId] )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);


CREATE  TABLE [PlanoSaude] (
  [PlanoSaudeId] INT IDENTITY(1,1) NOT NULL ,
  [Descricao] VARCHAR(100) NOT NULL 
  PRIMARY KEY ([PlanoSaudeId])); 