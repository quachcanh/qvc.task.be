﻿--
-- Script was generated by Devart dbForge Studio 2020 for MySQL, Version 9.0.338.0
-- Product home page: http://www.devart.com/dbforge/mysql/studio
-- Script date 2/24/2023 8:44:43 AM
-- Server version: 8.0.31
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
USE quachcanh_qvc_task;

--
-- Drop procedure `Proc_Inserts_Job`
--
DROP PROCEDURE IF EXISTS Proc_Inserts_Job;

--
-- Drop table `assign`
--
DROP TABLE IF EXISTS assign;

--
-- Drop table `company`
--
DROP TABLE IF EXISTS company;

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
-- Drop procedure `Proc_GetAll_Employee`
--
DROP PROCEDURE IF EXISTS Proc_GetAll_Employee;

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
-- Drop procedure `Proc_GetById_Job`
--
DROP PROCEDURE IF EXISTS Proc_GetById_Job;

--
-- Drop procedure `Proc_GetJob_Complete_ByID`
--
DROP PROCEDURE IF EXISTS Proc_GetJob_Complete_ByID;

--
-- Drop procedure `Proc_GetJob_OutOfDate_ByID`
--
DROP PROCEDURE IF EXISTS Proc_GetJob_OutOfDate_ByID;

--
-- Drop procedure `Proc_GetJob_Processing_ByID`
--
DROP PROCEDURE IF EXISTS Proc_GetJob_Processing_ByID;

--
-- Drop procedure `Proc_GetJob_Todo_ByID`
--
DROP PROCEDURE IF EXISTS Proc_GetJob_Todo_ByID;

--
-- Drop procedure `Proc_Insert_Job`
--
DROP PROCEDURE IF EXISTS Proc_Insert_Job;

--
-- Drop table `job`
--
DROP TABLE IF EXISTS job;

--
-- Drop procedure `Proc_GetById_Project`
--
DROP PROCEDURE IF EXISTS Proc_GetById_Project;

--
-- Drop procedure `Proc_Insert_Project`
--
DROP PROCEDURE IF EXISTS Proc_Insert_Project;

--
-- Drop table `project`
--
DROP TABLE IF EXISTS project;

--
-- Set default database
--
USE quachcanh_qvc_task;

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
AVG_ROW_LENGTH = 1489,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci,
COMMENT = 'Bảng dự án';

--
-- Create index `ProjectCode` on table `project`
--
ALTER TABLE project
ADD UNIQUE INDEX ProjectCode (ProjectCode);



--
-- Create procedure `Proc_Insert_Project`
--
CREATE 
PROCEDURE Proc_Insert_Project (IN `@ProjectID` char(36), IN `@ProjectCode` varchar(20), IN `@ProjectName` varchar(255),
IN `@DepartmentID` char(36), IN `@StartDay` date, IN `@EndDay` date, IN `@Description` text, IN `@CreatedDate` datetime, IN `@CreatedBy` varchar(100), IN `@ModifiedDate` datetime, IN `@ModifiedBy` varchar(100))
COMMENT 'Thêm mới dự án theo phòng ban'
BEGIN
  INSERT INTO project (ProjectID, ProjectCode, ProjectName, DepartmentID, StartDay, EndDay, Description, CreatedDate, CreatedBy, ModifiedDate, ModifiedBy)
    VALUES (`@ProjectID`, `@ProjectCode`, `@ProjectName`, `@DepartmentID`, `@StartDay`, `@EndDay`, `@Description`, `@CreatedDate`, `@CreatedBy`, `@ModifiedDate`, `@ModifiedBy`);
END;

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
AVG_ROW_LENGTH = 5461,
CHARACTER SET utf8mb4,
COLLATE utf8mb4_general_ci,
COMMENT = 'Bảng công việc';

--
-- Create index `JobCode` on table `job`
--
ALTER TABLE job
ADD UNIQUE INDEX JobCode (JobCode);


