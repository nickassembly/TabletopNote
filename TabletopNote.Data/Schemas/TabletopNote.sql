CREATE TABLE Campaigns (
    CampaignId INTEGER PRIMARY KEY,
    CampaignName TEXT NOT NULL,
    CampaignDescription TEXT
);

CREATE TABLE CampaignDocuments (
    DocumentId INTEGER PRIMARY KEY,
    CampaignId INTEGER NOT NULL,
    DocumentName TEXT NOT NULL,
    DocumentDescription TEXT,
    DocumentContentType INTEGER NOT NULL,
    DocumentContent TEXT,
    IsGMVisibleOnly INTEGER NOT NULL,
    DocumentCreatedAt TEXT NOT NULL,
    DocumentUpdatedAt TEXT NOT NULL,
    FOREIGN KEY (CampaignId) REFERENCES Campaigns(CampaignId)
);

CREATE TABLE ReferenceDocuments (
    FileId INTEGER PRIMARY KEY,
    CampaignId INTEGER NOT NULL,
    ReferenceFileName TEXT NOT NULL,
    FileDescription TEXT,
    FilePath TEXT,
    Url TEXT,
    IsGMVisibleOnly INTEGER NOT NULL,
    FOREIGN KEY (CampaignId) REFERENCES Campaigns(CampaignId)
);

CREATE TABLE CalendarEvents (
    CalendarEventId INTEGER PRIMARY KEY,
    CampaignId INTEGER NOT NULL,
    EventName TEXT NOT NULL,
    EventDescription TEXT,
    EventStartDate TEXT NOT NULL,
    EventEndDate TEXT NOT NULL,
    IsGMVisibleOnly INTEGER NOT NULL,
    FOREIGN KEY (CampaignId) REFERENCES Campaigns(CampaignId)
);

