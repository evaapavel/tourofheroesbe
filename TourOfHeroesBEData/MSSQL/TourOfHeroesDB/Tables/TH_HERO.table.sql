CREATE TABLE TH_HERO
(
    ID          INT                 IDENTITY(30,1)     NOT NULL,
    NAME        NVARCHAR(MAX)                          NOT NULL,

    CONSTRAINT PK_TH_HERO  PRIMARY KEY ( ID )
);