--
-- Create procedure `Proc_Insert_Job`
--
CREATE 
PROCEDURE Proc_Insert_Job (IN `@JobID` char(36), IN `@JobCode` varchar(20), IN `@JobName` varchar(255), IN `@ProjectID` char(36), IN `@JobStatus` tinyint, IN `@JobTag` tinyint, IN `@StartTime` datetime, IN `@EndTime` datetime, IN `@Description` text, IN `@ParentID` char(36), IN `@CreatedDate` datetime, IN `@CreatedBy` varchar(100), IN `@ModifiedDate` datetime, IN `@ModifiedBy` varchar(100))
COMMENT 'Thêm công việc'
BEGIN
  INSERT INTO job (JobID, JobCode, JobName, ProjectID, JobStatus, JobTag, StartTime, EndTime, Description, ParentID, CreatedDate, CreatedBy, ModifiedDate, ModifiedBy)
    VALUES (`@JobID`, `@JobCode`, `@JobName`, `@ProjectID`, `@JobStatus`, `@JobTag`, `@StartTime`, `@EndTime`, `@Description`, `@ParentID`, `@CreatedDate`, `@CreatedBy`, `@ModifiedDate`, `@ModifiedBy`);
END;

--
-- Create procedure `Proc_GetJob_Todo_ByID`
--
CREATE 
PROCEDURE Proc_GetJob_Todo_ByID (IN `@Id` char(36))
COMMENT 'Lấy danh sách công việc cần thực hiện theo dự án'
BEGIN
  SELECT
    *
  FROM job j
  WHERE j.ProjectID = `@Id`
  AND j.JobStatus = 0;
END;

--
-- Create procedure `Proc_GetJob_Processing_ByID`
--
CREATE 
PROCEDURE Proc_GetJob_Processing_ByID (IN `@Id` char(36))
BEGIN
  SELECT
    *
  FROM job j
  WHERE j.ProjectID = `@Id`
  AND j.JobStatus = 1;
END;

--
-- Create procedure `Proc_GetJob_OutOfDate_ByID`
--
CREATE 
PROCEDURE Proc_GetJob_OutOfDate_ByID (IN `@Id` char(36))
COMMENT 'Lấy danh sách công việc quá hạn theo id dự án'
BEGIN
  SELECT
    *
  FROM job j
  WHERE j.ProjectID = `@Id`
  AND j.EndTime < CURDATE()
  AND j.JobStatus <> 2;
END;

--
-- Create procedure `Proc_GetJob_Complete_ByID`
--
CREATE 
PROCEDURE Proc_GetJob_Complete_ByID (IN `@Id` char(36))
COMMENT 'Lấy danh sách công việc đã hoàn thành theo dự án'
BEGIN
  SELECT
    *
  FROM job j
  WHERE j.ProjectID = `@Id`
  AND j.JobStatus = 2;
END;

--
-- Create procedure `Proc_GetById_Job`
--
CREATE 
PROCEDURE Proc_GetById_Job (IN `@Id` char(36))
COMMENT 'Lấy danh sách công việc theo id'
BEGIN
  SELECT
    *
  FROM job j
  WHERE j.ProjectID = `@Id`
  ORDER BY j.CreatedDate ASC;
END ;

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
AVG_ROW_LENGTH = 16384,
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
-- Create procedure `Proc_GetAll_Employee`
--
CREATE 
PROCEDURE Proc_GetAll_Employee ()
BEGIN
  SELECT
    *
  FROM employee e;
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
AVG_ROW_LENGTH = 4096,
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
  FROM department d
  ORDER BY d.CreatedDate ASC;
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
-- Create procedure `Proc_Inserts_Job`
--
CREATE 
PROCEDURE Proc_Inserts_Job (IN `@Values` text)
COMMENT 'Thêm mới nhiều công việc'
BEGIN
  SET @columnInput = 'INSERT INTO job (JobID, JobCode, JobName, ProjectID, JobStatus, JobTag, StartTime, EndTime, Description, ParentID, CreatedDate, CreatedBy, ModifiedDate, ModifiedBy) VALUES ';
  SET @filterQuery = CONCAT(@columnInput, `@Values`);
  PREPARE filterQuery FROM @filterQuery;
  EXECUTE filterQuery;
