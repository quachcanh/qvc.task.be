﻿--
-- Script was generated by Devart dbForge Studio 2020 for MySQL, Version 9.0.338.0
-- Product home page: http://www.devart.com/dbforge/mysql/studio
-- Script date 18/2/2023 4:03:19 PM
-- Server version: 8.0.32
-- Client version: 4.1
--

-- 
-- Disable foreign keys
-- 
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;

-- 
-- Set SQL mode
-- 
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

-- 
-- Set character set the client will use to send SQL statements to the server
--
SET NAMES 'utf8';

--
-- Set default database
--
USE `qvcanh_qvc-task`;

--
-- Drop table `assign`
--
DROP TABLE IF EXISTS assign;

--
-- Drop table `company`
--
DROP TABLE IF EXISTS company;

--
-- Drop table `job`
--
DROP TABLE IF EXISTS job;

--
-- Drop table `position`
--
DROP TABLE IF EXISTS position;

--
-- Drop table `role`
--
DROP TABLE IF EXISTS role;

--
-- Drop procedure `Proc_GetAll_Department`
--
DROP PROCEDURE IF EXISTS Proc_GetAll_Department;

--
-- Drop procedure `Proc_Insert_Department`
--
DROP PROCEDURE IF EXISTS Proc_Insert_Department;

--
-- Drop table `department`
--
DROP TABLE IF EXISTS department;

--
-- Drop procedure `Proc_GetById_Project`
--
DROP PROCEDURE IF EXISTS Proc_GetById_Project;

--
-- Drop table `project`
--
DROP TABLE IF EXISTS project;

--
-- Drop procedure `Proc_GetByUserPass_Employee`
--
DROP PROCEDURE IF EXISTS Proc_GetByUserPass_Employee;

--
-- Drop procedure `Proc_Insert_Employee`
--
DROP PROCEDURE IF EXISTS Proc_Insert_Employee;

--
-- Drop table `employee`
--
DROP TABLE IF EXISTS employee;

--
-- Set default database
--
USE `qvcanh_qvc-task`;

--
-- Create table `employee`
--
CREATE TABLE employee (
  EmployeeID char(36) NOT NULL DEFAULT '',
  EmployeeCode varchar(20) NOT NULL DEFAULT '',
  EmployeeName varchar(100) NOT NULL DEFAULT '',
  UserName varchar(128) NOT NULL DEFAULT '',
  Password varchar(128) NOT NULL DEFAULT '',
  AccountType tinyint DEFAULT NULL COMMENT 'Trạng thái tài khoản; 0: Ngưng sử dụng; 1: Đang sử dụng',
  DateOfBirth date DEFAULT NULL,
  Gender tinyint DEFAULT NULL COMMENT 'Giới tính; 0: Nữ; 1: Nam; 2: Khác',
  Email varchar(100) DEFAULT NULL,
  PhoneNumber varchar(50) DEFAULT NULL,
  Address varchar(255) DEFAULT NULL,
  CompanyID char(36) DEFAULT NULL,
  DepartmentID char(36) DEFAULT NULL,
  PositionID char(36) DEFAULT NULL,
  CreatedDate datetime DEFAULT NULL,
  CreatedBy varchar(100) DEFAULT NULL,
  ModifiedDate datetime DEFAULT NULL,
  ModifiedBy varchar(100) DEFAULT NULL,
  Code int DEFAULT NULL,
  PRIMARY KEY (EmployeeID)
)
ENGINE = INNODB,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci,
COMMENT = 'Bảng thông tin nhân viên';

--
-- Create index `Email` on table `employee`
--
ALTER TABLE employee
ADD UNIQUE INDEX Email (Email);

--
-- Create index `EmployeeCode` on table `employee`
--
ALTER TABLE employee
ADD UNIQUE INDEX EmployeeCode (EmployeeCode);

--
-- Create index `UserName` on table `employee`
--
ALTER TABLE employee
ADD UNIQUE INDEX UserName (UserName);



--
-- Create procedure `Proc_Insert_Employee`
--
CREATE 
PROCEDURE Proc_Insert_Employee (IN `@EmployeeID` char(36), IN `@EmployeeCode` varchar(20), IN `@EmployeeName` varchar(100), IN `@UserName` varchar(128), IN `@Password` varchar(128), IN `@AccountType` tinyint, IN `@DateOfBirth` date, IN `@Gender` tinyint, IN `@Email` varchar(100), IN `@PhoneNumber` varchar(50), IN `@Address` varchar(255), IN `@CompanyID` char(36), IN `@DepartmentID` char(36), IN `@PositionID` char(36), IN `@CreatedDate` datetime, IN `@CreatedBy` varchar(100), IN `@ModifiedDate` datetime, IN `@ModifiedBy` varchar(100), IN `@Code` int)
BEGIN
  INSERT INTO employee (EmployeeID, EmployeeCode, EmployeeName, UserName, Password, AccountType, DateOfBirth, Gender, Email, PhoneNumber, Address, CompanyID, DepartmentID, PositionID, CreatedDate, CreatedBy, ModifiedDate, ModifiedBy, Code)
    VALUES (`@EmployeeID`, `@EmployeeCode`, `@EmployeeName`, `@UserName`, `@Password`, `@AccountType`, `@DateOfBirth`, `@Gender`, `@Email`, `@PhoneNumber`, `@Address`, `@CompanyID`, `@DepartmentID`, `@PositionID`, `@CreatedDate`, `@CreatedBy`, `@ModifiedDate`, `@ModifiedBy`, `@Code`);
