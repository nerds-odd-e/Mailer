[String]$dbname = "MailerDB";

# Open ADO.NET Connection with Windows authentification to local SQLEXPRESS.
$con = New-Object Data.SqlClient.SqlConnection;
$con.ConnectionString = "Data Source=.\SQLEXPRESS;Initial Catalog=master;Integrated Security=True;";
$con.Open();


# Create the database.
$sql = "IF  NOT EXISTS (SELECT * FROM sys.databases WHERE name = '$dbname')
    BEGIN
        CREATE DATABASE [$dbname]
    END;"
$cmd = New-Object Data.SqlClient.SqlCommand $sql, $con;
$cmd.ExecuteNonQuery();		
Write-Host "Database $dbname is created!";
$sql = "
use $dbname  
IF  NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Contact')
    BEGIN
        CREATE Table Contact (ID int IDENTITY(1,1) PRIMARY KEY, Email VARCHAR(50) not null);
    END;
IF  NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Course')
    BEGIN
        create table course (    Id int Identity primary key, CourseName varchar(255) not null, StartDate smalldatetime not null, EndDate smalldatetime not null)
    END;
";
$cmd = New-Object Data.SqlClient.SqlCommand $sql, $con;
$cmd.ExecuteNonQuery();	

[String]$dbname = "MailerDBTest";
$sql = "IF  NOT EXISTS (SELECT * FROM sys.databases WHERE name = '$dbname')
    BEGIN
        CREATE DATABASE [$dbname]
    END
    ELSE
    BEGIN
    	DROP DATABASE [$dbname]
    	CREATE DATABASE [$dbname]
    END;"
$cmd = New-Object Data.SqlClient.SqlCommand $sql, $con;
$cmd.ExecuteNonQuery();		
Write-Host "Test Database $dbname is created!";

$sql = "
use $dbname  
IF  NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Contact')
    BEGIN
        CREATE Table Contact (ID int IDENTITY(1,1) PRIMARY KEY, Email VARCHAR(50) not null);
    END;
IF  NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Course')
    BEGIN
        create table course (    Id int Identity primary key, CourseName varchar(255) not null, StartDate smalldatetime not null, EndDate smalldatetime not null)
    END;
    ";
$cmd = New-Object Data.SqlClient.SqlCommand $sql, $con;
$cmd.ExecuteNonQuery();	

# Close & Clear all objects.
$cmd.Dispose();
$con.Close();
$con.Dispose();