END;

-- 
-- Dumping data for table role
--
-- Table quachcanh_qvc_task.role does not contain any data (it is empty)

-- 
-- Dumping data for table project
--
INSERT INTO project VALUES
('00000000-0000-0000-0000-000000000000', 'PR54886', 'Công việc cá nhân', '8d5ca0b7-4e6a-4fac-afbb-679d15acd36b', '2023-02-19', '2023-02-21', '123123', '2023-02-19 21:51:06', NULL, '2023-02-19 21:51:06', NULL),
('001e6977-92e2-42e7-9056-f83715cd7523', 'PR96307', 'sfhsfsdfds', '7826309a-c87e-4a50-bcab-f221f2b9ea3f', '0001-01-01', '0001-01-01', NULL, '2023-02-23 02:48:15', NULL, '2023-02-23 02:48:15', NULL),
('0d3b9ac4-88a6-42a0-b674-a0698993774c', 'PR20453', 'Đồ án tốt nghiệp', '9c1c57b1-947a-422f-bbce-201166adc958', '2023-02-22', '2023-02-24', 'Phải xong. huhu', '2023-02-22 22:22:49', NULL, '2023-02-22 22:22:49', NULL),
('3f7c8d86-02d0-4e0e-b045-30d912d785dd', 'PR75514', 'CUKCUK', '7826309a-c87e-4a50-bcab-f221f2b9ea3f', '2023-02-22', '2023-02-25', NULL, '2023-02-22 21:37:09', NULL, '2023-02-22 21:37:09', NULL),
('44334c51-0a15-4bc2-981c-127d58d220e0', 'PR54832', 'sdfsdfsdfdsf', '9c1c57b1-947a-422f-bbce-201166adc958', '0001-01-01', '0001-01-01', NULL, '2023-02-23 02:48:20', NULL, '2023-02-23 02:48:20', NULL),
('6455e633-397c-4f65-a89e-2c4bea5511ba', 'PR41630', 'hsfsfsdfsdf', '9c1c57b1-947a-422f-bbce-201166adc958', '0001-01-01', '0001-01-01', NULL, '2023-02-23 02:48:27', NULL, '2023-02-23 02:48:27', NULL),
('65bcead1-04bb-4c32-8c60-e325090b4812', 'PR34970', 'sdfsdfsdfsdfs', '9c1c57b1-947a-422f-bbce-201166adc958', '0001-01-01', '0001-01-01', NULL, '2023-02-23 02:48:24', NULL, '2023-02-23 02:48:24', NULL),
('66b3dc4f-924e-4abd-9c70-955e894794f8', 'PR05370', 'fdgdfg', '4a28ade9-bde1-49e0-81dd-84fe0957e009', '0001-01-01', '0001-01-01', NULL, '2023-02-23 03:06:22', NULL, '2023-02-23 03:06:22', NULL),
('6bce991c-df8f-489c-85cb-bfe431118e1c', 'PR06644', 'sdfsdfsdfsdf', '8d5ca0b7-4e6a-4fac-afbb-679d15acd36b', '0001-01-01', '0001-01-01', NULL, '2023-02-23 02:48:11', NULL, '2023-02-23 02:48:11', NULL),
('94744585-bcf6-4711-ac32-ede30ce7b6c9', 'PR45377', 'sdfsdfsdf', '9c1c57b1-947a-422f-bbce-201166adc958', '0001-01-01', '0001-01-01', NULL, '2023-02-23 02:48:18', NULL, '2023-02-23 02:48:18', NULL),
('a1fa93c8-2d82-4800-aaa1-91b330dd0eaa', 'PR42598', 'dfsdfsdf', '8d5ca0b7-4e6a-4fac-afbb-679d15acd36b', '0001-01-01', '0001-01-01', NULL, '2023-02-23 02:48:08', NULL, '2023-02-23 02:48:08', NULL);