END;

--
-- Create procedure `Proc_GetByUserPass_Employee`
--
CREATE 
PROCEDURE Proc_GetByUserPass_Employee (IN `@UserName` varchar(128), IN `@Email` varchar(100), IN `@Password` varchar(128))
COMMENT 'Kiểm tra đăng nhập'
BEGIN
  SELECT
    *
  FROM employee e
  WHERE (e.UserName = `@UserName`
  AND e.Password = `@Password`)
  OR (e.Email = `@Email`
  AND e.Password = `@Password`);
END;

--
-- Create table `project`
--
CREATE TABLE project (
  ProjectID char(36) NOT NULL DEFAULT '',
  ProjectCode varchar(20) NOT NULL DEFAULT '',
  ProjectName varchar(255) NOT NULL DEFAULT '',
  DepartmentID char(36) NOT NULL DEFAULT '',
  StartDay date DEFAULT NULL,
  EndDay date DEFAULT NULL,
  Description text DEFAULT NULL,
  CreatedDate datetime DEFAULT NULL,
  CreatedBy varchar(100) DEFAULT NULL,
  ModifiedDate datetime DEFAULT NULL,
  ModifiedBy varchar(100) DEFAULT NULL,
  PRIMARY KEY (ProjectID)
)
ENGINE = INNODB,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci,
COMMENT = 'Bảng dự án';

--
-- Create index `ProjectCode` on table `project`
--
ALTER TABLE project
ADD UNIQUE INDEX ProjectCode (ProjectCode);



--
-- Create procedure `Proc_GetById_Project`
--
CREATE 
PROCEDURE Proc_GetById_Project (IN `@Id` char(36))
COMMENT 'Lấy danh sách dự án theo id phòng ban'
BEGIN
  SELECT
    *
  FROM project p
  WHERE p.DepartmentID = `@Id`
  ORDER BY p.CreatedDate ASC;
END;

--
-- Create table `department`
--
CREATE TABLE department (
  DepartmentID char(36) NOT NULL DEFAULT '',
  DepartmentCode varchar(20) NOT NULL DEFAULT '',
  DepartmentName varchar(255) NOT NULL DEFAULT '',
  CompanyID char(36) DEFAULT '',
  ParentID char(36) DEFAULT NULL,
  CreatedDate datetime DEFAULT NULL,
  CreatedBy varchar(100) DEFAULT NULL,
  ModifiedDate datetime DEFAULT NULL,
  ModifiedBy varchar(100) DEFAULT NULL,
  PRIMARY KEY (DepartmentID)
)
ENGINE = INNODB,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci,
COMMENT = 'Bảng phòng ban';

--
-- Create index `DepartmentCode` on table `department`
--
ALTER TABLE department
ADD UNIQUE INDEX DepartmentCode (DepartmentCode);



--
-- Create procedure `Proc_Insert_Department`
--
CREATE 
PROCEDURE Proc_Insert_Department (IN `@DepartmentID` char(36), IN `@DepartmentCode` varchar(20), IN `@DepartmentName` varchar(255), IN `@CompanyID` char(36), IN `@ParentID` char(36), IN `@CreatedDate` datetime, IN `@CreatedBy` varchar(100), IN `@ModifiedDate` datetime, IN `@ModifiedBy` varchar(100))
COMMENT 'Thêm mới phòng ban'
BEGIN
  INSERT INTO department (DepartmentID, DepartmentCode, DepartmentName, CompanyID, ParentID, CreatedDate, CreatedBy, ModifiedDate, ModifiedBy)
    VALUES (`@DepartmentID`, `@DepartmentCode`, `@DepartmentName`, `@CompanyID`, `@ParentID`, `@CreatedDate`, `@CreatedBy`, `@ModifiedDate`, `@ModifiedBy`);
END;

--
-- Create procedure `Proc_GetAll_Department`
--
CREATE 
PROCEDURE Proc_GetAll_Department ()
COMMENT 'Lất tất cả danh sách phòng ban'
BEGIN
  SELECT
    *
  FROM department d;
END;

--
-- Create table `role`
--
CREATE TABLE role (
  RoleID varchar(255) NOT NULL DEFAULT '',
  Access tinyint NOT NULL,
  Description text DEFAULT NULL,
  CreatedDate datetime DEFAULT NULL,
  CreatedBy varchar(100) DEFAULT NULL,
  ModifiedDate datetime DEFAULT NULL,
  ModifiedBy varchar(100) DEFAULT NULL,
  PRIMARY KEY (RoleID)
)
ENGINE = INNODB,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci,
COMMENT = 'Bảng phân quyền';

