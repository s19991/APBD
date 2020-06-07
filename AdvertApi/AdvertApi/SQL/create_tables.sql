--s19991

create table Building (
    IdBuilding int not null,
    Street nvarchar(100) not null,
    StreetNumber int not null,
    City nvarchar(100) not null,
    Height decimal(6,2) not null,
    constraint Building_pk primary key (IdBuilding)
)

create table Client (
    IdClient int not null,
    FirstName nvarchar(100) not null,
    LastName nvarchar(100) not null,
    Email nvarchar(100) not null,
    Phone nvarchar(100) not null,
    Login nvarchar(100) not null,
    constraint Client_pk primary key (IdClient)
)

create table Banner (
    IdAdvertisement int not null,
    Name int not null,
    Price decimal(6,2) not null,
    IdCampaign int not null,
    Area decimal(6,2) not null,
    constraint Banner_pk primary key (IdAdvertisement)
)

create table Campaign (
    IdCampaign int not null,
    IdClient int not null,
    StartDate date not null,
    EndDate date not null,
    PricePerSquareMeter decimal(6,2) not null,
    FromIdBuilding int not null,
    ToIdBuilding int not null,
    constraint Campaign_pk primary key (IdCampaign)
)


alter table Banner add constraint Banner_Campaign
    foreign key (IdCampaign)
    references Campaign (IdCampaign);

alter table Campaign add constraint Campaign_Client
    foreign key (IdClient)
    references Client (IdClient);

-- todo zerknac na to, bo nie jestem pewien czy tak sie robi dwa powiazania
alter table Campaign add constraint Campaign_From_Building
    foreign key (FromIdBuilding)
    references Building (IdBuilding);

alter table Campaign add constraint Campaign_To_Building
    foreign key (ToIdBuilding)
    references Building (IdBuilding);