-- 
-- Dumping data for table position
--
-- Table quachcanh_qvc_task.position does not contain any data (it is empty)

-- 
-- Dumping data for table job
--
INSERT INTO job VALUES
('01490574-3011-485c-a674-f962d13b5505', 'JB00705', 'dfsf', 'a1fa93c8-2d82-4800-aaa1-91b330dd0eaa', 0, 0, '0001-01-01 00:00:00', '0001-01-01 00:00:00', NULL, 'e55b42bc-66d7-4b46-ace3-a8f138e347fc', '2023-02-23 22:23:24', NULL, '2023-02-23 22:23:24', NULL),
('04b79acf-d296-4acc-b0ea-8844fed1028a', 'JB96997', 'cv1', '0d3b9ac4-88a6-42a0-b674-a0698993774c', 1, 0, '0001-01-01 00:00:00', '0001-01-01 00:00:00', NULL, '3cad3c59-485d-4c1e-98dd-44a2b72fe0d5', '2023-02-22 22:38:40', NULL, '2023-02-22 22:38:40', NULL),
('306e90dc-f2f1-4719-a6d5-61ec430a6a59', 'JB66506', 'Cv12334', '00000000-0000-0000-0000-000000000000', 1, 0, '2023-02-25 15:24:00', '2023-03-11 15:24:00', NULL, '00000000-0000-0000-0000-000000000000', '2023-02-23 22:21:02', NULL, '2023-02-23 22:21:02', NULL),
('3cad3c59-485d-4c1e-98dd-44a2b72fe0d5', 'JB87886', 'dsfgfdsdfsdf', '0d3b9ac4-88a6-42a0-b674-a0698993774c', 2, 1, '2023-02-22 15:40:00', '1999-12-31 17:00:00', 'sdfsdfsdf', '00000000-0000-0000-0000-000000000000', '2023-02-22 22:38:40', NULL, '2023-02-22 22:38:40', NULL),
('3fa2d473-933d-4abf-aa03-6317af5f672b', 'JB72472', 'cv12', '00000000-0000-0000-0000-000000000000', 0, 0, '0001-01-01 00:00:00', '0001-01-01 00:00:00', NULL, '306e90dc-f2f1-4719-a6d5-61ec430a6a59', '2023-02-23 22:21:02', NULL, '2023-02-23 22:21:02', NULL),
('6256b95c-e1bd-44b9-aba6-a8e258a03213', 'JB72480', 'ssdf', '00000000-0000-0000-0000-000000000000', 0, 0, '0001-01-01 00:00:00', '0001-01-01 00:00:00', NULL, '306e90dc-f2f1-4719-a6d5-61ec430a6a59', '2023-02-23 22:21:02', NULL, '2023-02-23 22:21:02', NULL),
('6640aaeb-d0fe-4485-abd2-956d7c79d552', 'JB92798', 'cv2', '0d3b9ac4-88a6-42a0-b674-a0698993774c', 0, 0, '0001-01-01 00:00:00', '0001-01-01 00:00:00', NULL, '3cad3c59-485d-4c1e-98dd-44a2b72fe0d5', '2023-02-22 22:38:40', NULL, '2023-02-22 22:38:40', NULL),
('69790501-a84f-4dfe-acea-4ed6f0af60ab', 'JB34926', 'xvrdf', '00000000-0000-0000-0000-000000000000', 0, 0, '0001-01-01 00:00:00', '0001-01-01 00:00:00', NULL, '306e90dc-f2f1-4719-a6d5-61ec430a6a59', '2023-02-23 22:21:02', NULL, '2023-02-23 22:21:02', NULL),
('8257c869-122e-44e0-8697-9dfa2c6c7096', 'JB81171', 'cv12', '94744585-bcf6-4711-ac32-ede30ce7b6c9', 0, 0, '0001-01-01 00:00:00', '0001-01-01 00:00:00', NULL, 'a8c3432f-faf2-4437-b4df-4c989e7d31f2', '2023-02-23 22:19:32', NULL, '2023-02-23 22:19:32', NULL),
('90f34f2f-5541-48f9-aeed-00c7c9fc9001', 'JB25200', 'CV1', '66b3dc4f-924e-4abd-9c70-955e894794f8', 1, 1, '2023-02-08 14:23:00', '2023-02-25 14:24:00', '123', '00000000-0000-0000-0000-000000000000', '2023-02-23 21:21:26', NULL, '2023-02-23 21:21:26', NULL),
('a8c3432f-faf2-4437-b4df-4c989e7d31f2', 'JB78744', 'VC123', '94744585-bcf6-4711-ac32-ede30ce7b6c9', 0, 0, '2023-02-23 15:19:00', '2023-03-04 15:23:00', NULL, '00000000-0000-0000-0000-000000000000', '2023-02-23 22:19:32', NULL, '2023-02-23 22:19:32', NULL),
('b489164a-ef62-40a4-87e1-9ae6e71b7060', 'JB31641', 'sg', '00000000-0000-0000-0000-000000000000', 0, 0, '0001-01-01 00:00:00', '0001-01-01 00:00:00', NULL, '306e90dc-f2f1-4719-a6d5-61ec430a6a59', '2023-02-23 22:21:02', NULL, '2023-02-23 22:21:02', NULL),
('e55b42bc-66d7-4b46-ace3-a8f138e347fc', 'JB71688', 'dfgdfgdf', 'a1fa93c8-2d82-4800-aaa1-91b330dd0eaa', 0, 0, '2023-02-14 15:24:00', '2023-02-25 03:22:00', NULL, '00000000-0000-0000-0000-000000000000', '2023-02-23 22:23:24', NULL, '2023-02-23 22:23:24', NULL);

