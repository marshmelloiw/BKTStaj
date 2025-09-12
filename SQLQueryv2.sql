CREATE TABLE UserTypes (
    type_id INT IDENTITY(1,1) PRIMARY KEY,
    type_name VARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE Users (
    id INT IDENTITY(1,1) PRIMARY KEY,
    first_name VARCHAR(100) NOT NULL,
    last_name VARCHAR(100) NOT NULL,
    email VARCHAR(100) NOT NULL UNIQUE,
    cell_number VARCHAR(20) NOT NULL UNIQUE,
    user_type_id INT NOT NULL,
    CONSTRAINT user_type_key FOREIGN KEY (user_type_id) REFERENCES UserTypes(type_id)
);

CREATE TABLE Stations (
    station_id INT IDENTITY(1,1) PRIMARY KEY,
    name_of_station VARCHAR(100) NOT NULL,
    location VARCHAR(200),
    capacity INT NOT NULL
);

CREATE TABLE Bicycles (
    id INT IDENTITY(1,1) PRIMARY KEY,
    bicycle_code VARCHAR(50) UNIQUE NOT NULL,
    model VARCHAR(100),
    color VARCHAR(50),
    status VARCHAR(20) CHECK(status IN ('available', 'rented', 'maintenance')),
    current_station_id INT,
    CONSTRAINT station_key FOREIGN KEY (current_station_id) REFERENCES Stations(station_id)
);

CREATE TABLE RentingProcesses (
    process_id INT IDENTITY(1,1) PRIMARY KEY,
    user_id INT NOT NULL,
    bicycle_id INT NOT NULL,
    start_station_id INT NOT NULL,
    end_station_id INT,
    start_time DATETIME NOT NULL,
    return_time DATETIME,
    CONSTRAINT user_key FOREIGN KEY (user_id) REFERENCES Users(id),
    CONSTRAINT bicycle_key FOREIGN KEY (bicycle_id) REFERENCES Bicycles(id),
    CONSTRAINT start_station_key FOREIGN KEY (start_station_id) REFERENCES Stations(station_id),
    CONSTRAINT end_station_key FOREIGN KEY (end_station_id) REFERENCES Stations(station_id)
);

CREATE TABLE Maintenance (
    maintenance_id INT IDENTITY(1,1) PRIMARY KEY,
    bicycle_id INT NOT NULL,
    maintenance_start_date DATETIME NOT NULL,
    maintenance_end_date DATETIME,
    maintenance_notes TEXT,
    CONSTRAINT maintenance_bicycle_key FOREIGN KEY (bicycle_id) REFERENCES Bicycles(id)
);