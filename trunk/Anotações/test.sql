
-- -----------------------------------------------------
-- Table `mydb`.`Funcionario`
-- -----------------------------------------------------
CREATE  TABLE [Funcionario] (
  [idFuncionario] INT NOT NULL ,
  [Nome] VARCHAR(100) NULL ,
  [RG] VARCHAR(11) NULL ,
  [UserId] INT NULL ,
  [endereco] VARCHAR(150) NULL ,
  [funcao] INT NULL ,
  PRIMARY KEY ([idFuncionario]) ,
  CONSTRAINT [UserId]
    FOREIGN KEY ([UserId] )
    REFERENCES [dbo].[aspnet_Users] ([UserId])
    ON UPDATE NO ACTION);


-- -----------------------------------------------------
-- Table `mydb`.`Telefone`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `mydb`.`Telefone` (
  `idTelefone` INT NOT NULL ,
  `Numero` VARCHAR(15) NULL ,
  `Tipo` INT NULL ,
  PRIMARY KEY (`idTelefone`) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Especialidade`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `mydb`.`Especialidade` (
  `idEspecialidade` INT NOT NULL ,
  `Descricao` VARCHAR(45) NULL ,
  PRIMARY KEY (`idEspecialidade`) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Funcionario_Especialidade`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `mydb`.`Funcionario_Especialidade` (
  `idFuncionario_Especialidade` INT NOT NULL ,
  `idFuncionario` INT NULL ,
  `idEspecialidade` INT NULL ,
  PRIMARY KEY (`idFuncionario_Especialidade`) ,
  INDEX `Pessoa` (`idFuncionario` ASC) ,
  INDEX `Especialidade` (`idEspecialidade` ASC) ,
  CONSTRAINT `Pessoa`
    FOREIGN KEY (`idFuncionario` )
    REFERENCES `mydb`.`Funcionario` (`idFuncionario` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `Especialidade`
    FOREIGN KEY (`idEspecialidade` )
    REFERENCES `mydb`.`Especialidade` (`idEspecialidade` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Paciente`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `mydb`.`Paciente` (
  `idPaciente` INT NOT NULL ,
  `Nome` VARCHAR(45) NULL ,
  `Endereco` VARCHAR(45) NULL ,
  `ObservacaoMedico` VARCHAR(45) NULL ,
  PRIMARY KEY (`idPaciente`) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Consulta`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `mydb`.`Consulta` (
  `idConsulta` INT NOT NULL ,
  `PacienteId` INT NULL ,
  `MedicoId` INT NULL ,
  `Data` DATE NULL ,
  `Hora` TIME NULL ,
  `Status` INT NULL ,
  PRIMARY KEY (`idConsulta`) ,
  INDEX `Pessoa` (`MedicoId` ASC) ,
  INDEX `Paciente` (`PacienteId` ASC) ,
  CONSTRAINT `Pessoa`
    FOREIGN KEY (`MedicoId` )
    REFERENCES `mydb`.`Funcionario` (`idFuncionario` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `Paciente`
    FOREIGN KEY (`PacienteId` )
    REFERENCES `mydb`.`Paciente` (`idPaciente` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Funcionario_Telefone`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `mydb`.`Funcionario_Telefone` (
  `idFuncionario_Telefone` INT NOT NULL ,
  `idFuncionario` INT NULL ,
  `idTelefone` INT NULL ,
  PRIMARY KEY (`idFuncionario_Telefone`) ,
  INDEX `Telefone` (`idTelefone` ASC) ,
  INDEX `Funcionario` (`idFuncionario` ASC) ,
  CONSTRAINT `Telefone`
    FOREIGN KEY (`idTelefone` )
    REFERENCES `mydb`.`Telefone` (`idTelefone` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `Funcionario`
    FOREIGN KEY (`idFuncionario` )
    REFERENCES `mydb`.`Funcionario` (`idFuncionario` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Paciente_Telefone`
-- -----------------------------------------------------
CREATE  TABLE IF NOT EXISTS `mydb`.`Paciente_Telefone` (
  `idPaciente_Telefone` INT NOT NULL ,
  `idPaciente` INT NULL ,
  `idTelefone` INT NULL ,
  PRIMARY KEY (`idPaciente_Telefone`) ,
  INDEX `Paciente` (`idPaciente` ASC) ,
  INDEX `Telefone` (`idTelefone` ASC) ,
  CONSTRAINT `Paciente`
    FOREIGN KEY (`idPaciente` )
    REFERENCES `mydb`.`Paciente` (`idPaciente` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `Telefone`
    FOREIGN KEY (`idTelefone` )
    REFERENCES `mydb`.`Telefone` (`idTelefone` )
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;



SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