-- 
-- Dumping data for table employee
--
INSERT INTO employee VALUES
('7ef92324-82cb-488d-89f4-e6bd075ce807', 'quachcanh', 'Quách Cảnh', 'quachcanh', '123qwe', 1, '1999-06-01', 1, 'canhquach45@gmail.com', '0973137312', NULL, NULL, NULL, NULL, '2023-02-19 18:52:55', NULL, '2023-02-19 18:52:55', NULL, 0);

-- 
-- Dumping data for table department
--
INSERT INTO department VALUES
('4a28ade9-bde1-49e0-81dd-84fe0957e009', 'DP59026', 'Ahihi', '00000000-0000-0000-0000-000000000000', '00000000-0000-0000-0000-000000000000', '2023-02-19 22:53:49', NULL, '2023-02-19 22:53:49', NULL),
('7826309a-c87e-4a50-bcab-f221f2b9ea3f', 'DP15751', 'MISA', '00000000-0000-0000-0000-000000000000', '00000000-0000-0000-0000-000000000000', '2023-02-19 18:53:23', NULL, '2023-02-19 18:53:23', NULL),
('8d5ca0b7-4e6a-4fac-afbb-679d15acd36b', 'CANHAN', 'Cá nhân', NULL, NULL, '2023-02-19 18:52:55', 'Quách Cảnh', '2023-02-19 18:52:55', ''),
('9c1c57b1-947a-422f-bbce-201166adc958', 'DP85456', 'HUMG', '00000000-0000-0000-0000-000000000000', '00000000-0000-0000-0000-000000000000', '2023-02-19 18:53:36', NULL, '2023-02-19 18:53:36', NULL);

-- 
-- Dumping data for table company
--
-- Table quachcanh_qvc_task.company does not contain any data (it is empty)

-- 
-- Dumping data for table assign
--
-- Table quachcanh_qvc_task.assign does not contain any data (it is empty)

-- 
-- Restore previous SQL mode
-- 
/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;

-- 
-- Enable foreign keys
-- 
/*!40014 SET FOREIGN_KEY_CHECKS = @OLD_FOREIGN_KEY_CHECKS */;