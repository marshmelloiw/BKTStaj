DROP TABLE IF EXISTS Maintenance;
DROP TABLE IF EXISTS RentingProcesses;
DROP TABLE IF EXISTS Bicycles;
DROP TABLE IF EXISTS Stations;
DROP TABLE IF EXISTS Users;

CREATE TABLE Users(
	id int NOT NULL primary key,
	first_name varchar(100) NOT NULL,
	last_name varchar(100) NOT NULL,
	email varchar(100) not null UNIQUE,
	cell_number varchar(20) not null unique,
	user_type varchar(10) not null check(user_type in ('Student', 'Staff'))
);

CREATE TABLE Stations (
    station_id INT PRIMARY KEY,
    name_of_station VARCHAR(100),
    location VARCHAR(200),
    capacity INT
);

CREATE TABLE Bicycles (
    id INT PRIMARY KEY,
    bicycle_code VARCHAR(50) UNIQUE,
    model VARCHAR(100),
    color VARCHAR(50),
    status VARCHAR(20) CHECK(status IN ('available', 'rented', 'maintenance')),
    current_station_id INT,
    CONSTRAINT station_key FOREIGN KEY (current_station_id) REFERENCES Stations(station_id)
);

CREATE TABLE RentingProcesses (
    process_id INT PRIMARY KEY,
    user_id INT,
    bicycle_id INT,
    start_station_id INT,
    end_station_id INT,
    start_time DATETIME,
    return_time DATETIME,
    
    CONSTRAINT user_key FOREIGN KEY (user_id) REFERENCES Users(id),
    CONSTRAINT bicycle_key FOREIGN KEY (bicycle_id) REFERENCES Bicycles(id),
    CONSTRAINT start_station_key FOREIGN KEY (start_station_id) REFERENCES Stations(station_id),
    CONSTRAINT end_station_key FOREIGN KEY (end_station_id) REFERENCES Stations(station_id)
);

CREATE TABLE Maintenance (
    maintenance_log_id INT PRIMARY KEY,
    bicycle_id INT,
    maintenance_start_date DATETIME,
    maintenance_end_date DATETIME,
    maintenance_notes VARCHAR(500),

    CONSTRAINT mbicycle_key FOREIGN KEY (bicycle_id) REFERENCES Bicycles(id)
);