CREATE TABLE Campaigns (
    CampaignId INTEGER PRIMARY KEY,
    CampaignName TEXT NOT NULL,
    CampaignNotes TEXT,
    CampaignCreatedAt TEXT NOT NULL,
    CampaignUpdatedAt TEXT NOT NULL
);

CREATE TABLE CampaignDocuments (
    DocumentId INTEGER PRIMARY KEY,
    CampaignId INTEGER NOT NULL,
    DocumentName TEXT NOT NULL,
    DocumentDescription TEXT,
    DocumentContentType INTEGER NOT NULL,
    Content TEXT,
    IsGMOnly INTEGER NOT NULL,
    DocumentCreatedAt TEXT NOT NULL,
    DocumentUpdatedAt TEXT NOT NULL,
    FOREIGN KEY (CampaignId) REFERENCES Campaigns(CampaignId)
);

CREATE TABLE ReferenceDocuments (
    FileId INTEGER PRIMARY KEY,
    CampaignId INTEGER NOT NULL,
    FileName TEXT NOT NULL,
    FileDescription TEXT,
    FilePath TEXT,
    Url TEXT,
    FOREIGN KEY (CampaignId) REFERENCES Campaigns(CampaignId)
);

