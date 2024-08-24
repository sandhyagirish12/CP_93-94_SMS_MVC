USE [master]
GO
/****** Object:  Database [SalaryManagement]    Script Date: 24-08-2024 12:29:44 ******/
CREATE DATABASE [SalaryManagement]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SalaryManagement', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\SalaryManagement.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SalaryManagement_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\SalaryManagement_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [SalaryManagement] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SalaryManagement].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SalaryManagement] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SalaryManagement] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SalaryManagement] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SalaryManagement] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SalaryManagement] SET ARITHABORT OFF 
GO
ALTER DATABASE [SalaryManagement] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SalaryManagement] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SalaryManagement] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SalaryManagement] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SalaryManagement] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SalaryManagement] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SalaryManagement] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SalaryManagement] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SalaryManagement] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SalaryManagement] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SalaryManagement] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SalaryManagement] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SalaryManagement] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SalaryManagement] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SalaryManagement] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SalaryManagement] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SalaryManagement] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SalaryManagement] SET RECOVERY FULL 
GO
ALTER DATABASE [SalaryManagement] SET  MULTI_USER 
GO
ALTER DATABASE [SalaryManagement] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SalaryManagement] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SalaryManagement] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SalaryManagement] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SalaryManagement] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [SalaryManagement] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'SalaryManagement', N'ON'
GO
ALTER DATABASE [SalaryManagement] SET QUERY_STORE = ON
GO
ALTER DATABASE [SalaryManagement] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [SalaryManagement]
GO
/****** Object:  Table [dbo].[AdminData]    Script Date: 24-08-2024 12:29:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdminData](
	[AdminId] [int] NOT NULL,
	[Fname] [varchar](50) NULL,
	[Lname] [varchar](50) NULL,
	[Username] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[Email] [varchar](50) NULL,
	[Created_at] [datetime] NULL,
	[Deleted_at] [datetime] NULL,
	[Updated_at] [datetime] NULL,
 CONSTRAINT [PK_AdminData] PRIMARY KEY CLUSTERED 
(
	[AdminId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Deduction]    Script Date: 24-08-2024 12:29:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Deduction](
	[DeductionId] [int] NOT NULL,
	[Dname] [varchar](50) NULL,
	[Percentage] [decimal](18, 0) NULL,
	[Amount] [decimal](18, 0) NULL,
 CONSTRAINT [PK_Deduction] PRIMARY KEY CLUSTERED 
(
	[DeductionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeeData]    Script Date: 24-08-2024 12:29:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeData](
	[EmployeeId] [varchar](50) NOT NULL,
	[Fname] [varchar](50) NULL,
	[Lname] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[Password] [varchar](50) NULL,
	[Gender] [varchar](10) NULL,
	[DOB] [varchar](10) NULL,
	[Age] [int] NULL,
	[PlaceofBirth] [varchar](50) NULL,
	[Bloodgroup] [varchar](10) NULL,
	[Fathername] [varchar](50) NULL,
	[Mothername] [varchar](50) NULL,
	[Spousename] [varchar](50) NULL,
	[Joiningdate] [varchar](50) NULL,
	[Department] [varchar](50) NULL,
	[Designation] [varchar](50) NULL,
	[Basicpay] [decimal](18, 0) NULL,
	[Gradepay] [decimal](18, 0) NULL,
	[Increment] [decimal](18, 0) NULL,
	[HRA] [decimal](18, 0) NULL,
	[DA] [decimal](18, 0) NULL,
	[Accountno] [varchar](50) NULL,
	[IFSC] [varchar](50) NULL,
	[Bankname] [varchar](50) NULL,
	[PAddress] [varchar](100) NULL,
	[CAddress] [varchar](100) NULL,
	[Contactno] [varchar](25) NULL,
	[Remarks] [varchar](100) NULL,
	[Created_at] [datetime] NULL,
	[Deleted_at] [datetime] NULL,
	[Updated_at] [datetime] NULL,
 CONSTRAINT [PK_EmployeeData] PRIMARY KEY CLUSTERED 
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LeaveDetails]    Script Date: 24-08-2024 12:29:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LeaveDetails](
	[LeaveId] [int] NOT NULL,
	[LType] [varchar](20) NULL,
	[Description] [nchar](200) NULL,
	[EmployeeId] [varchar](50) NOT NULL,
	[FromDate] [date] NULL,
	[Todate] [date] NULL,
	[Status] [varchar](50) NULL,
	[LeavesTaken] [int] NULL,
	[LeavesRemaining] [int] NULL,
 CONSTRAINT [PK_LeaveDetails] PRIMARY KEY CLUSTERED 
(
	[LeaveId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoanDetails]    Script Date: 24-08-2024 12:29:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoanDetails](
	[LoanId] [int] NOT NULL,
	[EmployeeId] [varchar](50) NULL,
	[LoanNo] [varchar](50) NULL,
	[LType] [varchar](50) NULL,
	[BankName] [varchar](50) NULL,
	[IFSC] [varchar](50) NULL,
	[Installment] [decimal](18, 0) NULL,
	[StartDate] [date] NULL,
	[Tenure] [int] NULL,
	[Description] [varchar](100) NULL,
	[Status] [varchar](25) NULL,
 CONSTRAINT [PK_LoanDetails] PRIMARY KEY CLUSTERED 
(
	[LoanId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payroll]    Script Date: 24-08-2024 12:29:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payroll](
	[PayrollId] [int] NOT NULL,
	[EmployeeId] [varchar](50) NULL,
	[Month] [varchar](10) NULL,
	[Year] [varchar](10) NULL,
	[DeductionId] [int] NULL,
	[AllowanceId] [int] NULL,
	[LeaveId] [int] NULL,
	[LoanId] [int] NULL,
	[NetSalary] [decimal](18, 0) NULL,
 CONSTRAINT [PK_Payroll] PRIMARY KEY CLUSTERED 
(
	[PayrollId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [SalaryManagement] SET  READ_WRITE 
GO