--
-- Create table `position`
--
CREATE TABLE position (
  PositionID char(36) NOT NULL DEFAULT '',
  PositionCode varchar(20) NOT NULL DEFAULT '',
  PositionName varchar(100) NOT NULL DEFAULT '',
  CreatedDate datetime DEFAULT NULL,
  CreatedBy varchar(100) DEFAULT NULL,
  ModifiedDate datetime DEFAULT NULL,
  ModifiedBy varchar(100) DEFAULT NULL,
  PRIMARY KEY (PositionID)
)
ENGINE = INNODB,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci,
COMMENT = 'Bảng vị trí công việc';

--
-- Create index `PositionCode` on table `position`
--
ALTER TABLE position
ADD UNIQUE INDEX PositionCode (PositionCode);

--
-- Create table `job`
--
CREATE TABLE job (
  JobID char(36) NOT NULL DEFAULT '',
  JobCode varchar(20) NOT NULL DEFAULT '',
  JobName varchar(255) NOT NULL DEFAULT '',
  ProjectID char(36) NOT NULL DEFAULT '',
  JobStatus tinyint DEFAULT NULL COMMENT 'Trạng thái công việc; 0: Cần thực hiện; 1: Đang thực hiện; 2: Đã hoàn thành;',
  JobTag tinyint DEFAULT NULL COMMENT 'Tag công việc; 0: Quan trọng; 1: Khần cấp',
  StartTime datetime DEFAULT NULL,
  EndTime datetime DEFAULT NULL,
  Description text DEFAULT NULL,
  ParentID char(36) DEFAULT NULL,
  CreatedDate datetime DEFAULT NULL,
  CreatedBy varchar(100) DEFAULT NULL,
  ModifiedDate datetime DEFAULT NULL,
  ModifiedBy varchar(100) DEFAULT NULL,
  PRIMARY KEY (JobID)
)
ENGINE = INNODB,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci,
COMMENT = 'Bảng công việc';

--
-- Create index `JobCode` on table `job`
--
ALTER TABLE job
ADD UNIQUE INDEX JobCode (JobCode);

--
-- Create table `company`
--
CREATE TABLE company (
  CompanyID char(36) NOT NULL DEFAULT '',
  CompanyCode varchar(128) NOT NULL DEFAULT '',
  CompanyName varchar(255) NOT NULL DEFAULT '',
  Email varchar(100) DEFAULT NULL,
  CreatedDate datetime DEFAULT NULL,
  CreatedBy varchar(100) DEFAULT NULL,
  ModifiedDate datetime DEFAULT NULL,
  ModifiedBy varchar(100) DEFAULT NULL,
  PRIMARY KEY (CompanyID)
)
ENGINE = INNODB,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci,
COMMENT = 'Bảng tên công ty';

--
-- Create index `CompanyCode` on table `company`
--
ALTER TABLE company
ADD UNIQUE INDEX CompanyCode (CompanyCode);

--
-- Create table `assign`
--
CREATE TABLE assign (
  AssignID char(36) NOT NULL DEFAULT '',
  EmployeeID char(36) NOT NULL DEFAULT '',
  JobID char(36) NOT NULL DEFAULT '',
  Description text DEFAULT NULL,
  CreatedDate datetime DEFAULT NULL,
  CreatedBy varchar(100) DEFAULT NULL,
  ModifiedDate datetime DEFAULT NULL,
  ModifiedBy varchar(100) DEFAULT NULL,
  PRIMARY KEY (AssignID)
)
ENGINE = INNODB,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci,
COMMENT = 'Bảng giao việc';

-- 
-- Dumping data for table role
--
-- Table `qvcanh_qvc-task`.role does not contain any data (it is empty)

-- 
-- Dumping data for table project
--
-- Table `qvcanh_qvc-task`.project does not contain any data (it is empty)

-- 
-- Dumping data for table position
--
-- Table `qvcanh_qvc-task`.position does not contain any data (it is empty)

-- 
-- Dumping data for table job
--
-- Table `qvcanh_qvc-task`.job does not contain any data (it is empty)

-- 
-- Dumping data for table employee
--
-- Table `qvcanh_qvc-task`.employee does not contain any data (it is empty)

-- 
-- Dumping data for table department
--
-- Table `qvcanh_qvc-task`.department does not contain any data (it is empty)

-- 
-- Dumping data for table company
--
-- Table `qvcanh_qvc-task`.company does not contain any data (it is empty)

-- 
-- Dumping data for table assign
--
-- Table `qvcanh_qvc-task`.assign does not contain any data (it is empty)

-- 
-- Restore previous SQL mode
-- 
/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;

-- 
-- Enable foreign keys
-- 
/*!40014 SET FOREIGN_KEY_CHECKS = @OLD_FOREIGN_KEY_CHECKS */;